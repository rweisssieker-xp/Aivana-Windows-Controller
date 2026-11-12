// Isolated intake foundation. No HTTP route, transport, or live readiness gate
// uses this module yet. A pending receipt is not a delivered confirmation.
const testPage = `<!doctype html><html lang="de"><meta charset="utf-8"><meta name="viewport" content="width=device-width"><title>TEST – Vertrag widerrufen</title><h1>TEST: Vertrag widerrufen</h1><p>Isolierte Testfunktion: Es wird keine Bestätigung versendet. Nicht für echte Widerrufe verwenden.</p><label>Name <input id="name" maxlength="160" autocomplete="name"></label><label>Vertrag identifizieren <input id="contract" maxlength="1000"></label><label>E-Mail für die Eingangsbestätigung <input id="email" maxlength="254" type="email" autocomplete="email"></label><p>Keine Kauf- oder Wiederherstellungscodes eingeben.</p><button id="preview" type="button">Weiter</button><section><p id="details"></p><button id="edit" type="button" hidden>Angaben ändern</button><button id="confirm" type="button" hidden>Widerruf bestätigen</button></section><p id="status" role="status"></p><button id="new" type="button" hidden>Neuen Testvorgang beginnen (kann einen zweiten Datensatz erzeugen)</button><script>
const el=id=>document.getElementById(id); const names=['name','contract','email']; const storageKey='makrivo_withdrawal_test_id'; let snapshot=null,pending=false;
const validId=id=>typeof id==='string'&&/^[0-9a-f]{8}-[0-9a-f]{4}-4[0-9a-f]{3}-[89ab][0-9a-f]{3}-[0-9a-f]{12}$/i.test(id);
function newId(){const id=crypto.randomUUID();try{sessionStorage.setItem(storageKey,id);}catch{}return id;}
function loadId(){try{const id=sessionStorage.getItem(storageKey);if(validId(id))return id;}catch{}return newId();}
let requestId=loadId();
function invalidate(){snapshot=null;for(const id of names)el(id).disabled=false;el('confirm').hidden=true;el('edit').hidden=true;el('preview').hidden=false;el('details').textContent='';}
el('preview').onclick=()=>{ if(pending)return; const name=el('name').value.trim(), contract=el('contract').value.trim(), email=el('email').value.trim(); if(!name||!contract||!email){el('status').textContent='Bitte alle Angaben ausfüllen.';return;} snapshot={name,contract,email};for(const id of names)el(id).disabled=true;el('details').textContent='Ich widerrufe den Vertrag. Name: '+name+'; Vertrag: '+contract+'; Bestätigung an: '+email; el('confirm').hidden=false;el('edit').hidden=false;el('preview').hidden=true;el('confirm').disabled=false;el('status').textContent='Bitte Angaben prüfen.'; };
el('edit').onclick=()=>{if(pending)return;invalidate();el('status').textContent='Bitte Angaben ändern und erneut prüfen.';};
el('new').onclick=()=>{if(pending)return;invalidate();requestId=newId();for(const id of names)el(id).value='';el('new').hidden=true;el('status').textContent='Neuer Testvorgang.';};
el('confirm').onclick=async()=>{ if(!snapshot||pending)return; if(names.some(id=>el(id).value.trim()!==snapshot[id])){invalidate();el('status').textContent='Angaben wurden geändert. Bitte erneut prüfen.';return;} pending=true;el('confirm').disabled=true;el('edit').disabled=true;try { const response=await fetch('/withdrawal',{method:'POST',headers:{'Content-Type':'application/json'},body:JSON.stringify({...snapshot,confirmed:true,requestId})});if(response.status===409){el('status').textContent='TEST: Diese Kennung gehört zu anderen Angaben. Ein neuer Testvorgang kann einen zweiten Datensatz erzeugen.';el('new').hidden=false;return;}if(!response.ok)throw Error('failed');el('status').textContent='TEST: Erklärung gespeichert. Eine Eingangsbestätigung wurde nicht versendet.';el('confirm').hidden=true;el('edit').hidden=true;el('new').hidden=false;} catch {el('status').textContent='TEST: Speicherung konnte nicht bestätigt werden. Erneut versuchen.';el('confirm').disabled=false;} finally {pending=false;el('edit').disabled=false;} };
</script></html>`;

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
  if (req.method === 'GET') return new Response(testPage,{headers:{...headers,'Content-Type':'text/html;charset=utf-8','Content-Security-Policy':"default-src 'none'; script-src 'unsafe-inline'; connect-src 'self'; form-action 'none'; base-uri 'none'; frame-ancestors 'none'"}});
  if (req.method !== 'POST') return new Response('Method not allowed',{status:405,headers});
  if (req.headers.get('Origin') !== e.PUBLIC_ORIGIN) return new Response('Invalid origin',{status:403,headers});
  if (!/^application\/json(?:\s*;|$)/i.test(req.headers.get('Content-Type') || '')) return new Response('Invalid content type',{status:415,headers});
  try {
    const payload=JSON.parse(await readBounded(req));
    const result=await acceptWithdrawal(e.DB,payload);
    return new Response(JSON.stringify(result),{status:202,headers:{...headers,'Content-Type':'application/json;charset=utf-8'}});
  } catch (err) {
    const status=err instanceof RangeError ? 413 : err instanceof SyntaxError || err instanceof TypeError ? 400 : err?.message === 'Withdrawal request conflict' ? 409 : 503;
    return new Response(JSON.stringify({error:'Test request could not be completed'}),{status,headers:{...headers,'Content-Type':'application/json;charset=utf-8'}});
  }
}

