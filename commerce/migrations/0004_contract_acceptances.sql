CREATE TABLE contract_documents(language TEXT NOT NULL CHECK(language IN ('de','en')), version TEXT NOT NULL, text TEXT NOT NULL, PRIMARY KEY(language,version));
CREATE TRIGGER contract_documents_no_replace BEFORE INSERT ON contract_documents WHEN EXISTS(SELECT 1 FROM contract_documents WHERE language=NEW.language AND version=NEW.version) BEGIN SELECT RAISE(ABORT,'contract versions are immutable'); END;
CREATE TRIGGER contract_documents_no_update BEFORE UPDATE ON contract_documents BEGIN SELECT RAISE(ABORT,'contract versions are immutable'); END;
CREATE TRIGGER contract_documents_no_delete BEFORE DELETE ON contract_documents BEGIN SELECT RAISE(ABORT,'contract versions are immutable'); END;
CREATE TABLE contract_acceptances(order_id TEXT PRIMARY KEY REFERENCES orders(id), language TEXT NOT NULL CHECK(language IN ('de','en')), version TEXT NOT NULL, text TEXT NOT NULL, digest TEXT NOT NULL, accepted_at INTEGER NOT NULL);
CREATE TRIGGER contract_acceptances_no_replace BEFORE INSERT ON contract_acceptances WHEN EXISTS(SELECT 1 FROM contract_acceptances WHERE order_id=NEW.order_id) BEGIN SELECT RAISE(ABORT,'contract acceptances are immutable'); END;
CREATE TRIGGER contract_acceptances_no_update BEFORE UPDATE ON contract_acceptances BEGIN SELECT RAISE(ABORT,'contract acceptances are immutable'); END;
CREATE TRIGGER contract_acceptances_no_delete BEFORE DELETE ON contract_acceptances BEGIN SELECT RAISE(ABORT,'contract acceptances are immutable'); END;
