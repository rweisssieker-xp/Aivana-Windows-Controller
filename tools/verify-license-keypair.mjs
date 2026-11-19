// Reads one PKCS#8 PEM from standard input and checks an existing public ring.
// It does not generate, write, install, back up or escrow any signing key.
import {createPrivateKey,createPublicKey,createHash,randomBytes,sign,verify,constants} from 'node:crypto';
import {readFileSync,statSync} from 'node:fs';

const validId=value=>typeof value==='string'&&/^[A-Za-z0-9._-]{1,64}$/.test(value);
const privateEnvelope=/^-----BEGIN PRIVATE KEY-----\s+[A-Za-z0-9+/=\s]+-----END PRIVATE KEY-----\s*$/;
const publicEnvelope=/^-----BEGIN (?:RSA )?PUBLIC KEY-----\s+[A-Za-z0-9+/=\s]+-----END (?:RSA )?PUBLIC KEY-----\s*$/;

async function readPrivateInput(){
  let size=0;const parts=[];
  for await(const part of process.stdin){
    size+=part.length;if(size>16384)throw new Error('invalid input');parts.push(part);
  }
  return Buffer.concat(parts).toString('utf8');
}

function parseArguments(args){
  if(args.length!==4||args[0]!=='--key-id'||args[2]!=='--public-ring'||!validId(args[1])||!args[3])throw new Error('invalid configuration');
  return {id:args[1],path:args[3]};
}

async function main(){
  const {id,path}=parseArguments(process.argv.slice(2));
  if(statSync(path).size>1024*1024)throw new Error('invalid public ring');
  const ring=JSON.parse(readFileSync(path,'utf8'));
  if(!ring||typeof ring!=='object'||Array.isArray(ring)||!Object.hasOwn(ring,id)||!Object.keys(ring).length)throw new Error('invalid public ring');
  let selected;
  for(const [keyId,pem] of Object.entries(ring)){
    if(!validId(keyId)||typeof pem!=='string'||pem.length>16384||/PRIVATE/i.test(pem)||!publicEnvelope.test(pem))throw new Error('invalid public key');
    const publicKey=createPublicKey(pem);
    if(publicKey.asymmetricKeyType!=='rsa'||publicKey.asymmetricKeyDetails?.modulusLength<3072)throw new Error('invalid public key');
    if(keyId===id)selected=publicKey;
  }
  const value=await readPrivateInput();
  if(!privateEnvelope.test(value))throw new Error('invalid private key');
  const privateKey=createPrivateKey({key:value,format:'pem',type:'pkcs8'});
  if(privateKey.asymmetricKeyType!=='rsa'||privateKey.asymmetricKeyDetails?.modulusLength<3072)throw new Error('invalid private key');
  const supplied=createPublicKey(privateKey).export({type:'spki',format:'der'});
  const expected=selected.export({type:'spki',format:'der'});
  if(!supplied.equals(expected))throw new Error('different keys');
  const challenge=randomBytes(32);
  const signature=sign('sha256',challenge,{key:privateKey,padding:constants.RSA_PKCS1_PADDING});
  if(!verify('sha256',challenge,{key:selected,padding:constants.RSA_PKCS1_PADDING},signature))throw new Error('invalid signature');
  process.stdout.write(`Public SPKI SHA-256: ${createHash('sha256').update(expected).digest('hex')}\n`);
}

try { await main(); }
catch { process.stderr.write('Key verification failed; inspect the protected input and public ring.\n');process.exitCode=1; }
