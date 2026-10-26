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
  activationSql,
} from "../worker.mjs";
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
function database() {
  const d = new DatabaseSync(":memory:");
  d.exec(readFileSync(new URL("../schema.sql", import.meta.url), "utf8"));
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
test("checkout refuses live price in test mode before creating a session", async () => {
  const original = globalThis.fetch;
  try {
    let creates = 0;
    globalThis.fetch = async (url, options) => {
      if (options.method === "POST") creates++;
      return Response.json({
        active: true,
        type: "one_time",
        currency: "eur",
        unit_amount: 2900,
        tax_behavior: "inclusive",
        product: "prod_a",
        livemode: true,
      });
    };
    const r = await worker.fetch(
      new Request("https://buyer.test/api/checkout", {
        method: "POST",
        headers: { Origin: "https://buyer.test" },
        body: '{"acceptTerms":true}',
      }),
      configured(database()),
    );
    assert.equal(r.status, 503);
    assert.equal(creates, 0);
  } finally {
    globalThis.fetch = original;
  }
});
test("checkout recovery code is hash-only and restores cookie without public session proof", async () => {
  const original = globalThis.fetch;
  try {
    const db = database(),
      e = configured(db);
    globalThis.fetch = async (url, options) =>
      Response.json(
        options.method === "POST"
          ? {
              id: "cs_test",
              mode: "payment",
              livemode: false,
              url: "https://checkout.stripe.com/c/pay/test",
            }
          : {
              active: true,
              type: "one_time",
              currency: "eur",
              unit_amount: 2900,
              tax_behavior: "inclusive",
              product: "prod_a",
              livemode: false,
            },
      );
    const r = await worker.fetch(
      new Request("https://buyer.test/api/checkout", {
        method: "POST",
        headers: { Origin: e.PUBLIC_ORIGIN },
        body: '{"acceptTerms":true}',
      }),
      e,
    );
    assert.equal(r.status, 200);
    const { recoveryCode } = await r.json();
    assert.match(recoveryCode, /^[A-Za-z0-9_-]{43}$/);
    assert.notEqual(
      db.raw.prepare("SELECT claim_hash FROM orders").get().claim_hash,
      recoveryCode,
    );
    const restore = await worker.fetch(
      new Request("https://buyer.test/api/claim/restore", {
        method: "POST",
        headers: { Origin: e.PUBLIC_ORIGIN },
        body: JSON.stringify({ recoveryCode }),
      }),
      e,
    );
    assert.equal(restore.status, 200);
    assert.match(restore.headers.get("Set-Cookie"), /HttpOnly/);
    const a = await worker.fetch(
      new Request("https://buyer.test/api/order", {
        headers: { Authorization: `Bearer ${recoveryCode}` },
      }),
      e,
    );
    assert.equal(a.status, 200);
  } finally {
    globalThis.fetch = original;
  }
});
test("checkout response requires Stripe host, HTTPS, payment and matching mode", async () => {
  const original = globalThis.fetch;
  try {
    for (const patch of [
      { url: "https://evil.test/pay" },
      { url: "http://checkout.stripe.com/pay" },
      { mode: "subscription" },
      { livemode: true },
    ]) {
      globalThis.fetch = async (url, options) =>
        Response.json(
          options.method === "POST"
            ? {
                id: "cs_test",
                mode: "payment",
                livemode: false,
                url: "https://checkout.stripe.com/pay",
                ...patch,
              }
            : {
                active: true,
                type: "one_time",
                currency: "eur",
                unit_amount: 2900,
                tax_behavior: "inclusive",
                product: "prod_a",
                livemode: false,
              },
        );
      const r = await worker.fetch(
        new Request("https://buyer.test/api/checkout", {
          method: "POST",
          headers: { Origin: "https://buyer.test" },
          body: '{"acceptTerms":true}',
        }),
        configured(database()),
      );
      assert.equal(r.status, 502);
      assert.equal((await r.json()).url, undefined);
    }
  } finally {
    globalThis.fetch = original;
  }
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
