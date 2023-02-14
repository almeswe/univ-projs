const path      = require('path')
const express   = require('express')
const app       = express();
const sqlite3   = require('sqlite3').verbose();

const db        = new sqlite3.Database('books.db');

const port = 8888;
const root = __dirname;

const src    = path.join(root, 'src')
const views  = path.join(src, 'views')
const public = path.join(src, 'public')

function appLog(msg) {
    console.log(`[APP] ${msg}`);
}

function appSendError(res, code, abbreviation='') {
    res.status(code);
    res.render('error', { code: code, abbreviation: abbreviation });
}

function appSetRoutes() {
    app.get('/', function (req, res) {
        db.all("SELECT * FROM Books", function(err, rows) {
            res.render('form', { data: rows });
        });
    });
    app.post('/', function (req, res) {
        db.run('INSERT INTO Books(name, author, number) VALUES (?, ?, ?)',
            req.body.bookname,
            req.body.bookauthor,
            req.body.booknumber
        );
        res.redirect('/');
    });
    app.use(function (req, res) {
        appSendError(res, 404, 'Not Found');
    });
}

function appInitSql() {
    db.serialize(function() {
        db.run('CREATE TABLE IF NOT EXISTS Books (id INTEGER PRIMARY KEY, name TEXT, author TEXT, number TEXT)');
    });
}

function appStart() {
    app.set('views', views)
    app.set('view engine', 'ejs')
    app.use(express.urlencoded({ extended: true }));
    //app.use(express.json());
    app.use(express.static(public));
    appInitSql();
    appSetRoutes();
    app.listen(port, function () {
        appLog(`Server running on port: ${port}`);
    })
}

appStart();