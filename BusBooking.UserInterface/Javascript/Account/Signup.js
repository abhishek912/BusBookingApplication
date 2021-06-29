if (document.cookie.indexOf("role") != -1 && $.cookie("role") != "") {
    window.location.href = "https://localhost:44316/index.html";
}

function Register() {
    if (!ValidateInput()) {
        return false;
    }

    var name = document.getElementById("name").value;
    var contact = document.getElementById("contact").value;
    var email = document.getElementById("email").value;
    var password = document.getElementById("password").value;
    var admin = document.getElementById("admin").checked;


    var data =
    {
        UserId: 0,
        FullName: name,
        Contact: contact,
        Email: email,
        Password: password,
        admin: admin == true ? 1 : 0
    };

    SendRequest(data);
}

function SendRequest(data) {
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var result = this.responseText.split(', ');
            if (result[0] == "true") {
                alert("You are rgistered successfully!!!");
                window.location.href = "https://localhost:44316/Account/Login.html";
            } else {
                if (result[1] == "Contact already exists") {
                    document.getElementById("contact").style.backgroundColor = '#F5A9A9';
                    document.getElementById("contactError").innerHTML = "Contact already exists.";
                    document.getElementById("contactError").style.display = "block";
                    return false;
                }
                else if (result[1] == "Email already exists") {
                    document.getElementById("email").style.backgroundColor = '#F5A9A9';
                    document.getElementById("emailError").innerHTML = "Email already exists.";
                    document.getElementById("emailError").style.display = "block";
                    return false;
                } else {
                    //go to error page.
                    alert(result[1]);
                }
            }
        }
    };
    var url = "https://localhost:44336/User/signup";
    xhttp.open("POST", url, true);
    xhttp.setRequestHeader("Content-Type", "application/json");
    xhttp.send(JSON.stringify(data));
}

function ValidateInput() {
    if (ValidateName() && ValidateContact() && ValidateEmail() && ValidatePassword() && ValidateRePassword()) {
        return true;
    }
    return false;
}

function ValidateName() {
    var name = document.getElementById("name");
    var re = /^[A-Za-z ]+$/;
    var n = name.value.trim();
    if (n.length > 0 && re.test(n)) {
        name.style.backgroundColor = '#BCF5A9';
        document.getElementById("nameError").style.display = "none";
        return true;
    }
    else {
        name.style.backgroundColor = '#F5A9A9';
        document.getElementById("nameError").style.display = "block";
        return false;
    }
}

function ValidateContact() {
    var contact = document.getElementById("contact");
    var re = /^[7-9][0-9]*$/;
    var n = contact.value;
    if ((n.length > 7 && n.length < 11) && re.test(n)) {
        contact.style.backgroundColor = '#BCF5A9';
        document.getElementById("contactError").style.display = "none";
        return true;
    }
    else {
        contact.style.backgroundColor = '#F5A9A9';
        document.getElementById("contactError").innerHTML = "Invalid Contact Number";
        document.getElementById("contactError").style.display = "block";
        return false;
    }
}

function ValidateEmail() {
    var email = document.getElementById("email");
    var re = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
    var e = email.value.trim();
    if (e.length > 0 && re.test(e)) {
        email.style.backgroundColor = '#BCF5A9';
        document.getElementById("emailError").style.display = "none";
        return true;
    }
    else {
        email.style.backgroundColor = '#F5A9A9';
        document.getElementById("emailError").innerHTML = "Please Enter Correct Email.";
        document.getElementById("emailError").style.display = "block";
        return false;
    }
}

function ValidatePassword()
{
    var password = document.getElementById("password");
    var re = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$/;

    if (re.test(password.value) == false) {
        password.style.backgroundColor = '#F5A9A9';
        document.getElementById("passwordError").innerHTML = "Use 8 or more characters with a mix of letters, numbers & symbols.";
        document.getElementById("passwordError").style.display = "block";
        return false;
    } else {
        password.style.backgroundColor = '#BCF5A9';
        document.getElementById("passwordError").style.display = "none";
        return true;
    }
}

function ValidateRePassword() {
    var password = document.getElementById("password").value;
    var repassword = document.getElementById("repassword");

    if (repassword.value != password) {
        repassword.style.backgroundColor = '#F5A9A9';
        document.getElementById("repasswordError").innerHTML = "Password do not match.";
        document.getElementById("repasswordError").style.display = "block";
        return false;
    } else {
        repassword.style.backgroundColor = '#BCF5A9';
        document.getElementById("repasswordError").style.display = "none";
        return true;
    }
}







