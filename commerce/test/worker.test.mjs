const fixtureDocuments=JSON.stringify({de:{version:'test-v1',text:'Testbedingungen'},en:{version:'test-v1',text:'Test terms'}});
async function fixtureDigest(lang) { const d=JSON.parse(fixtureDocuments)[lang]; if(!d)return null; return Buffer.from(await crypto.subtle.digest('SHA-256',new TextEncoder().encode(JSON.stringify({language:lang,version:d.version,text:d.text})))).toString('base64url'); }
const testSigner = await crypto.subtle.generateKey(
  {
    name: "RSASSA-PKCS1-v1_5",
    modulusLength: 3072,
    publicExponent: new Uint8Array([1, 0, 1]),
    hash: "SHA-256",
  },
  true,
  ["sign", "verify"],
);
const testPrivatePem =
  "-----BEGIN PRIVATE KEY-----\n" +
  Buffer.from(
    await crypto.subtle.exportKey("pkcs8", testSigner.privateKey),
  ).toString("base64") +
  "\n-----END PRIVATE KEY-----";
import { test } from "node:test";
import assert from "node:assert/strict";
import { DatabaseSync } from "node:sqlite";
import { readFileSync } from "node:fs";
import {
  validateSession,
  verifyWebhook,
  signLicense,
} from "../worker.mjs";
import { activationSql } from "../activation-sql.mjs";
test("paid checkout exact contract", () => {
  const env = {
    STRIPE_PRICE_ID: "price_a",
    STRIPE_PRODUCT_ID: "prod_a",
    STRIPE_LIVE: "false",
  };
  const s = {
    payment_intent: "pi_test",
    payment_status: "paid",
    mode: "payment",
    currency: "eur",
    amount_total: 2900,
    livemode: false,
    line_items: {
      data: [{ quantity: 1, price: { id: "price_a", product: "prod_a" } }],
    },
  };
  assert.equal(validateSession(s, env), true);
  for (const patch of [
    { mode: "subscription" },
    { payment_status: "unpaid" },
    { amount_total: 2800 },
    { livemode: true },
  ])
    assert.equal(validateSession({ ...s, ...patch }, env), false);
  assert.equal(validateSession({ ...s, line_items: { data: [] } }, env), false);
});
test("real SQLite seat cap, retries and revocation", () => {
  const db = new DatabaseSync(":memory:");
  db.exec(readFileSync(new URL("../schema.sql", import.meta.url), "utf8"));
  db.prepare("INSERT INTO orders(id,claim_hash,created_at) VALUES(?,?,?)").run(
    "o",
    "h",
    1,
  );
  db.prepare(
    "INSERT INTO entitlements(id,order_id,session_id) VALUES(?,?,?)",
  ).run("l", "o", "cs");
  const put = db.prepare(activationSql);
  for (let i = 0; i < 4; i++) assert.equal(put.run("l", String(i)).changes, 1);
  assert.equal(put.run("l", "4").changes, 0);
  assert.equal(put.run("l", "0").changes, 0);
  assert.throws(() =>
    db
      .prepare("INSERT INTO entitlements(id,order_id,session_id) VALUES(?,?,?)")
      .run("x", "o", "cs"),
  );
  db.prepare("UPDATE entitlements SET revoked=1").run();
  db.prepare("UPDATE devices SET active=0 WHERE device_id=?").run("0");
  assert.equal(put.run("l", "4").changes, 0);
});
test("raw webhook HMAC and timestamp", async () => {
  const body = '{"id":"evt"}',
    secret = "test";
  const key = await crypto.subtle.importKey(
    "raw",
    new TextEncoder().encode(secret),
    { name: "HMAC", hash: "SHA-256" },
    false,
    ["sign"],
  );
  const t = Math.floor(Date.now() / 1000);
  const sig = Buffer.from(
    await crypto.subtle.sign(
      "HMAC",
      key,
      new TextEncoder().encode(`${t}.${body}`),
    ),
  ).toString("hex");
  assert.equal(await verifyWebhook(body, `t=${t},v1=${sig}`, secret), true);
  assert.equal(
    await verifyWebhook(body + " ", `t=${t},v1=${sig}`, secret),
    false,
  );
  assert.equal(
    await verifyWebhook(body, `t=${t - 301},v1=${sig}`, secret),
    false,
  );
});
test("worker RSA envelope exact bytes", async () => {
  const pair = await crypto.subtle.generateKey(
    {
      name: "RSASSA-PKCS1-v1_5",
      modulusLength: 3072,
      publicExponent: new Uint8Array([1, 0, 1]),
      hash: "SHA-256",
    },
    true,
    ["sign", "verify"],
  );
  const der = Buffer.from(
    await crypto.subtle.exportKey("pkcs8", pair.privateKey),
  ).toString("base64");
  const e = await signLicense(
    "12345678-1234-1234-1234-123456789abc",
    "a".repeat(64),
    {
      LICENSE_PRIVATE_KEY_PEM: `-----BEGIN PRIVATE KEY-----\n${der}\n-----END PRIVATE KEY-----`,
      LICENSE_KEY_ID: "test",
    },
  );
  assert.equal(
    await crypto.subtle.verify(
      "RSASSA-PKCS1-v1_5",
      pair.publicKey,
      Buffer.from(e.signature, "base64url"),
      Buffer.from(e.payload, "base64url"),
    ),
    true,
  );
  assert.equal(JSON.parse(Buffer.from(e.payload, "base64url")).maxDevices, 4);
});
import worker from "../worker.mjs";
test("checkout can recover a lost response without creating a second session or replacing an existing claim", async () => {
  const db = database(), e = configured(db), original = globalThis.fetch;
  const sessions = new Map(); let posts = 0;
  try {
    globalThis.fetch = async (url, options) => {
      if (String(url).includes('/prices/')) return Response.json({active:true,type:'one_time',currency:'eur',unit_amount:2900,tax_behavior:'inclusive',product:'prod_a',livemode:false});
      if (options.method === 'POST') {
        posts++;
        const key = options.headers['Idempotency-Key'];
        assert.ok(key);
        if (!sessions.has(key)) sessions.set(key, {id:'cs_test_recover',mode:'payment',livemode:false,status:'open',payment_status:'unpaid',url:'https://checkout.stripe.com/c/pay/test',metadata:{orderId:new URLSearchParams(options.body).get('metadata[orderId]')}});
        return Response.json(sessions.get(key));
      }
      return Response.json([...sessions.values()][0]);
    };
    const request = (path, data, cookie='') => worker.fetch(new Request(e.PUBLIC_ORIGIN+path,{method:'POST',headers:{Origin:e.PUBLIC_ORIGIN,Cookie:cookie},body:JSON.stringify(data)}),e);
    const prep = await request('/api/checkout/prepare',{acceptTerms:true,lang:'de',contractDigest:await fixtureDigest('de')});
    assert.equal(prep.status, 200);
    const {recoveryCode} = await prep.json();
    assert.match(recoveryCode, /^[A-Za-z0-9_-]{43}$/);
    assert.equal(posts, 0);
    const first = await request('/api/checkout',{acceptTerms:true,recoveryCode},'makrivo_claim='+ 'A'.repeat(43));
    assert.equal(first.status,200);
    assert.equal(first.headers.get('Set-Cookie'),null);
    const second = await request('/api/checkout',{acceptTerms:true,recoveryCode},'makrivo_claim='+ 'B'.repeat(43));
    assert.equal(second.status,200);
    assert.equal(second.headers.get('Set-Cookie'),null);
    assert.equal((await second.json()).url,'https://checkout.stripe.com/c/pay/test');
    assert.equal(sessions.size,1);
    const unauthorized = await request('/api/checkout',{acceptTerms:true,recoveryCode:'C'.repeat(43),sessionId:'cs_test_recover'});
    assert.equal(unauthorized.status,401);
    assert.equal((await unauthorized.json()).url,undefined);
  } finally { globalThis.fetch=original; }
});
function database() {
  const d = new DatabaseSync(":memory:");
  d.exec(readFileSync(new URL("../schema.sql", import.meta.url), "utf8"));
  const docs=JSON.parse(fixtureDocuments);for (const [lang,doc] of Object.entries(docs)) d.prepare('INSERT INTO contract_documents VALUES(?,?,?)').run(lang,doc.version,doc.text);
  return {
    raw: d,
    async batch(statements) {
      d.exec("BEGIN IMMEDIATE");
      try {
        const results = statements.map((s) => s._run());
        d.exec("COMMIT");
        return results;
      } catch (e) {
        d.exec("ROLLBACK");
        throw e;
      }
    },
    prepare(sql) {
      return {
        bind(...v) {
          const p = d.prepare(sql);
          return {
            _run() {
              return p.run(...v);
            },
            async first() {
              return p.get(...v) || null;
            },
            async all() {
              return { results: p.all(...v) };
            },
            async run() {
              return p.run(...v);
            },
          };
        },
      };
    },
  };
}
test("public session IDs cannot authorize and disabled checkout fails closed", async () => {
  const db = database();
  const e = {
    DB: db,
    CONTRACT_VERSION_DE:'test-v1', CONTRACT_VERSION_EN:'test-v1',
    SALES_ENABLED: "true",
    LEGAL_APPROVED: "true",
    PUBLIC_ORIGIN: "https://buyer.test",
    STRIPE_LIVE: "false",
    STRIPE_SECRET_KEY: "test",
    STRIPE_WEBHOOK_SECRET: "test",
    STRIPE_PRICE_ID: "price_a",
    STRIPE_PRODUCT_ID: "prod_a",
    LICENSE_PRIVATE_KEY_PEM: testPrivatePem,
    LICENSE_KEY_ID: "test",
    LEGAL_TERMS_URL: "https://buyer.test/terms",
    PRIVACY_URL: "https://buyer.test/privacy",
    SELLER_IMPRINT_URL: "https://buyer.test/imprint",
  };
  assert.equal(
    (
      await worker.fetch(
        new Request("https://buyer.test/api/order?session_id=cs_public"),
        e,
      )
    ).status,
    401,
  );
  assert.equal(
    (
      await worker.fetch(
        new Request("https://buyer.test/api/checkout", {
          method: "POST",
          headers: { Origin: "https://evil.test" },
          body: "{}",
        }),
        e,
      )
    ).status,
    403,
  );
  assert.equal(
    (
      await worker.fetch(
        new Request("https://buyer.test/api/checkout", { method: "POST" }),
        {},
      )
    ).status,
    503,
  );
});
test("parallel seat attempts and deactivation reuse actual SQL", async () => {
  const db = database().raw;
  db.prepare("INSERT INTO orders VALUES(?,?,?,?)").run("o", "h", 1, "cs");
  db.prepare(
    "INSERT INTO entitlements(id,order_id,session_id) VALUES(?,?,?)",
  ).run("l", "o", "cs");
  await Promise.all(
    Array.from({ length: 20 }, (_, i) =>
      Promise.resolve().then(() =>
        db.prepare(activationSql).run("l", String(i)),
      ),
    ),
  );
  assert.equal(
    db.prepare("SELECT count(*) n FROM devices WHERE active=1").get().n,
    4,
  );
  db.prepare("UPDATE devices SET active=0 WHERE device_id=?").run("0");
  assert.equal(db.prepare(activationSql).run("l", "4").changes, 1);
  assert.equal(
    db.prepare("SELECT count(*) n FROM devices WHERE active=1").get().n,
    4,
  );
});
test("hostile payment variants rejected", () => {
  const e = {
      STRIPE_PRICE_ID: "p",
      STRIPE_PRODUCT_ID: "x",
      STRIPE_LIVE: "false",
    },
    s = {
      payment_intent: "pi_test",
      payment_status: "paid",
      mode: "payment",
      currency: "eur",
      amount_total: 2900,
      livemode: false,
      line_items: { data: [{ quantity: 1, price: { id: "p", product: "x" } }] },
    };
  for (const patch of [
    { currency: "usd" },
    { amount_total: 0 },
    { payment_status: "no_payment_required" },
    { line_items: { has_more: true, data: s.line_items.data } },
    {
      line_items: { data: [{ quantity: 2, price: { id: "p", product: "x" } }] },
    },
    {
      line_items: {
        data: [{ quantity: 1, price: { id: "wrong", product: "x" } }],
      },
    },
    {
      line_items: {
        data: [{ quantity: 1, price: { id: "p", product: "wrong" } }],
      },
    },
  ])
    assert.equal(validateSession({ ...s, ...patch }, e), false);
});
test("revocation tombstones survive out-of-order fulfillment", () => {
  const d = database().raw;
  d.prepare("INSERT INTO orders VALUES(?,?,?,?)").run("o", "h", 1, "cs");
  d.prepare("INSERT OR IGNORE INTO revoked_payments VALUES(?)").run("pi");
  d.prepare(
    "INSERT OR IGNORE INTO entitlements(id,order_id,session_id,payment_intent,revoked) VALUES(?,?,?,?,EXISTS(SELECT 1 FROM revoked_payments WHERE payment_intent=?))",
  ).run("l", "o", "cs", "pi", "pi");
  assert.equal(d.prepare("SELECT revoked FROM entitlements").get().revoked, 1);
  assert.equal(d.prepare(activationSql).run("l", "d").changes, 0);
});

