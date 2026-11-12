import { test } from 'node:test';
import assert from 'node:assert/strict';
import { runInNewContext } from 'node:vm';
import worker from '../worker.mjs';

test('purchase page selects German and English without changing the purchase contract', async () => {
  const get = async (url, headers = {}) => {
    const res = await worker.fetch(new Request(url, { headers }), {});
    return { html: await res.text(), headers: res.headers };
  };
  const de = await get('https://buyer.test/?lang=de');
  const en = await get('https://buyer.test/?lang=en');
  const preferred = await get('https://buyer.test/', { 'Accept-Language': 'de-DE,de;q=0.9,en;q=0.7' });
  const weighted = await get('https://buyer.test/', { 'Accept-Language': 'en;q=0.2,de;q=0.9' });
  const forbidden = await get('https://buyer.test/', { 'Accept-Language': 'de;q=0,en;q=0.8' });
  assert.match(de.html, /<html lang="de">/);
  assert.match(de.html, /Einmalkauf/);
  assert.match(de.html, /vier eigenen PCs/);
  assert.match(en.html, /<html lang="en">/);
  assert.match(en.html, /one-time purchase/);
  assert.match(preferred.html, /<html lang="de">/);
  assert.match(weighted.html, /<html lang="de">/);
  assert.match(forbidden.html, /<html lang="en">/);
  for (const page of [de.html, en.html]) {
    assert.match(page, /id="terms"/);
    assert.match(page, /id="recovery"/);
    assert.match(page, /\/api\/checkout/);
    assert.match(page, /\/api\/licenses\/activate/);
    assert.match(page, /\/api\/licenses\/deactivate/);
    assert.match(page, /\?lang=de/);
    assert.match(page, /\?lang=en/);
  }
  assert.equal(de.headers.get('Cache-Control'), 'no-store');
});

test('purchase data export is clearly scoped in both supported languages', async () => {
  const get = async lang => (await worker.fetch(new Request(`https://buyer.test/?lang=${lang}`),{})).text();
  const de=await get('de'),en=await get('en');
  for(const html of [de,en]) { assert.match(html,/<button id="dataExport">/); assert.match(html,/<button id="termsDownload">/); assert.doesNotMatch(html,/href="\/api\/order\/(?:export|terms)"/); }
  assert.match(de,/Kaufdaten aus MAKRIVO herunterladen/);
  assert.match(de,/Stripe und andere Dienstleister/);
  assert.match(en,/Download MAKRIVO purchase data/);
  assert.match(en,/Stripe and other providers/);
});

test('private download buttons send same-origin POSTs, download blobs, and translate failures', async () => {
  for(const [language,message] of [['de','Bitte stelle zuerst deinen Kauf wieder her.'],['en','Please restore your purchase first.']]) {
    const html=await (await worker.fetch(new Request(`https://buyer.test/?lang=${language}`),{})).text();
    const script=html.match(/<script>([\s\S]*?)<\/script>/)[1];
    const elements=Object.fromEntries(['status','backup','saved','buy','terms','recovery','copyRecovery','continue','restore','restoreCode','resume','order','activate','dataExport','termsDownload'].map(id=>[id,{}]));
    let sent,downloaded=false,content,mode='success',error='Order claim required';
    const document={getElementById:id=>elements[id],querySelectorAll:()=>[],createElement:()=>({click(){downloaded=true;}})};
    const mockUrl={createObjectURL(blob){content=blob;return 'blob:test';},revokeObjectURL(){}};
    const fetch=async(path,opts)=>{sent={path,opts};return mode==='success'?{ok:true,blob:async()=>new Blob([path==='\/api\/order\/terms'?'Terms sample':JSON.stringify({order:{reference:'sample'}})])}:{ok:false,json:async()=>({error})};};
    runInNewContext(script,{document,fetch,Blob,URL:mockUrl,setTimeout:fn=>fn()});
    await elements.dataExport.onclick();
    assert.equal(sent.path,'/api/order/export');assert.equal(sent.opts.method,'POST');assert.equal(sent.opts.credentials,'same-origin');assert.equal(sent.opts.body,'{}');
    assert.equal(sent.opts.headers.Authorization,undefined);assert.equal(sent.opts.headers.Cookie,undefined);
    assert.equal(downloaded,true);assert.deepEqual(JSON.parse(await content.text()),{order:{reference:'sample'}});
    downloaded=false;await elements.termsDownload.onclick();assert.equal(sent.path,'/api/order/terms');assert.equal(sent.opts.method,'POST');assert.equal(downloaded,true);assert.equal(await content.text(),'Terms sample');
    mode='error';downloaded=false;await elements.dataExport.onclick();assert.equal(downloaded,false);assert.equal(elements.status.textContent,message);
    error='Export too large; contact support';await elements.dataExport.onclick();assert.match(elements.status.textContent,language==='de'?/zu viele Gerätedaten/:/too many device records/);
    error='No paid order';await elements.termsDownload.onclick();assert.match(elements.status.textContent,language==='de'?/nach der Zahlung/:/after payment/);
    error='Terms copy unavailable';await elements.termsDownload.onclick();assert.match(elements.status.textContent,language==='de'?/keine gespeicherte Fassung/:/saved copy is unavailable/);
  }
});

