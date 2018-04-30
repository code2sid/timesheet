window.apiURL = 'http://localhost:50792/api/timesheet';
var jsonresponse;
$(document).on('click', '#GetData', function () {
    $.ajax(apiURL + "/GetEntitiesData", {
        type: "GET",
        contentType: "application/json",
    }).done(function (de) {

        var row = "";

        for (var i = 0; i < de.Users.length; i++) {
            row = "<tr> \
                <td>" + de.Users[i].Id + "</td>\
                <td>" + de.Users[i].Name + "</td>\
                <td>" + de.Users[i].RoleId + "</td>\
                <td>"+ de.Users[i].Password + "</td>\
            </tr>"
            $("#tblUsers").append(row);
        }
        row = "";
        for (var i = 0; i < de.Projects.length; i++) {
            row = "<tr> \
                <td>" + de.Projects[i].Id + "</td>\
                <td>" + de.Projects[i].Name + "</td>\
                <td>" + de.Projects[i].UserId + "</td>\
            </tr>"
            $("#tblProjects").append(row);
        }

        row = "";
        for (var i = 0; i < de.Projects.length; i++) {
            row = "<tr> \
                <td>" + de.Projects[i].Id + "</td>\
                <td>" + de.Projects[i].Name + "</td>\
                <td>" + de.Projects[i].UserId + "</td>\
            </tr>"
            $("#tblProjects").append(row);
        }
        row = "";
        for (var i = 0; i < de.Tasks.length; i++) {
            row = "<tr> \
                <td>" + de.Tasks[i].Id + "</td>\
                <td>" + de.Tasks[i].Name + "</td>\
                <td>" + de.Tasks[i].ProjectId + "</td>\
                <td>" + de.Tasks[i].TaskTypeId + "</td>\
            </tr>"
            $("#tblTasks").append(row);
        }
        row = "";
        for (var i = 0; i < de.TaskType.length; i++) {
            row = "<tr> \
                <td>" + de.TaskType[i].Id + "</td>\
                <td>" + de.TaskType[i].Name + "</td>\
            </tr>"
            $("#tblTaskTypes").append(row);
        }
    }).fail(function (xhr, status, error) {
        alert("Could not reach the API: " + error);
    });
});

$(document).on('click', '#AddData', function () {
    var insertJson = JSON.stringify({ d: jsonresponse });
    $.ajax(apiURL + "/InsertEntitiesData", {
        type: "POST",
        data: insertJson,
        contentType: "application/json",
    }).done(function (de) {

        alert("Data Inserted!!!")

    }).fail(function (xhr, status, error) {
        alert("Could not reach the API: " + error);
    });
});
function loadJSON(callback) {

    var xobj = new XMLHttpRequest();
    xobj.overrideMimeType("application/json");
    xobj.open('GET', 'src/assets/DataEntries.json', true);
    xobj.onreadystatechange = function () {
        if (xobj.readyState == 4 && xobj.status == "200") {
            callback(xobj.responseText);
        }
    }
    xobj.send(null);
}

loadJSON(function (response) {
    jsonresponse = response;

});