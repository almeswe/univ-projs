class SshService {
  static event;
  static buf;
  static connected = false;

  static response(res) {
    if (res.status && res.status !== 0) {
      throw res.error;
    }
    if (SshService.event === "connect") {
      SshService.connected = true;
    }
    if (SshService.event === "disconnect") {
      SshService.connected = false;
    }
    if (SshService.event === "execute") {
      SshService.connected = true;
      if (!res.status && res.data) {
        SshService.buf += res.data;
      }
    }
  }

  static transfer(ws, req) {
    console.log(`transfer: ${req}`)
    ws.send(JSON.stringify({
      ...req,
      event: SshService.event,
      token: localStorage.getItem("token")
    }));
  }

  static connect(ws, creds) {
    const { host, pass } = creds;
    SshService.event = "connect";
    SshService.transfer(ws, {
      host: host,
      pass: pass
    });
  }

  static execute(ws, creds) {
    SshService.buf = "";
    const { command } = creds;
    SshService.event = "execute";
    SshService.transfer(ws, {
      command: command,
    });
  }

  static disconnect(ws) {
    SshService.event = "disconnect";
    SshService.transfer(ws, {
    });
  }
}

export default SshService;