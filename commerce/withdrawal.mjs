// Isolated intake foundation. No HTTP route, transport, or live readiness gate
// uses this module yet. A pending receipt is not a delivered confirmation.
const testPage = `<!doctype html><html lang="de"><meta charset="utf-8"><meta name="viewport" content="width=device-width"><title>TEST – Vertrag widerrufen</title><h1>TEST: Vertrag widerrufen</h1><p>Isolierte Testfunktion: Es wird keine Bestätigung versendet. Nicht für echte Widerrufe verwenden.</p><p>TEST: Alternativer Kontakt im Entwurf: info@aivana-gmbh.ai; Aivana GmbH, Paulusstr. 45a, Hinterhaus – LOFT45, 33602 Bielefeld.</p><label>Name <input id="name" maxlength="160" autocomplete="name"></label><label>Vertrag identifizieren <input id="contract" maxlength="1000"></label><label>E-Mail für die Eingangsbestätigung <input id="email" maxlength="254" type="email" autocomplete="email"></label><p>Keine Kauf- oder Wiederherstellungscodes eingeben.</p><button id="preview" type="button">Weiter</button><section><p id="details"></p><button id="edit" type="button" hidden>Angaben ändern</button><button id="confirm" type="button" hidden>Widerruf bestätigen</button></section><p id="status" role="status"></p><button id="new" type="button" hidden>Neuen Testvorgang beginnen (kann einen zweiten Datensatz erzeugen)</button><script>
const el=id=>document.getElementById(id); const names=['name','contract','email']; const storageKey='makrivo_withdrawal_test_id'; let snapshot=null,pending=false;
const validId=id=>typeof id==='string'&&/^[0-9a-f]{8}-[0-9a-f]{4}-4[0-9a-f]{3}-[89ab][0-9a-f]{3}-[0-9a-f]{12}$/i.test(id);
function newId(){const id=crypto.randomUUID();try{sessionStorage.setItem(storageKey,id);}catch{}return id;}
function loadId(){try{const id=sessionStorage.getItem(storageKey);if(validId(id))return id.toLowerCase();}catch{}return newId();}
function clearCompletedId(){try{if(sessionStorage.getItem(storageKey)?.toLowerCase()===requestId)sessionStorage.removeItem(storageKey);}catch{}}
let requestId=loadId();
function invalidate(){snapshot=null;for(const id of names)el(id).disabled=false;el('confirm').hidden=true;el('edit').hidden=true;el('preview').hidden=false;el('details').textContent='';}
el('preview').onclick=()=>{ if(pending)return; const name=el('name').value.trim(), contract=el('contract').value.trim(), email=el('email').value.trim(); if(!name||!contract||!email){el('status').textContent='Bitte alle Angaben ausfüllen.';return;} snapshot={name,contract,email};for(const id of names)el(id).disabled=true;el('details').textContent='Ich widerrufe den Vertrag. Name: '+name+'; Vertrag: '+contract+'; Bestätigung an: '+email; el('confirm').hidden=false;el('edit').hidden=false;el('preview').hidden=true;el('confirm').disabled=false;el('status').textContent='Bitte Angaben prüfen.';el('confirm').focus?.(); };
el('edit').onclick=()=>{if(pending)return;invalidate();el('status').textContent='Bitte Angaben ändern und erneut prüfen.';el('name').focus?.();};
el('new').onclick=()=>{if(pending)return;invalidate();requestId=newId();for(const id of names)el(id).value='';el('new').hidden=true;el('status').textContent='Neuer Testvorgang.';};
el('confirm').onclick=async()=>{ if(!snapshot||pending)return; if(names.some(id=>el(id).value.trim()!==snapshot[id])){invalidate();el('status').textContent='Angaben wurden geändert. Bitte erneut prüfen.';return;} pending=true;el('confirm').disabled=true;el('edit').disabled=true;try { const response=await fetch('/withdrawal',{method:'POST',headers:{'Content-Type':'application/json'},body:JSON.stringify({...snapshot,confirmed:true,requestId})});if(response.status===409){el('status').textContent='TEST: Diese Kennung gehört zu anderen Angaben. Ein neuer Testvorgang kann einen zweiten Datensatz erzeugen.';el('new').hidden=false;return;}if(response.status===429){el('status').textContent='TEST: Vorübergehend begrenzt. Für diesen Versuch wurde keine Speicherung bestätigt. Alternativer Kontakt im Entwurf: info@aivana-gmbh.ai.';el('confirm').disabled=false;return;}if(response.status!==202)throw Error('failed');const result=await response.json();if(!result||result.status!=='receipt_pending'||result.requestId!==requestId||!Number.isSafeInteger(result.receivedAt)||result.receivedAt<=0||result.receivedAt>8640000000000000)throw Error('invalid response');clearCompletedId();el('status').textContent='TEST: Erklärung gespeichert. Eine Eingangsbestätigung wurde nicht versendet.';el('confirm').hidden=true;el('edit').hidden=true;el('new').hidden=false;} catch {el('status').textContent='TEST: Speicherung konnte nicht bestätigt werden. Erneut versuchen. Alternativer Kontakt im Entwurf: info@aivana-gmbh.ai.';el('confirm').disabled=false;} finally {pending=false;el('edit').disabled=false;} };
</script></html>`;

