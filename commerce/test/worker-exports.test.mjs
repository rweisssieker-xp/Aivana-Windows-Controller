import { test } from 'node:test';
import assert from 'node:assert/strict';
import * as worker from '../worker.mjs';

test('named Worker exports are callable entry points accepted by workerd', () => {
  const invalid = Object.entries(worker)
    .filter(([name, value]) => name !== 'default' && typeof value !== 'function')
    .map(([name]) => name);
  assert.deepEqual(invalid, [], `Non-callable Worker exports: ${invalid.join(', ')}`);
});
