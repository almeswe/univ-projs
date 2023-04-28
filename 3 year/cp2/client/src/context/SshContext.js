import { createContext } from "react";
import { useState, useEffect, useContext } from "react";

import { WSS_URL } from "../http";

import SshService from "../services/SshService";

const SshContext = createContext();

export function useSsh() {
  return useContext(SshContext);
}

export function SshProvider(params) {
  const [ws, setWs] = useState();

  const [sshError, setSshError] = useState("");
  const [sshData, setData] = useState("");
  const [sshWaiting, setWaiting] = useState(false);
  const [sshConnected, setConnected] = useState(false);

  function connect() {
    const ws = new WebSocket(WSS_URL, ["json"]);
    ws.addEventListener("message", (event) => {
      const res = JSON.parse(event.data);
      try {
        setSshError("");
        SshService.response(res);
        setData(SshService.buf);
        setConnected(SshService.connected);
      }
      catch (e) {
        setConnected(false);
        setSshError(`${e}`);
      }
      finally {
        setWaiting(false);
      }
    });
    setWs(ws);
  }

  function sshConnect(creds) {
    setWaiting(true);
    SshService.connect(ws, creds);
  }

  function sshExecute(creds) {
    setWaiting(true);
    SshService.execute(ws, creds);
  }

  function sshDisconnect() {
    setWaiting(true);
    SshService.disconnect(ws);
  }

  useEffect(() => connect(), []);

  const value = {
    sshData,
    sshError,
    sshWaiting,
    sshConnected,
    sshConnect,
    sshExecute,
    sshDisconnect
  };
  return (
    <SshContext.Provider value={value}>
      {params.child}
    </SshContext.Provider>
  );
}