// Static translations only. User input is never interpolated into markup.
// The localized flow is test-only and does not establish legal compliance.
function testPageFor(language) {
  const navigation='<nav aria-label="Language / Sprache"><a id="langDe" href="/withdrawal?lang=de" hreflang="de">Deutsch</a> · <a id="langEn" href="/withdrawal?lang=en" hreflang="en">English</a></nav><p>Sprachwechsel löscht eingegebene Angaben.</p>';
  const guard="for(const id of ['langDe','langEn']){const link=el(id);if(link)link.onclick=event=>{if(pending){event.preventDefault();el('status').textContent='Bitte warten, bis die Anfrage abgeschlossen ist.';}};}\n";
  const selected=language==='en'?'en':'de';
  const page=testPage.replace('</h1>','</h1>'+navigation).replace('</script>',guard+'</script>')
    .replace('JSON.stringify({...snapshot,confirmed:true,requestId})',`JSON.stringify({...snapshot,confirmed:true,requestId,language:'${selected}'})`);
  if(language!=='en')return page;
  const translations=[
    ['<html lang="de">','<html lang="en">'],
    ['Vertrag widerrufen','Withdraw from contract'],
    ['Isolierte Testfunktion: Es wird keine Bestätigung versendet. Nicht für echte Widerrufe verwenden.','Isolated test only. This test does not send a confirmation. Do not use for an actual withdrawal.'],
    ['Alternativer Kontakt im Entwurf','Alternative contact in the draft'],
    ['Sprachwechsel löscht eingegebene Angaben.','Changing language clears entered details.'],
    ['Bitte warten, bis die Anfrage abgeschlossen ist.','Wait for the request to finish.'],
    ['Vertrag identifizieren','Identify the contract'],
    ['E-Mail für die Eingangsbestätigung','Email for the receipt confirmation'],
    ['Keine Kauf- oder Wiederherstellungscodes eingeben.','Do not enter purchase or recovery codes.'],
    ['>Weiter</button>','>Continue</button>'],
    ['Angaben ändern','Change details'],
    ['Widerruf bestätigen','Confirm withdrawal'],
    ['Neuen Testvorgang beginnen (kann einen zweiten Datensatz erzeugen)','Start a new test (may create a second record)'],
    ['Bitte alle Angaben ausfüllen.','Complete all fields.'],
    ['Ich widerrufe den Vertrag. Name: ','I withdraw from the contract. Name: '],
    ['; Vertrag: ','; Contract: '],
    ['; Bestätigung an: ','; Confirmation to: '],
    ['Bitte Angaben prüfen.','Review the details.'],
    ['Bitte Angaben ändern und erneut prüfen.','Change the details and review them again.'],
    ['Neuer Testvorgang.','New test request.'],
    ['Angaben wurden geändert. Bitte erneut prüfen.','Details changed. Review them again.'],
    ['TEST: Diese Kennung gehört zu anderen Angaben. Ein neuer Testvorgang kann einen zweiten Datensatz erzeugen.','TEST: This identifier belongs to different details. Starting a new test may create a second record.'],
    ['TEST: Vorübergehend begrenzt. Für diesen Versuch wurde keine Speicherung bestätigt.','TEST: Temporarily limited. Storage was not confirmed for this attempt.'],
    ['TEST: Erklärung gespeichert. Eine Eingangsbestätigung wurde nicht versendet.','TEST: Declaration stored. No receipt confirmation was sent.'],
    ['TEST: Speicherung konnte nicht bestätigt werden. Erneut versuchen.','TEST: Storage could not be confirmed. Try again.'],
  ];
  // Replace longer phrases first so shared substrings cannot partially alter
  // another phrase before it is translated.
  translations.sort((a,b)=>b[0].length-a[0].length);
  return translations.reduce((result,[from,to])=>result.replaceAll(from,to),page);
}

