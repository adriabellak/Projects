async function getUsersData() {
    let data = await fetch("/showInfo");
    let usersData = await data.json();
    const table = document.getElementById('users-table');

    for (i = 0; i < Object.keys(usersData).length; i++) {
        table.appendChild(createRow(usersData[i]));
    }

    filterTable();
}

//cambiar nombres a los del json
function createRow(user) {
    const row = document.createElement('tr');

    const name = document.createElement('td');
    name.innerText = user.nombre + " " + user.apellidos;

    const gender = document.createElement('td');
    gender.innerText = user.sexo;

    const age = document.createElement('td');
    age.innerText = user.edad;

    const level = document.createElement('td');
    level.innerText = user.curlvl;

    const score = document.createElement('td');
    score.innerText = user.moves;

    row.appendChild(name);
    row.appendChild(gender);
    row.appendChild(age);
    row.appendChild(level);
    row.appendChild(score);
    return row;
}

// búsqueda exacta 
function filterTable() {
    var nameInput, ageInput, genderInput, levelInput, scoreInput, nameFilter, ageFilter, genderFilter, levelFilter, scoreFilter, table, tr, tdName, tdAge, tdGender, tdLevel, tdScore, i, nameValue, ageValue, genderValue, levelValue, scoreValue, count, scoreSum, ageSum, levelSum, fem, masc, other, elements;

    nameInput = document.getElementById("nameInput");
    ageInput = document.getElementById("ageInput");
    genderInput = document.getElementById("genderInput");
    levelInput = document.getElementById("levelInput");
    scoreInput = document.getElementById("scoreInput");

    nameFilter = nameInput.value.toUpperCase();
    ageFilter = ageInput.value.toUpperCase();
    genderFilter = genderInput.value.toUpperCase();
    levelFilter = levelInput.value.toUpperCase();
    scoreFilter = scoreInput.value.toUpperCase();

    count = 0;
    ageSum = 0;
    scoreSum = 0;
    levelSum = 0;
    fem = 0;
    masc = 0;
    other = 0;

    table = document.getElementById("users-table");
    tr = table.getElementsByTagName("tr");
    for (i = 0; i < tr.length; i++) {
        tdName = tr[i].getElementsByTagName("td")[0];
        tdGender = tr[i].getElementsByTagName("td")[1];
        tdAge = tr[i].getElementsByTagName("td")[2];
        tdLevel = tr[i].getElementsByTagName("td")[3];
        tdScore = tr[i].getElementsByTagName("td")[4];
        if (tdName && tdAge && tdGender && tdLevel) {
            nameValue = tdName.textContent || tdName.innerText;
            ageValue = tdAge.textContent || tdAge.innerText;
            genderValue = tdGender.textContent || tdGender.innerText;
            levelValue = tdLevel.textContent || tdLevel.innerText;
            scoreValue = tdScore.textContent || tdScore.innerText;
            if ((nameValue.toUpperCase().indexOf(nameFilter) > -1) && (ageValue.toUpperCase() == ageFilter || ageFilter == "") && (genderValue.toUpperCase() == genderFilter || genderFilter == "") && (levelValue.toUpperCase() == levelFilter || levelFilter == "") && (scoreValue.toUpperCase() == scoreFilter || scoreFilter == "")) {
                tr[i].style.display = "";
                count++;
                scoreSum += parseInt(scoreValue);
                ageSum += parseInt(ageValue);
                levelSum += parseInt(levelValue);
                console.log(genderValue);
                if (genderValue.toUpperCase() == "FEMENINO") {
                    fem++;
                } else if (genderValue.toUpperCase() == "MASCULINO") {
                    masc++;
                } else {
                    other++
                }
            } else {
                tr[i].style.display = "none";
            }
        }
    }

    if (count) {
        document.getElementById("promedios-label").innerText = "Promedios";
        document.getElementById("ageAverage").innerText = "Edad: " + ageSum / count;
        document.getElementById("levelAverage").innerText = "Nivel: " + levelSum / count;
        document.getElementById("scoreAverage").innerText = "Puntaje: " + scoreSum / count;
        if (genderFilter == "") {
            document.getElementById("generos-label").innerText = "Géneros";
            document.getElementById("fem").innerText = "Femenino: " + fem;
            document.getElementById("masc").innerText = "Masculino: " + masc;
            document.getElementById("other").innerText = "Otro: " + other;
        } else {
            elements = document.getElementsByClassName("genders");
            for (i = 0; i < elements.length; i++) {
                elements[i].innerText = "";
            }
        }
    } else {
        elements = document.getElementById("statistics").children;
        for (i = 0; i < elements.length; i++) {
            elements[i].textContent = "";
        }
    }
    document.getElementById("count").innerText = "Resultados encontrados: " + count;
}

async function saveInLocal() {
    const mail = document.getElementById("mail").value;
    const password = document.getElementById("pswd").value;
    localStorage.setItem("email", mail);
    localStorage.setItem("password", password);
    const values = { 'email': mail, 'pswd': password };
    let response = await fetch('/uploadLocal', {
        method: 'POST',
        headers: { "Content-Type": "application/json;charset=utf-8" },
        body: JSON.stringify(values)
    });
    return;
}

async function deleteLocalUser() {
    const mail = localStorage.getItem('email');
    const password = localStorage.getItem('pswd');
    const values = { 'email': mail, 'pswd': password };
    localStorage.removeItem("email");
    localStorage.removeItem("password");
    let response = await fetch('/deleteLocal', {
        method: 'DELETE',
        headers: { "Content-Type": "application/json;charset=utf-8" },
        body: JSON.stringify(values)
    });
    alert("Logged Out");
    location.reload();
}

window.addEventListener('load', () => {
    if (localStorage.getItem('email')) {
        document.getElementById('boton-sesion').innerHTML = '<a id="boton-sesion" onclick="deleteLocalUser()" href="#">Salir</a>'
            // ' < input type = "submit" onclick = "deleteLocalUser()" value = "Logout" class = "button" id = "logout-button" > ';
    } else {
        document.getElementById('boton-sesion').innerHTML = '<a href = "login.html" > Iniciar sesión </a>';
    }
});