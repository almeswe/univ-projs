const env = require("dotenv");
const jwt = require("jsonwebtoken");

env.config();

class TokenService {
    static makeRefreshToken(payload) {
        return jwt.sign(
            payload, 
            process.env.JWT_REFRESH_TOKEN,
            { expiresIn: "15d" }
        );
    }

    static makeAccessToken(payload) {
        return jwt.sign(
            payload, 
            process.env.JWT_ACCESS_TOKEN,
            { expiresIn: "15m" }
        );
    }

    static makeTokens(payload) {
        return {
            accessToken: TokenService.makeAccessToken(payload),
            refreshToken: TokenService.makeRefreshToken(payload),
        };
    }

    static verifyAccessToken(accessToken) {
        try {
            return jwt.verify(
                accessToken, 
                process.env.JWT_ACCESS_TOKEN
            );
        }
        catch (e) {
            return undefined;
        }
    }

    static verifyRefreshToken(refreshToken) {
        try {
            return jwt.verify(
                refreshToken, 
                process.env.JWT_REFRESH_TOKEN
            );
        }
        catch (e) {
            return undefined;
        }
    }
}

module.exports = TokenService;