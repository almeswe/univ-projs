const WssController = require("../controllers/wss");

function wssSetRoutes(wss) {
  wss.on("connection", WssController.onConnection);
}

module.exports = wssSetRoutes;