function SearchBus() {
    if (!ValidateInput()) {
        return false;
    }

    var source = document.getElementById("source").value;
    var destination = document.getElementById("destination").value;
    var date = document.getElementById("travel-date").value;
    var time = document.getElementById("travel-time").value;
    var timetype = document.getElementById("timetype").value;

    var data = {
        source: source,
        destination: destination,
        date: date,
        time: time,
        timetype: timetype
    };

    SendRequest(data);
}

function ValidateInput() {
    if (ValidateSource() && ValidateDestination() && ValidateDate()) {
        return true;
    }
    return false;
}

function ValidateSource() {
    var source = document.getElementById("source");
    var re = /[A-Za-z ]+$/;
    var b = source.value.trim();
    if (b.length > 0 && re.test(b)) {
        source.style.backgroundColor = 'white';
        return true;
    }
    else {
        source.style.backgroundColor = '#F5A9A9';
        return false;
    }
}

function ValidateDestination() {
    var destination = document.getElementById("destination");
    var re = /[A-Za-z ]+$/;
    var b = destination.value.trim();
    if (b.length > 0 && re.test(b)) {
        destination.style.backgroundColor = 'white';
        return true;
    }
    else {
        destination.style.backgroundColor = '#F5A9A9';
        return false;
    }
}

function ValidateDate() {
    var date = document.getElementById("travel-date");
    if (date.value == "") {
        date.style.backgroundColor = '#F5A9A9';
        return false;
    } else {
        date.style.backgroundColor = 'white';
        return true;
    }
}

function SendRequest(data) {
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            if (JSON.parse(this.responseText).length > 0) {
                ShowData(JSON.parse(this.responseText));
            } else {
                alert("No bus found for this search result.")
            }
        }
    };
    var url = "https://localhost:44336/Home/Search";
    xhttp.open("POST", url, true);
    xhttp.setRequestHeader("Content-Type", "application/json");
    xhttp.send(JSON.stringify(data));
}

function ShowData(data) {
    var count = 1;
    $("#search-result-table-body").html("");

    for (var i = 0; i < data.length; i++) {
        var tableRow = "<tr>" +
            "<td>" + count + "</td>" +
            "<td>" + data[i].busName + "</td>" +
            "<td>" + data[i].source + "</td>" +
            "<td>" + data[i].departureTime + "</td>" +
            "<td>" + data[i].destination + "</td>" +
            "<td>" + data[i].arrivalTime + "</td>" +
            "<td>" + data[i].fare + "</td>" +
            "<td>" + data[i].availableSeats + "</td>";
            if (data[i].availableSeats == 0) {
                tableRow += "<td><input type = 'button' class = btn-primary id =" + data[i].busId + " value = Book disabled></td>";
            } else {
                tableRow += "<td><input type = 'button' class = btn-primary id =" + data[i].busId + " value = Book onclick=CreateBooking(this.id)></td>";
            }
            tableRow+="</tr>";

        $("#search-result-table-body").append(tableRow);
        count += 1;
    }
}

function CreateBooking(busId) {
    localStorage["BusId"] = busId;
    localStorage["BookingDate"] = document.getElementById("travel-date").value;
    window.location.href = "https://localhost:44316/Booking/Create.html";
}

function Logout() {
    alert("logout");
    $.cookie('role', "", { path: '/' });
    $.cookie('userId', "", { path: '/' });
    //$.cookie('token', "");
    window.location.href = "https://localhost:44316/index.html";
}