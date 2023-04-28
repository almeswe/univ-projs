const ApiException = require("../exceptions/api");
//const SshException = require("../exceptions/ssh");

module.exports = function (err, req, res, next) {
  if (err instanceof ApiException) {
    return res.status(err.status).json({
      error: `${err.message}`
    });
  }
  return res.status(500).json({
    error: `Internal server exception. ${err.message}`
  });
};