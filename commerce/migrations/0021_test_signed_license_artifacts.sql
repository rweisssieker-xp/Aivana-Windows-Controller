-- Synthetic test records only. This table never controls a real license.
CREATE TABLE IF NOT EXISTS test_confirmation_payment_bindings(
  order_id TEXT PRIMARY KEY REFERENCES test_confirmation_dispatch_proofs(order_id),
  session_id TEXT NOT NULL,
  payment_intent TEXT NOT NULL,
  created_at INTEGER NOT NULL
);
CREATE TRIGGER IF NOT EXISTS test_confirmation_payment_bindings_check BEFORE INSERT ON test_confirmation_payment_bindings
BEGIN
  SELECT CASE WHEN EXISTS(SELECT 1 FROM test_confirmation_payment_bindings WHERE order_id=NEW.order_id)
    THEN RAISE(ABORT,'test payment bindings are immutable') END;
  SELECT CASE WHEN NOT EXISTS(SELECT 1 FROM test_confirmation_dispatch_proofs p JOIN entitlements e ON e.order_id=p.order_id JOIN orders o ON o.id=e.order_id WHERE p.order_id=NEW.order_id AND p.created_at=NEW.created_at AND e.session_id=NEW.session_id AND o.session_id=NEW.session_id AND e.payment_intent=NEW.payment_intent)
    THEN RAISE(ABORT,'test payment binding mismatch') END;
END;
CREATE TRIGGER IF NOT EXISTS test_confirmation_payment_bindings_no_update BEFORE UPDATE ON test_confirmation_payment_bindings BEGIN SELECT RAISE(ABORT,'test payment bindings are immutable'); END;
CREATE TRIGGER IF NOT EXISTS test_confirmation_payment_bindings_no_delete BEFORE DELETE ON test_confirmation_payment_bindings BEGIN SELECT RAISE(ABORT,'test payment bindings are immutable'); END;
CREATE TABLE IF NOT EXISTS test_signed_license_artifacts(
  license_id TEXT NOT NULL REFERENCES entitlements(id),
  device_id TEXT NOT NULL,
  artifact_digest TEXT NOT NULL,
  created_at INTEGER NOT NULL,
  PRIMARY KEY(license_id,device_id)
);
CREATE TRIGGER IF NOT EXISTS test_signed_license_artifacts_no_update BEFORE UPDATE ON test_signed_license_artifacts BEGIN SELECT RAISE(ABORT,'test artifacts are immutable'); END;
CREATE TRIGGER IF NOT EXISTS test_signed_license_artifacts_no_delete BEFORE DELETE ON test_signed_license_artifacts BEGIN SELECT RAISE(ABORT,'test artifacts are immutable'); END;
CREATE TRIGGER IF NOT EXISTS test_signed_license_artifacts_no_replace BEFORE INSERT ON test_signed_license_artifacts WHEN EXISTS(SELECT 1 FROM test_signed_license_artifacts WHERE license_id=NEW.license_id AND device_id=NEW.device_id) BEGIN SELECT RAISE(ABORT,'test artifacts are immutable'); END;
