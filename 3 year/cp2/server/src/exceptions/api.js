class ApiException extends Error {
    status;
    constructor(status, message) {
        super(message);
        this.status = status;
    }

    static Api401(message) {
        return new ApiException(401, message);
    }

    static Api400(message) {
        return new ApiException(400, message);
    }
}

module.exports = ApiException;