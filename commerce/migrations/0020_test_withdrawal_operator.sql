-- Synthetic data only. No free-form notes or real operator identities.
CREATE TABLE IF NOT EXISTS withdrawal_test_operator_events(event_id TEXT PRIMARY KEY,request_id TEXT NOT NULL REFERENCES withdrawal_requests(id),action TEXT NOT NULL CHECK(action IN ('investigating','escalated')),created_at INTEGER NOT NULL);
CREATE INDEX IF NOT EXISTS withdrawal_test_operator_events_request ON withdrawal_test_operator_events(request_id,created_at);
CREATE TRIGGER IF NOT EXISTS withdrawal_test_operator_events_no_replace BEFORE INSERT ON withdrawal_test_operator_events WHEN EXISTS(SELECT 1 FROM withdrawal_test_operator_events WHERE event_id=NEW.event_id) BEGIN SELECT RAISE(ABORT,'test operator evidence is append only'); END;
CREATE TRIGGER IF NOT EXISTS withdrawal_test_operator_events_no_update BEFORE UPDATE ON withdrawal_test_operator_events BEGIN SELECT RAISE(ABORT,'test operator evidence is append only'); END;
CREATE TRIGGER IF NOT EXISTS withdrawal_test_operator_events_no_delete BEFORE DELETE ON withdrawal_test_operator_events BEGIN SELECT RAISE(ABORT,'test operator evidence is append only'); END;
