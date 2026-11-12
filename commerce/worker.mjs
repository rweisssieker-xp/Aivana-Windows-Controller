import { activationSql } from './activation-sql.mjs';

const enc = new TextEncoder();
const b64 = (b) =>
  btoa(String.fromCharCode(...new Uint8Array(b)))
    .replaceAll("+", "-")
    .replaceAll("/", "_")
    .replaceAll("=", "");
const json = (v, status = 200, headers = {}) =>
  new Response(JSON.stringify(v), {
    status,
    headers: {
      "Content-Type": "application/json",
      "Cache-Control": "no-store",
      ...headers,
    },
  });
const hash = async (s) =>
  b64(await crypto.subtle.digest("SHA-256", enc.encode(s)));
const validDocumentText = text => typeof text === 'string' && Boolean(text.trim()) && text.length<=20000 && !/[\u0000-\u0009\u000B-\u001F\u007F]/.test(text);
async function versionedDocument(e, language, version) {
  try {
    if (!['de','en'].includes(language) || !e.DB) return null;
    if (typeof version !== 'string' || !/^[A-Za-z0-9._-]{1,64}$/.test(version)) return null;
    const d=await e.DB.prepare('SELECT text FROM contract_documents WHERE language=? AND version=?').bind(language,version).first();
    if (!d || !validDocumentText(d.text)) return null;
    return { language, version, text:d.text };
  } catch { return null; }
}
async function contractDocument(e, language) {
  return versionedDocument(e, language, e[language === 'de' ? 'CONTRACT_VERSION_DE' : 'CONTRACT_VERSION_EN']);
}
async function consentDocuments(e, language) {
  if (!['de','en'].includes(language)) return null;
  const suffix=language.toUpperCase();
  const early=await versionedDocument(e,language,e['CONSENT_EARLY_VERSION_'+suffix]);
  const ack=await versionedDocument(e,language,e['CONSENT_ACK_VERSION_'+suffix]);
  if (!early || !ack || early.version===ack.version || early.text===ack.text) return null;
  return [{kind:'early_start',...early},{kind:'withdrawal_ack',...ack}];
}
async function contractDigest(document) { return hash(JSON.stringify(document)); }
async function snapshotDigest(language,termsDigest,earlyDigest,ackDigest) { return hash(JSON.stringify({language,termsDigest,earlyDigest,ackDigest})); }
async function orderSnapshot(e,order) {
  if (!['de','en'].includes(order.return_lang)) return null;
  const term=await e.DB.prepare('SELECT language,version,text,digest FROM contract_acceptances WHERE order_id=?').bind(order.id).first();
  const rows=(await e.DB.prepare('SELECT kind,language,version,text,digest FROM contract_declarations WHERE order_id=? ORDER BY kind').bind(order.id).all()).results;
  if (!term || rows.length!==2 || term.language!==order.return_lang || !validDocumentText(term.text) || !/^[A-Za-z0-9._-]{1,64}$/.test(term.version)) return null;
  if (await contractDigest({language:term.language,version:term.version,text:term.text})!==term.digest) return null;
  for(const [i,kind] of ['early_start','withdrawal_ack'].entries()) {
    const d=rows[i];
    if(d.kind!==kind || d.language!==order.return_lang || !validDocumentText(d.text) || !/^[A-Za-z0-9._-]{1,64}$/.test(d.version) || await contractDigest({kind:d.kind,language:d.language,version:d.version,text:d.text})!==d.digest) return null;
  }
  return {language:term.language,terms:{version:term.version,text:term.text},early:{version:rows[0].version,text:rows[0].text},ack:{version:rows[1].version,text:rows[1].text},digest:await snapshotDigest(term.language,term.digest,rows[0].digest,rows[1].digest)};
}
export function validateSession(s, e) {
  const a = s.line_items?.data;
  return (
    typeof s.payment_intent === "string" &&
    s.payment_intent.startsWith("pi_") &&
    s.payment_status === "paid" &&
    s.mode === "payment" &&
    s.currency === "eur" &&
    s.amount_total === 2900 &&
    s.livemode === (e.STRIPE_LIVE === "true") &&
    a?.length === 1 &&
    a[0].quantity === 1 &&
    a[0].price?.id === e.STRIPE_PRICE_ID &&
    (typeof a[0].price.product === "string"
      ? a[0].price.product
      : a[0].price.product?.id) === e.STRIPE_PRODUCT_ID &&
    !s.line_items.has_more
  );
}
export async function verifyWebhook(
  raw,
  header,
  secret,
  now = Math.floor(Date.now() / 1000),
) {
  if (!secret || raw.length > 262144) return false;
  const parts = (header || "").split(",");
  const ts = parts.filter((p) => p.startsWith("t="));
  if (ts.length !== 1) return false;
  const t = Number(ts[0].slice(2));
  if (!Number.isInteger(t) || Math.abs(now - t) > 300) return false;
  const key = await crypto.subtle.importKey(
    "raw",
    enc.encode(secret),
    { name: "HMAC", hash: "SHA-256" },
    false,
    ["sign"],
  );
  const bytes = new Uint8Array(
    await crypto.subtle.sign("HMAC", key, enc.encode(`${t}.${raw}`)),
  );
  return parts
    .filter((p) => /^v1=[a-f0-9]{64}$/.test(p))
    .some((p) => {
      const s = p.slice(3);
      let diff = 0;
      for (let i = 0; i < 32; i++)
        diff |= bytes[i] ^ parseInt(s.slice(i * 2, i * 2 + 2), 16);
      return diff === 0;
    });
}
export async function signLicense(id, deviceId, e) {
  const payload = b64(
    enc.encode(
      JSON.stringify({
        schemaVersion: 1,
        product: "MAKRIVO",
        licenseId: id,
        majorVersion: 1,
        maxDevices: 4,
        deviceId,
        issuedAt: new Date().toISOString(),
      }),
    ),
  );
  const key = await signingKey(e);
  return {
    payload,
    signature: b64(
      await crypto.subtle.sign(
        "RSASSA-PKCS1-v1_5",
        key,
        Uint8Array.from(
          atob(payload.replaceAll("-", "+").replaceAll("_", "/")),
          (c) => c.charCodeAt(0),
        ),
      ),
    ),
    keyId: e.LICENSE_KEY_ID,
  };
}
async function signingKey(e) {
  if (
    typeof e.LICENSE_KEY_ID !== "string" ||
    !/^[A-Za-z0-9._-]{1,64}$/.test(e.LICENSE_KEY_ID)
  )
    throw Error("Invalid signing key ID");
  if (
    typeof e.LICENSE_PRIVATE_KEY_PEM !== "string" ||
    e.LICENSE_PRIVATE_KEY_PEM.length > 16384 ||
    !/^-----BEGIN PRIVATE KEY-----[\sA-Za-z0-9+/=]+-----END PRIVATE KEY-----\s*$/.test(
      e.LICENSE_PRIVATE_KEY_PEM,
    )
  )
    throw Error("PKCS8 private key required");
  const der = Uint8Array.from(
    atob(e.LICENSE_PRIVATE_KEY_PEM.replace(/-----[^-]+-----|\s/g, "")),
    (c) => c.charCodeAt(0),
  );
  const key = await crypto.subtle.importKey(
    "pkcs8",
    der,
    { name: "RSASSA-PKCS1-v1_5", hash: "SHA-256" },
    false,
    ["sign"],
  );

  if (key.type !== "private" || key.algorithm.modulusLength < 3072)
    throw Error("RSA3072 or stronger required");
  return key;
}
async function ready(e) {
  // Live checkout must remain disabled until durable contract confirmation
  // delivery and its failure handling are implemented and independently tested.
  if (e.STRIPE_LIVE === 'true') return false;
  try {
    for (const k of [
      "PUBLIC_ORIGIN",
      "LEGAL_TERMS_URL",
      "PRIVACY_URL",
      "SELLER_IMPRINT_URL",
    ]) {
      if (new URL(e[k]).protocol !== "https:") return false;
    }
    if (new URL(e.PUBLIC_ORIGIN).origin !== e.PUBLIC_ORIGIN) return false;
    await signingKey(e);
    if (!(await contractDocument(e, 'de')) || !(await contractDocument(e, 'en')) || !(await consentDocuments(e,'de')) || !(await consentDocuments(e,'en'))) return false;
  } catch {
    return false;
  }
  return (
    providerReady(e) &&
    e.SALES_ENABLED === "true" &&
    [
      "PUBLIC_ORIGIN",
      "STRIPE_SECRET_KEY",
      "STRIPE_WEBHOOK_SECRET",
      "STRIPE_PRICE_ID",
      "STRIPE_PRODUCT_ID",
      "LICENSE_PRIVATE_KEY_PEM",
      "LICENSE_KEY_ID",
      "LEGAL_TERMS_URL",
      "PRIVACY_URL",
      "SELLER_IMPRINT_URL",
    ].every((k) => e[k]) &&
    e.LEGAL_APPROVED === "true" &&
    e.DB
  );
}
async function stripe(e, path, body, idempotencyKey) {
  const r = await fetch(`https://api.stripe.com/v1/${path}`, {
    method: body ? "POST" : "GET",
    headers: {
      Authorization: `Bearer ${e.STRIPE_SECRET_KEY}`,
      ...(body ? { "Content-Type": "application/x-www-form-urlencoded" } : {}),
      ...(idempotencyKey ? { "Idempotency-Key": idempotencyKey } : {}),
    },
    body: body ? new URLSearchParams(body) : undefined,
  });
  if (!r.ok) throw Error("Payment provider unavailable");
  return r.json();
}
async function bounded(req, max = 8192) {
  const reader = req.body?.getReader();
  if (!reader) return "";
  let bytes = 0,
    out = "";
  const decoder = new TextDecoder("utf-8", { fatal: true });
  while (true) {
    const { value, done } = await reader.read();
    if (done) break;
    bytes += value.length;
    if (bytes > max) {
      await reader.cancel();
      throw Error("Request too large");
    }
    out += decoder.decode(value, { stream: true });
  }
  return out + decoder.decode();
}
async function auth(req, e) {
  const bearer = req.headers.get("Authorization");
  const cookie = req.headers
    .get("Cookie")
    ?.match(/(?:^|; )makrivo_claim=([A-Za-z0-9_-]{43})(?:;|$)/)?.[1];
  const secret = bearer?.match(/^Bearer ([A-Za-z0-9_-]{43})$/)?.[1] || cookie;
  if (!secret) return null;
  return e.DB.prepare(
    "SELECT o.id,e.id AS license_id,e.revoked FROM orders o LEFT JOIN entitlements e ON e.order_id=o.id WHERE claim_hash=?",
  )
    .bind(await hash(secret))
    .first();
}
async function webhook(req, e) {
  const raw = await bounded(req, 262144);
  if (
    !(await verifyWebhook(
      raw,
      req.headers.get("Stripe-Signature"),
      e.STRIPE_WEBHOOK_SECRET,
    ))
  )
    return json({ error: "Invalid signature" }, 400);
  const event = JSON.parse(raw);
  if (!event.id || typeof event.type !== "string")
    return json({ error: "Invalid event" }, 400);
  if (event.livemode !== (e.STRIPE_LIVE === "true"))
    return json({ error: "Wrong environment" }, 400);
  if (["charge.refunded", "charge.dispute.created"].includes(event.type)) {
    const pi = event.data?.object?.payment_intent;
    if (typeof pi === "string")
      await e.DB.batch([
        e.DB.prepare(
          "INSERT OR IGNORE INTO revoked_payments(payment_intent) VALUES(?)",
        ).bind(pi),
        e.DB.prepare(
          "UPDATE entitlements SET revoked=1 WHERE payment_intent=?",
        ).bind(pi),
      ]);
    return json({ received: true });
  }
  if (
    ![
      "checkout.session.completed",
      "checkout.session.async_payment_succeeded",
    ].includes(event.type)
  )
    return json({ received: true });
  const sid = event.data?.object?.id;
  if (!/^cs_[A-Za-z0-9_]+$/.test(sid || ""))
    return json({ error: "Invalid session" }, 400);
  const s = await stripe(
    e,
    `checkout/sessions/${encodeURIComponent(sid)}?expand[]=line_items.data.price.product`,
  );
  if (!validateSession(s, e))
    return json({ error: "Payment not eligible" }, 400);
  const order = await e.DB.prepare("SELECT id FROM orders WHERE session_id=?")
    .bind(s.id)
    .first();
  if (!order || s.metadata?.orderId !== order.id)
    return json({ error: "Unknown order" }, 400);
  await e.DB.batch([
    e.DB.prepare(
      `INSERT OR IGNORE INTO entitlements(id,order_id,session_id,payment_intent,revoked) VALUES(?,?,?,?,EXISTS(SELECT 1 FROM revoked_payments WHERE payment_intent=?))`,
    ).bind(
      crypto.randomUUID(),
      order.id,
      s.id,
      s.payment_intent,
      s.payment_intent,
    ),
    e.DB.prepare(
      `INSERT OR IGNORE INTO confirmation_jobs(order_id,created_at) SELECT o.order_id,? FROM confirmation_obligations o JOIN entitlements e ON e.order_id=o.order_id WHERE o.order_id=? AND e.session_id=? AND e.payment_intent=?`,
    ).bind(Date.now(),order.id,s.id,s.payment_intent),
    e.DB.prepare("INSERT OR IGNORE INTO events(id,kind) SELECT ?,? WHERE EXISTS(SELECT 1 FROM entitlements WHERE order_id=? AND session_id=? AND payment_intent=?)").bind(
      event.id,event.type,order.id,s.id,s.payment_intent,
    ),
  ]);
  const mapping=await e.DB.prepare("SELECT 1 AS ok FROM entitlements WHERE order_id=? AND session_id=? AND payment_intent=?").bind(order.id,s.id,s.payment_intent).first();
  if (!mapping) return json({ error: "Payment mapping conflict" },409);
  return json({ received: true });
}
function accountReady(e) {
  try {
    return (
      Boolean(e.DB) &&
      new URL(e.PUBLIC_ORIGIN).protocol === "https:" &&
      new URL(e.PUBLIC_ORIGIN).origin === e.PUBLIC_ORIGIN
    );
  } catch {
    return false;
  }
}
function providerReady(e) {
  return Boolean(
    e.DB &&
    e.STRIPE_SECRET_KEY &&
    e.STRIPE_WEBHOOK_SECRET &&
    e.STRIPE_PRICE_ID &&
    e.STRIPE_PRODUCT_ID &&
    ["true", "false"].includes(e.STRIPE_LIVE),
  );
}
// Milliseconds since epoch. Only short-lived abuse counters are pruned here.
async function pruneRateLimits(env, now) {
  await env.DB.prepare(
    "DELETE FROM rate_limits WHERE expires_at IS NULL OR expires_at <= ?",
  )
    .bind(now)
    .run();
}
export default {
  async scheduled(_controller, env, _ctx) {
    // Use execution time, never an event-supplied future timestamp that could
    // delete current request buckets. Errors propagate for operator visibility.
    await pruneRateLimits(env, Date.now());
  },
  async fetch(req, e) {
    try {
      const u = new URL(req.url);
      if (u.pathname === "/" && req.method === "GET") return page(e, req);
      // Existing payment processing is independent of new-sale and signer switches.
      if (u.pathname === "/api/stripe/webhook" && req.method === "POST") {
        if (!providerReady(e))
          return json({ error: "Payment processing is not configured" }, 503);
        return await webhook(req, e);
      }
      if (["/api/checkout", "/api/checkout/prepare", "/api/checkout/preview"].includes(u.pathname) && !(await ready(e)))
        return json({ error: "New sales are not configured" }, 503);
      if (!accountReady(e))
        return json({ error: "Order access is not configured" }, 503);
      if (!["GET", "POST"].includes(req.method))
        return json({ error: "Method not allowed" }, 405);
      if (
        req.method === "POST" &&
        req.headers.get("Origin") !== e.PUBLIC_ORIGIN
      )
        return json({ error: "Invalid origin" }, 403);
      const now = Date.now();
      const minute = Math.floor(now / 60000);
      await pruneRateLimits(e, now);
      // Pseudonymous, not anonymous: input IP exists in request memory only.
      const bucket = await hash(
        "MAKRIVO|rate-limit|" +
          minute +
          "|" +
          (req.headers.get("CF-Connecting-IP") || "unknown"),
      );
      const expiresAt = (minute + 2) * 60000;
      const limit = await e.DB.prepare(
        "INSERT INTO rate_limits(bucket,hits,expires_at) VALUES(?,1,?) ON CONFLICT(bucket) DO UPDATE SET hits=hits+1 RETURNING hits",
      )
        .bind(bucket, expiresAt)
        .first();
      if (limit.hits > 30) return json({ error: "Try later" }, 429);
      if (u.pathname === "/api/checkout/prepare" && req.method === "POST") {
        const input = JSON.parse(await bounded(req));
        if (input.acceptTerms !== true) return json({ error: "Accept purchase terms" }, 400);
        if (input.acceptEarlyStart !== true || input.acknowledgeWithdrawal !== true) return json({ error: "Accept both declarations" }, 400);
        const document = await contractDocument(e, input.lang);
        if (!document || input.contractDigest !== await contractDigest(document)) return json({ error: "Contract changed" }, 409);
        const declarations=await consentDocuments(e,input.lang);
        if (!declarations || input.earlyStartDigest !== await contractDigest(declarations[0]) || input.withdrawalAckDigest !== await contractDigest(declarations[1])) return json({ error: "Declarations changed" },409);
        const secret = b64(crypto.getRandomValues(new Uint8Array(32))), checkoutToken=b64(crypto.getRandomValues(new Uint8Array(32))), id = crypto.randomUUID(), acceptedAt = Date.now();
        const acceptedDigest=await snapshotDigest(document.language,input.contractDigest,input.earlyStartDigest,input.withdrawalAckDigest);
        const lang = input.lang === "de" || input.lang === "en" ? input.lang : "";
        await e.DB.batch([
          e.DB.prepare("INSERT INTO orders(id,claim_hash,created_at) VALUES(?,?,?)").bind(id, await hash(secret), acceptedAt),
          e.DB.prepare("INSERT INTO checkout_attempts(order_id,return_lang) VALUES(?,?)").bind(id, lang),
          e.DB.prepare("INSERT INTO contract_acceptances(order_id,language,version,text,digest,accepted_at) VALUES(?,?,?,?,?,?)").bind(id, document.language, document.version, document.text, input.contractDigest, acceptedAt),
          ...declarations.map((d,i)=>e.DB.prepare("INSERT INTO contract_declarations(order_id,kind,language,version,text,digest,accepted_at) VALUES(?,?,?,?,?,?,?)").bind(id,d.kind,d.language,d.version,d.text,i===0?input.earlyStartDigest:input.withdrawalAckDigest,acceptedAt)),
          e.DB.prepare("INSERT INTO confirmation_obligations(order_id,created_at) VALUES(?,?)").bind(id, acceptedAt),
          e.DB.prepare("INSERT INTO checkout_authorizations(order_id,token_hash,snapshot_digest,expires_at) VALUES(?,?,?,?)").bind(id,await hash(checkoutToken),acceptedDigest,acceptedAt+20*60*1000),
        ]);
        return json({ recoveryCode: secret, checkoutToken });
      }
      if (u.pathname === "/api/checkout/preview" && req.method === "POST") {
        const input=JSON.parse(await bounded(req));
        if (!/^[A-Za-z0-9_-]{43}$/.test(input.recoveryCode || "")) return json({error:"Invalid recovery code"},401);
        const order=await e.DB.prepare("SELECT o.id,a.return_lang FROM orders o LEFT JOIN checkout_attempts a ON a.order_id=o.id WHERE o.claim_hash=?").bind(await hash(input.recoveryCode)).first();
        if (!order) return json({error:"Invalid recovery code"},401);
        const snapshot=await orderSnapshot(e,order);
        if (!snapshot) return json({error:"Declarations unavailable"},409);
        const checkoutToken=b64(crypto.getRandomValues(new Uint8Array(32)));
        await e.DB.prepare("INSERT INTO checkout_authorizations(order_id,token_hash,snapshot_digest,expires_at,used_at) VALUES(?,?,?,?,NULL) ON CONFLICT(order_id) DO UPDATE SET token_hash=excluded.token_hash,snapshot_digest=excluded.snapshot_digest,expires_at=excluded.expires_at,used_at=NULL").bind(order.id,await hash(checkoutToken),snapshot.digest,Date.now()+20*60*1000).run();
        return json({language:snapshot.language,terms:snapshot.terms,early:snapshot.early,ack:snapshot.ack,checkoutToken});
      }
      if (u.pathname === "/api/checkout" && req.method === "POST") {
        const input = JSON.parse(await bounded(req));
        if (input.acceptTerms !== true)
          return json({ error: "Accept purchase terms" }, 400);
        if (!/^[A-Za-z0-9_-]{43}$/.test(input.recoveryCode || ""))
          return json({ error: "Invalid recovery code" }, 401);
        const order = await e.DB.prepare("SELECT o.id,o.created_at,o.session_id,a.return_lang FROM orders o LEFT JOIN checkout_attempts a ON a.order_id=o.id WHERE o.claim_hash=?")
          .bind(await hash(input.recoveryCode)).first();
        if (!order) return json({ error: "Invalid recovery code" }, 401);
        const snapshot=await orderSnapshot(e,order);
        if (!snapshot) return json({ error: "Declarations unavailable" },409);
        if (!/^[A-Za-z0-9_-]{43}$/.test(input.checkoutToken || "")) return json({ error: "Preview required" },409);
        const tokenHash=await hash(input.checkoutToken);
        let authorization=await e.DB.prepare("SELECT token_hash,snapshot_digest,expires_at,used_at FROM checkout_authorizations WHERE order_id=?").bind(order.id).first();
        if (!authorization || authorization.token_hash!==tokenHash || authorization.snapshot_digest!==snapshot.digest || authorization.expires_at<=Date.now()) return json({ error: "Preview required" },409);
        let sessionId = order.session_id;
        if (!sessionId) {
        if (authorization.used_at !== null) return json({ error: "Checkout needs support" },409);
        // Legacy orders lack the fixed creation parameters required for a safe retry.
        if (order.return_lang === null) return json({ error: "Checkout needs support" }, 409);
        // Stripe can prune idempotency keys after 24 hours. Never retry creation
        // after the conservative cutoff; uncertain attempts need reconciliation.
        if (Date.now() - order.created_at >= 20 * 60 * 60 * 1000)
          return json({ error: "Checkout needs support" }, 409);
        const price = await stripe(
          e,
          "prices/" + encodeURIComponent(e.STRIPE_PRICE_ID),
        );
        if (
          price.livemode !== (e.STRIPE_LIVE === "true") ||
          !price.active ||
          price.type !== "one_time" ||
          price.currency !== "eur" ||
          price.unit_amount !== 2900 ||
          price.tax_behavior !== "inclusive" ||
          price.product !== e.STRIPE_PRODUCT_ID
        )
          return json({ error: "Price is not configured" }, 503);
        // Only these fixed locale values may enter Stripe return URLs.
        const returnPath = order.return_lang ? `/?lang=${order.return_lang}` : "/";
        const s = await stripe(e, "checkout/sessions", {
          mode: "payment",
          "line_items[0][price]": e.STRIPE_PRICE_ID,
          "line_items[0][quantity]": "1",
          success_url: `${e.PUBLIC_ORIGIN}${returnPath}`,
          cancel_url: `${e.PUBLIC_ORIGIN}${returnPath}`,
          "metadata[orderId]": order.id,
          "payment_intent_data[metadata][orderId]": order.id,
          "consent_collection[terms_of_service]": "required",
          "adaptive_pricing[enabled]": "false",
          "automatic_tax[enabled]": "true",
        }, `makrivo-checkout-${order.id}`);
        if (!/^cs_[A-Za-z0-9_]+$/.test(s.id || "") || s.mode !== "payment" || s.livemode !== (e.STRIPE_LIVE === "true"))
          return json({ error: "Invalid checkout response" }, 502);
        const commitAt=Date.now();
        await e.DB.batch([
          e.DB.prepare("UPDATE orders SET session_id=? WHERE id=? AND (session_id IS NULL OR session_id=?) AND EXISTS(SELECT 1 FROM checkout_authorizations WHERE order_id=? AND token_hash=? AND snapshot_digest=? AND expires_at>?)")
            .bind(s.id,order.id,s.id,order.id,tokenHash,snapshot.digest,commitAt),
          e.DB.prepare("UPDATE checkout_authorizations SET used_at=COALESCE(used_at,?) WHERE order_id=? AND token_hash=? AND snapshot_digest=? AND expires_at>? AND EXISTS(SELECT 1 FROM orders WHERE id=? AND session_id=?)")
            .bind(commitAt,order.id,tokenHash,snapshot.digest,commitAt,order.id,s.id),
        ]);
        const stored = await e.DB.prepare("SELECT session_id FROM orders WHERE id=?").bind(order.id).first();
        authorization=await e.DB.prepare("SELECT token_hash,snapshot_digest,expires_at,used_at FROM checkout_authorizations WHERE order_id=?").bind(order.id).first();
        if (stored?.session_id !== s.id || !authorization || authorization.token_hash!==tokenHash || authorization.snapshot_digest!==snapshot.digest || authorization.used_at===null || authorization.expires_at<=Date.now()) return json({ error: "Preview required" },409);
        sessionId = s.id;
        }
        // An idempotent POST may return a cached open response after payment.
        const s = await stripe(e, `checkout/sessions/${encodeURIComponent(sessionId)}`);
        if (s.status !== "open" || s.payment_status !== "unpaid")
          return json({ error: "Checkout no longer open" }, 409);
        let checkoutUrl;
        try {
          checkoutUrl = new URL(s.url);
        } catch {
          return json({ error: "Invalid checkout response" }, 502);
        }
        if (
          s.id !== sessionId || s.metadata?.orderId !== order.id ||
          s.mode !== "payment" ||
          s.livemode !== (e.STRIPE_LIVE === "true") ||
          checkoutUrl.protocol !== "https:" ||
          checkoutUrl.hostname !== "checkout.stripe.com" ||
          checkoutUrl.username ||
          checkoutUrl.password ||
          checkoutUrl.port
        )
          return json({ error: "Invalid checkout response" }, 502);
        const finalized=await e.DB.prepare("UPDATE checkout_authorizations SET used_at=COALESCE(used_at,?) WHERE order_id=? AND token_hash=? AND snapshot_digest=? AND expires_at>? AND EXISTS(SELECT 1 FROM orders WHERE id=? AND session_id=?) RETURNING used_at")
          .bind(Date.now(),order.id,tokenHash,snapshot.digest,Date.now(),order.id,sessionId).first();
        if (!finalized) return json({ error: "Preview required" },409);
        return json({ url: s.url });
      }
      if (u.pathname === "/api/claim/restore" && req.method === "POST") {
        const input = JSON.parse(await bounded(req));
        const claim = input.recoveryCode;
        if (!/^[A-Za-z0-9_-]{43}$/.test(claim || ""))
          return json({ error: "Invalid recovery code" }, 401);
        const order = await e.DB.prepare(
          "SELECT id FROM orders WHERE claim_hash=?",
        )
          .bind(await hash(claim))
          .first();
        if (!order) return json({ error: "Invalid recovery code" }, 401);
        return json({ restored: true }, 200, {
          "Set-Cookie": `makrivo_claim=${claim}; Path=/; Secure; HttpOnly; SameSite=Lax; Max-Age=31536000`,
        });
      }
      const a = await auth(req, e);
      if (!a) return json({ error: "Order claim required" }, 401);
      if (u.pathname === "/api/order/export" && req.method === "POST") {
        // Scope the export to the authenticated claim; never return the claim,
        // its hash, Stripe references, provider data, or any other order.
        const order = await e.DB.prepare(`SELECT o.id,o.created_at,
          e.id AS license_id,e.revoked,
          c.language,c.version,c.text,c.accepted_at
          FROM orders o LEFT JOIN entitlements e ON e.order_id=o.id
          LEFT JOIN contract_acceptances c ON c.order_id=o.id WHERE o.id=?`)
          .bind(a.id).first();
        if (!order) return json({ error: "Not found" }, 404);
        const deviceRows = order.license_id
          ? (await e.DB.prepare("SELECT device_id,active FROM devices WHERE license_id=? ORDER BY device_id LIMIT 1001").bind(order.license_id).all()).results
          : [];
        const declarations = (await e.DB.prepare("SELECT kind,language,version,text,accepted_at FROM contract_declarations WHERE order_id=? ORDER BY kind").bind(a.id).all()).results;
        // Never silently present a truncated record as a complete export.
        if (deviceRows.length > 1000) return json({ error: "Export too large; contact support" }, 413);
        return new Response(JSON.stringify({
          format: "makrivo-purchase-data-v1",
          scope: "Selected data for this MAKRIVO purchase: order, accepted terms and declarations, and devices. Recovery-code hashes, payment identifiers, Stripe, support, infrastructure logs and backups are not included; contact Aivana for other data.",
          order: { reference: order.id, prepared_at_ms: order.created_at,
            status: order.revoked ? "revoked" : order.license_id ? "paid" : "pending",
            license_reference: order.license_id || null },
          acceptance: order.language ? { language: order.language, version: order.version,
            text: order.text, accepted_at_ms: order.accepted_at } : null,
          declarations: declarations.map(d=>({kind:d.kind,language:d.language,version:d.version,text:d.text,accepted_at_ms:d.accepted_at})),
          devices: deviceRows.map(d => ({ device_code: d.device_id, active: d.active === 1 })),
        }, null, 2), { headers: {
          "Content-Type": "application/json;charset=utf-8",
          "Content-Disposition": 'attachment; filename="MAKRIVO-purchase-data.json"',
          "Cache-Control": "no-store", "X-Content-Type-Options": "nosniff",
          "Referrer-Policy": "no-referrer",
        }});
      }
      if (u.pathname === "/api/order/terms" && req.method === "POST") {
        if (!a.license_id) return json({ error: "No paid order" }, 403);
        const accepted = await e.DB.prepare("SELECT language,version,text,accepted_at FROM contract_acceptances WHERE order_id=?").bind(a.id).first();
        if (!accepted) return json({ error: "Terms copy unavailable" }, 404);
        const acceptedTime = new Date(accepted.accepted_at).toISOString();
        return new Response(`MAKRIVO — ${accepted.language}\nVersion: ${accepted.version}\nAccepted: ${acceptedTime}\n\n${accepted.text}\n`, { headers: { "Content-Type":"text/plain;charset=utf-8", "Content-Disposition":"attachment; filename=MAKRIVO-terms.txt", "Cache-Control":"no-store", "X-Content-Type-Options":"nosniff" } });
      }
      if (u.pathname === "/api/order" && req.method === "GET") {
        const devices = a.license_id
          ? await e.DB.prepare(
              "SELECT device_id FROM devices WHERE license_id=? AND active=1",
            )
              .bind(a.license_id)
              .all()
          : { results: [] };
        return json({
          status: a.revoked ? "revoked" : a.license_id ? "paid" : "pending",
          devices: devices.results,
        });
      }
      if (
        ["/api/licenses/activate", "/api/licenses/deactivate"].includes(
          u.pathname,
        ) &&
        req.method === "POST"
      ) {
        if (!a.license_id || a.revoked)
          return json({ error: "No active entitlement" }, 403);
        const { deviceId } = JSON.parse(await bounded(req));
        if (!/^[a-f0-9]{64}$/.test(deviceId || ""))
          return json({ error: "Invalid device code" }, 400);
        if (u.pathname.endsWith("/deactivate")) {
          await e.DB.prepare(
            "UPDATE devices SET active=0 WHERE license_id=? AND device_id=?",
          )
            .bind(a.license_id, deviceId)
            .run();
          return json({ deactivated: true });
        }
        // There is no trusted delivery transport or delivery acknowledgement yet.
        // A recorded obligation therefore fails closed, even if a pending job is absent.
        const obligation = await e.DB.prepare("SELECT 1 AS required FROM confirmation_obligations WHERE order_id=?").bind(a.id).first();
        if (obligation) return json({ error: "Contract confirmation delivery unavailable" }, 503);
        try {
          await signingKey(e);
        } catch {
          return json({ error: "License signing is not configured" }, 503);
        }
        await e.DB.prepare(activationSql).bind(a.license_id, deviceId).run();
        const seated = await e.DB.prepare(
          "SELECT 1 AS ok FROM devices d JOIN entitlements e ON e.id=d.license_id WHERE d.license_id=? AND d.device_id=? AND d.active=1 AND e.revoked=0",
        )
          .bind(a.license_id, deviceId)
          .first();
        if (!seated) return json({ error: "Four PC limit reached" }, 409);
        const signed = await signLicense(a.license_id, deviceId, e);
        // This conditional write is the issuance linearization point: a refund
        // or deactivation committed before it prevents the signed file leaving.
        const issued = await e.DB.prepare(
          `UPDATE devices SET active=1 WHERE license_id=? AND device_id=? AND active=1 AND EXISTS(SELECT 1 FROM entitlements e WHERE e.id=devices.license_id AND e.revoked=0 AND NOT EXISTS(SELECT 1 FROM confirmation_obligations o WHERE o.order_id=e.order_id)) RETURNING 1 AS ok`,
        ).bind(a.license_id,deviceId).first();
        if (!issued) return json({ error: "No active entitlement" },403);
        return json(signed);
      }
      return json({ error: "Not found" }, 404);
    } catch {
      return json({ error: "Request could not be completed" }, 400);
    }
  },
};
async function page(e, req) {
  const explicit = new URL(req.url).searchParams.get("lang");
  const preferences = (req.headers.get("Accept-Language") || "").split(",").map((entry, index) => {
    const [tag, ...params] = entry.trim().toLowerCase().split(";");
    const code = tag.split("-")[0];
    const quality = params.find(p => p.trim().startsWith("q="));
    const value = quality ? Number(quality.trim().slice(2)) : 1;
    return { code, index, value };
  }).filter(p => ["de", "en"].includes(p.code) && Number.isFinite(p.value) && p.value > 0 && p.value <= 1)
    .sort((a, b) => b.value - a.value || a.index - b.index);
  const language = explicit === "de" || explicit === "en" ? explicit : preferences[0]?.code || "en";
  const strings = {
    en: {
      title: "MAKRIVO purchase and activation",
      offer: "Aivana GmbH · €29.00 including tax · one-time purchase · one person · four own PCs. Commercial use permitted. No subscription. Perpetual license for major version 1.",
      enabled: "Sales configured.", disabled: "Checkout is currently unavailable.",
      terms: "Purchase terms", privacy: "Privacy", imprint: "Seller imprint", accept: "I accept the purchase terms.", buy: "Buy for €29.00",
      backupTitle: "Save your recovery code", backupInfo: "This secret restores your purchase in another browser. Save it privately before continuing. Never share it. The declarations selected for this purchase are saved and cannot be changed here; reload to start a new purchase.",
      resume: "Resume payment with saved code", resumeInfo: "If payment was interrupted, enter your saved code and choose Resume payment to review the terms and declarations originally saved for this purchase. After payment, enter your saved code and choose Restore purchase to access your license. Restoring replaces the purchase currently selected in this browser; keep the other purchase’s code to restore it later.", copy: "Copy recovery code", saved: "I saved this recovery code privately.", continue: "Continue to secure payment", restoreTitle: "Restore purchase", restoreLabel: "Saved recovery code", restoreButton: "Restore purchase", activation: "Activate your PC",
      documentCopy: "Download your accepted purchase terms", dataExport: "Download MAKRIVO purchase data", dataScope: "Contains data for this MAKRIVO purchase. Stripe and other providers may hold additional data; contact Aivana for other requests.",
      activationInfo: "After payment, enter your saved recovery code above and choose Restore purchase. Copy the device code shown in the Windows app. Download the license and import it in MAKRIVO on that PC.",
      refresh: "Refresh payment and PCs", active: "Your active PCs", refreshInfo: "Refresh to view your active PCs and available seats.",
      offline: "Deactivate a PC you no longer use to release an activation seat. Remove its local license file too; an already issued offline license cannot be remotely disabled.",
      code: "Device code (64 characters)", download: "Download license", copied: "Recovery code copied. Save it privately.", saveFirst: "Save the recovery code first.", restored: "Purchase restored.",
      order: "Order: ", activePCs: ". Active PCs: ", ofFour: " of 4. Available seats: ", states: {pending:"pending",paid:"paid",revoked:"revoked"},
      deactivate: "Deactivate this PC", aria: "Deactivate PC ", confirm: "Release this PC activation seat? Remove the license file on that PC. Existing offline files cannot be remotely disabled.", released: "PC deactivated. Activation seat released.",
      languageWarning: "Complete the purchase or wait for the request to finish before changing language.", errors: {"Export too large; contact support":"This purchase has too many device records to download. Please contact support.","No paid order":"Purchase terms can be downloaded after payment.","Terms copy unavailable":"A saved copy is unavailable for this purchase. Please contact support.","Contract changed":"The purchase terms changed. Reload this page and review them again.","Order claim required":"Please restore your purchase first.","No active entitlement":"This purchase has no active license.","Invalid device code":"The device code is invalid.","Four PC limit reached":"The limit of four PCs has been reached. Deactivate a PC to free a place.","License signing is not configured":"License creation is currently unavailable.","Order access is not configured":"Order access is currently unavailable.","Method not allowed":"This request is not allowed.","Invalid origin":"The request came from an invalid address.","Invalid checkout response":"The payment page could not be opened.","Not found":"The requested page was not found.","Request could not be completed":"The request could not be completed.","Accept purchase terms":"Please accept the purchase terms.","Invalid recovery code":"The recovery code is invalid.","Try later":"Too many requests. Please try again later.","Price is not configured":"The price is not configured yet.","New sales are not configured":"Purchases are not currently available.","Checkout no longer open":"This payment page can no longer be used. If you paid, restore your purchase. Otherwise contact support.","Checkout needs support":"Please contact support before retrying this purchase."}, genericError: "Request could not be completed."
    },
    de: {
      title: "MAKRIVO kaufen und aktivieren",
      offer: "Aivana GmbH · 29,00 € inklusive Steuern · Einmalkauf · eine Person · bis zu vier eigenen PCs. Gewerbliche Nutzung erlaubt. Kein Abo. Zeitlich unbefristete Lizenz für Hauptversion 1.",
      enabled: "Kauf ist verfügbar.", disabled: "Der Kauf ist derzeit nicht verfügbar.",
      terms: "Kaufbedingungen", privacy: "Datenschutz", imprint: "Impressum", accept: "Ich akzeptiere die Kaufbedingungen.", buy: "Für 29,00 € kaufen",
      backupTitle: "Wiederherstellungscode speichern", backupInfo: "Mit diesem geheimen Code stellst du deinen Kauf in einem anderen Browser wieder her. Speichere ihn vor dem Fortfahren sicher und gib ihn niemals weiter. Die für diesen Kauf gewählten Erklärungen sind gespeichert und hier gesperrt; lade die Seite für einen neuen Kauf neu.",
      resume: "Zahlung mit gespeichertem Code fortsetzen", resumeInfo: "Falls die Zahlung unterbrochen wurde, gib deinen gespeicherten Code ein und wähle Zahlung fortsetzen. Prüfe anschließend die ursprünglich für diesen Kauf gespeicherten Bedingungen und Erklärungen. Gib nach der Zahlung deinen Code ein und wähle Kauf wiederherstellen. Das Wiederherstellen ersetzt den in diesem Browser ausgewählten Kauf; bewahre den Code des anderen Kaufs auf, um ihn später wiederherzustellen.", copy: "Wiederherstellungscode kopieren", saved: "Ich habe den Wiederherstellungscode sicher gespeichert.", continue: "Weiter zur sicheren Zahlung", restoreTitle: "Kauf wiederherstellen", restoreLabel: "Gespeicherter Wiederherstellungscode", restoreButton: "Kauf wiederherstellen", activation: "PC aktivieren",
      documentCopy: "Akzeptierte Kaufbedingungen herunterladen", dataExport: "Kaufdaten aus MAKRIVO herunterladen", dataScope: "Enthält Daten zu diesem MAKRIVO-Kauf. Stripe und andere Dienstleister können weitere Daten verarbeiten; für weitere Anfragen wenden Sie sich an Aivana.",
      activationInfo: "Gib nach der Zahlung oben deinen gespeicherten Wiederherstellungscode ein und wähle Kauf wiederherstellen. Kopiere den Gerätecode aus der Windows-App. Lade die Lizenz herunter und importiere sie auf diesem PC in MAKRIVO.",
      refresh: "Zahlungsstatus und PCs aktualisieren", active: "Deine aktiven PCs", refreshInfo: "Aktualisiere die Ansicht, um aktive PCs und freie Plätze zu sehen.",
      offline: "Deaktiviere einen nicht mehr genutzten PC, um einen Aktivierungsplatz freizugeben. Entferne auch dessen lokale Lizenzdatei; bereits ausgestellte Offline-Lizenzen können nicht aus der Ferne deaktiviert werden.",
      code: "Gerätecode (64 Zeichen)", download: "Lizenz herunterladen", copied: "Wiederherstellungscode kopiert. Bewahre ihn sicher auf.", saveFirst: "Speichere zuerst den Wiederherstellungscode.", restored: "Kauf wiederhergestellt.",
      order: "Bestellung: ", activePCs: ". Aktive PCs: ", ofFour: " von 4. Freie Plätze: ", states: {pending:"ausstehend",paid:"bezahlt",revoked:"widerrufen"},
      deactivate: "Diesen PC deaktivieren", aria: "PC deaktivieren: ", confirm: "Aktivierungsplatz dieses PCs freigeben? Entferne die Lizenzdatei auf diesem PC. Bestehende Offline-Lizenzdateien können nicht aus der Ferne deaktiviert werden.", released: "PC deaktiviert. Der Aktivierungsplatz wurde freigegeben.",
      languageWarning: "Bitte zuerst den Kauf abschließen oder warten, bis die Anfrage abgeschlossen ist, bevor du die Sprache wechselst.",
      errors: {"Export too large; contact support":"Dieser Kauf enthält zu viele Gerätedaten für den Download. Bitte kontaktiere den Support.","No paid order":"Die Kaufbedingungen können nach der Zahlung heruntergeladen werden.","Terms copy unavailable":"Für diesen Kauf liegt keine gespeicherte Fassung vor. Bitte kontaktiere den Support.","Contract changed":"Die Kaufbedingungen haben sich geändert. Lade die Seite neu und prüfe sie erneut.","Checkout no longer open":"Diese Zahlungsseite kann nicht mehr verwendet werden. Falls du bezahlt hast, stelle deinen Kauf wieder her. Andernfalls kontaktiere den Support.","Checkout needs support":"Bitte kontaktiere den Support, bevor du diesen Kauf erneut versuchst.","New sales are not configured":"Der Kauf ist noch nicht verfügbar.","Order access is not configured":"Der Zugriff auf Bestellungen ist derzeit nicht verfügbar.","Method not allowed":"Diese Anfrage ist nicht zulässig.","Invalid origin":"Die Anfrage stammt von einer ungültigen Adresse.","Try later":"Zu viele Anfragen. Bitte versuche es später erneut.","Accept purchase terms":"Bitte akzeptiere die Kaufbedingungen.","Price is not configured":"Der Preis ist noch nicht eingerichtet.","Invalid checkout response":"Die Zahlungsseite konnte nicht geöffnet werden.","Invalid recovery code":"Der Wiederherstellungscode ist ungültig.","Order claim required":"Bitte stelle zuerst deinen Kauf wieder her.","No active entitlement":"Für diesen Kauf ist keine aktive Lizenz vorhanden.","Invalid device code":"Der Gerätecode ist ungültig.","License signing is not configured":"Die Lizenzerstellung ist derzeit nicht verfügbar.","Four PC limit reached":"Das Limit von vier PCs ist erreicht.","Not found":"Die angeforderte Seite wurde nicht gefunden.","Request could not be completed":"Die Anfrage konnte nicht abgeschlossen werden."},
      genericError: "Die Anfrage konnte nicht abgeschlossen werden."
    }
  };
  strings.en.errors["Contract confirmation delivery unavailable"] = "Contract confirmation delivery is not available. Please contact support before activating this purchase.";
  strings.de.errors["Contract confirmation delivery unavailable"] = "Die Zustellung der Vertragsbestätigung ist derzeit nicht verfügbar. Bitte kontaktiere den Support, bevor du diesen Kauf aktivierst.";
  strings.en.errors["Accept both declarations"] = "Please review and separately confirm both declarations.";
  strings.de.errors["Accept both declarations"] = "Bitte prüfe und bestätige beide Erklärungen einzeln.";
  strings.en.errors["Declarations changed"] = "The declarations changed. Reload the page and review them again.";
  strings.de.errors["Declarations changed"] = "Die Erklärungen haben sich geändert. Lade die Seite neu und prüfe sie erneut.";
  strings.en.errors["Declarations unavailable"] = "This earlier payment attempt cannot be resumed. Contact support.";
  strings.de.errors["Declarations unavailable"] = "Dieser ältere Zahlungsvorgang kann nicht fortgesetzt werden. Bitte kontaktiere den Support.";
  strings.en.errors["Preview required"] = "The review expired or changed. Cancel this review and load it again.";
  strings.de.errors["Preview required"] = "Die Prüfung ist abgelaufen oder wurde geändert. Brich diese Ansicht ab und lade sie erneut.";
  Object.assign(strings.en,{resumeHeading:"Review the declarations saved for this purchase",resumeLanguage:"Original language",resumeEarly:"Earlier start declaration",resumeAck:"Earlier acknowledgement",resumeReviewed:"I reviewed these previously saved terms and declarations and wish to continue this payment.",resumeGo:"Continue this payment",cancel:"Cancel and start over",reviewRequired:"Please review and confirm the saved declarations first.",reviewChanged:"The selected declarations changed. Cancel and start a new purchase.",cancelled:"Purchase preparation cancelled.",wait:"Please wait for the current request to finish."});
  Object.assign(strings.de,{resumeHeading:"Für diesen Kauf gespeicherte Erklärungen prüfen",resumeLanguage:"Ursprüngliche Sprache",resumeEarly:"Frühere Erklärung zum vorzeitigen Beginn",resumeAck:"Frühere Kenntnisbestätigung",resumeReviewed:"Ich habe diese früher gespeicherten Bedingungen und Erklärungen geprüft und möchte diesen Zahlungsvorgang fortsetzen.",resumeGo:"Diesen Zahlungsvorgang fortsetzen",cancel:"Abbrechen und neu beginnen",reviewRequired:"Bitte prüfe und bestätige zuerst die gespeicherten Erklärungen.",reviewChanged:"Die gewählten Erklärungen haben sich geändert. Brich ab und beginne einen neuen Kauf.",cancelled:"Kaufvorbereitung abgebrochen.",wait:"Bitte warte, bis die aktuelle Anfrage abgeschlossen ist."});
  const t = strings[language];
  const enabled = await ready(e);
  const document = await contractDocument(e, language);
  const documentDigest = document ? await contractDigest(document) : null;
  const declarations = await consentDocuments(e,language);
  const declarationDigests = declarations ? await Promise.all(declarations.map(contractDigest)) : [null,null];
  const escapeText = text => text.replaceAll("&", "&amp;").replaceAll("<", "&lt;").replaceAll(">", "&gt;").replaceAll("\"", "&quot;").replaceAll("'", "&#39;");
  const declarationControls = declarations ? declarations.map((d,i)=>`<label><input type="checkbox" id="${i===0?'earlyStart':'withdrawalAck'}"><span style="white-space:pre-wrap">${escapeText(d.text)}</span> (${escapeText(d.version)})</label>`).join('') : '';
  const link = (url, label) => {
    try {
      const u = new URL(url);
      if (u.protocol !== "https:") return "";
      return (
        '<a href="' +
        u.href.replaceAll("&", "&amp;").replaceAll('"', "&quot;") +
        '">' +
        label +
        "</a>"
      );
    } catch {
      return "";
    }
  };
  return new Response(
    `<!doctype html><html lang="${language}"><meta charset="utf-8"><meta name="viewport" content="width=device-width"><title>${t.title}</title><style>body{font:18px system-ui;max-width:48rem;margin:3rem auto;padding:1rem}button,input{font:inherit;padding:.7rem}label{display:block;margin:1rem 0}#status{white-space:pre-wrap}</style><h1>MAKRIVO</h1><nav aria-label="Language / Sprache"><a data-language-switch href="/?lang=de" hreflang="de">Deutsch</a> · <a data-language-switch href="/?lang=en" hreflang="en">English</a></nav><p>${t.offer}</p><p>${enabled ? t.enabled : t.disabled}</p><p>${link(e.LEGAL_TERMS_URL, t.terms)} · ${link(e.PRIVACY_URL, t.privacy)} · ${link(e.SELLER_IMPRINT_URL, t.imprint)}</p><section id="newPurchase">${document ? `<section><h2>${t.terms} (${escapeText(document.version)})</h2><pre style="white-space:pre-wrap"><span>${escapeText(document.text)}</span></pre></section>` : ""}<label><input type="checkbox" id="terms"> ${t.accept}</label>${declarationControls}<button id="buy" ${enabled ? "" : "disabled"}>${t.buy}</button><button id="cancelNew" type="button">${t.cancel}</button><section id="backup" hidden><h2>${t.backupTitle}</h2><p>${t.backupInfo}</p><output id="recovery"></output><button id="copyRecovery">${t.copy}</button><p><label><input type="checkbox" id="saved"> ${t.saved}</label><button id="continue">${t.continue}</button></p></section></section><h2>${t.restoreTitle}</h2><p>${t.resumeInfo}</p><label for="restoreCode">${t.restoreLabel}</label><input id="restoreCode" autocomplete="off" type="password"><button id="restore">${t.restoreTitle}</button><button id="resume" ${enabled ? "" : "disabled"}>${t.resume}</button><section id="resumePreview" hidden><h3>${t.resumeHeading}</h3><p>${t.resumeLanguage}: <span id="resumeLang"></span></p><h4>${t.terms}</h4><pre id="resumeTerms" style="white-space:pre-wrap"></pre><h4>${t.resumeEarly}</h4><pre id="resumeEarlyText" style="white-space:pre-wrap"></pre><h4>${t.resumeAck}</h4><pre id="resumeAckText" style="white-space:pre-wrap"></pre><label><input id="resumeReviewed" type="checkbox"> ${t.resumeReviewed}</label><button id="resumeContinue">${t.resumeGo}</button><button id="cancelPreview" type="button">${t.cancel}</button></section><h2>${t.activation}</h2><p>${t.activationInfo}</p><p><button id="termsDownload">${t.documentCopy}</button></p><p><button id="dataExport">${t.dataExport}</button></p><p>${t.dataScope}</p><button id="order">${t.refresh}</button><h3>${t.active}</h3><p id="seats" role="status">${t.refreshInfo}</p><ul id="devices"></ul><p>${t.offline}</p><label for="device">${t.code}</label><input id="device" maxlength="64" autocomplete="off" size="40"><button id="activate">${t.download}</button><p id="status" role="status"></p><script>const T=${JSON.stringify(t).replaceAll("<", "\\u003c")};let activeRequests=0,checkoutLocked=false;async function call(path,data){activeRequests++;try{const r=await fetch(path,{method:data?'POST':'GET',headers:{'Content-Type':'application/json'},body:data?JSON.stringify(data):undefined});const v=await r.json();if(!r.ok)throw Error(v.error);return v}finally{activeRequests--}}function fail(e){status.textContent=T.errors[e.message]||(Object.keys(T.errors).length?T.genericError:e.message)}const status=document.getElementById('status');function lockEvidence(locked){for(const id of ['terms','earlyStart','withdrawalAck']){const control=document.getElementById(id);if(control)control.disabled=locked;}}for(const a of document.querySelectorAll('[data-language-switch]'))a.addEventListener('click',event=>{if(activeRequests>0||checkoutLocked){event.preventDefault();status.textContent=T.languageWarning;}});document.getElementById('buy').onclick=()=>{if(checkoutLocked)return;checkoutLocked=true;lockEvidence(true);return call('/api/checkout/prepare',{acceptTerms:document.getElementById('terms').checked,acceptEarlyStart:document.getElementById('earlyStart')?.checked===true,acknowledgeWithdrawal:document.getElementById('withdrawalAck')?.checked===true,earlyStartDigest:${JSON.stringify(declarationDigests[0])},withdrawalAckDigest:${JSON.stringify(declarationDigests[1])},lang:'${language}',contractDigest:${JSON.stringify(documentDigest)}}).then(v=>{document.getElementById('recovery').textContent=v.recoveryCode;document.getElementById('copyRecovery').onclick=()=>navigator.clipboard.writeText(v.recoveryCode).then(()=>status.textContent=T.copied).catch(fail);document.getElementById('backup').hidden=false;document.getElementById('continue').onclick=()=>{if(activeRequests>0)return;if(!document.getElementById('saved').checked){status.textContent=T.saveFirst;return;}if(!document.getElementById('terms').checked||!document.getElementById('earlyStart')?.checked||!document.getElementById('withdrawalAck')?.checked){status.textContent=T.reviewChanged;return;}return call('/api/checkout',{acceptTerms:true,recoveryCode:v.recoveryCode,checkoutToken:v.checkoutToken}).then(result=>location.assign(result.url)).catch(fail);};}).catch(e=>{checkoutLocked=false;lockEvidence(false);fail(e)});};document.getElementById('cancelNew').onclick=()=>{if(activeRequests>0){status.textContent=T.wait;return;}checkoutLocked=false;lockEvidence(false);for(const id of ['terms','earlyStart','withdrawalAck']){const control=document.getElementById(id);if(control)control.checked=false;}document.getElementById('backup').hidden=true;document.getElementById('recovery').textContent='';document.getElementById('copyRecovery').onclick=null;document.getElementById('continue').onclick=null;status.textContent=T.cancelled;};document.getElementById('cancelPreview').onclick=()=>{if(activeRequests>0){status.textContent=T.wait;return;}checkoutLocked=false;document.getElementById('resumePreview').hidden=true;document.getElementById('newPurchase').hidden=false;document.getElementById('restoreCode').disabled=false;document.getElementById('restoreCode').value='';document.getElementById('restore').disabled=false;document.getElementById('resume').disabled=false;document.getElementById('resumeReviewed').checked=false;document.getElementById('resumeContinue').onclick=null;status.textContent=T.cancelled;};document.getElementById('resume').onclick=()=>{if(activeRequests>0||checkoutLocked)return;const code=document.getElementById('restoreCode').value.trim();document.getElementById('restoreCode').disabled=true;document.getElementById('restore').disabled=true;document.getElementById('resume').disabled=true;return call('/api/checkout/preview',{recoveryCode:code}).then(v=>{if(!v||!['de','en'].includes(v.language)||!v.terms||!v.early||!v.ack||!['terms','early','ack'].every(k=>typeof v[k].version==='string'&&typeof v[k].text==='string')||!/^[A-Za-z0-9_-]{43}$/.test(v.checkoutToken||''))throw Error('Request could not be completed');document.getElementById('resumeLang').textContent=v.language;document.getElementById('resumeTerms').textContent=v.terms.version+'\\n'+v.terms.text;document.getElementById('resumeEarlyText').textContent=v.early.version+'\\n'+v.early.text;document.getElementById('resumeAckText').textContent=v.ack.version+'\\n'+v.ack.text;document.getElementById('resumeReviewed').checked=false;document.getElementById('newPurchase').hidden=true;document.getElementById('resumePreview').hidden=false;checkoutLocked=true;document.getElementById('resumeContinue').onclick=()=>{if(activeRequests>0)return;if(!document.getElementById('resumeReviewed').checked){status.textContent=T.reviewRequired;return;}return call('/api/checkout',{acceptTerms:true,recoveryCode:code,checkoutToken:v.checkoutToken}).then(result=>location.assign(result.url)).catch(fail);};}).catch(err=>{document.getElementById('restoreCode').disabled=false;document.getElementById('restore').disabled=false;document.getElementById('resume').disabled=false;fail(err);});};document.getElementById('restore').onclick=()=>call('/api/claim/restore',{recoveryCode:document.getElementById('restoreCode').value.trim()}).then(()=>{document.getElementById('restoreCode').value='';status.textContent=T.restored;}).catch(fail);async function refreshOrder(){const v=await call('/api/order');const list=document.getElementById('devices');list.replaceChildren();document.getElementById('seats').textContent=T.order+(T.states[v.status]||v.status)+T.activePCs+v.devices.length+T.ofFour+Math.max(0,4-v.devices.length)+'.';for(const device of v.devices){const item=document.createElement('li');const code=document.createElement('code');code.textContent=device.device_id;const button=document.createElement('button');button.textContent=T.deactivate;button.setAttribute('aria-label',T.aria+device.device_id);button.onclick=async()=>{if(!confirm(T.confirm))return;button.disabled=true;try{await call('/api/licenses/deactivate',{deviceId:device.device_id});await refreshOrder();status.textContent=T.released;}catch(e){button.disabled=false;fail(e);}};item.append(code,document.createTextNode(' '),button);list.append(item);}}document.getElementById('order').onclick=()=>refreshOrder().catch(fail);async function downloadPrivate(path,filename){activeRequests++;try{const r=await fetch(path,{method:'POST',credentials:'same-origin',headers:{'Content-Type':'application/json'},body:'{}'});if(!r.ok){const v=await r.json();throw Error(v.error);}const blob=await r.blob();const a=document.createElement('a');const u=URL.createObjectURL(blob);a.href=u;a.download=filename;a.click();setTimeout(()=>URL.revokeObjectURL(u),1000);}catch(e){fail(e);}finally{activeRequests--;}}const dataExportButton=document.getElementById('dataExport');if(dataExportButton)dataExportButton.onclick=()=>downloadPrivate('/api/order/export','MAKRIVO-purchase-data.json');const termsDownloadButton=document.getElementById('termsDownload');if(termsDownloadButton)termsDownloadButton.onclick=()=>downloadPrivate('/api/order/terms','MAKRIVO-terms.txt');document.getElementById('activate').onclick=()=>call('/api/licenses/activate',{deviceId:document.getElementById('device').value.trim()}).then(v=>{const a=document.createElement('a');a.href=URL.createObjectURL(new Blob([JSON.stringify(v)],{type:'application/json'}));a.download='MAKRIVO.license.json';a.click();setTimeout(()=>URL.revokeObjectURL(a.href),1000);refreshOrder().catch(fail)}).catch(fail);</script></html>`,
    {
      headers: {
        "Content-Type": "text/html;charset=utf-8",
        "Cache-Control": "no-store",
        "X-Content-Type-Options": "nosniff",
        "Referrer-Policy": "no-referrer",
        "Content-Security-Policy":
          "default-src 'self'; script-src 'unsafe-inline'; style-src 'unsafe-inline'; frame-ancestors 'none'; base-uri 'none'; form-action 'self'",
      },
    },
  );
}
