PRAGMA foreign_keys=ON;
CREATE TABLE orders(id TEXT PRIMARY KEY, claim_hash TEXT NOT NULL UNIQUE, created_at INTEGER NOT NULL, session_id TEXT UNIQUE);
CREATE TABLE entitlements(id TEXT PRIMARY KEY, order_id TEXT NOT NULL UNIQUE REFERENCES orders(id), session_id TEXT NOT NULL UNIQUE, payment_intent TEXT UNIQUE, revoked INTEGER NOT NULL DEFAULT 0 CHECK(revoked IN(0,1)));
CREATE TABLE devices(license_id TEXT NOT NULL REFERENCES entitlements(id), device_id TEXT NOT NULL, active INTEGER NOT NULL DEFAULT 1 CHECK(active IN(0,1)), PRIMARY KEY(license_id,device_id));
CREATE TABLE events(id TEXT PRIMARY KEY, kind TEXT NOT NULL);
CREATE TABLE revoked_payments(payment_intent TEXT PRIMARY KEY);
CREATE TABLE rate_limits(bucket TEXT PRIMARY KEY, hits INTEGER NOT NULL, expires_at INTEGER NOT NULL);
CREATE INDEX rate_limits_expiry ON rate_limits(expires_at);
