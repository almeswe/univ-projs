const cors = require("cors");
const express = require("express");
const cookie = require("cookie-parser");

const authRoutes = require("./src/routes/auth");

const exceptionMiddleware = require("./src/middlewares/exception");

const app = express();
app.use(express.json());
app.use(cookie());
app.use(cors());

app.use(authRoutes);
app.use(exceptionMiddleware);

app.listen(8080);