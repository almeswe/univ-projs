const SshService = require("../services/ssh");
const TokenService = require("../services/token");
const sshService = new SshService();

const SshException = require("../exceptions/ssh");

class WssController {
  static protect(method, ws, req) {
    if (!req.token) {
      throw SshException.Ssh2("Token was not specified.");
    }
    const user = TokenService.verifyAccessToken(req.token);
    if (!user) {
      throw SshException.Ssh2("Access denied.");
    }
    //req.user = user;
    req.id = user.id;
    method(ws, req);
  }

  static exception(ws, e) {
    if (e instanceof SshException) {
      ws.send(JSON.stringify(e.toJson()));
    }
    else {
      ws.send(JSON.stringify({
        status: 3,
        error: `Internal server error occured: ${e}`
      }));
    }
  }

  static connection(ws) {
    console.log("new connection.");
    ws.on("message", (data) => { 
      WssController.message(ws, data); 
    });
  }

  static message(ws, data) {
    try {
      const req = JSON.parse(data);
      if (!req.event) {
        throw SshException.Ssh1("Event type is not specified.");
      }
      const event = wsEventMap[req.event];
      if (!event) {
        throw SshException.Ssh1(`Unknown event met: ${req.event}`);
      }
      WssController.protect(wsEventMap[req.event], ws, req);
    }
    catch (e) {
      WssController.exception(ws, e);
    }
  }

  static connect(ws, req) {
    try {
      if (!req.host || !req.pass) {
        throw SshException.Ssh1("You must specify host and password.");
      }
      sshService.connect(ws, req.id, req.host, req.pass);
    }
    catch (e) {
      WssController.exception(ws, e);
    }
  }

  static execute(ws, req) {
    try {
      if (!req.command) {
        throw SshException.Ssh1("You must specify command to execute.");
      }
      sshService.execute(ws, req.id, req.command);
    }
    catch (e) {
      WssController.exception(ws, e);
    }
  }

  static disconnect(ws, req) {
    sshService.disconnect(ws, req.id);
    ws.send(JSON.stringify({
      status: 0,
    }));
  }
}

const wsEventMap = {
  "connect":    WssController.connect,
  "execute":    WssController.execute,
  "disconnect": WssController.disconnect,
};

module.exports = WssController;