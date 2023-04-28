const UserService = require("../services/auth");
const ApiException = require("../exceptions/api");

const JWT_EXPIRATION_15M = 15 * 60 * 1000;
const JWT_EXPIRATION_15D = 15 * 24 * 3600 * 1000;

class AuthController {
  async signup(req, res, next) {
    const body = req.body;
    try {
      if (!body.email || !body.username || !body.password) {
        throw ApiException.Api400("You must specify email, username & password.");
      }
      await UserService.signup({
        email: body.email,
        username: body.username,
        password: body.password 
      });
      return res.status(200).json("");
    }
    catch (e) {
      next(e);
    }
  }

  async signin(req, res, next) {
    const body = req.body;
    try {
      if (!body.email || !body.password) {
        throw ApiException.Api400("You must specify email & password.");
      }
      const data = await UserService.signin({
        email: body.email,
        password: body.password 
      });
      res.cookie('refreshToken', data.refreshToken, {
        maxAge: JWT_EXPIRATION_15D, 
        httpOnly: true
      });
      return res.status(200).json(data);
    }
    catch (e) {
      next(e);
    }
  }

  async refresh(req, res, next) {
    try {
      const { refreshToken } = req.cookies;
      if (!refreshToken) {
        throw ApiException.Api401();
      }
      const data = await UserService.refresh({
        refreshToken: refreshToken 
      });
      res.cookie('refreshToken', data.refreshToken, {
        maxAge: JWT_EXPIRATION_15D,
        httpOnly: true
      });
      return res.status(200).json(data);
    }
    catch (e) {
      next(e);
    }
  }

  async signout(req, res, next) {
    try {
      const { refreshToken } = req.cookies;
      if (!refreshToken) {
        throw ApiException.Api401();
      }
      const data = await UserService.signout({
        refreshToken: refreshToken 
      });
      res.clearCookie('refreshToken');
      return res.status(200).json("");
    }
    catch (e) {
      next(e);
    }
  }
}

module.exports = AuthController;