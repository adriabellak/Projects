const express = require("express"); // Mounting Server
const mysql = require("mysql"); // Database handling
const { reset } = require("nodemon"); // Helps avoiding server restart on development
const path = require('path'); // For using different local files
const app = express();
var LocalStorage = require('node-localstorage').LocalStorage,
    localStorage = new LocalStorage('./scratch');


app.use(express.json()); // Body Parser for handling raw json
app.use(express.urlencoded({ extended: false })); // Handle form submissions
app.use(express.static(path.join(__dirname, "/../Frontend"))); // Use a static folder (Frontend) for html and css

// Create connection with local database
const db = mysql.createConnection({
    host: "localhost",
    user: "root",
    password: "STEAMadmin",
    database: "steam_users",
    port: "3306",
});

db.connect((err) => {
    if (err) {
        throw err;
    } else {
        console.log("connected");
    }
});

app.post('/signup', (req, res) => {
    const newMember = req.body;
    if (newMember.pswd.length >= 8) {
        db.query(`INSERT INTO users(email, pswd, nombre, apellidos, edad, sexo, curlvl, moves) values('${newMember.email}','${newMember.pswd}','${newMember.nombre}','${newMember.apellidos}','${newMember.edad}','${newMember.sexo}', 1 , 0);`);
        res.redirect('login.html');
    } else {
        res.redirect('index.html');
    }
});

app.post('/login', (req, res) => {
    const logIn = req.body;
    db.query(`SELECT * FROM users WHERE email = '${logIn.email}' AND pswd = '${logIn.pswd}';`, (err, rows, fiels) => {
        if (err) {
            throw err;
        } else {
            if (rows[0] != undefined) {
                res.redirect('juego.html');
            } else {
                console.log("Invalid email or password");
                res.redirect('login.html');
            }
        }
    });
});

// Send all users for showing in table
app.get("/showInfo", (req, res) => {
    db.query("SELECT * FROM users", (error, rows, fields) => {
        if (error) {
            throw error
        } else {
            res.json(rows);
        }
    });
});

app.post('/uploadLocal', (req, res) => {
    const values = req.body;
    localStorage.setItem('email', values.email);
    localStorage.setItem('pswd', values.pswd);
    console.log(localStorage.getItem('email'));

});

app.delete('deleteLocal', (req, res) => {
    const values = req.body;
    localStorage.removeItem('email');
    localStorage.removeItem('pswd');
    console.log('user deleted');
    res.redirect('login.html');
});

app.post('/sendData', (req, res) => {
    const unityInfo = req.body;
    console.log(unityInfo);
    console.log(localStorage.getItem('email'));
    console.log(unityInfo.currentLevel);
    db.query(`UPDATE users SET curlvl = ${parseInt(unityInfo.currentLevel)}, moves = ${parseInt(unityInfo.totalMoves)} WHERE email = '${localStorage.getItem('email')}' AND pswd = '${localStorage.getItem('pswd')}';`);
});


const PORT = process.env.PORT || 5000; // Port for deployment or local (for development)
app.listen(PORT, () => console.log(`Server started on ${PORT}`)); // Listening onthe decided port