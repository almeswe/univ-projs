const { Client } = require('ssh2');

const readline = require('readline');
const stdin = readline.createInterface({
  input: process.stdin,
  output: process.stdout,
  terminal: false
});

function receiveResponse(err, stream) {
  if (err) {
    throw err;
  }
  stream.stdout.on("data", (data) => {
    console.log(`${data}`);
  });
  stream.stderr.on("data", (data) => {
    console.log(`${data}`);
  });
}

const conn = new Client();
conn.on("error", (err) => {
  console.log("error");
  console.log(JSON.stringify(err));
});
conn.connect({
  host: '192.168.100.97',
  port: 22,
  username: 'almeswe',
  password: "__kincaid1"
});
stdin.on("line", (line) => {
  if (line === "exit") {
    conn.end();
    process.exit(0);
  }
  conn.exec(`${line}`, receiveResponse);
});