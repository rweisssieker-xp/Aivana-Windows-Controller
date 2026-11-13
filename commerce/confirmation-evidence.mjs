// Isolated preparation only; no Worker route, delivery, or release authorization.
const encoder = new TextEncoder();
const validVersion = value => typeof value === 'string' && /^[A-Za-z0-9._-]{1,64}$/.test(value);
const validText = value => typeof value === 'string' && Boolean(value.trim()) && value.length <= 20000 && !/[\u0000-\u0009\u000B-\u001F\u007F]/.test(value);
const validTime = value => Number.isSafeInteger(value) && value > 0 && value <= 8640000000000000;

async function digest(document) {
  const bytes = new Uint8Array(await crypto.subtle.digest('SHA-256', encoder.encode(JSON.stringify(document))));
  return btoa(String.fromCharCode(...bytes)).replaceAll('+', '-').replaceAll('/', '_').replaceAll('=', '');
}

export async function buildConfirmationEvidence(input) {
  const fail = () => { throw new Error('Invalid confirmation evidence'); };
  if (!input || typeof input.orderId !== 'string' || !/^[A-Za-z0-9_-]{1,128}$/.test(input.orderId) || !['de', 'en'].includes(input.language)) fail();
  if (!input.acceptance || !Array.isArray(input.declarations) || input.declarations.length !== 2) fail();
  const validate = async (row, kind) => {
    if (!row || row.order_id !== input.orderId || row.language !== input.language || !validVersion(row.version) || !validText(row.text) || !validTime(row.accepted_at)) fail();
    const document = kind
      ? { kind, language: row.language, version: row.version, text: row.text }
      : { language: row.language, version: row.version, text: row.text };
    if ((kind && row.kind !== kind) || typeof row.digest !== 'string' || row.digest !== await digest(document)) fail();
    return { ...document, digest: row.digest, acceptedAt: row.accepted_at };
  };
  const earlyRows = input.declarations.filter(row => row?.kind === 'early_start');
  const ackRows = input.declarations.filter(row => row?.kind === 'withdrawal_ack');
  if (earlyRows.length !== 1 || ackRows.length !== 1) fail();
  const terms = await validate(input.acceptance);
  const early = await validate(earlyRows[0], 'early_start');
  const ack = await validate(ackRows[0], 'withdrawal_ack');
  if (early.version === ack.version || early.text === ack.text || early.acceptedAt !== terms.acceptedAt || ack.acceptedAt !== terms.acceptedAt) fail();
  return { orderId: input.orderId, language: input.language, terms, declarations: [early, ack] };
}
