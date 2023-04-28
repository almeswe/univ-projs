const SshException = require("../exceptions/ssh");
const SshService = require("../services/ssh");
const sshService = new SshService();

class WssController {
  static sendException(ws, e) {
    if (e instanceof SshException) {
      ws.send(JSON.stringify(e.toJson()));
    }
    else {
      ws.send(JSON.stringify({
        status: 3,
        error: `Internal error occured: ${e}`
      }));
    }
  }

  static onConnection(ws) {
    console.log("New connection");
    ws.on("message", (data) => { 
      WssController.onMessage(ws, data); 
    });
  }

  static onMessage(ws, data) {
    try {
      const req = JSON.parse(data);
      if (!req.event) {
        throw SshException.Ssh1("Event type is not specified.");
      }
      const event = wsEventMap[req.event];
      if (!event) {
        throw SshException.Ssh1(`Unknown event met: ${req.event}`);
      }
      wsEventMap[req.event](ws, req);
    }
    catch (e) {
      WssController.sendException(ws, e);
    }
  }

  static onWsConnect(ws, req) {
    try {
      if (!req.host || !req.pass) {
        throw SshException.Ssh1("You must specify host and password.");
      }
      sshService.connect(ws, 0, req.host, req.pass);
    }
    catch (e) {
      WssController.sendException(ws, e);
    }
  }

  static onWsExecute(ws, req) {
    try {
      if (!req.command) {
        throw new SshException.Ssh("You must specify command to execute.");
      }
      sshService.execute(ws, 0, req.command);
    }
    catch (e) {
      WssController.sendException(ws, e);
    }
  }

  static onWsDisconnect(ws, req) {
    sshService.disconnect(ws, 0);
    ws.send(JSON.stringify({
      status: 0,
    }));
  }
}

const wsEventMap = {
  "connect":    WssController.onWsConnect,
  "execute":    WssController.onWsExecute,
  "disconnect": WssController.onWsDisconnect,
};

module.exports = WssController;