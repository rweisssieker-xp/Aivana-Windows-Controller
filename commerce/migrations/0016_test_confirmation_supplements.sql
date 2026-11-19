-- Test-only: no backfill, no customer-facing documents or delivery evidence.
CREATE TABLE IF NOT EXISTS test_confirmation_supplements(
  order_id TEXT NOT NULL REFERENCES contract_acceptances(order_id),
  kind TEXT NOT NULL CHECK(kind IN ('withdrawal_notice','withdrawal_form')),
  language TEXT NOT NULL CHECK(language IN ('de','en')),
  version TEXT NOT NULL,
  text TEXT NOT NULL,
  digest TEXT NOT NULL,
  captured_at INTEGER NOT NULL,
  PRIMARY KEY(order_id,kind)
);
CREATE TRIGGER IF NOT EXISTS test_confirmation_supplements_before_insert
BEFORE INSERT ON test_confirmation_supplements
BEGIN
  SELECT CASE WHEN EXISTS(SELECT 1 FROM test_confirmation_supplements WHERE order_id=NEW.order_id AND kind=NEW.kind)
    THEN RAISE(ABORT,'supplement snapshots are immutable') END;
  SELECT CASE WHEN NOT EXISTS(SELECT 1 FROM contract_acceptances WHERE order_id=NEW.order_id AND language=NEW.language AND accepted_at=NEW.captured_at)
    THEN RAISE(ABORT,'supplement acceptance mismatch') END;
END;
CREATE TRIGGER IF NOT EXISTS test_confirmation_supplements_no_update BEFORE UPDATE ON test_confirmation_supplements BEGIN SELECT RAISE(ABORT,'supplement snapshots are immutable'); END;
CREATE TRIGGER IF NOT EXISTS test_confirmation_supplements_no_delete BEFORE DELETE ON test_confirmation_supplements BEGIN SELECT RAISE(ABORT,'supplement snapshots are immutable'); END;
