const sock = require("ws");
const cors = require("cors");
const express = require("express");
const cookie = require("cookie-parser");

const authRoutes = require("./src/routes/auth");
const userRoutes = require("./src/routes/user");

const exceptionMiddleware = require("./src/middlewares/exception");

const env = require("dotenv");
const app = express();
env.config();
app.use(express.json());
app.use(cookie());
app.use(cors({
  credentials: true,
  origin: process.env.CLIENT_URL
}));

app.use(authRoutes);
app.use(userRoutes);
app.use(exceptionMiddleware);

const wssSetRoutes = require("./src/routes/wss");
const wss = new sock.WebSocketServer({
  port: process.env.SOCK_SERVER_PORT,
});
wssSetRoutes(wss);
/*
wss.on("connection", (ws) => {
  //SshService.connect(user, host, pass);
  console.log('New client connected!');
  ws.on('close', () => console.log('Client has disconnected!'));
  ws.onerror = () => {
    console.log('websocket error');
  }
  ws.on('message', data => {
    try {
      const req = JSON.parse(data);

    }
    catch (e) {
      console.log(`${e}`);
    }
  });
});*/

app.listen(process.env.REST_SERVER_PORT);