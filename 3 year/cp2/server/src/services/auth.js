const bcrypt = require("bcryptjs");
const UserService = require("../services/user"); 
const TokenService = require("../services/token");
const ApiException = require("../exceptions/api");

class AuthService {
  static async renewTokens(id) {
    const tokens = TokenService.generate({ id: id });
    await UserService.updateToken(id, tokens.refreshToken);
    return tokens;
  }

  static async signup(creds) {
    const { email } = creds;
    if (await UserService.exists(email)) {
      throw ApiException.Api400("User with this email is already registered.");
    }
    await UserService.create(creds);
    const user = await UserService.findByEmail(email);
    if (!user) {
      throw ApiException.Api400("User was not registered for some reason.");
    }
    return user;
  }

  static async signin(creds) {
    const { email, password } = creds;
    const user = await UserService.findByEmail(email);
    if (!user) {
      throw ApiException.Api400("You specified incorrect email.");
    }
    if (!await bcrypt.compare(password, user.password)) {
      throw ApiException.Api400("You specified incorrect password.");
    }
    const tokens = await AuthService.renewTokens(user.id);
    return {
      ...tokens,
      user: user
    };
  }

  static async refresh(creds) {
    const { refreshToken } = creds;
    const verified = TokenService.verifyRefreshToken(refreshToken);
    if (!verified) {
      throw ApiException.Api401();
    }
    const user = await UserService.findByToken(refreshToken);
    if (!user) {
      throw ApiException.Api401();
    }
    const tokens = await AuthService.renewTokens(user.id);
    return {
      ...tokens,
      user: user
    };
  }
}

module.exports = AuthService;