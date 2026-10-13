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
export const activationSql = `INSERT INTO devices(license_id,device_id,active) SELECT ?1,?2,1 WHERE EXISTS(SELECT 1 FROM entitlements WHERE id=?1 AND revoked=0) AND (SELECT count(*) FROM devices WHERE license_id=?1 AND active=1)<4 ON CONFLICT(license_id,device_id) DO UPDATE SET active=1 WHERE devices.active=0 AND EXISTS(SELECT 1 FROM entitlements WHERE id=?1 AND revoked=0) AND (SELECT count(*) FROM devices WHERE license_id=?1 AND active=1)<4`;
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
async function stripe(e, path, body) {
  const r = await fetch(`https://api.stripe.com/v1/${path}`, {
    method: body ? "POST" : "GET",
    headers: {
      Authorization: `Bearer ${e.STRIPE_SECRET_KEY}`,
      ...(body ? { "Content-Type": "application/x-www-form-urlencoded" } : {}),
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
    e.DB.prepare("INSERT OR IGNORE INTO events(id,kind) VALUES(?,?)").bind(
      event.id,
      event.type,
    ),
  ]);
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
export default {
  async fetch(req, e) {
    try {
      const u = new URL(req.url);
      if (u.pathname === "/" && req.method === "GET") return page(e);
      // Existing payment processing is independent of new-sale and signer switches.
      if (u.pathname === "/api/stripe/webhook" && req.method === "POST") {
        if (!providerReady(e))
          return json({ error: "Payment processing is not configured" }, 503);
        return await webhook(req, e);
      }
      if (u.pathname === "/api/checkout" && !(await ready(e)))
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
      const bucket = `${req.headers.get("CF-Connecting-IP") || "unknown"}:${Math.floor(Date.now() / 60000)}`;
      const limit = await e.DB.prepare(
        "INSERT INTO rate_limits(bucket,hits) VALUES(?,1) ON CONFLICT(bucket) DO UPDATE SET hits=hits+1 RETURNING hits",
      )
        .bind(bucket)
        .first();
      if (limit.hits > 30) return json({ error: "Try later" }, 429);
      if (u.pathname === "/api/checkout" && req.method === "POST") {
        const input = JSON.parse(await bounded(req));
        if (input.acceptTerms !== true)
          return json({ error: "Accept purchase terms" }, 400);
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
        const secret = b64(crypto.getRandomValues(new Uint8Array(32))),
          id = crypto.randomUUID();
        await e.DB.prepare(
          "INSERT INTO orders(id,claim_hash,created_at) VALUES(?,?,?)",
        )
          .bind(id, await hash(secret), Date.now())
          .run();
        const s = await stripe(e, "checkout/sessions", {
          mode: "payment",
          "line_items[0][price]": e.STRIPE_PRICE_ID,
          "line_items[0][quantity]": "1",
          success_url: `${e.PUBLIC_ORIGIN}/`,
          cancel_url: `${e.PUBLIC_ORIGIN}/`,
          "metadata[orderId]": id,
          "payment_intent_data[metadata][orderId]": id,
          "consent_collection[terms_of_service]": "required",
          "automatic_tax[enabled]": "true",
        });
        let checkoutUrl;
        try {
          checkoutUrl = new URL(s.url);
        } catch {
          return json({ error: "Invalid checkout response" }, 502);
        }
        if (
          s.mode !== "payment" ||
          s.livemode !== (e.STRIPE_LIVE === "true") ||
          checkoutUrl.protocol !== "https:" ||
          checkoutUrl.hostname !== "checkout.stripe.com" ||
          checkoutUrl.username ||
          checkoutUrl.password ||
          checkoutUrl.port
        )
          return json({ error: "Invalid checkout response" }, 502);
        await e.DB.prepare("UPDATE orders SET session_id=? WHERE id=?")
          .bind(s.id, id)
          .run();
        return json({ url: s.url, recoveryCode: secret }, 200, {
          "Set-Cookie": `makrivo_claim=${secret}; Path=/; Secure; HttpOnly; SameSite=Lax; Max-Age=31536000`,
        });
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
        return json(await signLicense(a.license_id, deviceId, e));
      }
      return json({ error: "Not found" }, 404);
    } catch {
      return json({ error: "Request could not be completed" }, 400);
    }
  },
};
async function page(e) {
  const enabled = await ready(e);
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
    `<!doctype html><html lang="en"><meta charset="utf-8"><meta name="viewport" content="width=device-width"><title>MAKRIVO purchase and activation</title><style>body{font:18px system-ui;max-width:48rem;margin:3rem auto;padding:1rem}button,input{font:inherit;padding:.7rem}label{display:block;margin:1rem 0}#status{white-space:pre-wrap}</style><h1>MAKRIVO</h1><p>Aivana GmbH · €29.00 including tax · one-time purchase · one person · four own PCs. Commercial use permitted. No subscription. Perpetual license for major version 1.</p><p>${enabled ? "Sales configured." : "Checkout is unavailable until operator configuration and legal approval are complete."}</p><p>${link(e.LEGAL_TERMS_URL, "Purchase terms")} · ${link(e.PRIVACY_URL, "Privacy")} · ${link(e.SELLER_IMPRINT_URL, "Seller imprint")}</p><label><input type="checkbox" id="terms"> I accept the purchase terms.</label><button id="buy" ${enabled ? "" : "disabled"}>Buy for €29.00</button><section id="backup" hidden><h2>Save your recovery code</h2><p>This secret restores your purchase in another browser. Save it privately before continuing. Never share it.</p><output id="recovery"></output><button id="copyRecovery">Copy recovery code</button><p><label><input type="checkbox" id="saved"> I saved this recovery code privately.</label><button id="continue">Continue to secure payment</button></p></section><h2>Restore purchase</h2><label for="restoreCode">Saved recovery code</label><input id="restoreCode" autocomplete="off" type="password"><button id="restore">Restore purchase</button><h2>Activate your PC</h2><p>Return in this browser after payment. Copy the device code shown in the Windows app. Download the license and import it in MAKRIVO on that PC. Keep this browser profile to access your order.</p><button id="order">Refresh payment and PCs</button><h3>Your active PCs</h3><p id="seats" role="status">Refresh to view your active PCs and available seats.</p><ul id="devices"></ul><p>Deactivate a PC you no longer use to release an activation seat. Remove its local license file too; an already issued offline license cannot be remotely disabled.</p><label for="device">Device code (64 characters)</label><input id="device" maxlength="64" autocomplete="off" size="40"><button id="activate">Download license</button><p id="status" role="status"></p><script>async function call(path,data){const r=await fetch(path,{method:data?'POST':'GET',headers:{'Content-Type':'application/json'},body:data?JSON.stringify(data):undefined});const v=await r.json();if(!r.ok)throw Error(v.error);return v}function fail(e){status.textContent=e.message}const status=document.getElementById('status');document.getElementById('buy').onclick=()=>call('/api/checkout',{acceptTerms:document.getElementById('terms').checked}).then(v=>{document.getElementById('recovery').textContent=v.recoveryCode;document.getElementById('copyRecovery').onclick=()=>navigator.clipboard.writeText(v.recoveryCode).then(()=>status.textContent='Recovery code copied. Save it privately.').catch(fail);document.getElementById('backup').hidden=false;document.getElementById('continue').onclick=()=>{if(document.getElementById('saved').checked)location.assign(v.url);else fail(Error('Save the recovery code first.'));};}).catch(fail);document.getElementById('restore').onclick=()=>call('/api/claim/restore',{recoveryCode:document.getElementById('restoreCode').value.trim()}).then(()=>{document.getElementById('restoreCode').value='';status.textContent='Purchase restored.';}).catch(fail);async function refreshOrder(){const v=await call('/api/order');const list=document.getElementById('devices');list.replaceChildren();document.getElementById('seats').textContent='Order: '+v.status+'. Active PCs: '+v.devices.length+' of 4. Available seats: '+Math.max(0,4-v.devices.length)+'.';for(const device of v.devices){const item=document.createElement('li');const code=document.createElement('code');code.textContent=device.device_id;const button=document.createElement('button');button.textContent='Deactivate this PC';button.setAttribute('aria-label','Deactivate PC '+device.device_id);button.onclick=async()=>{if(!confirm('Release this PC activation seat? Remove the license file on that PC. Existing offline files cannot be remotely disabled.'))return;button.disabled=true;try{await call('/api/licenses/deactivate',{deviceId:device.device_id});await refreshOrder();status.textContent='PC deactivated. Activation seat released.';}catch(e){button.disabled=false;fail(e);}};item.append(code,document.createTextNode(' '),button);list.append(item);}}document.getElementById('order').onclick=()=>refreshOrder().catch(fail);document.getElementById('activate').onclick=()=>call('/api/licenses/activate',{deviceId:document.getElementById('device').value.trim()}).then(v=>{const a=document.createElement('a');a.href=URL.createObjectURL(new Blob([JSON.stringify(v)],{type:'application/json'}));a.download='MAKRIVO.license.json';a.click();setTimeout(()=>URL.revokeObjectURL(a.href),1000);refreshOrder().catch(fail)}).catch(fail);</script></html>`,
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
