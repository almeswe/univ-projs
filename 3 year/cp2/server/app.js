const sock = require("ws");
const cors = require("cors");
const express = require("express");
const cookie = require("cookie-parser");

const authRoutes = require("./src/routes/auth");

const exceptionMiddleware = require("./src/middlewares/exception");

const env = require("dotenv");
const app = express();
env.config();
app.use(express.json());
app.use(cookie());
app.use(cors({
  credentials: true,
  origin: [
    process.env.CLIENT_URL
  ]
}));

app.use(authRoutes);
app.use(exceptionMiddleware);

const wssSetRoutes = require("./src/routes/wss");
const wss = new sock.WebSocketServer({
  port: process.env.SOCK_SERVER_PORT,
});
wssSetRoutes(wss);
app.listen(process.env.REST_SERVER_PORT);