const ApiException = require("../exceptions/api");

module.exports = function (err, req, res, next) {
    console.log(err.message);
    if (err instanceof ApiException) {
        return res.status(err.status).json({
            error: `${err.message}`
        });
    }
    return res.status(500).json({
        error: `Internal server exception. ${err.message}`
    });
};