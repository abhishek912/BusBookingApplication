function LoadData() {

    var BookingId = localStorage["BookingId"];
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            ShowData(JSON.parse(this.responseText));
        }
    };
    var url = "https://localhost:44336/Booking/Details?BookingId=";
    xhttp.open("GET", url + BookingId, true);
    xhttp.send();
}

function ShowData(data) {
    var dateTime = data.ticketBookedOn.split('T');
    var date = dateTime[0];
    var time = dateTime[1].substring(0,5);

    document.getElementById("bookingId").innerHTML = data.bookingId;
    document.getElementById("fullName").innerHTML = data.bookedBy;
    document.getElementById("contact").innerHTML = data.bookingContact;
    document.getElementById("email").innerHTML = data.bookingEmail;
    document.getElementById("source").innerHTML = data.source + "(" + data.departureTime + ")";
    document.getElementById("destination").innerHTML = data.destination + "(" + data.arrivalTime + ")";
    document.getElementById("travelDate").innerHTML = data.bookingDate;
    document.getElementById("bookingDate").innerHTML = date + " / " + time;
    document.getElementById("totalAmount").innerHTML = data.totalFare;
    document.getElementById("noOfPsngrs").innerHTML = data.noOfPassengers;

    $("#psnger-details-table-body").html("");

    /*if (data.status == 0) {
        $("#action").remove();
    }*/

    var oldBooking = false;

    var date = new Date();
    var year = date.getFullYear();
    var month = date.getMonth() + 1;
    var day = date.getDate();
    var bookingDate = data.bookingDate.split('-');

    if (parseInt(bookingDate[0]) < year) {
        oldBooking = true;
    }else if (parseInt(bookingDate[0]) == year) {
        if (parseInt(bookingDate[1]) < month) {
            oldBooking = true;
        } else if (parseInt(bookingDate[1]) == month) {
            if (parseInt(bookingDate[2]) < day) {
                oldBooking = true;
            }
        }
    }

    var psngrs = data.passengerList;
    for (var i = 0; i < psngrs.length; i++) {
        var row = "<tr>" +
            "<td>" + (i + 1) + "</td>" +
            "<td>" + "<input type=text value=" + psngrs[i].pName + " readonly />" + "</td>" +
            "<td>" + "<input type=text value=" + psngrs[i].pContact + " readonly/>" + "</td>";
        if (psngrs[i].pEmail == "") {
            row += "<td>" + "<input type=text readonly/>" + "</td>";
        } else {
            row += "<td>" + "<input type=text value=" + psngrs[i].pEmail + " readonly/>" + "</td>";
        }

        row += "<td>" + "<input type=number value=" + psngrs[i].psngrAge + " readonly/>" + "</td>";
        /*if (data.status == 1 && oldBooking == false) {
            row += "<td>" + "<input type=button value=CANCEL onclick='CancelPsngr()'/>" + "</td>";
        }*/
        row += "</tr>";
        $("#psnger-details-table-body").append(row);
    }
    if (data.status == 0) {
        $("#cancel").remove();
        var msg = "<label style='background-color:#F56C63; color:white;'>This booking has been cancelled...</label>";
        $("#bookingStatus").append(msg);
    }

    if (oldBooking == true) {
        //$("#action").remove();
        $("#cancel").remove();
        var msg = "<label style='background-color:#F56C63; color:white;'>This is past booking...</label>";
        $("#bookingStatus").append(msg);
    }
}

function CancelBooking() {
    var bookingId = document.getElementById("bookingId").innerHTML;
    var msg = "Are you sure, You want to Cancel the booking?"; 
    var resp = confirm(msg);
    if (resp == true) {
        var xhttp = new XMLHttpRequest();
        xhttp.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                if (this.responseText == "true") {
                    alert("Ticket has been cencelled successfully.");
                    window.location.href = "https://localhost:44316/Booking/index.html";
                }
            }
        };
        var url = "https://localhost:44336/Booking/Cancel?BookingId=";
        xhttp.open("GET", url + bookingId, true);
        xhttp.send();
    }
}