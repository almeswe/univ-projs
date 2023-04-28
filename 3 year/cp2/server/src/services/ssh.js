const ssh2 = require("ssh2");
const SshException = require("../exceptions/ssh");

class SshService {
  pool;
  constructor() {
    this.pool = {};
  }

  connect(ws, id, host, pass) {
    this.disconnect(id);
    const conn = new ssh2.Client();
    const [user, addr] = host.split("@");
    if (!user || !addr) {
      throw SshException.Ssh1("Bad hostname");
    }
    conn.on("error", (error) => {
      ws.send(JSON.stringify({
        "status": 1,
        "error": `${error}`
      }));
    });
    conn.on("ready", () => {
      ws.send(JSON.stringify({
        "status": 0
      }));
    });
    conn.connect({
      host: addr,
      port: 22,
      username: user,
      password: pass
    });
    this.pool[`${id}`] = conn;
  }

  execute(ws, id, command) {
    const conn = this.pool[`${id}`];
    if (!conn) {
      throw SshException.Ssh1(`Does not connected to ssh server.`);
    }
    conn.exec(command, (error, stream) => {
      if (error) {
        throw SshException.Ssh1(`${error}`);
      }
      stream.on("close", () => {
        ws.send(JSON.stringify({
          status: 0,
        }));
      });
      stream.stdout.on("data", (data) => {
        ws.send(JSON.stringify({
          data: `${data}`,
        }));
      });
      stream.stderr.on("data", (data) => {
        ws.send(JSON.stringify({
          data: `${data}`,
        }));
      });
    });
  }

  disconnect(ws, id) {
    try {
      const conn = this.pool[`${id}`];
      if (conn) {
        conn.end();
        this.pool[`${id}`] = undefined;
      }
    }
    finally { }
  }
}

module.exports = SshService;