const ApiException = require("../exceptions/api");
const TokenService = require("../services/token");

module.exports = function (req, res, next) {
    try {
        const header = req.headers.authorization;
        if (!header) {
            throw ApiException.Api401();
        }
        const token = header.split(' ')[1];
        if (!token) {
            throw ApiException.Api401();
        }
        const user = TokenService.verifyAccessToken(token); 
        if (!user) {
            throw ApiException.Api401();
        }
        res.user = user;
        next();
    }
    catch (e) {
        next(ApiException.Api401());
    }
};