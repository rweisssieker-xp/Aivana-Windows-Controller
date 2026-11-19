-- Prior isolated test forms were German-only; no live declarations exist here.
CREATE TABLE IF NOT EXISTS withdrawal_test_languages(request_id TEXT PRIMARY KEY REFERENCES withdrawal_requests(id),language TEXT NOT NULL CHECK(language IN ('de','en')));
CREATE TABLE IF NOT EXISTS withdrawal_test_language_migration(version TEXT PRIMARY KEY);
INSERT INTO withdrawal_test_languages(request_id,language) SELECT id,'de' FROM withdrawal_requests WHERE NOT EXISTS(SELECT 1 FROM withdrawal_test_language_migration WHERE version='0018') ON CONFLICT(request_id) DO NOTHING;
INSERT INTO withdrawal_test_language_migration(version) VALUES('0018') ON CONFLICT(version) DO NOTHING;
