function LoadData() {
    var BusId = localStorage["BusId"];
    var FullName = $.cookie("fullName");
    var BookingDate = localStorage["BookingDate"];

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            CheckValid(JSON.parse(this.responseText), BookingDate);
            ShowData(JSON.parse(this.responseText), FullName, BookingDate);
        }
    };
    var url = "https://localhost:44336/BusServiceProvider/Details?BusId=";
    xhttp.open("GET", url + BusId, true);
    xhttp.send();
}

function CheckValid(data, BookingDate) {
    var date = new Date();
    var hours = date.getHours();
    var minutes = date.getMinutes();
    var busTime = data.departureTime.split(':');

    var futureBooking = false;
    var date = new Date();
    var year = date.getFullYear();
    var month = date.getMonth() + 1;
    var day = date.getDate();
    var bookingDate = BookingDate.split('-');

    if (parseInt(bookingDate[0]) > year) {
        futureBooking = true;
    } else if (parseInt(bookingDate[0]) == year) {
        if (parseInt(bookingDate[1]) > month) {
            futureBooking = true;
        } else if (parseInt(bookingDate[1]) == month) {
            if (parseInt(bookingDate[2]) > day) {
                futureBooking = true;
            }
        }
    }

    if (!futureBooking && hours > parseInt(busTime[0])) {
        alert("No tickets available, the bus has departed!!!");
        window.location.href = "https://localhost:44316/index.html";
    } else if (!futureBooking && hours == parseInt(busTime[0])) {
        if (minutes >= parseInt(busTime[1])) {
            alert("No tickets available, the bus has departed!!!");
            window.location.href = "https://localhost:44316/index.html";
        }
    }
}

function ShowData(data, FullName, BookingDate) {
    document.getElementById("fullName").innerHTML = FullName;
    document.getElementById("source").innerHTML = data.source + "(" + data.departureTime + ")";
    document.getElementById("destination").innerHTML = data.destination + "(" + data.arrivalTime + ")";
    document.getElementById("date").innerHTML = BookingDate;
    document.getElementById("fare").innerHTML = data.fare;
    document.getElementById("availableSeats").value = data.availableSeats;
}

function ValidateNoOfPsngr() {
    var number = document.getElementById("noOfPsngrs");
    var availableSeats = document.getElementById("availableSeats").value;
    if (parseInt(number.value) > 0 && parseInt(number.value) <= parseInt(availableSeats)) {
        number.style.backgroundColor = '#BCF5A9';
        document.getElementById("noOfPsngrError").style.display = "none";
        return true;
    }
    else {
        number.style.backgroundColor = '#F5A9A9';
        document.getElementById("noOfPsngrError").innerHTML = "Passenger count cannot be zero"+"<br/>"+"Count should be less than Available seats!!!";
        document.getElementById("noOfPsngrError").style.display = "block";
        return false;
    }
}

function GenerateTable() {
    var count = document.getElementById("noOfPsngrs").value;
    $("#psnger-details-table-body").html("");
    for (var i = 0; i < parseInt(count); i++) {
        var row = "<tr>" +
            "<td>" + (i + 1) + "</td>" +
            "<td>" + "<input type=text onblur='ValidatePName(this)'/>" + "</td>" +
            "<td>" + "<input type=text onblur='ValidatePContact(this)'/>" + "</td>" +
            "<td>" + "<input type=text onblur='ValidatePEmail(this)'/>" + "</td>" +
            "<td>" + "<input type=number onblur='ValidatePAge(this)'/>" + "</td>" +
            "<td>" + "<input type=button value=REMOVE onclick='RemoveRow()'/>" + "</td>" +
            "</tr>";
        $("#psnger-details-table-body").append(row);
    }
    var farepp = document.getElementById("fare").innerHTML;
    $("#total-fare").html((parseInt(farepp) * parseInt(count)) + "/-");
    $("#confirm").removeAttr("disabled");
}

function AddRow() {
    var count = document.getElementById("noOfPsngrs").value;
    if (count == "") {
        document.getElementById("noOfPsngrs").value = 1;
    } else {
        document.getElementById("noOfPsngrs").value = (parseInt(count) + 1);
    }
    
    var row = "<tr>" +
        "<td>" + ((count == "" ? 0 : parseInt(count)) + 1) + "</td>" +
        "<td>" + "<input type=text onblur='ValidatePName(this)'/>" + "</td>" +
        "<td>" + "<input type=text onblur='ValidatePContact(this)'/>" + "</td>" +
        "<td>" + "<input type=text onblur='ValidatePEmail(this)'/>" + "</td>" +
        "<td>" + "<input type=number onblur='ValidatePAge(this)'/>" + "</td>" +
        "<td>" + "<input type=button value=REMOVE onclick='RemoveRow()'/>" + "</td>" +
        "</tr>";
    $("#psnger-details-table-body").append(row);
    var farepp = document.getElementById("fare").innerHTML;
    var totalFare = document.getElementById("total-fare").innerHTML;
    $("#total-fare").html((parseInt(farepp) + parseInt(totalFare)) + "/-");
    $("#confirm").removeAttr("disabled");
}

