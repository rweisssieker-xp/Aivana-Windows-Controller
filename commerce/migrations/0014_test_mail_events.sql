-- Only synthetic, allowlisted test events; no email addresses or message bodies.
CREATE TABLE IF NOT EXISTS mail_delivery_events(event_id TEXT PRIMARY KEY,message_id TEXT NOT NULL,kind TEXT NOT NULL,event_at INTEGER NOT NULL);
CREATE INDEX IF NOT EXISTS mail_delivery_events_message ON mail_delivery_events(message_id);
