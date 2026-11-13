import { generateKeyPairSync, randomUUID } from 'node:crypto';
import { mkdtempSync, readFileSync, writeFileSync, rmSync, realpathSync } from 'node:fs';
import { tmpdir } from 'node:os';
import { basename, dirname, join } from 'node:path';
import { fileURLToPath } from 'node:url';
import { spawnSync } from 'node:child_process';
import { signLicense } from '../commerce/worker.mjs';

const repository = fileURLToPath(new URL('../', import.meta.url));
const temporary = mkdtempSync(join(tmpdir(), 'makrivo-interop-'));
try {
  const pair = generateKeyPairSync('rsa', { modulusLength: 3072 });
  const keyId = 'ephemeral-interop';
  const deviceId = 'a'.repeat(64);
  const envelope = await signLicense(randomUUID(), deviceId, {
    LICENSE_PRIVATE_KEY_PEM: pair.privateKey.export({ type: 'pkcs8', format: 'pem' }),
    LICENSE_KEY_ID: keyId,
  });
  const workerFixture = join(temporary, 'worker.json');
  const dotnetFixture = join(temporary, 'dotnet.json');
  // Only public material and test licenses ever leave process memory.
  writeFileSync(workerFixture, JSON.stringify({ envelopeJson: JSON.stringify(envelope), publicKeys: {
    [keyId]: pair.publicKey.export({ type: 'spki', format: 'pem' }),
  }, deviceId }));
  const result = spawnSync('dotnet', ['run', '--project', 'tests/Aivana.Commerce.Interop.Tests', '-c', 'Release', '--', workerFixture, dotnetFixture], {
    cwd: repository, encoding: 'utf8', windowsHide: true, timeout: 120000,
  });
  if (result.stdout) process.stdout.write(result.stdout);
  if (result.stderr) process.stderr.write(result.stderr);
  if (result.error || result.status !== 0) throw new Error('Worker/.NET verification failed', { cause: result.error });
  const fixture = JSON.parse(readFileSync(dotnetFixture, 'utf8'));
  const signed = JSON.parse(fixture.envelopeJson);
  const key = await crypto.subtle.importKey('spki', Buffer.from(fixture.publicKeyPem.replace(/-----[^-]+-----|\s/g, ''), 'base64'),
    { name: 'RSASSA-PKCS1-v1_5', hash: 'SHA-256' }, false, ['verify']);
  if (!await crypto.subtle.verify('RSASSA-PKCS1-v1_5', key, Buffer.from(signed.signature, 'base64url'), Buffer.from(signed.payload, 'base64url')))
    throw new Error('.NET-issued license rejected by WebCrypto');
  console.log('PASS .NET-issued license verifies with Worker-compatible WebCrypto');
} finally {
  const resolved = realpathSync(temporary);
  if (dirname(resolved) !== realpathSync(tmpdir()) || !basename(resolved).startsWith('makrivo-interop-'))
    throw new Error('Refusing cleanup outside the owned temporary directory');
  rmSync(resolved, { recursive: true });
}
