"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const bunyan = require("bunyan");
const stream = require("stream");
let infoStream = new stream.Writable();
infoStream.writable = true;
infoStream.write = (info) => {
    console.log(JSON.parse(info).msg);
    return true;
};
exports.logger = bunyan.createLogger({
    name: 'myapp',
    streams: [
        {
            level: 'info',
            stream: infoStream
        },
        {
            level: 'error',
            path: `error.log`
        }
    ]
});
//# sourceMappingURL=logger.js.map