// Shared formatter for synthetic confirmations only. Callers validate evidence.
export function renderTestConfirmation({orderId,evidence,facts=''}){
  const titles=evidence.language==='de'?['Kaufbedingungen','Vorzeitiger Beginn','Kenntnisbestätigung','Widerrufsbelehrung','Widerrufsformular']:['Purchase terms','Early performance','Acknowledgement','Withdrawal notice','Withdrawal form'];
  const docs=[evidence.terms,...evidence.declarations,...(evidence.supplements||[])];
  return 'MAKRIVO TEST — '+orderId+'\n\n'+(facts?facts+'\n\n':'')+docs.map((d,i)=>`${titles[i]} (${d.version}; ${new Date(d.acceptedAt??d.capturedAt).toISOString()})\n${d.text}`).join('\n\n');
}
