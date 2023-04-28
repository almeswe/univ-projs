import { useRef } from "react";
import { useSsh } from "../../context/SshContext";

import HomeConnectForm from "../../components/ConnectForm";
import HomeTerminalForm from "../../components/TerminalForm";

function AppHome() {
  const { 
    sshData,
    sshError, 
    sshWaiting, 
    sshConnected, 
    sshConnect,
    sshExecute,
    sshDisconnect 
  } = useSsh();

  const hostRef = useRef("");
  const passRef = useRef("");
  const commandRef = useRef("");

  function onConnect(e) {
    e.preventDefault();
    sshConnect({
      host: hostRef.current.value,
      pass: passRef.current.value
    });
  }

  function onDisconnect(e) {
    sshDisconnect();
  }

  function onExecute(e) {
    if (e.key === "Enter") {
      sshExecute({
        command: commandRef.current.value
      });
    }
  }

  if (sshConnected) {
    return (
      <HomeTerminalForm
        data={sshData}
        command={commandRef}
        submitExecute={onExecute}
        submitDisconnect={onDisconnect}
      ></HomeTerminalForm>
    );
  }
  return (
    <HomeConnectForm
      host={hostRef}
      pass={passRef}
      error={sshError}
      loading={sshWaiting}
      submitForm={onConnect}
    ></HomeConnectForm>
  );
}

export default AppHome;