async function readBounded(req) {
  const reader=req.body?.getReader(); if (!reader) return '';
  const chunks=[]; let length=0;
  while (true) { const {done,value}=await reader.read(); if (done) break; length+=value.byteLength; if(length>8192) throw new RangeError('Body too large'); chunks.push(value); }
  const bytes=new Uint8Array(length); let offset=0; for(const chunk of chunks){bytes.set(chunk,offset);offset+=chunk.byteLength;}
  return new TextDecoder('utf-8',{fatal:true}).decode(bytes);
}

export async function withdrawalTestFetch(req, e) {
  // Test-only: not mounted by the production Worker; cannot run in live mode.
  if (e?.WITHDRAWAL_TEST_ONLY !== 'true' || e?.STRIPE_LIVE !== 'false' || !e?.DB)
    return new Response('Test unavailable', {status:503});
  let url; try { url=new URL(req.url); if (new URL(e.PUBLIC_ORIGIN).origin !== e.PUBLIC_ORIGIN || url.origin !== e.PUBLIC_ORIGIN) throw Error(); }
  catch { return new Response('Invalid test origin', {status:503}); }
  if (url.pathname !== '/withdrawal') return new Response('Not found',{status:404});
  const headers={'Cache-Control':'no-store','X-Content-Type-Options':'nosniff','Referrer-Policy':'no-referrer'};
  if (req.method === 'GET') return new Response(testPageFor(url.searchParams.get('lang')),{headers:{...headers,'Content-Type':'text/html;charset=utf-8','Content-Security-Policy':"default-src 'none'; script-src 'unsafe-inline'; connect-src 'self'; form-action 'none'; base-uri 'none'; frame-ancestors 'none'"}});
  if (req.method !== 'POST') return new Response('Method not allowed',{status:405,headers});
  if (req.headers.get('Origin') !== e.PUBLIC_ORIGIN) return new Response('Invalid origin',{status:403,headers});
  if (!/^application\/json(?:\s*;|$)/i.test(req.headers.get('Content-Type') || '')) return new Response('Invalid content type',{status:415,headers});
  try {
    const payload=JSON.parse(await readBounded(req));
    if (e.WITHDRAWAL_TEST_RATE_LIMIT_ENABLED === 'true') {
      const validated=validateWithdrawal(payload);
      testProtectionSource(e,req);
      const localized=await hasWithdrawalLanguageTable(e.DB);
      if (!localized && validated.language!=='de') throw new Error('Test language unavailable');
      const sql=localized ? 'SELECT r.name,r.contract_reference,r.contact_email,r.received_at,l.language FROM withdrawal_requests r JOIN withdrawal_receipts q ON q.request_id=r.id LEFT JOIN withdrawal_test_languages l ON l.request_id=r.id WHERE r.id=?' : "SELECT r.name,r.contract_reference,r.contact_email,r.received_at,'de' AS language FROM withdrawal_requests r JOIN withdrawal_receipts q ON q.request_id=r.id WHERE r.id=?";
      const previous=await e.DB.prepare(sql).bind(validated.id).first();
      if(previous&&previous.language===validated.language&&previous.name===validated.name&&previous.contract_reference===validated.contract&&previous.contact_email===validated.email)
        return new Response(JSON.stringify({requestId:validated.id,receivedAt:previous.received_at,status:'receipt_pending'}),{status:202,headers:{...headers,'Content-Type':'application/json;charset=utf-8'}});
      const retryAfter=await testAbuseLimit(e,req,validated.email);
      if (retryAfter) return new Response(JSON.stringify({error:'Test request rate limited',alternative:'info@aivana-gmbh.ai'}),{status:429,headers:{...headers,'Content-Type':'application/json;charset=utf-8','Retry-After':String(retryAfter)}});
    }
    const result=await acceptWithdrawal(e.DB,payload);
    return new Response(JSON.stringify(result),{status:202,headers:{...headers,'Content-Type':'application/json;charset=utf-8'}});
  } catch (err) {
    const status=err instanceof RangeError ? 413 : err instanceof SyntaxError || err instanceof TypeError ? 400 : err?.message === 'Withdrawal request conflict' ? 409 : 503;
    return new Response(JSON.stringify({error:'Test request could not be completed',...(status===503?{alternative:'info@aivana-gmbh.ai'}:{})}),{status,headers:{...headers,'Content-Type':'application/json;charset=utf-8'}});
  }
}

