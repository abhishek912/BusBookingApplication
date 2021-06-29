function LoadData() {
    var UserId = $.cookie("userId");

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            ShowData(JSON.parse(this.responseText));
        }
    };
    var url = "https://localhost:44336/BusServiceProvider/Index?Id=";
    xhttp.open("GET", url + UserId, true);
    xhttp.send();
}

function ShowData(data) {
    var count = 1;

    for (var i = 0; i < data.length; i++) {
        var tableRow = "<tr>"+
            "<td>" + count + "</td>" +
            "<td>" + data[i].busName + "</td>" +
            "<td>" + data[i].busNo + "</td>"+
            "<td>" +
            "<input type=button id = " + data[i].busId + " onclick='BusDetails(this.id)' value='Show Details'/>" + "</td>" +
            "<td>" +
            "<input type=button id = " + data[i].busId + " onclick='BusBookingDetails(this.id)' value='Bookings' /></td>";
        
        tableRow += "</tr>";

        $("#mybus-table").append(tableRow);
        count += 1;
    }
}

function BusBookingDetails(id) {
    localStorage["BusId"] = id;
    window.location.href = "https://localhost:44316/BSP/BusBookings.html";
}

function BusDetails(id) {
    localStorage["BusId"] = id;
    window.location.href = "https://localhost:44316/BSP/Details.html";
}