test('German page preserves the recovery code during language switching and guides the buyer', async () => {
  const html = await (await worker.fetch(new Request('https://buyer.test/?lang=de'), {})).text();
  const script = html.match(/<script>([\s\S]*?)<\/script>/)[1];
  const elements = Object.fromEntries(['status','backup','saved','buy','terms','recovery','copyRecovery','continue','restore','restoreCode','resume','order','activate'].map(id => [id, {}]));
  elements.backup.hidden = true;
  elements.saved.checked = false;
  elements.terms.checked = true;
  const anchors = [{}, {}];
  const document = {
    getElementById: id => elements[id],
    querySelectorAll: () => anchors.map(a => ({ addEventListener: (_, fn) => { a.click = fn; } })),
  };
  runInNewContext(script, { document, fetch: async () => ({ ok: true, json: async () => ({recoveryCode:'test-secret',url:'https://checkout.stripe.com/test'}) }) });
  await elements.buy.onclick();
  assert.equal(elements.recovery.textContent, 'test-secret');
  assert.equal(elements.backup.hidden, false);
  let stopped = false;
  anchors[1].click({preventDefault(){stopped=true;}});
  assert.equal(stopped, true);
  assert.match(elements.status.textContent, /Kauf abschließen/);
  elements.continue.onclick();
  assert.match(elements.status.textContent, /Speichere zuerst den Wiederherstellungscode/);
  elements.saved.checked = true;
  stopped = false;
  anchors[1].click({preventDefault(){stopped=true;}});
  assert.equal(stopped, true);
});

test('switching languages stays blocked while checkout is in flight and after success, but unlocks after failure for retry', async () => {
  const html = await (await worker.fetch(new Request('https://buyer.test/?lang=de'), {})).text();
  const script = html.match(/<script>([\s\S]*?)<\/script>/)[1];
  const elements = Object.fromEntries(['status','backup','saved','buy','terms','recovery','copyRecovery','continue','restore','restoreCode','resume','order','activate'].map(id => [id, {}]));
  elements.backup.hidden=true; elements.terms.checked=true;
  const anchor={}; let resolveFetch; let count=0;
  const document={getElementById:id=>elements[id], querySelectorAll:()=>[{addEventListener:(_,fn)=>{anchor.click=fn;}}]};
  runInNewContext(script,{document,fetch:()=>{count++;return new Promise(resolve=>{resolveFetch=resolve;});}});
  const first=elements.buy.onclick();
  let stopped=false;anchor.click({preventDefault(){stopped=true;}});assert.equal(stopped,true);
  resolveFetch({ok:false,json:async()=>({error:'Price is not configured'})});await first;
  stopped=false;anchor.click({preventDefault(){stopped=true;}});assert.equal(stopped,false);
  const retry=elements.buy.onclick(); assert.equal(count,2);
  resolveFetch({ok:true,json:async()=>({recoveryCode:'secret',url:'https://checkout.stripe.com/test'})});await retry;
  elements.saved.checked=true;stopped=false;anchor.click({preventDefault(){stopped=true;}});assert.equal(stopped,true);
});

test('German buyer receives actionable translated checkout errors', async () => {
  const html = await (await worker.fetch(new Request('https://buyer.test/?lang=de'), {})).text();
  const script = html.match(/<script>([\s\S]*?)<\/script>/)[1];
  const elements = Object.fromEntries(['status','backup','saved','buy','terms','recovery','copyRecovery','continue','restore','restoreCode','resume','order','activate'].map(id => [id, {}]));
  elements.backup.hidden = true;
  elements.terms.checked = false;
  const document = { getElementById: id => elements[id], querySelectorAll: () => [] };
  let sent;
  runInNewContext(script, { document, fetch: async (_, options) => { sent = JSON.parse(options.body); return { ok: false, json: async () => ({error:'Accept purchase terms'}) }; } });
  await elements.buy.onclick();
  assert.equal(sent.acceptTerms, false);
  assert.equal(sent.lang, 'de');
  assert.equal(elements.status.textContent, 'Bitte akzeptiere die Kaufbedingungen.');
  assert.equal(elements.backup.hidden, true);
});

