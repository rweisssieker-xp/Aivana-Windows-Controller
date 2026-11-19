-- Apply only after 0011. This adds an index and does not delete purchase data.
CREATE INDEX IF NOT EXISTS checkout_authorizations_expiry ON checkout_authorizations(expires_at);
