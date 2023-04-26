const db = require("../config/db");
const bcrypt = require("bcryptjs");
const User = require("../models/user");

class UserService {
    static async exists(email) {
        const query = `
            SELECT * FROM User WHERE 
                u_email=\'${email}\';
        `;
        const [res, _] = await db.execute(query);
        return res.length != 0;
    }

    static async create(creds) {
        const { email, username, password } = creds;
        const hash = await bcrypt.hash(password, 7);
        const query = `
            INSERT INTO User (
                u_email,
                u_name,
                u_password
            )
            VALUES (
                \'${email}\',
                \'${username}\',
                \'${hash}\'
            );
        `;
        await db.execute(query);
    }

    static async findOne(attr, value) {
        const query = `
            SELECT * FROM User WHERE 
                ${attr}=${value};
        `;
        const [res, _] = await db.execute(query);
        if (res.length == 0) {
            return undefined;
        }
        return new User(res[0]);
    }

    static async findById(id) {
        return await UserService.findOne("u_id", id);
    }

    static async findByEmail(email) {
        return await UserService.findOne("u_email", `\'${email}\'`);
    }

    static async updateOne(id, attr, value) {
        const query = `
            UPDATE User SET ${attr}=${value} 
                WHERE u_id=${id}; 
        `;
        console.log(query);
        await db.execute(query);
    }

    static async updateToken(id, token) {
        await UserService.updateOne(id, "u_reftok", `\'${token}\'`);
    }
}

module.exports = UserService;