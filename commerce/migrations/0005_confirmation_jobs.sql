CREATE TABLE confirmation_obligations(order_id TEXT PRIMARY KEY REFERENCES orders(id), created_at INTEGER NOT NULL);
INSERT INTO confirmation_obligations(order_id,created_at) SELECT order_id,accepted_at FROM contract_acceptances;
CREATE TRIGGER confirmation_obligations_no_replace BEFORE INSERT ON confirmation_obligations WHEN EXISTS(SELECT 1 FROM confirmation_obligations WHERE order_id=NEW.order_id) BEGIN SELECT RAISE(ABORT,'confirmation obligations are immutable'); END;
CREATE TRIGGER confirmation_obligations_no_update BEFORE UPDATE ON confirmation_obligations BEGIN SELECT RAISE(ABORT,'confirmation obligations are immutable'); END;
CREATE TRIGGER confirmation_obligations_no_delete BEFORE DELETE ON confirmation_obligations BEGIN SELECT RAISE(ABORT,'confirmation obligations are immutable'); END;
CREATE TABLE confirmation_jobs(order_id TEXT PRIMARY KEY REFERENCES confirmation_obligations(order_id), created_at INTEGER NOT NULL);
INSERT INTO confirmation_jobs(order_id,created_at) SELECT o.order_id,CAST(strftime('%s','now') AS INTEGER)*1000 FROM confirmation_obligations o JOIN entitlements e ON e.order_id=o.order_id WHERE e.revoked=0;
