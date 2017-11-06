import * as restify from 'restify';
import sampleRouteController from '../controllers/SampleRouteController'

function sampleRoute(api: restify.Server) {
    const routeCtrl = new sampleRouteController();
    api.get('/api/ping', routeCtrl.get);
}

module.exports.routes = sampleRoute;