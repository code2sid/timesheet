function getProjects(clientId) {

    $.ajax("http://localhost:50792/api/timesheet/getprojects", {
        type: "GET",
        //data: JSON.stringify({ data: "Hello world!" }),
        contentType: "application/json",
    }).done(function (clients) {
        for (var i = 0; i < clients.length; i++) {
            console.log(clients[i].Id);
        }
    }).fail(function (xhr, status, error) {
        alert("Could not reach the API: " + error);
    });

}