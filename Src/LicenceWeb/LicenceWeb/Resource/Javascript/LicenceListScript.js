

$(window).on('load', function () {

    var getfooter = document.getElementById('footertext');
    getfooter.innerText = 'Copyright ©' + new Date().getFullYear() + ' by Blue Byte Systems, Inc.';

    if (localStorage.getItem('UserName') === null && localStorage.getItem('Password') === null) {
        var NewUrl = window.location.origin + "/Index.html";
        window.location.href = '';
        window.location.href = NewUrl;
        return false;
    }

    authenticateUser();
    var UserName = localStorage.getItem('UserName').toString().replace(/"/g, "");
    $('#lblusername').text(UserName);
    //Promise.resolve(authenticateUser()).then(LoadData(4, ''));
    LoadData(4, '');



});

var LoadAllData = '';
var modal = document.getElementById('DeleteModel');


// When the user clicks anywhere outside of the modal, close it
window.onclick = function (event) {
    if (event.target == modal) {

        modal.style.display = "none";
    }
}


function authenticateUser() {
    var body = {
        grant_type: 'password',
        username: localStorage.getItem('UserName').toString().replace(/"/g, ""),
        password: localStorage.getItem('Password').toString().replace(/"/g, "")
    };

    $.ajax({
        url: ApiServerPath + 'token',
        type: 'POST',
        contentType: 'application/x-www-form-urlencoded;charset=utf-8',
        data: body, /* right */
        async: false,
        dataType: 'json',
        success: function (data) {
            //called when successful

            // alert(data);
            var token = data.access_token;
            localStorage.setItem('token', token);
        },
        error: function (xhr, textStatus, errorMessage) {
            console.log(errorMessage);
        },
    });


}

function LoadData(SerchId, string, isFilter) {
    var DataLoad = { SerchName: SerchId, Serachstring: string };

    $.ajax({
        async: true,
        cors: true,
        url: ApiServerPath + "api/LicenceData/getlicencedata",
        method: "POST",
        secure: true,
        beforeSend: function (xhr) {
            xhr.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('token'));
        },
        contentType: "application/json",
        data: JSON.stringify(DataLoad),
        //data: DataLoad,
        dataType: "json",
        success: function (data) {
            var data = JSON.parse(data);
            LoadAllData = '';
            LoadAllData = data;
            console.log(LoadAllData);
            console.log(data);
            var AppendHtml = '';
            if (data.length == 0) {
                AppendHtml += " <tr>";
                AppendHtml += " <td> No Record Avaliable...</td>";
                AppendHtml += " </tr>";
                $('#tbllicence').html('');
                $('#tbllicence').html(AppendHtml);
                LoadPaging(isFilter);
            }
            else {
                CreateTable(data, isFilter);
            }
        },
        error: function (jqXhr, textStatus, errorMessage) {
            console.log(errorMessage);
        }
    });

}

function CreateTable(data, isFilter) {
    var AppendHtml = '';
    AppendHtml += "   <thead> <tr>";
    AppendHtml += " <th style='width:30%' class='th-sm'>License Key</th>";
    AppendHtml += " <th style='width=10%' class='th-sm'>Expiration Date</th>";
    AppendHtml += " <th style='width:20%' class='th-sm'>Customer Name</th>";
    AppendHtml += " <th style='width:20%' class='th-sm'>Customer Email</th>";

    AppendHtml += " <th style='width=10%' class='th-sm'>Delete</th>";
    AppendHtml += " <th style='width=10%' class='th-sm'>Download</th>";
    AppendHtml += " </tr>   </thead> <tbody>";

    for (var i = 0; i < data.length; i++) {
        AppendHtml += " <tr>";
        AppendHtml += " <td class='parentCell' style='width:30%;'><div class='scrollable'><center>" + data[i].Licensekey + "</center></div></td>";
        AppendHtml += " <td class='parentCell' style='width=10%'><center>" + data[i].Expirationdate + "</center></td>";
        AppendHtml += " <td class='parentCell' style='width:20%'><center>" + data[i].CustomerName + "</center></td>";
        AppendHtml += " <td class='parentCell' style='width:20%'><center>" + data[i].CustomerEmail + "</center></td>";

        AppendHtml += "<td style='width=10%'><center><input type='button' class='w3-red' value='Delete' onclick='DeleteLicence(" + data[i].Id + ")'/></center></td>";
        AppendHtml += "<td style='width=10%'><center><input type='button' value='Download' onclick='Download(" + data[i].Id + ")'/></center></td>";
        AppendHtml += " </tr>";
    }
    AppendHtml += "</tbody>";

    $('#tbllicence').html('');
    $('#tbllicence').html(AppendHtml);
    LoadPaging(isFilter);

    console.log(AppendHtml);

}

function DeleteLicence(id) {
    $("#DeleteModel").attr("data-id", id);
    $('#DeleteModel').css("display", "block");
}

function Deletecancel() {
    $('#DeleteModel').css("display", "none");
}

function Deletesubmit() {

    var DeleteId = $("#DeleteModel").attr("data-id");
    var d = new Date();
    var deletedate = d.getDate() + "/" + (d.getMonth() + 1) + "/" + d.getFullYear();

    var formData = {
        Id: DeleteId,
        DeleteDate: deletedate
    };

    $.ajax({
        method: "POST",
        url: ApiServerPath + "api/LicenceData/deletelicence",
        secure: true,
        async: true,
        cors: true,
        beforeSend: function (xhr) {
            xhr.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('token'));
        },
        contentType: "application/json",
        data: JSON.stringify(formData),
        dataType: "json",
        success: function (data, status, xhr) {
            var data = JSON.parse(data);
            if (data == '') {
                $('#DeleteModel').css("display", "none");
                alert("Data not deleted....");

            }
            if (data == 'Success') {
                $('#DeleteModel').css("display", "none");
                LoadData(4, '', 1);
            }

        },
        error: function (jqXhr, textStatus, errorMessage) {
            console.log(errorMessage);
        }
    });


}

function OpenAddLicence() {

    $("#expdt").val('');
    $("#cname").val('');
    $("#cemail").val('');
    $("#cemail").next(".validation").remove();
    $("#cname").next(".validation").remove();
    $("#expdt").next(".validation").remove();

    $('#myModal').css("display", "block");
}

function AddLicence() {

    var Customername = $("#cname").val();
    var CustomerEmail = $("#cemail").val();
    var Expirydate = $("#expdt").val();

    var d = new Date();
    var SetExpirydate = d.getDate() + "/" + (d.getMonth() + 1) + "/" + d.getFullYear();


    if (Expirydate != '') {
        if (new Date() > new Date(Expirydate)) {
            $("#expdt").next(".validation").remove();
            $("#expdt").after("<div class='validation' style='color:red;margin-bottom: 20px;'>Select greater date</div>");
            $("#expdt").focus();
            return false;
        }
        else {
            $("#expdt").next(".validation").remove();
        }
    } else {
        $("#expdt").next(".validation").remove();
    }

    if (Expirydate != '') {
        var curdate = new Date(Expirydate);
        Expirydate = curdate.getDate() + "/" + (curdate.getMonth() + 1) + "/" + curdate.getFullYear();
    }

    if (Customername == "") {
        if ($("#cname").next(".validation").length == 0) // only add if not added
        {
            $("#cname").next(".validation").remove();
            $("#cname").after("<div class='validation' style='color:red;margin-bottom: 20px;'>Please enter customer name</div>");
            $("#cname").focus();
        }
        return false;
    }
    else {
        $("#cname").next(".validation").remove();
    }

    if (CustomerEmail == "") {
        if ($("#cemail").next(".validation").length == 0) // only add if not added
        {
            $("#cemail").next(".validation").remove();
            $("#cemail").after("<div class='validation' style='color:red;margin-bottom: 20px;'>Please enter customer email</div>");
            $("#cemail").focus();
        }
        return false;
    }
    else {
        $("#cemail").next(".validation").remove();
    }

    if (isEmail(CustomerEmail) == false) {
        if ($("#cemail").next(".validation").length == 0) // only add if not added
        {
            $("#cemail").next(".validation").remove();
            $("#cemail").after("<div class='validation' style='color:red;margin-bottom: 20px;'>Enter valid email address</div>");
            $("#cemail").focus();
        }
        return false;
    }
    else {
        $("#cemail").next(".validation").remove();
    }

    var formsaveData = {
        ExpirationDate: Expirydate,
        CustomerName: Customername,
        CustomerEmail: CustomerEmail
    };

    $.ajax({
        method: "POST",
        url: ApiServerPath + "api/LicenceData/createlicence",
        secure: true,
        async: true,
        cors: true,
        beforeSend: function (xhr) {
            xhr.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('token'));
        },
        data: JSON.stringify(formsaveData),
        contentType: "application/json",
        dataType: "json",
        success: function (data, status, xhr) {
            var data = JSON.parse(data);
            $('#myModal').css("display", "none");
            LoadData(4, '', 1);
            //alert("Data saved....");
            // $(window).on("load", LoadPaging);

        },
        error: function (jqXhr, textStatus, errorMessage) {
            console.log(errorMessage);
        }
    });

}

function isEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
}

function Logout() {
    localStorage.removeItem('UserName');
    localStorage.removeItem('Password');

    var NewUrl = window.location.origin + "/Index.html";
    window.location.href = '';
    window.location.href = NewUrl;
}

function Download(id) {
    var storedata = '';
    var Licensekey = '';
    var Expirationdate = '';
    var CustomerName = '';
    var CustomerEmail = '';
    var Id = '';
    for (var i = 0; i < LoadAllData.length; i++) {
        if (LoadAllData[i].Id == id) {
            Licensekey = LoadAllData[i].Licensekey;
            Expirationdate = LoadAllData[i].Expirationdate;
            CustomerName = LoadAllData[i].CustomerName;
            CustomerEmail = LoadAllData[i].CustomerEmail;
            Id = LoadAllData[i].Id;
        }
    }

    var formsaveData = {
        ExpirationDate: Expirationdate,
        CustomerName: CustomerName,
        CustomerEmail: CustomerEmail,
        Licensekey: Licensekey,
        Id: Id
    };

    $.ajax({
        method: "POST",
        url: ApiServerPath + "api/LicenceData/loadsignaturedetail",
        secure: true,
        async: true,
        cors: true,
        beforeSend: function (xhr) {
            xhr.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('token'));
        },
        data: JSON.stringify(formsaveData),
        contentType: "application/json",
        dataType: "json",
        success: function (data, status, xhr) {

            console.log(data);
            var data = JSON.parse(data);
            //storedata = '<Licensekey>' + Licensekey + '<Licensekey> \n';
            storedata += data;
            var blob = new Blob([storedata], {
                type: 'application/json'
            });
            var link = document.createElement('a');
            link.href = window.URL.createObjectURL(blob);
            var FileName = CustomerName.trim();
            FileName = FileName.replace(/[&\/\\#,+()$~%.'":*?<>{}]/g, '');
            link.download = FileName + "_licensKey.XML";
            link.click();
        },
        error: function (jqXhr, textStatus, errorMessage) {
            console.log(errorMessage);
        }
    });



}

function myFunction() {

    var columnid = $('#selcolumn option:selected').val();
    var input = document.getElementById("myInput");
    var filter = input.value.toUpperCase();
    var data_filter = [];

    if (filter.length > 0)
        LoadData(columnid, filter, 1);
    else
        LoadData(4, '', 1);

}

function AscsortTable() {
    var columnid = $('#selcolumn option:selected').val();
    var table, rows, switching, i, x, y, shouldSwitch;
    table = document.getElementById("tbllicence");
    switching = true;
    /*Make a loop that will continue until
    no switching has been done:*/
    while (switching) {
        //start by saying: no switching is done:
        switching = false;
        rows = table.rows;
        console.log(rows.length);
        tr = table.getElementsByTagName("tr");
        console.log(tr.length);
        /*Loop through all table rows (except the
        first, which contains table headers):*/
        for (i = 1; i < (rows.length - 1) ; i++) {
            //start by saying there should be no switching:
            shouldSwitch = false;
            /*Get the two elements you want to compare,
            one from current row and one from the next:*/
            x = rows[i].getElementsByTagName("TD")[columnid];
            y = rows[i + 1].getElementsByTagName("TD")[columnid];
            //check if the two rows should switch place:
            if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
                //if so, mark as a switch and break the loop:
                shouldSwitch = true;
                break;
            }
        }
        if (shouldSwitch) {
            /*If a switch has been marked, make the switch
            and mark that a switch has been done:*/
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;

        }
    }

}