function RemoveRow() {
    $('table').on('click', 'input[value="REMOVE"]', function () {
        $(this).closest('tr').remove();
    });
    var count = document.getElementById("noOfPsngrs").value;
    document.getElementById("noOfPsngrs").value = count - 1;

    var farepp = document.getElementById("fare").innerHTML;
    var totalFare = document.getElementById("total-fare").innerHTML;
    $("#total-fare").html((parseInt(totalFare) - parseInt(farepp)) + "/-");
    if (count - 1 == 0) {
        $("#confirm").attr("disabled", true);
    }
}

function ValidatePName(ele) {
    var re = /^[A-Za-z ]+$/;
    var n = ele.value.trim();
    if (n.length > 0 && re.test(n)) {
        ele.style.backgroundColor = 'white';
        return true;
    }
    else {
        ele.style.backgroundColor = '#F5A9A9';
        return false;
    }
}

function ValidatePContact(ele) {
    var re = /^[7-9][0-9]*$/;
    var n = ele.value;
    if ((n.length > 7 && n.length < 11) && re.test(n)) {
        ele.style.backgroundColor = 'white';
        return true;
    }
    else {
        ele.style.backgroundColor = '#F5A9A9';
        return false;
    }
}

function ValidatePEmail(ele) {
    if (ele.value.length == 0) {
        ele.style.backgroundColor = 'white';
        return true;
    }
    var re = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
    var e = ele.value.trim();
    if (e.length > 0 && re.test(e)) {
        ele.style.backgroundColor = 'white';
        return true;
    }
    else {
        ele.style.backgroundColor = '#F5A9A9';
        return false;
    }
}

function ValidatePAge(ele) {
    var b = ele.value;
    if (b!="" && parseInt(b)<=100) {
        ele.style.backgroundColor = 'white';
        return true;
    }
    else {
        ele.style.backgroundColor = '#F5A9A9';
        return false;
    }
}

function ConfirmBooking() {
    if (document.cookie.indexOf("role") == -1 || $.cookie("role") == "") {
        window.location.href = "https://localhost:44316/Account/Login.html";
    }
    
    if (!(ValidateNoOfPsngr())) {
        return false;
    }

    if (confirm("Confirm Booking?") == false) {
        return false;
    } else {
        var result = ValidatePsngrTable();
        if (!result) {
            return false;
        }
    }

    var userId = $.cookie("userId");
    var busId = localStorage["BusId"];
    var count = document.getElementById("noOfPsngrs").value;
    var bookingDate = localStorage["BookingDate"];
    var bookingTime = document.getElementById('source').innerHTML.match(/\((.*)\)/)[1];
    var amount = document.getElementById("total-fare").innerHTML;
    psngrList = FetchPsngrTableData();

    var bookingData =
    {
        bookingId: 0,
        userId: userId,
        busId: busId,
        noOfPassengers: parseInt(count),
        bookingDate: bookingDate,
        bookingTime: bookingTime,
        amountPaid: parseFloat(amount),
        passengerDetails: psngrList
    }

    SendRequest(bookingData);
}

function ValidatePsngrTable() {
    var flag = true;
    $('#psngr-details-table tbody tr').each(function (index, tr) {
        var tds = $(tr).find('td');
        if (tds.length > 1) {
                var pName = $(tds[1]).find("input").val().trim();
                var pContact = $(tds[2]).find("input").val();
                var pEmail = $(tds[3]).find("input").val();
                var pAge = $(tds[4]).find("input").val();

                var re = /^[A-Za-z ]+$/;
                if (!(pName.length > 0 && re.test(pName))) {
                    alert("Passenger Name cannot be empty.");
                    flag = false;
                    return false;
                }
                re = /^[7-9][0-9]*$/;
                if (!((pContact.length > 7 && pContact.length < 11) && re.test(pContact))) {
                    alert("Passenger Contact cannot be empty.");
                    flag = false;
                    return false;
                }
                re = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
                if (pEmail.length > 0) {
                    if (!(pEmail.trim().length > 0 && re.test(pEmail))) {
                        alert("Incorrect Email.");
                        flag = false;
                        return false;
                    }
                }
                if (!(pAge != "" && parseInt(pAge) <= 100)) {
                    alert("Incorrect Age.");
                    flag = false;
                    return false;
                }
        }
    });
    return flag;
}

function SendRequest(data) {
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            if (this.responseText == "true") {
                alert("Success, Ticket is booked successfully.");
                window.location.href = "https://localhost:44316/Booking/index.html";
            } else {
                alert("Error: Some internal error occurred.");
            }
        }
    };
    var url = "https://localhost:44336/Booking/Create";
    xhttp.open("POST", url, true);
    xhttp.setRequestHeader("Content-Type", "application/json");
    xhttp.send(JSON.stringify(data));
}

function FetchPsngrTableData() {
    var psngrData = [];
    var i = 0;

    $('#psngr-details-table tbody tr').each(function (index, tr) {
        var tds = $(tr).find('td');
        if (tds.length > 1) {
            psngrData[i++] = {
                psngrId: 0,
                bookingId: 0,
                pName: $(tds[1]).find("input").val(),
                pContact: $(tds[2]).find("input").val(),
                pEmail: $(tds[3]).find("input").val(),
                psngrAge: parseInt($(tds[4]).find("input").val())
            }
        }
    });
    return psngrData;
}