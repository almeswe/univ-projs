require('dotenv').config()
const { v4: uuidv4 } = require('uuid');
const express = require("express");
const app = express();
const jwt = require('jsonwebtoken');
const cors = require("cors");

var users = [
  {
    "id": uuidv4(),
    "email": "almeswe",
    "psswd": "password",
    "items": [
      {
        "id": uuidv4(),
        "text": "user 1 item 1",
        "isChecked": false
      },
      {
        "id": uuidv4(),
        "text": "user 1 item 2",
        "isChecked": true
      },
    ]
  },
  {
    "id": uuidv4(),
    "email": "max",
    "psswd": "dush",
    "items": [
      {
        "id": uuidv4(),
        "text": "user 2 item 1",
        "isChecked": false
      },
      {
        "id": uuidv4(),
        "text": "user 2 item 2",
        "isChecked": true
      },
    ]
  }
];

app.use(cors());
app.use(express.json())

function authVerifyHandler(req, res, err, user, next) {
  if (err || !user) {
    return res.sendStatus(403);
  }
  let date = Date.parse(user.adate);
  let diff = (Date.now() - date) / 1000;
  if (diff > 20) {
    return res.sendStatus(403);
  }
  let copy = [...users].filter(suser => 
    user.email == suser.email && user.psswd == suser.psswd);
  if (copy.length != 1) {
    return res.sendStatus(403);
  }
  req.user = copy[0];
  next();
}

function authVerify(req, res, next) {
  const authHeader = req.headers['authorization'];
  if (authHeader) {
    const token = authHeader.split(' ')[1]
    const secret = process.env.ACCESS_TOKEN_SECRET;
    if (token) {
      jwt.verify(token, secret, (err, user) => 
        authVerifyHandler(req, res, err, user, next));
    }
  }
}

app.post("/signup", function(req, res) {
  const email = req.body.email;
  const psswd = req.body.psswd;
  if (!email || !psswd) {
    return res.sendStatus(401);
  }
  let copy = [...users].filter(user => 
    user.email == email && user.psswd == psswd);
  if (copy.length > 0) {
    return res.sendStatus(401);
  }
  users.push({
    "id": uuidv4(),
    "email": email,
    "psswd": psswd,
    "items": []
  });
  return res.sendStatus(200);
});

app.post("/signin", function(req, res) {
  const email = req.body.email;
  const psswd = req.body.psswd;
  if (!email || !psswd) {
    return res.sendStatus(401);
  }
  const user = {
    email: email,
    psswd: psswd,
    adate: new Date(Date.now()).toString()
  };
  let copy = [...users].filter(user => 
    user.email == email && user.psswd == psswd);
  if (copy.length != 1) {
    return res.sendStatus(401);
  }
  const token = jwt.sign(user, 
    process.env.ACCESS_TOKEN_SECRET);
  return res.json({ token: token });
});

app.get("/api/items", authVerify, function(req, res) {
  res.json(req.user.items);
});

app.post("/api/items/add", authVerify, function(req, res) {
  const body = req.body;
  const item = {
    id: uuidv4(),
    text: body.text,
    isChecked: false
  };
  req.user.items.push(item);
  res.json(item);
});

app.put("/api/items/check/:id", authVerify, function(req, res) {
  const body = req.params;
  for (i = 0; i < req.user.items.length; i++) {
    if (body.id == req.user.items[i].id) {
      req.user.items[i].isChecked = 
        !req.user.items[i].isChecked;
      res.json({});
      return;
    }
  }
  res.status(400);
  res.json({
    error: "Cannot find item with this id!" 
  });
});

app.delete("/api/items/delete/:id", authVerify, function(req, res) {
  const body = req.params;
  const copy = [...req.user.items].filter(
    item => item.id != body.id);
  if (copy.length != req.user.items.length) {
    req.user.items = copy;
    res.json({});
  }
  else {
    res.status(400);
    res.json({
      error: "Cannot find item with this id!" 
    });
  }
});

app.listen(8000, function(err) {});