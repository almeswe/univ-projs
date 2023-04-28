import axios from "axios";
import AuthService from "../services/AuthService";

export const API_URL = "http://localhost:8080";
export const WSS_URL = "ws://localhost:8081";

const $base =  axios.create({
  withCredentials: true,
  baseURL: API_URL
});

$base.interceptors.request.use((config) =>{
  config.headers["Content-Type"] = "application/json";
  config.headers["Access-Control-Max-Age"] = "600";
  const token = localStorage.getItem("token");
  if (token) {
    config.headers["Authorization"] = `Bearer ${token}`;
  }
  return config;
});

$base.interceptors.response.use(
  (config) => {
    return config;
  },
  async (error) => {
    console.log("refresh");
    const req = error.config;
    if (error.response.status === 401 && error.config && error.config._isRetry) {
      const res = await AuthService.refresh();
      localStorage.setItem("token", res.data.accessToken);
      return $base.request(req);
    }
    throw error;
  }
);

export default $base;