"""Regression checks for technical identifiers in translated documentation."""
from pathlib import Path
import contextlib
import io
import sys
import unittest
from unittest.mock import patch

sys.path.insert(0, str(Path(__file__).resolve().parents[1] / "tools"))
import check_documentation_all as checks


class HashIdentifierChecks(unittest.TestCase):
    def test_changed_or_omitted_model_hash_is_rejected(self):
        model_hash = "48e9c4ef12f7f995ca11fc7020b39990aa27bc67cc0fe116067676a000a04429"
        source = "Modellgewichte-SHA256: " + model_hash
        for translated in ("Model weights: omitted", "Model weights: " + model_hash.replace("676", "67", 1)):
            with self.subTest(translated=translated):
                self.assertEqual({model_hash}, checks.missing_hash_identifiers(source, translated))

    def test_revision_survives_translation_and_non_latin_punctuation(self):
        revision = "5c19002edce50beaa3a5c7eeefd8fe5269ee8d19"
        self.assertEqual(set(), checks.missing_hash_identifiers("Commit " + revision, "Ревизия:（" + revision + "）"))

    def test_dates_versions_and_added_provenance_are_not_source_hashes(self):
        self.assertEqual(set(), checks.missing_hash_identifiers("2026-10-12; 0.17.1; 65536", "Translated prose"))
        self.assertEqual(set(), checks.missing_hash_identifiers("Original prose", "sha256=" + "a" * 64))

    def test_full_checker_reports_missing_plain_text_revision(self):
        source = checks.ROOT / "docs/HANDBOOK.md"
        original_read = Path.read_text
        revision = "0123456789abcdef0123456789abcdef01234567"

        def read_with_new_revision(path, *args, **kwargs):
            text = original_read(path, *args, **kwargs)
            return text + "\nRevision: " + revision if path == source else text

        output = io.StringIO()
        with patch.object(Path, "read_text", read_with_new_revision), contextlib.redirect_stdout(output):
            result = checks.main()
        self.assertEqual(1, result)
        self.assertIn("Technical hash/revision changed or missing: docs/en-US/HANDBOOK.md: " + revision, output.getvalue())


if __name__ == "__main__":
    unittest.main()
