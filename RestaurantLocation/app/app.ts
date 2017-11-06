import * as fs from 'fs';
import * as restify from 'restify';
import { settings } from './config/config';
import { logger } from './services/logger';

export let api = restify.createServer({
    name: settings.name
});

//restify.CORS.ALLOW_HEADERS.push('authorization');
//api.use(restify.CORS());
api.pre(restify.pre.sanitizePath());
api.use(restify.plugins.acceptParser(api.acceptable));
api.use(restify.plugins.bodyParser());
api.use(restify.plugins.queryParser());
api.use(restify.plugins.authorizationParser());
api.use(restify.plugins.fullResponse());


fs.readdirSync(__dirname + '/routes').forEach((routeConfig: string) => {
    if (routeConfig.substr(-3) === '.js') {
        const route = require(__dirname + '/routes/' + routeConfig);
        route.routes(api);
    }
});

api.listen(settings.port,
    () => {
        logger.info(`INFO: ${settings.name} is running at ${api.url}`);
    });