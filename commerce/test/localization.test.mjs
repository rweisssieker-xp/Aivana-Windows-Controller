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
  assert.match(de.html, /<html lang="de">/);
  assert.match(de.html, /Einmalkauf/);
  assert.match(de.html, /vier eigenen PCs/);
  assert.match(en.html, /<html lang="en">/);
  assert.match(en.html, /one-time purchase/);
  assert.match(preferred.html, /<html lang="de">/);
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

test('German page preserves the recovery code during language switching and guides the buyer', async () => {
  const html = await (await worker.fetch(new Request('https://buyer.test/?lang=de'), {})).text();
  const script = html.match(/<script>([\s\S]*?)<\/script>/)[1];
  const elements = Object.fromEntries(['status','backup','saved','buy','terms','recovery','copyRecovery','continue','restore','restoreCode','order','activate'].map(id => [id, {}]));
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
  assert.match(elements.status.textContent, /Speichere den Wiederherstellungscode/);
  elements.continue.onclick();
  assert.match(elements.status.textContent, /Speichere zuerst den Wiederherstellungscode/);
  elements.saved.checked = true;
  stopped = false;
  anchors[1].click({preventDefault(){stopped=true;}});
  assert.equal(stopped, false);
});

test('German buyer receives actionable translated checkout errors', async () => {
  const html = await (await worker.fetch(new Request('https://buyer.test/?lang=de'), {})).text();
  const script = html.match(/<script>([\s\S]*?)<\/script>/)[1];
  const elements = Object.fromEntries(['status','backup','saved','buy','terms','recovery','copyRecovery','continue','restore','restoreCode','order','activate'].map(id => [id, {}]));
  elements.backup.hidden = true;
  elements.terms.checked = false;
  const document = { getElementById: id => elements[id], querySelectorAll: () => [] };
  let sent;
  runInNewContext(script, { document, fetch: async (_, options) => { sent = JSON.parse(options.body); return { ok: false, json: async () => ({error:'Accept purchase terms'}) }; } });
  await elements.buy.onclick();
  assert.equal(sent.acceptTerms, false);
  assert.equal(elements.status.textContent, 'Bitte akzeptiere die Kaufbedingungen.');
  assert.equal(elements.backup.hidden, true);
});
