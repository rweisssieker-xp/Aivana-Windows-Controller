// Browser plugin not available; regular Playwright fallback per frontend testing skill.
const { chromium } = require('playwright');
const http = require('node:http');
const fs = require('node:fs');
const path = require('node:path');
const assert = require('node:assert/strict');
(async () => {
 const root=path.resolve(__dirname,'../src/Aivana.MobileHost/wwwroot');
 let paired=false,commands=[];
 const state={Paused:false,Profiles:[{Id:'p',Name:'Arbeitsplatz',Actions:[{Id:'a',Name:'Besprechung starten',Icon:'▶',Enabled:true,Status:'Bereit'}]}],Results:[]};
 const server=http.createServer(async(req,res)=>{
  res.setHeader('Content-Type','application/json');
  if(req.url==='/api/pair'){paired=true;res.end('{}');return;}
  if(req.url?.startsWith('/api/')){
   if(!paired){res.writeHead(401);res.end('{}');return;}
   if(req.url==='/api/state'){res.end(JSON.stringify(state));return;}
   let body='';for await(const chunk of req)body+=chunk;commands.push(JSON.parse(body));res.end('{"Accepted":true}');return;
  }
  const file=path.join(root,req.url==='/'?'index.html':req.url.slice(1));
  if(!file.startsWith(root+path.sep)||!fs.existsSync(file)){res.writeHead(404);res.end();return;}
  res.setHeader('Content-Type',file.endsWith('.js')?'text/javascript':file.endsWith('.css')?'text/css':'text/html');res.end(fs.readFileSync(file));
 });
 await new Promise(r=>server.listen(0,'127.0.0.1',r));
 let browser;
 try {
  browser=await chromium.launch({headless:true,channel:process.env.MAKRIVO_BROWSER_CHANNEL || "msedge"});
  const page=await browser.newPage({viewport:{width:390,height:844}});
  await page.addInitScript(()=>{window.WebSocket=class{close(){};};});
  await page.goto(`http://127.0.0.1:${server.address().port}`);
  await page.getByLabel('Kopplungscode aus der Desktop-App').fill('123456');
  await page.getByRole('button',{name:'Koppeln',exact:true}).click();
  await page.getByRole('button',{name:'Starten',exact:true}).waitFor();
  await page.getByRole('button',{name:'Starten',exact:true}).click();
  await page.getByRole('status').filter({hasText:'Befehl angenommen'}).waitFor();
  assert.equal(commands.length,1);assert.equal(commands[0].ActionId,'a');assert.match(commands[0].CommandId,/^[0-9a-f-]{36}$/);
  assert.equal(await page.evaluate(()=>document.documentElement.scrollWidth<=innerWidth),true);
  fs.mkdirSync(path.resolve(__dirname,'../artifacts/mobile-browser'),{recursive:true});
  await page.screenshot({path:path.resolve(__dirname,'../artifacts/mobile-browser/phone.png'),fullPage:true});
  await page.setViewportSize({width:1024,height:768});await page.screenshot({path:path.resolve(__dirname,'../artifacts/mobile-browser/tablet.png'),fullPage:true});
  console.log('PASS mobile browser pairing flow, single command, UUID and phone/tablet layout (mock API; real HTTPS tested separately)');
 } finally {await browser?.close();await new Promise(r=>server.close(r));}
})().catch(e=>{console.error(e);process.exitCode=1;});
