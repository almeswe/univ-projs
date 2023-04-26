const bcrypt = require("bcryptjs");
const UserService = require("../services/user"); 
const TokenService = require("../services/token");
const ApiException = require("../exceptions/api");

class AuthService {
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
        if (!bcrypt.compare(password, user.password)) {
            throw ApiException.Api400("You specified incorrect password.");
        }
        const tokens = TokenService.makeTokens({ id: user.id });
        UserService.updateToken(user.id, tokens.refreshToken);
        return {
            ...tokens,
            user: user
        };
    }
}

module.exports = AuthService;