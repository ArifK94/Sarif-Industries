var currentEmail;

$(document).ready(function () {

    currentEmail = $('#email').val();

});


function emailCheck() {


    var newEmail = $('#email').val();

    $.ajax({

        type: "POST",
        url: "/Users/CheckEmail",
        data: '{userEmail: "' + newEmail + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {

            if (result) {           // if email exists

                if (currentEmail != newEmail) {

                    DisableUpdateButton(true);

                    document.getElementById("msg").innerHTML = "E-mail Exists!";
                }

            }
            else {                  // if email does not exist

                DisableUpdateButton(false);

                document.getElementById("msg").innerHTML = "";
            }
        }
    });
}

setInterval(function () {

    emailCheck();

}, 1000);


var element = document.getElementById("proceed-btn");

function DisableUpdateButton(state) {


    if (state) {
        element.classList.add("fadeOption");
        element.disabled = true;
    }
    else {
        element.classList.remove("fadeOption");
        element.disabled = false;
    }
}