class ApiException extends Error {
  status;
  constructor(status, message) {
    super(message);
    this.status = status;
  }

  static Api401() {
    return new ApiException(401, "Unauthorized access denied.");
  }

  static Api400(message) {
    return new ApiException(400, message);
  }
}

module.exports = ApiException;