test("oversized / stale / malformed webhook signatures rejected", async () => {
  assert.equal(
    await verifyWebhook("x".repeat(262145), "t=1,v1=" + "0".repeat(64), "s"),
    false,
  );
  assert.equal(
    await verifyWebhook("{}", "t=1,t=2,v1=" + "0".repeat(64), "s"),
    false,
  );
  assert.equal(
    await verifyWebhook("{}", "t=NaN,v1=" + "0".repeat(64), "s"),
    false,
  );
});
test("unknown and hostile claim cannot read paid receipt", async () => {
  const db = database();
  const e = {
    DB: db,
    CONTRACT_VERSION_DE:'test-v1', CONTRACT_VERSION_EN:'test-v1',
    SALES_ENABLED: "true",
    LEGAL_APPROVED: "true",
    PUBLIC_ORIGIN: "https://buyer.test",
    STRIPE_SECRET_KEY: "x",
    STRIPE_WEBHOOK_SECRET: "x",
    STRIPE_PRICE_ID: "x",
    STRIPE_PRODUCT_ID: "x",
    LICENSE_PRIVATE_KEY_PEM: testPrivatePem,
    LICENSE_KEY_ID: "x",
    LEGAL_TERMS_URL: "https://buyer.test/terms",
    PRIVACY_URL: "https://buyer.test/privacy",
    SELLER_IMPRINT_URL: "https://buyer.test/imprint",
  };
  for (const headers of [
    { Authorization: "Bearer " + "a".repeat(43) },
    { Cookie: "makrivo_claim=cs_public" },
    { Authorization: "Bearer ' OR 1=1 --" },
  ])
    assert.equal(
      (
        await worker.fetch(
          new Request("https://buyer.test/api/order", { headers }),
          e,
        )
      ).status,
      401,
    );
});
function configured(db) {
  return {
    DB: db,
    CONTRACT_VERSION_DE:'test-v1', CONTRACT_VERSION_EN:'test-v1',
    SALES_ENABLED: "true",
    LEGAL_APPROVED: "true",
    STRIPE_LIVE: "false",
    PUBLIC_ORIGIN: "https://buyer.test",
    STRIPE_SECRET_KEY: "sk_test",
    STRIPE_WEBHOOK_SECRET: "whsec_test",
    STRIPE_PRICE_ID: "price_a",
    STRIPE_PRODUCT_ID: "prod_a",
    LICENSE_PRIVATE_KEY_PEM: testPrivatePem,
    LICENSE_KEY_ID: "test",
    LEGAL_TERMS_URL: "https://buyer.test/terms",
    PRIVACY_URL: "https://buyer.test/privacy",
    SELLER_IMPRINT_URL: "https://buyer.test/imprint",
  };
}
async function signedEvent(
  e,
  type,
  id = "evt_test",
  object = { id: "cs_test" },
) {
  const raw = JSON.stringify({ id, type, livemode: false, data: { object } }),
    t = Math.floor(Date.now() / 1000);
  const key = await crypto.subtle.importKey(
    "raw",
    new TextEncoder().encode(e.STRIPE_WEBHOOK_SECRET),
    { name: "HMAC", hash: "SHA-256" },
    false,
    ["sign"],
  );
  const sig = Buffer.from(
    await crypto.subtle.sign(
      "HMAC",
      key,
      new TextEncoder().encode(`${t}.${raw}`),
    ),
  ).toString("hex");
  return new Request("https://buyer.test/api/stripe/webhook", {
    method: "POST",
    headers: { "Stripe-Signature": `t=${t},v1=${sig}` },
    body: raw,
  });
}
test("actual webhook duplicate parallel async failure and reordered refund transactions", async () => {
  const original = globalThis.fetch;
  try {
    const db = database(),
      e = configured(db);
    db.raw
      .prepare("INSERT INTO orders VALUES(?,?,?,?)")
      .run("o", "h", 1, "cs_test");
    const session = {
      id: "cs_test",
      metadata: { orderId: "o" },
      payment_intent: "pi_test",
      payment_status: "paid",
      mode: "payment",
      currency: "eur",
      amount_total: 2900,
      livemode: false,
      line_items: {
        data: [{ quantity: 1, price: { id: "price_a", product: "prod_a" } }],
      },
    };
    globalThis.fetch = async () => Response.json(session);
    let r = await worker.fetch(
      await signedEvent(e, "checkout.session.async_payment_failed"),
      e,
    );
    assert.equal(r.status, 200);
    assert.equal(
      db.raw.prepare("SELECT count(*) n FROM entitlements").get().n,
      0,
    );
    r = await worker.fetch(
      await signedEvent(e, "charge.refunded", "evt_refund", {
        payment_intent: "pi_test",
      }),
      e,
    );
    assert.equal(r.status, 200);
    const requests = await Promise.all(
      Array.from({ length: 8 }, () =>
        signedEvent(e, "checkout.session.completed"),
      ),
    );
    const responses = await Promise.all(
      requests.map((r) => worker.fetch(r, e)),
    );
    assert.ok(responses.every((r) => r.status === 200));
    assert.equal(
      db.raw.prepare("SELECT count(*) n FROM entitlements").get().n,
      1,
    );
    assert.equal(
      db.raw.prepare("SELECT revoked FROM entitlements").get().revoked,
      1,
    );
    assert.equal(db.raw.prepare("SELECT count(*) n FROM events").get().n, 1);
    r = await worker.fetch(
      await signedEvent(
        e,
        "checkout.session.async_payment_succeeded",
        "evt_async",
      ),
      e,
    );
    assert.equal(r.status, 200);
    assert.equal(
      db.raw.prepare("SELECT revoked FROM entitlements").get().revoked,
      1,
    );
  } finally {
    globalThis.fetch = original;
  }
});
async function prepareCheckout(e, lang) {
  const r = await worker.fetch(new Request(e.PUBLIC_ORIGIN + "/api/checkout/prepare", { method:"POST", headers:{Origin:e.PUBLIC_ORIGIN}, body:JSON.stringify({acceptTerms:true,lang,contractDigest:await fixtureDigest(lang)}) }),e);
  assert.equal(r.status,200);
  return (await r.json()).recoveryCode;
}
function checkoutRequest(e, recoveryCode, cookie = '') {
  return worker.fetch(new Request(e.PUBLIC_ORIGIN + "/api/checkout", {method:"POST",headers:{Origin:e.PUBLIC_ORIGIN,Cookie:cookie},body:JSON.stringify({acceptTerms:true,recoveryCode})}),e);
}
const goodPrice = {active:true,type:"one_time",currency:"eur",unit_amount:2900,tax_behavior:"inclusive",product:"prod_a",livemode:false};
test("lost database write and simultaneous retries use one idempotent Stripe session", async () => {
  const db=database(),e=configured(db),original=globalThis.fetch,prepare=db.prepare.bind(db),sessions=new Map();
  let failWrite=true; const keys=[];
  db.prepare=(sql)=>{
    const stmt=prepare(sql);
    if(!sql.startsWith('UPDATE orders SET session_id=')) return stmt;
    return {bind(...values){const bound=stmt.bind(...values);return {...bound,async run(){if(failWrite){failWrite=false;throw Error('simulated lost write');}return bound.run();}};}};
  };
  try {
    globalThis.fetch=async(url,options)=>{
      if(String(url).includes('/prices/')) return Response.json(goodPrice);
      if(options.method==='POST') {
        const key=options.headers['Idempotency-Key']; keys.push(key);
        if(!sessions.has(key)) sessions.set(key,{id:'cs_test_lost',mode:'payment',livemode:false,status:'open',payment_status:'unpaid',url:'https://checkout.stripe.com/pay',metadata:{orderId:new URLSearchParams(options.body).get('metadata[orderId]')}});
        await Promise.resolve(); return Response.json(sessions.get(key));
      }
      return Response.json([...sessions.values()][0]);
    };
    const code=await prepareCheckout(e,'de');
    assert.equal((await checkoutRequest(e,code)).status,400);
    assert.equal(db.raw.prepare('SELECT session_id FROM orders').get().session_id,null);
    const [a,b]=await Promise.all([checkoutRequest(e,code,'makrivo_claim='+'A'.repeat(43)),checkoutRequest(e,code,'makrivo_claim='+'B'.repeat(43))]);
    assert.equal(a.status,200);assert.equal(b.status,200);
    assert.equal(a.headers.get('Set-Cookie'),null);assert.equal(b.headers.get('Set-Cookie'),null);
    assert.equal(sessions.size,1);assert.equal(new Set(keys).size,1);assert.ok(keys.length>=2);
    assert.equal(db.raw.prepare('SELECT session_id FROM orders').get().session_id,'cs_test_lost');
  } finally {globalThis.fetch=original;}
});
test("provider conflict or lost provider response can be retried with the same idempotency key", async () => {
  const original=globalThis.fetch;
  try {
    for(const failure of ['conflict','timeout']) {
      const db=database(),e=configured(db),sessions=new Map(),keys=[];let first=true;
      globalThis.fetch=async(url,options)=>{
        if(String(url).includes('/prices/')) return Response.json(goodPrice);
        if(options.method==='POST') {
          const key=options.headers['Idempotency-Key'];keys.push(key);
          if(!sessions.has(key)) sessions.set(key,{id:'cs_test_retry',mode:'payment',livemode:false,status:'open',payment_status:'unpaid',url:'https://checkout.stripe.com/pay',metadata:{orderId:new URLSearchParams(options.body).get('metadata[orderId]')}});
          if(first){first=false;if(failure==='timeout')throw Error('simulated timeout');return new Response('',{status:409});}
          return Response.json(sessions.get(key));
        }
        return Response.json([...sessions.values()][0]);
      };
      const code=await prepareCheckout(e,'de'),failed=await checkoutRequest(e,code);
      assert.equal(failed.status,400);assert.equal((await failed.json()).url,undefined);
      assert.equal((await checkoutRequest(e,code)).status,200);
      assert.equal(sessions.size,1);assert.equal(new Set(keys).size,1);assert.equal(keys.length,2);
    }
  } finally {globalThis.fetch=original;}
});
test("lost response after committed session mapping resumes without another Stripe creation", async () => {
  const db=database(),e=configured(db),original=globalThis.fetch,prepare=db.prepare.bind(db);let lost=true,posts=0,session;
  db.prepare=sql=>{const stmt=prepare(sql);if(!sql.startsWith('UPDATE orders SET session_id='))return stmt;return {bind(...values){const bound=stmt.bind(...values);return {...bound,async run(){const result=await bound.run();if(lost){lost=false;throw Error('lost committed response');}return result;}};}};};
  try {
    globalThis.fetch=async(url,options)=>{
      if(String(url).includes('/prices/'))return Response.json(goodPrice);
      if(options.method==='POST'){posts++;session={id:'cs_test_commit',mode:'payment',livemode:false,status:'open',payment_status:'unpaid',url:'https://checkout.stripe.com/pay',metadata:{orderId:new URLSearchParams(options.body).get('metadata[orderId]')}};}
      return Response.json(session);
    };
    const code=await prepareCheckout(e,'de');assert.equal((await checkoutRequest(e,code)).status,400);
    assert.equal(db.raw.prepare('SELECT session_id FROM orders').get().session_id,'cs_test_commit');
    assert.equal((await checkoutRequest(e,code)).status,200);assert.equal(posts,1);
  } finally {globalThis.fetch=original;}
});
test("an ambiguous old attempt is never recreated after the idempotency window", async () => {
  const db=database(),e=configured(db),original=globalThis.fetch;let requests=0;
  try {
    const code=await prepareCheckout(e,'de');
    db.raw.prepare('UPDATE orders SET created_at=?').run(Date.now()-21*60*60*1000);
    globalThis.fetch=async()=>{requests++;throw Error('must not reach provider');};
    const r=await checkoutRequest(e,code);
    assert.equal(r.status,409);assert.equal((await r.json()).error,'Checkout needs support');assert.equal(requests,0);
  } finally {globalThis.fetch=original;}
});
test("checkout migration preserves existing orders and entitlements", () => {
  const db=new DatabaseSync(':memory:');
  db.exec('CREATE TABLE orders(id TEXT PRIMARY KEY, claim_hash TEXT NOT NULL UNIQUE, created_at INTEGER NOT NULL, session_id TEXT UNIQUE); CREATE TABLE entitlements(id TEXT PRIMARY KEY, order_id TEXT NOT NULL UNIQUE REFERENCES orders(id));');
  db.prepare('INSERT INTO orders VALUES(?,?,?,?)').run('existing','hashed',1,'cs_test');
  db.prepare('INSERT INTO entitlements VALUES(?,?)').run('license','existing');
  db.exec(readFileSync(new URL('../migrations/0003_checkout_attempts.sql',import.meta.url),'utf8'));
  assert.equal(db.prepare('SELECT session_id FROM orders WHERE id=?').get('existing').session_id,'cs_test');
  assert.equal(db.prepare('SELECT order_id FROM entitlements WHERE id=?').get('license').order_id,'existing');
  db.close();
});
test("a pre-migration open checkout resumes only with its saved claim", async () => {
  const db=database(),e=configured(db),original=globalThis.fetch,code='A'.repeat(43);
  const digest=Buffer.from(await crypto.subtle.digest('SHA-256',new TextEncoder().encode(code))).toString('base64url');
  db.raw.prepare('INSERT INTO orders VALUES(?,?,?,?)').run('old-order',digest,Date.now(),'cs_old');
  try {
    let posts=0;
    globalThis.fetch=async(_url,options)=>{if(options.method==='POST')posts++;return Response.json({id:'cs_old',mode:'payment',livemode:false,status:'open',payment_status:'unpaid',url:'https://checkout.stripe.com/pay',metadata:{orderId:'old-order'}});};
    const r=await checkoutRequest(e,code);assert.equal(r.status,200);assert.equal((await r.json()).url,'https://checkout.stripe.com/pay');assert.equal(posts,0);
    assert.equal((await checkoutRequest(e,'B'.repeat(43))).status,401);
    db.raw.prepare('UPDATE orders SET session_id=NULL WHERE id=?').run('old-order');
    assert.equal((await checkoutRequest(e,code)).status,409);assert.equal(posts,0);
  } finally {globalThis.fetch=original;}
});
test("checkout refuses live price in test mode before creating a session", async () => {
  const original=globalThis.fetch, e=configured(database()); let creates=0;
  try {
    globalThis.fetch=async (_url, options) => { if(options.method==='POST') creates++; return Response.json({...goodPrice,livemode:true}); };
    const code=await prepareCheckout(e,'de');
    assert.equal((await checkoutRequest(e,code)).status,503);
    assert.equal(creates,0);
  } finally { globalThis.fetch=original; }
});
test("checkout preserves whitelisted return language across retries", async () => {
  const original=globalThis.fetch, e=configured(database()), captured=[], sessions=new Map();
  try {
    globalThis.fetch=async (url, options) => {
      if(String(url).includes('/prices/')) return Response.json(goodPrice);
      if(options.method==='POST') {
        const body=new URLSearchParams(options.body); captured.push(body);
        const s={id:'cs_test_'+captured.length,mode:'payment',livemode:false,status:'open',payment_status:'unpaid',url:'https://checkout.stripe.com/c/pay/test',metadata:{orderId:body.get('metadata[orderId]')}};
        sessions.set(s.id,s); return Response.json(s);
      }
      return Response.json(sessions.get(String(url).split('/').pop()));
    };
    for(const lang of ['de','en']) {
      const code=await prepareCheckout(e,lang);
      assert.equal((await checkoutRequest(e,code)).status,200);
      assert.equal((await checkoutRequest(e,code)).status,200);
    }
    assert.equal(captured.length,2);
    assert.equal(captured[0].get('adaptive_pricing[enabled]'),'false');
    assert.equal(captured[0].get('success_url'),e.PUBLIC_ORIGIN+'/?lang=de');
    assert.equal(captured[1].get('cancel_url'),e.PUBLIC_ORIGIN+'/?lang=en');
    for(const lang of ['//attacker.test',null]) {
      const r=await worker.fetch(new Request(e.PUBLIC_ORIGIN+'/api/checkout/prepare',{method:'POST',headers:{Origin:e.PUBLIC_ORIGIN},body:JSON.stringify({acceptTerms:true,lang})}),e); assert.equal(r.status,409);
    }
  } finally { globalThis.fetch=original; }
});
test("checkout recovery code is hash-only and restores cookie without public session proof", async () => {
  const db=database(),e=configured(db),code=await prepareCheckout(e,'de');
  assert.match(code,/^[A-Za-z0-9_-]{43}$/);
  assert.notEqual(db.raw.prepare('SELECT claim_hash FROM orders').get().claim_hash,code);
  const restore=await worker.fetch(new Request(e.PUBLIC_ORIGIN+'/api/claim/restore',{method:'POST',headers:{Origin:e.PUBLIC_ORIGIN},body:JSON.stringify({recoveryCode:code})}),e);
  assert.equal(restore.status,200); assert.match(restore.headers.get('Set-Cookie'),/HttpOnly/);
  assert.equal((await worker.fetch(new Request(e.PUBLIC_ORIGIN+'/api/order',{headers:{Authorization:`Bearer ${code}`}}),e)).status,200);
});
test("checkout rejects invalid provider responses and closed or paid sessions", async () => {
  const original=globalThis.fetch;
  try {
    for(const patch of [{url:'https://evil.test/pay'},{url:'http://checkout.stripe.com/pay'},{mode:'subscription'},{livemode:true},{status:'complete'},{payment_status:'paid'}]) {
      const e=configured(database()); let session;
      globalThis.fetch=async (url,options) => {
        if(String(url).includes('/prices/')) return Response.json(goodPrice);
        if(options.method==='POST') {const body=new URLSearchParams(options.body); session={id:'cs_test',mode:'payment',livemode:false,status:'open',payment_status:'unpaid',url:'https://checkout.stripe.com/pay',metadata:{orderId:body.get('metadata[orderId]')},...patch};}
        return Response.json(session);
      };
      const code=await prepareCheckout(e,'de'),r=await checkoutRequest(e,code);
      assert.equal(r.status, patch.status || patch.payment_status ? 409 : 502); assert.equal((await r.json()).url,undefined);
    }
  } finally { globalThis.fetch=original; }
});
test("actual paid fulfillment then refund disables later issuance", async () => {
  const original = globalThis.fetch;
  try {
    const db = database(),
      e = configured(db);
    db.raw
      .prepare("INSERT INTO orders VALUES(?,?,?,?)")
      .run("o", "h", 1, "cs_test");
    globalThis.fetch = async () =>
      Response.json({
        id: "cs_test",
        metadata: { orderId: "o" },
        payment_intent: "pi_test",
        payment_status: "paid",
        mode: "payment",
        currency: "eur",
        amount_total: 2900,
        livemode: false,
        line_items: {
          data: [{ quantity: 1, price: { id: "price_a", product: "prod_a" } }],
        },
      });
    assert.equal(
      (
        await worker.fetch(
          await signedEvent(e, "checkout.session.completed"),
          e,
        )
      ).status,
      200,
    );
    assert.equal(
      db.raw.prepare("SELECT revoked FROM entitlements").get().revoked,
      0,
    );
    assert.equal(
      (
        await worker.fetch(
          await signedEvent(e, "charge.refunded", "evt_refund", {
            payment_intent: "pi_test",
          }),
          e,
        )
      ).status,
      200,
    );
    assert.equal(
      db.raw.prepare("SELECT revoked FROM entitlements").get().revoked,
      1,
    );
  } finally {
    globalThis.fetch = original;
  }
});
test("recovery rejects unknown code and cross-origin mutation", async () => {
  const e = configured(database());
  for (const [origin, code, expected] of [
    [e.PUBLIC_ORIGIN, "a".repeat(43), 401],
    ["https://evil.test", "a".repeat(43), 403],
    [e.PUBLIC_ORIGIN, "cs_public", 401],
  ]) {
    const r = await worker.fetch(
      new Request(e.PUBLIC_ORIGIN + "/api/claim/restore", {
        method: "POST",
        headers: { Origin: origin },
        body: JSON.stringify({ recoveryCode: code }),
      }),
      e,
    );
    assert.equal(r.status, expected);
    assert.equal(r.headers.get("Set-Cookie"), null);
  }
});
test("malformed, public, undersized and invalid-ID signing configs block checkout before provider", async () => {
  const pair = await crypto.subtle.generateKey(
    {
      name: "RSASSA-PKCS1-v1_5",
      modulusLength: 2048,
      publicExponent: new Uint8Array([1, 0, 1]),
      hash: "SHA-256",
    },
    true,
    ["sign", "verify"],
  );
  const small =
    "-----BEGIN PRIVATE KEY-----\n" +
    Buffer.from(
      await crypto.subtle.exportKey("pkcs8", pair.privateKey),
    ).toString("base64") +
    "\n-----END PRIVATE KEY-----";
  const pub =
    "-----BEGIN PUBLIC KEY-----\n" +
    Buffer.from(await crypto.subtle.exportKey("spki", pair.publicKey)).toString(
      "base64",
    ) +
    "\n-----END PUBLIC KEY-----";
  const original = globalThis.fetch;
  try {
    let calls = 0;
    globalThis.fetch = async () => {
      calls++;
      throw Error("must not call provider");
    };
    for (const patch of [
      { LICENSE_PRIVATE_KEY_PEM: "not a key" },
      { LICENSE_PRIVATE_KEY_PEM: small },
      { LICENSE_PRIVATE_KEY_PEM: pub },
      { LICENSE_KEY_ID: "" },
      { LICENSE_KEY_ID: "x".repeat(65) },
    ]) {
      const r = await worker.fetch(
        new Request("https://buyer.test/api/checkout", {
          method: "POST",
          headers: { Origin: "https://buyer.test" },
          body: '{"acceptTerms":true}',
        }),
        { ...configured(database()), ...patch },
      );
      assert.equal(r.status, 503);
    }
    assert.equal(calls, 0);
  } finally {
    globalThis.fetch = original;
  }
});
test("paused new sales preserve refunds, paid order read and recovery even with broken signer", async () => {
  const db = database(),
    e = {
      ...configured(db),
      SALES_ENABLED: "false",
      LICENSE_PRIVATE_KEY_PEM: "broken",
    };
  const code = "r".repeat(43),
    claimHash = Buffer.from(
      await crypto.subtle.digest("SHA-256", new TextEncoder().encode(code)),
    ).toString("base64url");
  db.raw
    .prepare("INSERT INTO orders VALUES(?,?,?,?)")
    .run("o", claimHash, 1, "cs_test");
  db.raw
    .prepare(
      "INSERT INTO entitlements(id,order_id,session_id,payment_intent) VALUES(?,?,?,?)",
    )
    .run("l", "o", "cs_test", "pi_test");
  const checkout = await worker.fetch(
    new Request(e.PUBLIC_ORIGIN + "/api/checkout", {
      method: "POST",
      headers: { Origin: e.PUBLIC_ORIGIN },
      body: '{"acceptTerms":true}',
    }),
    e,
  );
  assert.equal(checkout.status, 503);
  const order = await worker.fetch(
    new Request(e.PUBLIC_ORIGIN + "/api/order", {
      headers: { Authorization: "Bearer " + code },
    }),
    e,
  );
  assert.equal(order.status, 200);
  assert.equal((await order.json()).status, "paid");
  const restore = await worker.fetch(
    new Request(e.PUBLIC_ORIGIN + "/api/claim/restore", {
      method: "POST",
      headers: { Origin: e.PUBLIC_ORIGIN },
      body: JSON.stringify({ recoveryCode: code }),
    }),
    e,
  );
  assert.equal(restore.status, 200);
  const refund = await worker.fetch(
    await signedEvent(e, "charge.refunded", "evt_refund", {
      payment_intent: "pi_test",
    }),
    e,
  );
  assert.equal(refund.status, 200);
  assert.equal(
    db.raw.prepare("SELECT revoked FROM entitlements").get().revoked,
    1,
  );
  assert.equal(
    db.raw.prepare("SELECT count(*) n FROM revoked_payments").get().n,
    1,
  );
});
test("paused new sales still allow entitled activation with valid signer", async () => {
  const db = database(),
    e = { ...configured(db), SALES_ENABLED: "false", LEGAL_APPROVED: "false" },
    code = "s".repeat(43);
  const h = Buffer.from(
    await crypto.subtle.digest("SHA-256", new TextEncoder().encode(code)),
  ).toString("base64url");
  db.raw
    .prepare("INSERT INTO orders VALUES(?,?,?,?)")
    .run("o", h, 1, "cs_test");
  db.raw
    .prepare("INSERT INTO entitlements(id,order_id,session_id) VALUES(?,?,?)")
    .run("12345678-1234-1234-1234-123456789abc", "o", "cs_test");
  const r = await worker.fetch(
    new Request(e.PUBLIC_ORIGIN + "/api/licenses/activate", {
      method: "POST",
      headers: { Origin: e.PUBLIC_ORIGIN, Authorization: "Bearer " + code },
      body: JSON.stringify({ deviceId: "a".repeat(64) }),
    }),
    e,
  );
  assert.equal(r.status, 200);
  assert.ok((await r.json()).signature);
});
test("buyer page exposes device deactivation and authenticated handler releases one seat", async () => {
  const db = database(),
    e = configured(db),
    code = "d".repeat(43),
    id = "12345678-1234-1234-1234-123456789abc",
    device = "a".repeat(64);
  const h = Buffer.from(
    await crypto.subtle.digest("SHA-256", new TextEncoder().encode(code)),
  ).toString("base64url");
  db.raw
    .prepare("INSERT INTO orders VALUES(?,?,?,?)")
    .run("o", h, 1, "cs_test");
  db.raw
    .prepare("INSERT INTO entitlements(id,order_id,session_id) VALUES(?,?,?)")
    .run(id, "o", "cs_test");
  db.raw.prepare(activationSql).run(id, device);
  const page = await (
    await worker.fetch(new Request(e.PUBLIC_ORIGIN + "/"), e)
  ).text();
  assert.match(page, /Deactivate this PC/);
  assert.match(page, /Available seats:/);
  assert.match(page, /await call\('\/api\/licenses\/deactivate'/);
  const req = () =>
    new Request(e.PUBLIC_ORIGIN + "/api/licenses/deactivate", {
      method: "POST",
      headers: { Origin: e.PUBLIC_ORIGIN, Authorization: "Bearer " + code },
      body: JSON.stringify({ deviceId: device }),
    });
  assert.equal((await worker.fetch(req(), e)).status, 200);
  const order = await worker.fetch(
    new Request(e.PUBLIC_ORIGIN + "/api/order", {
      headers: { Authorization: "Bearer " + code },
    }),
    e,
  );
  assert.equal((await order.json()).devices.length, 0);
  assert.equal(
    db.raw.prepare("SELECT active FROM devices WHERE device_id=?").get(device)
      .active,
    0,
  );
  assert.equal((await worker.fetch(req(), e)).status, 200);
});

test("buyer rate buckets hash IP, retain 30 request limit and prune only expired rows", async () => {
  const db = database(),
    e = configured(db);
  const originalNow = Date.now;
  const now = 1800000000000;
  Date.now = () => now;
  try {
    db.raw
      .prepare("INSERT INTO rate_limits(bucket,hits,expires_at) VALUES(?,?,?)")
      .run("expired", 7, now - 1);
    db.raw
      .prepare("INSERT INTO rate_limits(bucket,hits,expires_at) VALUES(?,?,?)")
      .run("active", 7, now + 600000);
    for (let i = 0; i < 31; i++) {
      const r = await worker.fetch(
        new Request(e.PUBLIC_ORIGIN + "/api/order", {
          headers: { "CF-Connecting-IP": "203.0.113.42" },
        }),
        e,
      );
      assert.equal(r.status, i < 30 ? 401 : 429);
    }
    const rows = db.raw.prepare("SELECT * FROM rate_limits").all();
    assert.equal(
      rows.some((r) => r.bucket === "expired"),
      false,
    );
    assert.equal(rows.find((r) => r.bucket === "active").hits, 7);
    const bucket = rows.find((r) => r.bucket !== "active");
    assert.match(bucket.bucket, /^[A-Za-z0-9_-]{43}$/);
    assert.equal(bucket.hits, 31);
    assert.equal(bucket.expires_at, (Math.floor(now / 60000) + 2) * 60000);
    assert.equal(JSON.stringify(rows).includes("203.0.113.42"), false);
    Date.now = () => now + 60000;
    assert.equal(
      (
        await worker.fetch(
          new Request(e.PUBLIC_ORIGIN + "/api/order", {
            headers: { "CF-Connecting-IP": "203.0.113.42" },
          }),
          e,
        )
      ).status,
      401,
    );
  } finally {
    Date.now = originalNow;
  }
});
test("scheduled cleanup preserves newer buckets and commerce records without sale configuration", async () => {
  const db = database(),
    now = Date.now();
  db.raw
    .prepare("INSERT INTO orders VALUES(?,?,?,?)")
    .run("o", "h", 1, "cs_test");
  db.raw
    .prepare("INSERT INTO entitlements(id,order_id,session_id) VALUES(?,?,?)")
    .run("l", "o", "cs_test");
  db.raw.prepare("INSERT INTO revoked_payments VALUES(?)").run("pi_test");
  db.raw.prepare("INSERT INTO devices VALUES(?,?,?)").run("l", "device", 1);
  db.raw
    .prepare("INSERT INTO events VALUES(?,?)")
    .run("evt", "checkout.session.completed");
  db.raw
    .prepare("INSERT INTO rate_limits VALUES(?,?,?)")
    .run("expired", 2, now - 1);
  db.raw
    .prepare("INSERT INTO rate_limits VALUES(?,?,?)")
    .run("future", 2, now + 600000);
  await worker.scheduled({ scheduledTime: now + 999999999 }, { DB: db }, {});
  assert.deepEqual(
    db.raw
      .prepare("SELECT bucket FROM rate_limits")
      .all()
      .map((r) => r.bucket),
    ["future"],
  );
  for (const table of [
    "orders",
    "entitlements",
    "devices",
    "events",
    "revoked_payments",
  ])
    assert.equal(db.raw.prepare("SELECT count(*) n FROM " + table).get().n, 1);
});
test("explicit existing-install migration preserves buckets then prunes legacy raw IP rows", async () => {
  const db = database();
  db.raw.exec(
    "DROP TABLE rate_limits; CREATE TABLE rate_limits(bucket TEXT PRIMARY KEY,hits INTEGER NOT NULL);",
  );
  db.raw
    .prepare("INSERT INTO rate_limits VALUES(?,?)")
    .run("203.0.113.7:123", 9);
  db.raw.exec(
    readFileSync(
      new URL("../migrations/0002_rate_limit_retention.sql", import.meta.url),
      "utf8",
    ),
  );
  assert.equal(
    db.raw.prepare("SELECT expires_at FROM rate_limits").get().expires_at,
    null,
  );
  const now = Date.now();
  db.raw
    .prepare("INSERT INTO rate_limits VALUES(?,?,?)")
    .run("future", 1, now + 600000);
  await worker.scheduled({}, { DB: db }, {});
  assert.deepEqual(
    db.raw
      .prepare("SELECT bucket FROM rate_limits")
      .all()
      .map((r) => r.bucket),
    ["future"],
  );
  db.raw
    .prepare("INSERT INTO rate_limits(bucket,hits) VALUES(?,?)")
    .run("203.0.113.8:legacy", 1);
  assert.equal(
    (
      await worker.fetch(
        new Request("https://buyer.test/api/order"),
        configured(db),
      )
    ).status,
    401,
  );
  assert.equal(
    db.raw
      .prepare("SELECT count(*) n FROM rate_limits WHERE expires_at IS NULL")
      .get().n,
    0,
  );
  assert.equal(
    db.raw.prepare("SELECT hits FROM rate_limits WHERE bucket='future'").get()
      .hits,
    1,
  );
});

test('checkout rejects a changed or missing displayed contract version before creating an order', async () => {
  const db = database(); const e = configured(db);

  const post = input => worker.fetch(new Request(e.PUBLIC_ORIGIN+'/api/checkout/prepare', {method:'POST',headers:{Origin:e.PUBLIC_ORIGIN},body:JSON.stringify(input)}),e);
  assert.equal((await post({acceptTerms:true,lang:'de'})).status, 409);
  assert.equal(db.raw.prepare('SELECT count(*) n FROM orders').get().n,0);
  assert.equal((await post({acceptTerms:true,lang:'de',contractDigest:'wrong'})).status, 409);
  assert.equal(db.raw.prepare('SELECT count(*) n FROM orders').get().n,0);
});

test('checkout records the exact terms presented and accepted without trusting client text', async () => {
  const db=database(), e=configured(db);
  const original='First <script>alert(1)</script> terms';
  db.raw.prepare('INSERT INTO contract_documents VALUES(?,?,?)').run('de','v1',original); e.CONTRACT_VERSION_DE='v1';
  const page=await (await worker.fetch(new Request(e.PUBLIC_ORIGIN+'/?lang=de'),e)).text();
  assert.ok(page.includes('First &lt;script&gt;alert(1)&lt;/script&gt; terms'));
  assert.ok(!page.includes(original));
  const digest=Buffer.from(await crypto.subtle.digest('SHA-256',new TextEncoder().encode(JSON.stringify({language:'de',version:'v1',text:original})))).toString('base64url');
  const res=await worker.fetch(new Request(e.PUBLIC_ORIGIN+'/api/checkout/prepare',{method:'POST',headers:{Origin:e.PUBLIC_ORIGIN},body:JSON.stringify({lang:'de',acceptTerms:true,contractDigest:digest,contractText:'attacker'})}),e);
  assert.equal(res.status,200);
  const record=db.raw.prepare('SELECT language,version,text,digest,accepted_at FROM contract_acceptances').get();
  assert.equal(record.text,original);assert.equal(record.language,'de');assert.equal(record.version,'v1');assert.equal(record.digest,digest);assert.ok(record.accepted_at>0);
});

test('a paid buyer can download the accepted terms, while a pending or unrelated buyer cannot', async () => {
  const db=database(), e=configured(db), code='A'.repeat(43), other='B'.repeat(43);
  const h=Buffer.from(await crypto.subtle.digest('SHA-256',new TextEncoder().encode(code))).toString('base64url');
  const h2=Buffer.from(await crypto.subtle.digest('SHA-256',new TextEncoder().encode(other))).toString('base64url');
  db.raw.prepare('INSERT INTO orders VALUES(?,?,?,?)').run('o',h,1,'cs');
  db.raw.prepare('INSERT INTO orders VALUES(?,?,?,?)').run('o2',h2,1,'cs2');
  db.raw.prepare('INSERT INTO contract_acceptances VALUES(?,?,?,?,?,?)').run('o','de','v1','Original terms','digest',1);
  const request=c=>worker.fetch(new Request(e.PUBLIC_ORIGIN+'/api/order/terms',{headers:{Authorization:'Bearer '+c}}),e);
  assert.equal((await request(code)).status,403);
  db.raw.prepare('INSERT INTO entitlements(id,order_id,session_id) VALUES(?,?,?)').run('l','o','cs');
  const r=await request(code);assert.equal(r.status,200);assert.match(r.headers.get('Content-Type'),/^text\/plain/);assert.match(await r.text(),/Original terms/);
  assert.equal((await request(other)).status,403);
});

test('checkout remains disabled when contract documents are missing a valid version', async () => {
  const e=configured(database());delete e.CONTRACT_VERSION_DE;
  const r=await worker.fetch(new Request(e.PUBLIC_ORIGIN+'/api/checkout/prepare',{method:'POST',headers:{Origin:e.PUBLIC_ORIGIN},body:JSON.stringify({acceptTerms:true,lang:'de'})}),e);
  assert.equal(r.status,503);
});

test('changes after the buyer loaded the terms block the order and preserve prior evidence', async () => {
  const db=database(), e=configured(db), old=await fixtureDigest('de');
  db.raw.prepare('INSERT INTO contract_documents VALUES(?,?,?)').run('de','test-v2','A different version'); e.CONTRACT_VERSION_DE='test-v2';
  const res=await worker.fetch(new Request(e.PUBLIC_ORIGIN+'/api/checkout/prepare',{method:'POST',headers:{Origin:e.PUBLIC_ORIGIN},body:JSON.stringify({acceptTerms:true,lang:'de',contractDigest:old})}),e);
  assert.equal(res.status,409);assert.equal(db.raw.prepare('SELECT count(*) n FROM orders').get().n,0);
});

test('failed evidence storage rolls back the entire prepared order', async () => {
  const db=database(), e=configured(db), prepare=db.prepare.bind(db);
  db.prepare=sql=>sql.startsWith('INSERT INTO contract_acceptances')?{bind(){return {_run(){throw Error('storage failed');}};}}:prepare(sql);
  const r=await worker.fetch(new Request(e.PUBLIC_ORIGIN+'/api/checkout/prepare',{method:'POST',headers:{Origin:e.PUBLIC_ORIGIN},body:JSON.stringify({acceptTerms:true,lang:'de',contractDigest:await fixtureDigest('de')})}),e);
  assert.equal(r.status,400);assert.equal(db.raw.prepare('SELECT count(*) n FROM orders').get().n,0);assert.equal(db.raw.prepare('SELECT count(*) n FROM checkout_attempts').get().n,0);
});

test('contract acceptance migration preserves existing orders', () => {
  const db=new DatabaseSync(':memory:');db.exec('PRAGMA foreign_keys=ON; CREATE TABLE orders(id TEXT PRIMARY KEY,claim_hash TEXT NOT NULL,created_at INTEGER NOT NULL,session_id TEXT)');
  db.prepare('INSERT INTO orders VALUES(?,?,?,?)').run('existing','hashed',1,'cs_old');
  db.exec(readFileSync(new URL('../migrations/0004_contract_acceptances.sql',import.meta.url),'utf8'));
  assert.equal(db.prepare('SELECT session_id FROM orders WHERE id=?').get('existing').session_id,'cs_old');
  assert.equal(db.prepare('SELECT count(*) n FROM contract_acceptances').get().n,0);db.close();
});

test('live checkout stays disabled until contract confirmation delivery exists', async () => {
  const e=configured(database()); e.STRIPE_LIVE='true';
  const r=await worker.fetch(new Request(e.PUBLIC_ORIGIN+'/api/checkout/prepare',{method:'POST',headers:{Origin:e.PUBLIC_ORIGIN},body:JSON.stringify({acceptTerms:true,lang:'de',contractDigest:await fixtureDigest('de')})}),e);
  assert.equal(r.status,503);
});

test('contract versions cannot be overwritten or removed after publication', () => {
  const db=database().raw;
  assert.throws(()=>db.prepare('UPDATE contract_documents SET text=? WHERE language=? AND version=?').run('changed','de','test-v1'),/immutable/);
  assert.throws(()=>db.prepare('DELETE FROM contract_documents WHERE language=? AND version=?').run('de','test-v1'),/immutable/);
  assert.equal(db.prepare('SELECT text FROM contract_documents WHERE language=? AND version=?').get('de','test-v1').text,'Testbedingungen');
});

test('replace and upsert cannot bypass document version immutability', () => {
  const db=database().raw;db.exec('PRAGMA recursive_triggers=OFF');
  assert.throws(()=>db.prepare('INSERT OR REPLACE INTO contract_documents VALUES(?,?,?)').run('de','test-v1','substituted'),/immutable/);
  assert.throws(()=>db.prepare('INSERT INTO contract_documents VALUES(?,?,?) ON CONFLICT(language,version) DO UPDATE SET text=excluded.text').run('de','test-v1','substituted'),/immutable/);
  assert.equal(db.prepare('SELECT text FROM contract_documents WHERE language=? AND version=?').get('de','test-v1').text,'Testbedingungen');
});

test('stored acceptance cannot be rewritten, deleted or replaced by ordinary operations', () => {
  const db=database().raw;db.exec('PRAGMA recursive_triggers=OFF');
  db.prepare('INSERT INTO orders VALUES(?,?,?,?)').run('o','h',1,null);
  db.prepare('INSERT INTO contract_acceptances VALUES(?,?,?,?,?,?)').run('o','de','v1','Original','digest',1);
  assert.throws(()=>db.prepare('UPDATE contract_acceptances SET text=? WHERE order_id=?').run('Modified','o'),/immutable/);
  assert.throws(()=>db.prepare('DELETE FROM contract_acceptances WHERE order_id=?').run('o'),/immutable/);
  assert.throws(()=>db.prepare('INSERT OR REPLACE INTO contract_acceptances VALUES(?,?,?,?,?,?)').run('o','de','v2','Modified','new',2),/immutable/);
});

test('contract documents containing carriage returns cannot become active', async () => {
  const db=database(), e=configured(db);db.raw.prepare('INSERT INTO contract_documents VALUES(?,?,?)').run('de','crlf','First\r\nSecond');e.CONTRACT_VERSION_DE='crlf';
  const res=await worker.fetch(new Request(e.PUBLIC_ORIGIN+'/api/checkout/prepare',{method:'POST',headers:{Origin:e.PUBLIC_ORIGIN},body:JSON.stringify({acceptTerms:true,lang:'de'})}),e);
  assert.equal(res.status,503);
});