test('English buyer receives an actionable message when purchase terms are unchecked', async () => {
  const html=await (await worker.fetch(new Request('https://buyer.test/?lang=en'),{})).text();
  const script=html.match(/<script>([\s\S]*?)<\/script>/)[1];
  const elements=Object.fromEntries(['status','backup','saved','buy','terms','recovery','copyRecovery','continue','restore','restoreCode','resume','order','activate'].map(id=>[id,{}]));
  elements.terms.checked=false;
  const document={getElementById:id=>elements[id],querySelectorAll:()=>[]};
  runInNewContext(script,{document,fetch:async()=>({ok:false,json:async()=>({error:'Accept purchase terms'})})});
  await elements.buy.onclick();assert.match(elements.status.textContent,/accept.*terms/i);
});
test('buyer submits two independently selected consent values', async () => {
  const html=await (await worker.fetch(new Request('https://buyer.test/?lang=de'),{})).text();
  const script=html.match(/<script>([\s\S]*?)<\/script>/)[1];
  const elements=Object.fromEntries(['status','backup','saved','buy','terms','earlyStart','withdrawalAck','recovery','copyRecovery','continue','restore','restoreCode','resume','order','activate'].map(id=>[id,{}]));
  elements.terms.checked=true;elements.earlyStart.checked=true;elements.withdrawalAck.checked=false;
  let sent; const document={getElementById:id=>elements[id],querySelectorAll:()=>[]};
  runInNewContext(script,{document,fetch:async(_path,opts)=>{sent=JSON.parse(opts.body);return {ok:false,json:async()=>({error:'Accept both declarations'})};}});
  await elements.buy.onclick();assert.equal(sent.acceptEarlyStart,true);assert.equal(sent.acknowledgeWithdrawal,false);assert.match(elements.status.textContent,/beide Erklärungen einzeln/);
});
test('English activation explains missing purchase and the four-PC limit', async () => {
  const html=await (await worker.fetch(new Request('https://buyer.test/?lang=en'),{})).text();
  const script=html.match(/<script>([\s\S]*?)<\/script>/)[1];
  for(const [error,expected] of [['Order claim required',/restore.*purchase/i],['Invalid device code',/device code/i],['Four PC limit reached',/four.*PC|4.*PC/i],['No active entitlement',/active license/i]]) {
    const elements=Object.fromEntries(['status','backup','saved','buy','terms','recovery','copyRecovery','continue','restore','restoreCode','resume','order','activate','device'].map(id=>[id,{}]));
    elements.device.value='device-code';
    const document={getElementById:id=>elements[id],querySelectorAll:()=>[]};
    runInNewContext(script,{document,fetch:async()=>({ok:false,json:async()=>({error})})});
    await elements.activate.onclick();assert.match(elements.status.textContent,expected);
  }
});
test('confirmation delivery failures direct buyers to support in German and English', async () => {
  for (const [language,pattern] of [['de',/Vertragsbestätigung.*Support/],['en',/confirmation.*support/i]]) {
    const html=await (await worker.fetch(new Request(`https://buyer.test/?lang=${language}`),{})).text();
    const script=html.match(/<script>([\s\S]*?)<\/script>/)[1];
    const elements=Object.fromEntries(['status','backup','saved','buy','terms','recovery','copyRecovery','continue','restore','restoreCode','resume','order','activate','device'].map(id=>[id,{}]));
    elements.device.value='a'.repeat(64);
    const document={getElementById:id=>elements[id],querySelectorAll:()=>[]};
    runInNewContext(script,{document,fetch:async()=>({ok:false,json:async()=>({error:'Contract confirmation delivery unavailable'})})});
    await elements.activate.onclick(); assert.match(elements.status.textContent,pattern);
  }
});

test('language selection is blocked during another API request and unlocked after its failure', async () => {
  const html = await (await worker.fetch(new Request('https://buyer.test/?lang=en'), {})).text();
  const script = html.match(/<script>([\s\S]*?)<\/script>/)[1];
  const elements = Object.fromEntries(['status','backup','saved','buy','terms','recovery','copyRecovery','continue','restore','restoreCode','resume','order','activate'].map(id => [id, {}]));
  elements.restoreCode.value = 'test';
  const anchor={}; let resolveFetch;
  const document={getElementById:id=>elements[id],querySelectorAll:()=>[{addEventListener:(_,fn)=>{anchor.click=fn;}}]};
  runInNewContext(script,{document,fetch:()=>new Promise(resolve=>{resolveFetch=resolve;})});
  const pending=elements.restore.onclick();
  let stopped=false;anchor.click({preventDefault(){stopped=true;}});assert.equal(stopped,true);
  resolveFetch({ok:false,json:async()=>({error:'Invalid recovery code'})});await pending;
  stopped=false;anchor.click({preventDefault(){stopped=true;}});assert.equal(stopped,false);
});

