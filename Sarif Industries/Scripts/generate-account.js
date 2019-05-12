function generateRandomFirstSection() {

    var pass = document.getElementById("password");

    $('.btn-show-pass').next('input').attr('type', 'text');
    $('.btn-show-pass').find('i').removeClass('fa-eye');
    $('.btn-show-pass').find('i').addClass('fa-eye-slash');

    pass.value = "1";


    var firstnameURL = 'http://faker.hook.io/?property=name.firstName&locale=en_GB';
    var lastnameURL = 'http://faker.hook.io/?property=name.lastName&locale=en_GB';
    var emailURL = 'http://faker.hook.io/?property=internet.email&locale=en_GB';


    // first section
    fetchData(firstnameURL, "firstname");
    fetchData(lastnameURL, "surname");
    fetchData(emailURL, "email");



}

function generateRandomSecondSection() {

    var addressURL = 'http://faker.hook.io/?property=address.streetAddress&locale=en_GB';
    var postcodeURL = 'http://faker.hook.io/?property=address.zipCode&locale=en_GB';
    var phoneURL = 'http://faker.hook.io/?property=phone.phoneNumber&locale=en_GB';



    // second section 
    fetchData(addressURL, "address");
    fetchData(postcodeURL, "postcode");
    fetchData(phoneURL, "phone-number");


}

function fetchData(url, element) {
    fetch(url)
        .then(function (response) {
            return response.json();
        })
        .then(function (myJson) {

            var str = JSON.stringify(myJson);
            str = str.replace(/['"]+/g, '');
            document.getElementById(element).value = str;
        });
}