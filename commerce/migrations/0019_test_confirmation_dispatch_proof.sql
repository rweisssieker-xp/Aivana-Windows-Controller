-- Synthetic-only evidence and decisions. No real licence or entitlement writes.
CREATE TABLE IF NOT EXISTS test_confirmation_dispatch_proofs(
  order_id TEXT PRIMARY KEY REFERENCES confirmation_jobs(order_id),
  text_digest TEXT NOT NULL,
  facts_text TEXT NOT NULL,
  created_at INTEGER NOT NULL
);
CREATE TRIGGER IF NOT EXISTS test_confirmation_dispatch_proofs_no_replace BEFORE INSERT ON test_confirmation_dispatch_proofs WHEN EXISTS(SELECT 1 FROM test_confirmation_dispatch_proofs WHERE order_id=NEW.order_id) BEGIN SELECT RAISE(ABORT,'test proof is immutable'); END;
CREATE TRIGGER IF NOT EXISTS test_confirmation_dispatch_proofs_no_update BEFORE UPDATE ON test_confirmation_dispatch_proofs BEGIN SELECT RAISE(ABORT,'test proof is immutable'); END;
CREATE TRIGGER IF NOT EXISTS test_confirmation_dispatch_proofs_no_delete BEFORE DELETE ON test_confirmation_dispatch_proofs BEGIN SELECT RAISE(ABORT,'test proof is immutable'); END;
CREATE TABLE IF NOT EXISTS test_gate_decisions(id INTEGER PRIMARY KEY AUTOINCREMENT,license_id TEXT NOT NULL,device_id TEXT NOT NULL,decision TEXT NOT NULL CHECK(decision IN ('allow','block')),created_at INTEGER NOT NULL);
CREATE TRIGGER IF NOT EXISTS test_gate_decisions_no_update BEFORE UPDATE ON test_gate_decisions BEGIN SELECT RAISE(ABORT,'test decision is immutable'); END;
CREATE TRIGGER IF NOT EXISTS test_gate_decisions_no_delete BEFORE DELETE ON test_gate_decisions BEGIN SELECT RAISE(ABORT,'test decision is immutable'); END;
