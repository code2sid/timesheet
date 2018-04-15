var apiURL = 'http://localhost:50792/api/Login';

$(document).on('click', '#login', function () {

    var name = $("#name").val();
    var pwd = $("#pwd").val();


    $.ajax(apiURL + "/GetUserDetail/" + name + "/" + pwd, {
        type: "GET",
        contentType: "application/json",
    }).done(function (user) {
        if (user == undefined)
            alert("not a valid user");

        else {
            window.location = "/time.html?name=" + user.Name + "&userid=" + user.Id;
        }

    }).fail(function (xhr, status, error) {
        alert("Could not reach the API: " + error);
    });

});

