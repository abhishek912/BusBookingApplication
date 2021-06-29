
if (document.cookie.indexOf("role") != -1 && $.cookie("role") != "") {
    window.location.href = "https://localhost:44316/index.html";
}

function Authorize() {
    var contact = document.getElementById("contact").value;
    var password = document.getElementById("password").value;

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            if (this.responseText == "InValid") {
                alert("Invalid Credentials!!!");
                document.getElementById("contact").style.backgroundColor = '#F5A9A9';
                document.getElementById("password").style.backgroundColor = '#F5A9A9';
            } else {
                var data = this.responseText.split(', ');
                var date = new Date();
                date.setTime(date.getTime() + (10* 60 * 1000));
                $.cookie('role', data[0], { expires: date, path: '/' });
                $.cookie('userId', data[1], { expires: date, path: '/' });
                $.cookie('fullName', data[2], { expires: date, path: '/' });
                //$.cookie('token', data[3], { expires: date, path: '/' });
                window.location.href = "https://localhost:44316/index.html";
            }
        }
    };
    var url = "https://localhost:44336/User/login";
    xhttp.open("POST", url, true);
    xhttp.setRequestHeader("Content-Type", "application/json");
    xhttp.send(JSON.stringify({ contact: contact, password: password }));
}