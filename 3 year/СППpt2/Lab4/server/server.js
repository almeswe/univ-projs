const jwt     = require('jsonwebtoken');
const cors    = require('cors');
const dotenv  = require('dotenv');
const express = require('express');

const { v4: uuidv4 } = require('uuid');

dotenv.config();
const env = process.env;

const app = express();
app.use(cors());
app.use(express.json());

const http   = require('http');
const sockio = require('socket.io');

const httpServer = new http.Server(app);
const sockServer = new sockio.Server(httpServer, {
    cors: { origin: `http://${env.FRONT_END_DOMAIN}:${env.FRONT_END_PORT}` }
});

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

function authVerifyUser(data, err, user) {
    if (err || !user) {
        return false;
    }
    let date = Date.parse(user.adate);
    let diff = (Date.now() - date) / 1000;
    if (diff > 20) {
        return false;
    }
    let copy = [...users].filter(suser => 
        user.email == suser.email && user.psswd == suser.psswd);
    if (copy.length != 1) {
        return false;
    }
    data.user = copy[0];
    return true;
}
  
function authVerify(data) {
    var verified = false;
    const token = data.token;
    if (token) {
        const secret = process.env.ACCESS_TOKEN_SECRET;
        jwt.verify(token, secret, (err, user) => {
            verified = authVerifyUser(data, err, user);
        });
    }
    return verified;
}

function serverItemsRequest(socket, data) {
    let items = [];
    let status = 500;
    if (!authVerify(data)) {
        status = 401;
    }
    else {
        status = 200;
        items = data.user.items;
    }
    socket.emit('response', {
        route: 'api.items',
        query: items, 
        status: status,
    });
}

function serverItemAddRequest(socket, data) {
    let status = 500;
    if (!authVerify(data)) {
        status = 401;
    }
    else {
        status = 200;
        const item = {
            id: uuidv4(),
            text: data.query.text,
            isChecked: false
        };
        data.user.items.push(item);
    }
    socket.emit('response', {
        route: 'api.items.add',
        status: status,
    });
}

function serverItemCheckRequest(socket, data) {
    let status = 500;
    if (!authVerify(data)) {
        status = 401;
    }
    else {
        status = 400;
        for (i = 0; i < data.user.items.length; i++) {
            if (data.query.itemid == data.user.items[i].id) {
                data.user.items[i].isChecked = 
                    !data.user.items[i].isChecked;
                status = 200;
                break;
            }
        }
    }
    socket.emit('response', {
        route: 'api.items.check',
        status: status,
    });
}

function serverItemDeleteRequest(socket, data) {
    let status = 500;
    if (!authVerify(data)) {
        status = 401;
    }
    else {
        const copy = [...data.user.items].filter(
          item => item.id != data.query.itemid);
        if (copy.length != data.user.items.length) {
            status = 200;
            data.user.items = copy;
        }
        else {
            status = 400;
        }
    }
    socket.emit('response', {
        route: 'api.items.delete',
        status: status,
    });
}

function serverSignInRequest(socket, data) {
    let token = '';
    let status = 500;
    const email = data.query.email;
    const psswd = data.query.psswd;
    if (!email || !psswd) {
      status = 401;
    }
    else {
        const user = {
            email: email,
            psswd: psswd,
            adate: new Date(Date.now()).toString()
        };
        let copy = [...users].filter(user => 
            user.email == email && user.psswd == psswd);
        if (copy.length != 1) {
            status = 401;
        }
        else {
            status = 200;
            token = jwt.sign(user, 
                process.env.ACCESS_TOKEN_SECRET);
        }
    }
    socket.emit('response', {
        route: 'api.signin',
        status: status,
        token: token        
    });
}

function serverSignUpRequest(socket, data) {
    let status = 500;
    const email = data.query.email;
    const psswd = data.query.psswd;
    if (!email || !psswd) {
        status = 401;
    }
    else {
        let copy = [...users].filter(user => 
            user.email == email && user.psswd == psswd);
        if (copy.length > 0) {
            status = 401;
        }
        else {
            status = 200;
            users.push({
                "id": uuidv4(),
                "email": email,
                "psswd": psswd,
                "items": []
            });
        }
    }
    socket.emit('response', {
        route: 'api.signup',
        status: status
    });
}

sockServer.on('connection', (socket) => {
    console.log(`⚡: ${socket.id} user just connected!`);
    
    socket.on('api.signin', (data) => { serverSignInRequest(socket, data); });
    socket.on('api.signup', (data) => { serverSignUpRequest(socket, data); });
    socket.on('api.items',  (data) => { serverItemsRequest(socket, data); });
    socket.on('api.items.add',    (data) => { serverItemAddRequest(socket, data); });
    socket.on('api.items.check',  (data) => { serverItemCheckRequest(socket, data); });
    socket.on('api.items.delete', (data) => { serverItemDeleteRequest(socket, data); });
    socket.on('disconnect', () => {
    });
});

const port = parseInt(env.BACK_END_PORT);
httpServer.listen(port, () => {
    console.log('test');
});