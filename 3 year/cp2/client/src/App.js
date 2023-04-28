import { Route, Routes } from "react-router-dom";

import { useEffect } from "react";
import { useAuth } from "./context/AuthContext"; 

import AppHome from "./pages/AppHome/index";
import AppSignUp from "./pages/AppSignUp/index";
import AppSignIn from "./pages/AppSignIn/index";

import { SshProvider } from "./context/SshContext";
import PrivateRoute from "./components/PrivateRoute";

import "bootstrap/dist/css/bootstrap.min.css";

function App() {
  const { refresh } = useAuth();

  useEffect(() => {
    refresh();
  }, []);

  return (
    <Routes>
      <Route exact path="/" element={<PrivateRoute/>}>
        <Route exact path="/" element={
          <SshProvider child={<AppHome/>}>
          </SshProvider>
        }/>
      </Route>
      <Route 
        exact path={"/signup"} 
        element={<AppSignUp/>}>
      </Route>
      <Route 
        exact path={"/signin"} 
        element={<AppSignIn/>}>
      </Route>
    </Routes>
  );
};

export default App;