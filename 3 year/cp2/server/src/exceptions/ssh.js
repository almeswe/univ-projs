class SshException extends Error {
  error;
  status;
  constructor(status, message) {
    super(message);
    this.error = message;
    this.status = status;
  }

  toJson() {
    return {
      status: this.status,
      error: this.error
    };
  }

  static Ssh1(message) {
    return new SshException(1, message);
  }

  static Ssh2(message) {
      return new SshException(2, message);
  }
}
  
module.exports = SshException;