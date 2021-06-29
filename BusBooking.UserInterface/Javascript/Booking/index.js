function LoadData() {
    var UserId = $.cookie("userId");

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            ShowData(JSON.parse(this.responseText));
        }
    };
    var url = "https://localhost:44336/Booking/Index?Id=";
    xhttp.open("GET", url + UserId, true);
    xhttp.send();
}

function ShowData(data) {
    var count = 1;

    for (var i = 0; i < data.length; i++) {
        var tableRow = "";
        if (data[i].status == 0) {
            tableRow = "<tr style='background-color:#F56C63;'>";
        } else {
            var date = new Date();
            var year = date.getFullYear();
            var month = date.getMonth() + 1;
            var day = date.getDate();
            var bookingDate = data[i].bookingDate.split('-');

            if (parseInt(bookingDate[0]) < year) {
                tableRow = "<tr style='background-color:#D5CFCF;'>";
            }
            else if (parseInt(bookingDate[0]) == year){
                if (parseInt(bookingDate[1]) < month) {
                    tableRow = "<tr style='background-color:#D5CFCF;'>";
                } else if (parseInt(bookingDate[1]) == month) {
                    if (parseInt(bookingDate[2]) < day) {
                        tableRow = "<tr style='background-color:#D5CFCF;'>";
                    } else {
                        tableRow = "<tr>";
                    }
                } else {
                    tableRow = "<tr>";
                }
            }
            else {
                tableRow = "<tr>";
            }
        }
        tableRow += "<td>" + count + "</td>" +
            "<td>" + data[i].bookingId + "</td>" +
            "<td>" + data[i].busName + "</td>" +
            "<td>" + data[i].source + "(" + data[i].departureTime + ")" + "</td>" +
            "<td>" + data[i].destination + "(" + data[i].arrivalTime + ")" + "</td>" +
            "<td>" + data[i].bookingDate + "</td>" +
            "<td>" + data[i].noOfPassengers + "</td>" +
            "<td>" + "<input type=button id = " + data[i].bookingId + " onclick='BookingDetails(this.id)' value='Show Details'/></td>" +
            "</tr>";

        $("#myBooking-table").append(tableRow);
        count += 1;
    }
}

function BookingDetails(id) {
    localStorage["BookingId"] = id;
    window.location.href = "https://localhost:44316/Booking/Details.html";
}