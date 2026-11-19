import {test} from 'node:test';
import assert from 'node:assert/strict';
import {generateKeyPairSync} from 'node:crypto';
import {mkdtempSync,writeFileSync,rmSync,realpathSync} from 'node:fs';
import {tmpdir} from 'node:os';
import {join,dirname,basename} from 'node:path';
import {fileURLToPath} from 'node:url';
import {spawnSync} from 'node:child_process';

const script=fileURLToPath(new URL('./verify-license-keypair.mjs',import.meta.url));
const strong=generateKeyPairSync('rsa',{modulusLength:3072});
const other=generateKeyPairSync('rsa',{modulusLength:3072});
const weak=generateKeyPairSync('rsa',{modulusLength:2048});
const privatePem=k=>k.privateKey.export({type:'pkcs8',format:'pem'});
const publicPem=k=>k.publicKey.export({type:'spki',format:'pem'});

function invoke(privateValue,ring,keyId='test-key'){
  const folder=mkdtempSync(join(tmpdir(),'makrivo-keycheck-test-'));
  try{
    const file=join(folder,'public.json');writeFileSync(file,JSON.stringify(ring));
    const child=spawnSync(process.execPath,[script,'--key-id',keyId,'--public-ring',file],{input:privateValue,encoding:'utf8',windowsHide:true,timeout:30000});
    assert.equal(child.error,undefined);assert.ok(!`${child.stdout}${child.stderr}`.includes(privateValue));
    assert.ok(!`${child.stdout}${child.stderr}`.includes('BEGIN PRIVATE KEY'));
    return child;
  }finally{
    const path=realpathSync(folder);
    if(dirname(path)!==realpathSync(tmpdir())||!basename(path).startsWith('makrivo-keycheck-test-'))throw new Error('Refusing unsafe test cleanup');
    rmSync(path,{recursive:true});
  }
}

test('a valid 3072-bit signer matches the selected public key and reports only a fingerprint',()=>{
  const result=invoke(privatePem(strong),{'test-key':publicPem(strong)});
  assert.equal(result.status,0);assert.match(result.stdout,/SHA-256: [0-9a-f]{64}/);assert.equal(result.stderr,'');
});

test('another key, a missing ID and a weak key are refused without revealing private input',()=>{
  for(const [key,ring,id] of [[privatePem(strong),{'test-key':publicPem(other)},'test-key'],[privatePem(strong),{'another':publicPem(strong)},'test-key'],[privatePem(weak),{'test-key':publicPem(weak)},'test-key']]){
    const result=invoke(key,ring,id);assert.notEqual(result.status,0);assert.doesNotMatch(result.stdout,/SHA-256:/);
  }
});

test('refuses malformed private keys and private material in the public ring',()=>{
  for(const [key,ring] of [['not a key',{'test-key':publicPem(strong)}],[privatePem(strong)+privatePem(other),{'test-key':publicPem(strong)}],[privatePem(strong),{'test-key':privatePem(strong)}]]){
    const result=invoke(key,ring);assert.notEqual(result.status,0);assert.doesNotMatch(result.stdout,/SHA-256:/);
  }
});
