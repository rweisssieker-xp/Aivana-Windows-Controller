CREATE TABLE contract_documents(language TEXT NOT NULL CHECK(language IN ('de','en')), version TEXT NOT NULL, text TEXT NOT NULL, PRIMARY KEY(language,version));
CREATE TRIGGER contract_documents_no_update BEFORE UPDATE ON contract_documents BEGIN SELECT RAISE(ABORT,'contract versions are immutable'); END;
CREATE TRIGGER contract_documents_no_delete BEFORE DELETE ON contract_documents BEGIN SELECT RAISE(ABORT,'contract versions are immutable'); END;
CREATE TABLE contract_acceptances(order_id TEXT PRIMARY KEY REFERENCES orders(id), language TEXT NOT NULL CHECK(language IN ('de','en')), version TEXT NOT NULL, text TEXT NOT NULL, digest TEXT NOT NULL, accepted_at INTEGER NOT NULL);