function DescsortTable() {
    var columnid = $('#selcolumn option:selected').val();
    var table, rows, switching, i, x, y, shouldSwitch;
    table = document.getElementById("tbllicence");
    switching = true;
    /*Make a loop that will continue until
    no switching has been done:*/
    while (switching) {
        //start by saying: no switching is done:
        switching = false;
        rows = table.rows;
        console.log(rows.length);
        tr = table.getElementsByTagName("tr");
        console.log(tr.length);
        /*Loop through all table rows (except the
        first, which contains table headers):*/
        for (i = 1; i < (rows.length - 1) ; i++) {
            //start by saying there should be no switching:
            shouldSwitch = false;
            /*Get the two elements you want to compare,
            one from current row and one from the next:*/
            y = rows[i].getElementsByTagName("TD")[columnid];
            x = rows[i + 1].getElementsByTagName("TD")[columnid];

            //check if the two rows should switch place:
            if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
                //if so, mark as a switch and break the loop:
                shouldSwitch = true;
                break;
            }
        }
        if (shouldSwitch) {
            /*If a switch has been marked, make the switch
            and mark that a switch has been done:*/
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;

        }
    }

}

function LoadPaging(isFilter) {

    if (isFilter == 1) {
        $('#tbllicence').paging('destroy');
        $("div").remove(".paging-nav");
    }

    $('#tbllicence').paging({
        limit: 10,
        rowDisplayStyle: 'block',
        activePage: 0,
        rows: [5]
    });
}