function inputText(value, max) {
  if (typeof value !== 'string' || value.length > max || /[\u0000-\u001f\u007f]/.test(value) || !value.trim())
    throw new TypeError('Invalid withdrawal information');
  return value.trim();
}

function validateWithdrawal(input) {
  if (!input || input.confirmed !== true || typeof input.requestId !== 'string' ||
      !/^[0-9a-f]{8}-[0-9a-f]{4}-4[0-9a-f]{3}-[89ab][0-9a-f]{3}-[0-9a-f]{12}$/i.test(input.requestId))
    throw new TypeError('Invalid withdrawal confirmation or request identifier');
  const id = input.requestId.toLowerCase();
  const name = inputText(input.name, 160);
  const contract = inputText(input.contract, 1000);
  const email = inputText(input.email, 254);
  if (!/^[^\s@]+@[^\s@]+$/u.test(email)) throw new TypeError('Invalid confirmation email');
  const language=input.language===undefined?'de':input.language;
  if(!['de','en'].includes(language))throw new TypeError('Invalid test language');
  return {id,name,contract,email,language};
}

// Existing isolated databases lack migration 0018. They can process only the
// original German form; an unknown or failed schema lookup never falls back.
export async function hasWithdrawalLanguageTable(db){
  const result=await db.prepare("SELECT COUNT(*) AS present FROM sqlite_master WHERE type='table' AND name='withdrawal_test_languages'").bind().first();
  if(!result||![0,1].includes(result.present))throw new Error('Test language schema unavailable');
  return result.present===1;
}

// Optional test-only throttling. This is not a public abuse or legal solution:
// a hard limit can also reject a legitimate declaration and requires a tested
// alternative intake channel before any production use.
function testProtectionSource(env,req){
  const secret=env.WITHDRAWAL_TEST_RATE_LIMIT_SECRET;
  const source=req.headers.get('CF-Connecting-IP');
  if(env.WITHDRAWAL_TEST_TRUST_PROXY!=='true'||typeof secret!=='string'||!/^[A-Za-z0-9_-]{32,128}$/.test(secret)||typeof source!=='string'||!/^[0-9a-f:.]{3,45}$/i.test(source)) throw new Error('Test protection unavailable');
  return {secret,source};
}

