-- Test evidence only: deliberately no recipient, message body or automatic retries.
CREATE TABLE IF NOT EXISTS mail_dispatch_attempts(kind TEXT NOT NULL CHECK(kind IN ('purchase','withdrawal')),job_id TEXT NOT NULL,state TEXT NOT NULL CHECK(state IN ('processing','accepted','unknown')),started_at INTEGER NOT NULL,finished_at INTEGER,message_id TEXT,PRIMARY KEY(kind,job_id));