test('a buyer can resume with the saved code after a reload without storing secrets in browser storage or URLs', async () => {
  const html=await (await worker.fetch(new Request('https://buyer.test/?lang=de'),{})).text();
  const script=html.match(/<script>([\s\S]*?)<\/script>/)[1];
  const elements=Object.fromEntries(['status','backup','saved','buy','terms','recovery','copyRecovery','continue','restore','restoreCode','resume','order','activate'].map(id=>[id,{}]));
  elements.terms.checked=true;elements.restoreCode.value='saved-code';
  const sent=[];let redirected='';
  const document={getElementById:id=>elements[id],querySelectorAll:()=>[]};
  runInNewContext(script,{document,location:{assign:url=>{redirected=url;}},fetch:async(path,options)=>{sent.push({path,body:JSON.parse(options.body)});return {ok:true,json:async()=>({url:'https://checkout.stripe.com/pay'})};}});
  await elements.resume.onclick();
  assert.equal(sent[0].path,'/api/checkout');
  assert.equal(sent[0].body.recoveryCode,'saved-code');
  assert.equal(redirected,'https://checkout.stripe.com/pay');
  assert.doesNotMatch(script,/localStorage|sessionStorage/);
});

test('new buyer explicitly restores saved code after payment before accessing the order', async () => {
  const html=await (await worker.fetch(new Request('https://buyer.test/?lang=de'),{})).text();
  assert.match(html,/Gib nach der Zahlung oben deinen gespeicherten Wiederherstellungscode ein/);
  assert.match(html,/ersetzt den in diesem Browser ausgewählten Kauf/);
  const script=html.match(/<script>([\s\S]*?)<\/script>/)[1],saved='new-recovery-code';
  let selected='previous-purchase',redirected='',paid=false;
  const fetch=async(path,options)=>{
    const body=options.body ? JSON.parse(options.body) : null;
    if(path==='/api/checkout/prepare') return {ok:true,json:async()=>({recoveryCode:saved})};
    if(path==='/api/checkout') {assert.equal(body.recoveryCode,saved);paid=true;return {ok:true,json:async()=>({url:'https://checkout.stripe.com/pay'})};}
    if(path==='/api/claim/restore') {assert.equal(body.recoveryCode,saved);selected=body.recoveryCode;return {ok:true,json:async()=>({restored:true})};}
    if(path==='/api/order') return selected===saved&&paid ? {ok:true,json:async()=>({status:'paid',devices:[]})} : {ok:false,json:async()=>({error:'Order claim required'})};
    throw Error('Unexpected request');
  };
  const load=()=>{
    const elements=Object.fromEntries(['status','backup','saved','buy','terms','recovery','copyRecovery','continue','restore','restoreCode','resume','order','activate','seats','devices'].map(id=>[id,{}]));
    elements.terms.checked=true;elements.devices.replaceChildren=()=>{};
    const document={getElementById:id=>elements[id],querySelectorAll:()=>[]};
    runInNewContext(script,{document,fetch,location:{assign:url=>{redirected=url;}}});return elements;
  };
  const before=load();await before.buy.onclick();assert.equal(before.recovery.textContent,saved);
  before.saved.checked=true;await before.continue.onclick();
  assert.equal(redirected,'https://checkout.stripe.com/pay');assert.equal(selected,'previous-purchase');
  const after=load();after.restoreCode.value=saved;await after.restore.onclick();
  assert.equal(selected,saved);await after.order.onclick();
  assert.match(after.seats.textContent,/bezahlt/);
});

test('updated terms require the German buyer to reload and review again', async () => {
  const html=await (await worker.fetch(new Request('https://buyer.test/?lang=de'),{})).text(), script=html.match(/<script>([\s\S]*?)<\/script>/)[1];
  const elements=Object.fromEntries(['status','backup','saved','buy','terms','recovery','copyRecovery','continue','restore','restoreCode','resume','order','activate'].map(id=>[id,{}]));
  elements.terms.checked=true; const document={getElementById:id=>elements[id],querySelectorAll:()=>[]};
  runInNewContext(script,{document,fetch:async()=>({ok:false,json:async()=>({error:'Contract changed'})})});
  await elements.buy.onclick();assert.match(elements.status.textContent,/Lade die Seite neu/);
});
