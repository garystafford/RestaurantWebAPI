"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const chai_1 = require("chai");
require("mocha");
const app_1 = require("../../../app");
const supertest = require("supertest");
describe('sample route controller', () => {
    it('should return pong', (done) => {
        supertest(app_1.api)
            .get('/api/ping')
            .end((err, response) => {
            if (err) {
                done(err);
            }
            else {
                chai_1.expect(response.status).to.equal(200);
                chai_1.expect(response.body).to.equal('pong');
                done();
            }
        });
    });
});
//# sourceMappingURL=SampleRouteController.spec.js.map