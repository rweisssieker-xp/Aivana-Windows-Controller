-- Isolated test data only: HMAC buckets rather than recipient or source.
CREATE TABLE IF NOT EXISTS withdrawal_test_rate_limits(bucket TEXT PRIMARY KEY, hits INTEGER NOT NULL, expires_at INTEGER NOT NULL);
CREATE INDEX IF NOT EXISTS withdrawal_test_rate_limits_expiry ON withdrawal_test_rate_limits(expires_at);
