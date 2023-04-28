class User {
	id;
	email;
	username;
	password;
	refreshToken;

	constructor(row) {
		this.id = row.u_id;
		this.email = row.u_email;
		this.username = row.u_name;
		this.password = row.u_password;
		this.refreshToken = row.u_reftok;
	}
}

module.exports = User;