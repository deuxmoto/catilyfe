const express = require('express');
const path = require('path');
const logger = require('morgan');
const bodyParser = require('body-parser');

const app = express();
const router = express.Router();

app.use(logger('dev'));
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: false }));
app.use(express.static(path.join(__dirname, 'public')));

// errythang handled by Angular in index.html
app.use(function (req, res, next) {
    res.sendFile(path.join(__dirname, "public/index.html"));
});

const port = process.env.PORT || 8080;
app.listen(port);
console.log(`Listening on port ${port}`);

module.exports = app;
