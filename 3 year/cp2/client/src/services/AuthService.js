import axios from "axios";
import $base from "../http/index";
import { API_URL } from "../http/index";

class AuthService {
  static rethrow(e) {
    const data = e.response.data;
    var error = data.error;
    if (!error) {
      error = "occured during request.";
    }
    throw new Error(error);
  }

  static async signup(creds) {
    return $base.post("/signup", {
      email: creds.email,
      username: creds.username,
      password: creds.password
    })
    .catch((e) => {
      AuthService.rethrow(e);
    });
  }

  static async signin(creds) {
    return $base.post("/signin", {
      email: creds.email,
      password: creds.password
    })
    .catch((e) => {
      AuthService.rethrow(e);
    });
  };

  static async refresh() {
    return await axios.create({
      withCredentials: true,
      baseURL: API_URL
    })
    .post("/refresh")
    .catch((e) => {
      AuthService.rethrow(e);
    });
  }

  static async signout() {
    return await axios.create({
      withCredentials: true,
      baseURL: API_URL
    })
    .post("/signout")
    .catch((e) => {
      AuthService.rethrow(e);
    });
  }
}

export default AuthService;