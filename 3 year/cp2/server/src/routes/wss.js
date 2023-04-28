const WssController = require("../controllers/wss");

function wssSetRoutes(wss) {
  wss.on("connection", WssController.connection);
}

module.exports = wssSetRoutes;