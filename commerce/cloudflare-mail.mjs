// Isolated adapter: not connected to a route, cron job, or production binding.
// Caller must verify recipient, authorization, document completeness and delivery policy.
const address = value => typeof value === 'string' && value.length <= 254 && !/[\s\x00-\x1f\x7f]/.test(value) && /^[A-Za-z0-9.!#$%&'*+/=?^_{|}~-]+@[A-Za-z0-9]+(?:[A-Za-z0-9-]*[A-Za-z0-9])?(?:\.[A-Za-z0-9]+(?:[A-Za-z0-9-]*[A-Za-z0-9])?)+$/.test(value);
const header = value => typeof value === 'string' && value.trim() === value && value.length > 0 && value.length <= 200 && !/[\x00-\x1f\x7f]/.test(value);

export async function sendPreparedEmail(binding, message) {
  if (!binding || typeof binding.send !== 'function' || !message || !address(message.sender) || !address(message.recipient) || !header(message.subject) || typeof message.text !== 'string' || !message.text.trim() || new TextEncoder().encode(message.text).length > 200000 || /[\x00-\x08\x0b-\x1f\x7f]/.test(message.text)) {
    throw new TypeError('Invalid email preparation');
  }
  try {
    const result = await binding.send({from:message.sender,to:message.recipient,subject:message.subject,text:message.text});
    if (typeof result?.messageId !== 'string' || !result.messageId || result.messageId.length > 512 || /[\x00-\x20\x7f]/.test(result.messageId)) return {status:'outcome_unknown'};
    return {status:'accepted_by_provider',messageId:result.messageId};
  } catch {
    // A provider exception alone cannot prove whether acceptance occurred.
    return {status:'outcome_unknown'};
  }
}