async function testAbuseLimit(env,req,email){
  const {secret,source}=testProtectionSource(env,req);
  const now=Date.now(),end=(Math.floor(now/600000)+1)*600000,window=Math.floor(now/600000);
  const encoder=new TextEncoder();
  const key=await crypto.subtle.importKey('raw',encoder.encode(secret),{name:'HMAC',hash:'SHA-256'},false,['sign']);
  const bucket=async(kind,value)=>Array.from(new Uint8Array(await crypto.subtle.sign('HMAC',key,encoder.encode(JSON.stringify([kind,window,value])))),b=>b.toString(16).padStart(2,'0')).join('');
  const buckets=await Promise.all([bucket('source',source.toLowerCase()),bucket('recipient',email.toLowerCase()),bucket('global','')]);
  await env.DB.prepare('DELETE FROM withdrawal_test_rate_limits WHERE expires_at<=?').bind(now).run();
  const rows=(await env.DB.prepare('INSERT INTO withdrawal_test_rate_limits(bucket,hits,expires_at) VALUES (?,1,?),(?,1,?),(?,1,?) ON CONFLICT(bucket) DO UPDATE SET hits=hits+1 RETURNING bucket,hits').bind(buckets[0],end,buckets[1],end,buckets[2],end).all()).results;
  const counts=new Map(rows.map(row=>[row.bucket,row.hits]));
  if(buckets.some(x=>!Number.isSafeInteger(counts.get(x))))throw new Error('Test protection unavailable');
  return counts.get(buckets[0])>10||counts.get(buckets[1])>3||counts.get(buckets[2])>100 ? Math.max(1,Math.ceil((end-now)/1000)) : 0;
}

export async function acceptWithdrawal(db, input) {
  const {id,name,contract,email,language}=validateWithdrawal(input);
  const localized=await hasWithdrawalLanguageTable(db);
  if(!localized && language!=='de')throw new Error('Test language unavailable');
  const now = Date.now();
  const statements=[db.prepare('INSERT INTO withdrawal_requests(id,name,contract_reference,contact_email,received_at) VALUES(?,?,?,?,?) ON CONFLICT(id) DO NOTHING').bind(id,name,contract,email,now)];
  if(localized){
    statements.push(db.prepare('INSERT INTO withdrawal_test_languages(request_id,language) SELECT id,? FROM withdrawal_requests WHERE id=? AND name=? AND contract_reference=? AND contact_email=? AND changes()=1 ON CONFLICT(request_id) DO NOTHING').bind(language,id,name,contract,email));
    statements.push(db.prepare("INSERT INTO withdrawal_receipts(request_id,status) SELECT r.id,'pending' FROM withdrawal_requests r JOIN withdrawal_test_languages l ON l.request_id=r.id WHERE r.id=? AND r.name=? AND r.contract_reference=? AND r.contact_email=? AND l.language=? ON CONFLICT(request_id) DO NOTHING").bind(id,name,contract,email,language));
  }else statements.push(db.prepare("INSERT INTO withdrawal_receipts(request_id,status) SELECT id,'pending' FROM withdrawal_requests WHERE id=? AND name=? AND contract_reference=? AND contact_email=? ON CONFLICT(request_id) DO NOTHING").bind(id,name,contract,email));
  await db.batch(statements);
  const sql=localized?'SELECT r.name,r.contract_reference,r.contact_email,r.received_at,l.language FROM withdrawal_requests r LEFT JOIN withdrawal_test_languages l ON l.request_id=r.id WHERE r.id=?':"SELECT name,contract_reference,contact_email,received_at,'de' AS language FROM withdrawal_requests WHERE id=?";
  const record=await db.prepare(sql).bind(id).first();
  if (!record || record.language!==language || record.name !== name || record.contract_reference !== contract || record.contact_email !== email) throw new Error('Withdrawal request conflict');
  return { requestId:id, receivedAt:record.received_at, status:'receipt_pending' };
}

export function formatWithdrawalReceipt(record,language='de') {
  if (!record || !Number.isSafeInteger(record.received_at) || record.received_at < 0)
    throw new TypeError('Invalid withdrawal receipt');
  const name = inputText(record.name, 160);
  const contract = inputText(record.contract_reference, 1000);
  const email = inputText(record.contact_email, 254);
  if(!['de','en'].includes(language))throw new TypeError('Invalid receipt language');
  if(language==='en')return `Withdrawal receipt\n\nI withdraw from the following contract.\nName: ${name}\nContract: ${contract}\nContact: ${email}\nReceived: ${new Date(record.received_at).toISOString()}\n`;
  return `Eingang einer Widerrufserklärung\n\nHiermit widerrufe ich den folgenden Vertrag.\nName: ${name}\nVertrag: ${contract}\nKontakt: ${email}\nEingang: ${new Date(record.received_at).toISOString()}\n`;
}
