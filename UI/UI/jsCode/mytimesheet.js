﻿var apiURL = 'http://localhost:50792/api/timesheet';


function getProjects(clientId) {

    $.ajax(apiURL + "/getprojects", {
        type: "GET",
        data: clientId,
        contentType: "application/json",
    }).done(function (clients) {
        for (var i = 0; i < clients.length; i++) {
            console.log(clients[i].Id);
        }
    }).fail(function (xhr, status, error) {
        alert("Could not reach the API: " + error);
    });

}