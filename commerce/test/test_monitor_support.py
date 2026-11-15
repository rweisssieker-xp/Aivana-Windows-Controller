import importlib.util
import pathlib
import sqlite3
import unittest

path = pathlib.Path(__file__).resolve().parents[1] / 'monitor_support.py'
spec = importlib.util.spec_from_file_location('monitor_support', path)
monitor = importlib.util.module_from_spec(spec)
spec.loader.exec_module(monitor)

class SupportMonitorTests(unittest.TestCase):
    def setUp(self):
        self.db = sqlite3.connect(':memory:')
        self.db.row_factory = sqlite3.Row
        self.db.executescript("""
        CREATE TABLE mail_dispatch_attempts(kind TEXT,job_id TEXT,state TEXT,started_at INTEGER,finished_at INTEGER,message_id TEXT);
        CREATE TABLE mail_delivery_events(event_id TEXT,message_id TEXT,kind TEXT,event_at INTEGER);
        """)
        self.metrics = {'source': {'backlog_count': 0, 'oldest_message_timestamp_ms': 0},
                        'dead_letter': {'backlog_count': 0, 'oldest_message_timestamp_ms': 0}}
        self.enabled = True

    def api(self, path, body=None):
        if path == monitor.SUBSCRIPTION_PATH:
            return {'enabled': self.enabled}
        if path == monitor.SOURCE_METRICS_PATH:
            return self.metrics['source']
        if path == monitor.DLQ_METRICS_PATH:
            return self.metrics['dead_letter']
        if path == monitor.DATABASE_PATH:
            rows = self.db.execute(body['sql'], body['params']).fetchall()
            return [{'success': True, 'results': [dict(row) for row in rows]}]
        raise AssertionError('unexpected API request')

    def test_clean_delivered_case_is_healthy_if_subscription_enabled(self):
        self.db.execute("INSERT INTO mail_dispatch_attempts VALUES('purchase','internal-job','accepted',0,1,'message-id')")
        self.db.execute("INSERT INTO mail_delivery_events VALUES('event-id','message-id','delivered',2)")
        result = monitor.read_status(self.api, 2_000_000)
        self.assertEqual(result['status'], 'ok')
        self.assertEqual(result['issues'], [])
        self.assertNotIn('message-id', str(result))

    def test_uncertain_failure_stale_unmatched_and_queue_failure_are_reported(self):
        self.db.execute("INSERT INTO mail_dispatch_attempts VALUES('purchase','one','unknown',0,1,NULL)")
        self.db.execute("INSERT INTO mail_dispatch_attempts VALUES('withdrawal','two','processing',0,NULL,NULL)")
        self.db.execute("INSERT INTO mail_dispatch_attempts VALUES('purchase','three','accepted',0,1,'m3')")
        self.db.execute("INSERT INTO mail_delivery_events VALUES('event3','m3','bounced',2)")
        self.db.execute("INSERT INTO mail_delivery_events VALUES('event-other','unmatched','delivered',2)")
        self.metrics['dead_letter']['backlog_count'] = 1
        result = monitor.read_status(self.api, 2_000_000)
        self.assertEqual(result['status'], 'attention')
        self.assertEqual(set(result['issues']), {'unknown_attempts','stuck_attempts','provider_failures','unmatched_events','dead_letter_backlog'})
        self.assertNotIn('unmatched', str(result.get('details', {})))

    def test_disabled_subscription_and_expired_accepted_mail_are_not_healthy(self):
        self.enabled = False
        self.db.execute("INSERT INTO mail_dispatch_attempts VALUES('purchase','one','accepted',0,1,'message')")
        result = monitor.read_status(self.api, 2_000_000)
        self.assertEqual(set(result['issues']), {'subscription_disabled','delivery_timeout'})

    def test_missing_or_invalid_data_and_api_failure_do_not_report_healthy(self):
        with self.assertRaises(ValueError): monitor.read_status(self.api, -1)
        def fail(path, body=None): raise OSError('provider is offline')
        with self.assertRaises(OSError): monitor.read_status(fail, 2_000_000)
        self.metrics['source']['backlog_count'] = 'invalid'
        with self.assertRaises(ValueError): monitor.read_status(self.api, 2_000_000)

    def test_old_or_unknown_age_queue_backlog_requires_attention_without_mutation(self):
        before = self.db.total_changes
        self.metrics['source'] = {'backlog_count': 1, 'oldest_message_timestamp_ms': 1_000_000}
        result = monitor.read_status(self.api, 2_000_000)
        self.assertIn('event_queue_backlog', result['issues'])
        self.assertEqual(self.db.total_changes, before)
        self.metrics['source']['oldest_message_timestamp_ms'] = 0
        self.assertIn('event_queue_backlog', monitor.read_status(self.api, 2_000_000)['issues'])

    def test_case_view_lists_safe_pseudonymous_references_for_investigation(self):
        self.db.execute("INSERT INTO mail_dispatch_attempts VALUES('purchase','order-test','unknown',0,1,NULL)")
        self.db.execute("INSERT INTO mail_dispatch_attempts VALUES('withdrawal','withdrawal-test','accepted',0,1,'private-message-id')")
        result = monitor.read_test_cases(self.api, 2_000_000)
        self.assertEqual({(item['kind'],item['reference'],item['reason']) for item in result['cases']},
                         {('purchase','order-test','outcome_unknown'),('withdrawal','withdrawal-test','delivery_timeout')})
        self.assertNotIn('private-message-id',str(result))
        self.assertFalse(result['truncated'])

    def test_case_view_reports_truncation_and_rejects_unexpected_identifiers(self):
        for index in range(3):
            self.db.execute("INSERT INTO mail_dispatch_attempts VALUES('purchase',?,'unknown',0,1,NULL)",(f'job-{index}',))
        result=monitor.read_test_cases(self.api,2_000_000,limit=2)
        self.assertEqual(len(result['cases']),2)
        self.assertTrue(result['truncated'])
        self.db.execute("INSERT INTO mail_dispatch_attempts VALUES('purchase','bad identifier','unknown',0,1,NULL)")
        with self.assertRaises(ValueError):monitor.read_test_cases(self.api,2_000_000)

    def test_invalid_accepted_message_identifier_requires_immediate_review(self):
        for index, identifier in enumerate([None, '', 'has space', 'über', 'x'*513, 'abc\0def']):
            self.db.execute("INSERT INTO mail_dispatch_attempts VALUES('purchase',?,'accepted',1000,1000,?)",
                            (f'invalid-{index}',identifier))
        result=monitor.read_status(self.api,1001)
        self.assertEqual(result['counts']['provider_failures'],6)
        self.assertEqual(result['counts']['delivery_timeout'],0)
        cases=monitor.read_test_cases(self.api,1001)
        self.assertEqual(len(cases['cases']),6)
        self.assertTrue(all(case['reason']=='provider_or_matching_failure' for case in cases['cases']))

if __name__ == '__main__': unittest.main()
