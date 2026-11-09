CREATE TABLE checkout_attempts(order_id TEXT PRIMARY KEY REFERENCES orders(id), return_lang TEXT NOT NULL CHECK(return_lang IN ('de','en','')));
