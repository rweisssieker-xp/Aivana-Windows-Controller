-- Existing installations only. Fresh databases use schema.sql instead.
-- NULL identifies legacy buckets (which may contain raw IPs) for immediate cleanup.
ALTER TABLE rate_limits ADD COLUMN expires_at INTEGER;
CREATE INDEX rate_limits_expiry ON rate_limits(expires_at);
