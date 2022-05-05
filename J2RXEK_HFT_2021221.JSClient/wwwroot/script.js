let drivers = [];
let connection = null;
let driverIdtoUpdate = -1;
getdata();
setupSignalR();

function setupSignalR() {
        connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:65297/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("DriverCreated", (user, message) => {
        getdata();
    });

    connection.on("DriverDeleted", (user, message) => {
        getdata();
    });

    connection.on("DriverUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

async function getdata() {
    fetch('http://localhost:65297/driver')
        .then(x => x.json())
        .then(y => {
            drivers = y;
            //console.log(drivers);
            display();
        });
}


function display() {
    document.getElementById('resultarea').innerHTML = "";
    drivers.forEach(x => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + x.id + "</td><td>" + x.name + "</td><td>" +
        `<button type="button" onclick="remove(${x.id})">Delete</button>` +
        `<button type="button" onclick="showupdate(${x.id})">Update</button>` +
        "</td></tr>";
    });
}

function remove(id) {
    fetch('http://localhost:65297/driver/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); })
}

function showupdate(id) {
    document.getElementById('drivernametoupdate').value= drivers.find(x=>x['id']==id)['name']
    document.getElementById('drivernumbertoupdate').value= drivers.find(x=>x['id']==id)['number']
    document.getElementById('driverteamidtoupdate').value= drivers.find(x=>x['id']==id)['teamId']
    document.getElementById('driveragetoupdate').value= drivers.find(x=>x['id']==id)['age']
    document.getElementById('updateformdiv').style.display = 'flex';
    driverIdtoUpdate = id;

}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let name = document.getElementById("drivernametoupdate").value;
    let number = document.getElementById("drivernumbertoupdate").value;
    let teamid = document.getElementById("driverteamidtoupdate").value;
    let age = document.getElementById("driveragetoupdate").value;
    fetch('http://localhost:65297/driver', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { name: name, number: number, id: driverIdtoUpdate, age:age, teamId: teamid }),
    })
        .then(response => response)
        .then(data => {
            console.log('Succes:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); })
}


function create() {
    let name = document.getElementById("drivername").value;
    let number = document.getElementById("drivernumber").value;
    let teamid = document.getElementById("driverteamid").value;
    let age = document.getElementById("driverage").value;
    fetch('http://localhost:65297/driver', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { name: name, number: number, teamId: teamid, age: age }),
    })
        .then(response => response)
        .then(data => {
            console.log('Succes:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); })
}