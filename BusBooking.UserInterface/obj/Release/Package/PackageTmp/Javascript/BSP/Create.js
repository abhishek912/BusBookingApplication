function CreateBus() {
    if (document.cookie.indexOf("role") == -1 || $.cookie("role") == "") {
        window.location.href = "https://localhost:44316/Account/Login.html";
    }

    if (!ValidateInput()) {
        return false;
    }

    var userId = $.cookie("userId");
    var busId = 0;
    var busNo = document.getElementById("busNo").value;
    var busName = document.getElementById("busName").value.trim();
    var source = document.getElementById("source").value.trim();
    var deptTime = document.getElementById("deptTime").value;
    var destination = document.getElementById("destination").value.trim();
    var arrivalTime = document.getElementById("arrivalTime").value;
    var fare = document.getElementById("fare").value;
    var capacity = document.getElementById("capacity").value;
    var busSpecs = document.getElementById("busSpecs").value;

    var data =
    {
        userId: userId,
        busId: busId,
        busNo: busNo,
        busName: busName,
        source: source,
        departureTime: deptTime,
        destination: destination,
        arrivalTime: arrivalTime,
        fare: fare,
        availableSeats: capacity,
        busSpecs: busSpecs
    };

    SendRequest(data);
}

function SendRequest(data) {
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var result = this.responseText.split(', ');
            if (result[0] == "true") {
                alert("Success: " + "Bus Registered successfully!!!");
                window.location.href = "https://localhost:44316/BSP/index.html";
            } else {
                if (result[1] == "1") {
                    var busNo = document.getElementById("busNo");
                    busNo.style.backgroundColor = '#F5A9A9';
                    document.getElementById("busNoError").innerHTML = "Bus Number already exists!!!";
                    document.getElementById("busNoError").style.display = "block";
                    return false;
                } else {
                    alert("Error: " + "Bus cannot be registered, Error occured at database end!!!");
                    return false;
                }
            }
        }
    };
    var url = "https://localhost:44336/BusServiceProvider/Create";
    xhttp.open("POST", url, true);
    xhttp.setRequestHeader("Content-Type", "application/json");
    xhttp.send(JSON.stringify(data));
}

function ValidateInput() {
    if (ValidateBusNo() && ValidateBusName() && ValidateSource() && ValidateTime() && ValidateDestination() &&
        ValidateFare() && ValidateCapacity()) {
        return true;
    }
    return false;
}

function ValidateBusNo() {
    var busNo = document.getElementById("busNo");
    var re = /[A-Z][A-Z]\/[A-Z0-9][A-Z0-9]\/[A-Z0-9][A-Z0-9]\/[0-9][0-9][0-9][0-9]$/;
    if (busNo.value.length > 0 && re.test(busNo.value)) {
        busNo.style.backgroundColor = '#BCF5A9';
        document.getElementById("busNoError").style.display = "none";
        return true;
    }
    else {
        busNo.style.backgroundColor = '#F5A9A9';
        document.getElementById("busNoError").innerHTML = "Invalid Bus Number. Format to be followed: XX/XX/XX/XXXX";
        document.getElementById("busNoError").style.display = "block";
        return false;
    }
}

function ValidateBusName() {
    var busName = document.getElementById("busName");
    var re = /[A-Za-z ]+$/;
    var b = busName.value.trim();
    if (b.length > 0 && re.test(b)) {
        busName.style.backgroundColor = '#BCF5A9';
        document.getElementById("busNameError").style.display = "none";
        return true;
    }
    else {
        busName.style.backgroundColor = '#F5A9A9';
        document.getElementById("busNameError").innerHTML = "This field cannot be empty and contains only alphabets and spaces.";
        document.getElementById("busNameError").style.display = "block";
        return false;
    }
}

function ValidateSource() {
    var source = document.getElementById("source");
    var re = /[A-Za-z ]+$/;
    var b = source.value.trim();
    if (b.length > 0 && re.test(b)) {
        source.style.backgroundColor = '#BCF5A9';
        document.getElementById("sourceError").style.display = "none";
        return true;
    }
    else {
        source.style.backgroundColor = '#F5A9A9';
        document.getElementById("sourceError").innerHTML = "This field cannot be empty and contains only alphabets and spaces.";
        document.getElementById("sourceError").style.display = "block";
        return false;
    }
}

function ValidateDestination() {
    var destination = document.getElementById("destination");
    var re = /[A-Za-z ]+$/;
    var b = destination.value.trim();
    if (b.length > 0 && re.test(b)) {
        destination.style.backgroundColor = '#BCF5A9';
        document.getElementById("destinationError").style.display = "none";
        return true;
    }
    else {
        destination.style.backgroundColor = '#F5A9A9';
        document.getElementById("destinationError").innerHTML = "This field cannot be empty and contains only alphabets and spaces.";
        document.getElementById("destinationError").style.display = "block";
        return false;
    }
}

function ValidateFare() {
    var fare = document.getElementById("fare");
    var re = /^[1-9][0-9]*$/;
    var b = fare.value.toString();
    if (b.length > 0 && re.test(b)) {
        fare.style.backgroundColor = '#BCF5A9';
        document.getElementById("fareError").style.display = "none";
        return true;
    }
    else {
        fare.style.backgroundColor = '#F5A9A9';
        document.getElementById("fareError").innerHTML = "This field cannot be empty and the value must be greater than 0.";
        document.getElementById("fareError").style.display = "block";
        return false;
    }
}

function ValidateCapacity() {
    var cap = document.getElementById("capacity");
    var re = /^[1-9][0-9]*$/;
    var b = cap.value.toString();
    if (b.length > 0 && re.test(b)) {
        cap.style.backgroundColor = '#BCF5A9';
        document.getElementById("capacityError").style.display = "none";
        return true;
    }
    else {
        cap.style.backgroundColor = '#F5A9A9';
        document.getElementById("capacityError").innerHTML = "This field cannot be empty and the value must be greater than 0.";
        document.getElementById("capacityError").style.display = "block";
        return false;
    }
}

function ValidateTime() {
    var deptTime = document.getElementById("deptTime");
    var arrivalTime = document.getElementById("arrivalTime");

    if (deptTime.value != "") {
        deptTime.style.backgroundColor = '#BCF5A9';
        document.getElementById("deptTimeError").style.display = "none";
        return true;
    }
    else {
        deptTime.style.backgroundColor = '#F5A9A9';
        document.getElementById("deptTimeError").innerHTML = "This field cannot be empty.";
        document.getElementById("deptTimeError").style.display = "block";
        return false;
    }

    if (arrivalTime.value != "") {
        arrivalTime.style.backgroundColor = '#BCF5A9';
        document.getElementById("arrivalTimeError").style.display = "none";
        return true;
    }
    else {
        arrivalTime.style.backgroundColor = '#F5A9A9';
        document.getElementById("arrivalTimeError").innerHTML = "This field cannot be empty.";
        document.getElementById("arrivalTimeError").style.display = "block";
        return false;
    }
}