function inputText(value, max) {
  if (typeof value !== 'string' || value.length > max || /[\u0000-\u001f\u007f]/.test(value) || !value.trim())
    throw new TypeError('Invalid withdrawal information');
  return value.trim();
}

export async function acceptWithdrawal(db, input) {
  if (!input || input.confirmed !== true || typeof input.requestId !== 'string' ||
      !/^[0-9a-f]{8}-[0-9a-f]{4}-4[0-9a-f]{3}-[89ab][0-9a-f]{3}-[0-9a-f]{12}$/i.test(input.requestId))
    throw new TypeError('Invalid withdrawal confirmation or request identifier');
  const id = input.requestId.toLowerCase();
  const name = inputText(input.name, 160);
  const contract = inputText(input.contract, 1000);
  const email = inputText(input.email, 254);
  if (!/^[^\s@]+@[^\s@]+$/u.test(email)) throw new TypeError('Invalid confirmation email');
  const now = Date.now();
  await db.batch([
    db.prepare('INSERT INTO withdrawal_requests(id,name,contract_reference,contact_email,received_at) VALUES(?,?,?,?,?) ON CONFLICT(id) DO NOTHING').bind(id,name,contract,email,now),
    db.prepare("INSERT INTO withdrawal_receipts(request_id,status) SELECT id,'pending' FROM withdrawal_requests WHERE id=? AND name=? AND contract_reference=? AND contact_email=? ON CONFLICT(request_id) DO NOTHING").bind(id,name,contract,email),
  ]);
  const record = await db.prepare('SELECT name,contract_reference,contact_email,received_at FROM withdrawal_requests WHERE id=?').bind(id).first();
  if (!record || record.name !== name || record.contract_reference !== contract || record.contact_email !== email) throw new Error('Withdrawal request conflict');
  return { requestId:id, receivedAt:record.received_at, status:'receipt_pending' };
}

export function formatWithdrawalReceipt(record) {
  if (!record || !Number.isSafeInteger(record.received_at) || record.received_at < 0)
    throw new TypeError('Invalid withdrawal receipt');
  const name = inputText(record.name, 160);
  const contract = inputText(record.contract_reference, 1000);
  const email = inputText(record.contact_email, 254);
  return `Eingang einer Widerrufserklärung\n\nHiermit widerrufe ich den folgenden Vertrag.\nName: ${name}\nVertrag: ${contract}\nKontakt: ${email}\nEingang: ${new Date(record.received_at).toISOString()}\n`;
}
