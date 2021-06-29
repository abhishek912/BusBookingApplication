function LoadData() {
    var busId = localStorage["BusId"];

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            ShowData(JSON.parse(this.responseText));
        }
    };
    var url = "https://localhost:44336/BusServiceProvider/BusBookings?busId=";
    xhttp.open("GET", url + busId, true);
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
            else if (parseInt(bookingDate[0]) == year) {
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

        var currenDT = data[i].currentTimestamp.split('T');
        var d = currenDT[0];
        var t = currenDT[1].substring(0, 5);

        tableRow += "<td>" + count + "</td>" +
            "<td>" + data[i].bookingId + "</td>" +
            "<td>" + data[i].bookingDate + "</td>" +
            "<td>" + data[i].bookingTime + "</td>" +
            "<td>" + d + " / " + t + "</td>" +
            "<td>" + data[i].noOfPassengers + "</td>" +
            "<td>" + data[i].userContact + "</td>" +
            "<td>" + data[i].psngersContact + "</td>" +
            "<td>" + data[i].amountPaid + "</td>" +
            "</tr>";

        $("#myBusBookings-table").append(tableRow);
        count += 1;
    }
}