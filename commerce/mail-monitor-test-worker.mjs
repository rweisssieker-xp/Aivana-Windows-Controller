// Isolated Queue consumer. Do not add a public route or use for live customers.
import {consumeTestMailEvents} from './mail-events.mjs';
export default {async queue(batch,env){await consumeTestMailEvents(batch,env);}};
