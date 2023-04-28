import { createContext } from "react";
import { useState, useContext } from "react";
import { useNavigate } from "react-router-dom";

import AuthService from "../services/AuthService";

const AuthContext = createContext();

export function useAuth() {
  return useContext(AuthContext);
}

export function AuthProvider(params) {
  const redirect = useNavigate();
  const [user, setUser] = useState();

  function setUserAndToken(res) {
    if (res.data && res.data.accessToken) {
      setUser(res.data.user);
      localStorage.setItem("token", res.data.accessToken);
    }
  }

  async function signup(creds) {
    await AuthService.signup(creds);
    redirect("/signin");
  }

  async function signin(creds) {
    const res = await AuthService.signin(creds);
    setUserAndToken(res);
    redirect("/");
  }

  async function refresh() {
    const res = await AuthService.refresh();
    setUserAndToken(res);
    redirect("/");
  }

  async function signout() {
    await AuthService.signout();
    redirect("/signin");
  }

  const value = {
    user,
    signup,
    signin,
    refresh,
    signout
  };
  return (
    <AuthContext.Provider value={value}>
      {params.child}
    </AuthContext.Provider>
  );
}