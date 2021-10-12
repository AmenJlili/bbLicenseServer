//$(document).load(function () {
$(window).on('load', function () {

    if (localStorage.getItem('UserName') === null && localStorage.getItem('Password') === null) {
        return false;
    }

    var localusername = JSON.parse(localStorage.getItem('UserName'));
    var localpassword = JSON.parse(localStorage.getItem('Password'));

    if (localusername == UserName && localpassword == Password) {
        var NewUrl = window.location.origin + "/View/LicenceList.html";
        window.location.href = '';
        window.location.href = NewUrl;
    }

});

function Submit() {
    if ($('#txtusername').val() == '') {
        $("#txtusername").next(".validation").remove();
        $("#txtusername").after("<div class='validation' style='color:red;margin-bottom: 20px;'>Enter User Name</div>");
        $("#txtusername").focus();
        return false;
    }
    else {
        $("#txtusername").next(".validation").remove();
    }

    if ($('#txtpassword').val() == '') {
        $("#txtpassword").next(".validation").remove();
        $("#txtpassword").after("<div class='validation' style='color:red;margin-bottom: 20px;'>Enter Password</div>");
        $("#txtpassword").focus();
        return false;
    }
    else {
        $("#txtpassword").next(".validation").remove();
    }

    if ($('#txtusername').val() == UserName && $('#txtpassword').val() == Password) {

       // if ($('#chkremember').is(":checked")) {
            localStorage.setItem('UserName', JSON.stringify($('#txtusername').val()));
            localStorage.setItem('Password', JSON.stringify($('#txtpassword').val()));
        //}

        var NewUrl = window.location.origin + "/View/LicenceList.html";
        window.location.href = '';
        window.location.href = NewUrl;
        //window.location.replace(NewUrl);
        console.log(window.location);

        return true;
    }
    else {
        $("#txtpassword").after("<div class='validation' style='color:red;margin-bottom: 20px;'>Incorrect User Name or Password.</div>");
        $("#txtpassword").focus();
        return false;
    }


}



function Cancel() {

}


