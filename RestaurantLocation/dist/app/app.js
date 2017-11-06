"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const fs = require("fs");
const restify = require("restify");
const config_1 = require("./config/config");
const logger_1 = require("./services/logger");
exports.api = restify.createServer({
    name: config_1.settings.name
});
exports.api.pre(restify.pre.sanitizePath());
exports.api.use(restify.plugins.acceptParser(exports.api.acceptable));
exports.api.use(restify.plugins.bodyParser());
exports.api.use(restify.plugins.queryParser());
exports.api.use(restify.plugins.authorizationParser());
exports.api.use(restify.plugins.fullResponse());
fs.readdirSync(__dirname + '/routes').forEach((routeConfig) => {
    if (routeConfig.substr(-3) === '.js') {
        const route = require(__dirname + '/routes/' + routeConfig);
        route.routes(exports.api);
    }
});
exports.api.listen(config_1.settings.port, () => {
    logger_1.logger.info(`INFO: ${config_1.settings.name} is running at ${exports.api.url}`);
});
//# sourceMappingURL=app.js.map