import $base from "../http/index";

class UserService {
  static async getUser() {
    return $base.get("/");
  }
}

export default UserService;