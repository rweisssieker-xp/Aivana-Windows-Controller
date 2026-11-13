CREATE TABLE withdrawal_requests(id TEXT PRIMARY KEY, name TEXT NOT NULL, contract_reference TEXT NOT NULL, contact_email TEXT NOT NULL, received_at INTEGER NOT NULL);
CREATE TABLE withdrawal_receipts(request_id TEXT PRIMARY KEY REFERENCES withdrawal_requests(id), status TEXT NOT NULL CHECK(status IN ('pending','sent')));
