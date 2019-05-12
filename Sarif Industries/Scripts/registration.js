var element = document.getElementById("proceed-btn");

var isEmailValid = false;

$(document).ready(function () {

    var email = $('#email').val();
    var firstname = $('#firstname').val();
    var surname = $('#surname').val();
    var password = $('#password').val();

    if (email == "" || firstname == "" || surname == "" || password == "") {
        DisableUpdateBtn(true);
    }
});

function emailCheck() {

    var email = $('#email').val();

    $.ajax({

        type: "POST",
        url: "/Users/CheckEmail",
        data: '{userEmail: "' + email + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {

            if (result) {           // if email exists

                DisableUpdateBtn(true);

                document.getElementById("msg").innerHTML = "E-mail Exists!";

                isEmailValid = false;
            }
            else {                  // if email does not exist

                isEmailValid = true;

                document.getElementById("msg").innerHTML = "";


            }
        }
    });
}


function DisableUpdateBtn(state) {


    if (state) {
        element.classList.add("fadeOption");
        element.disabled = true;
    }
    else {
        element.classList.remove("fadeOption");
        element.disabled = false;
    }
}


setInterval(function () {

    var email = $('#email').val();
    var firstname = $('#firstname').val();
    var surname = $('#surname').val();
    var password = $('#password').val();

    if ((email != "" && firstname != "" && surname != "" && password != "")) {
        DisableUpdateBtn(false);
    }
    else {
        DisableUpdateBtn(true);
    }

}, 1000);