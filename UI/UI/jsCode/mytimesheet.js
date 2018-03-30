function getProjects(clientId) {

    $.ajax({
        type: 'GET',
        url: 'http://localhost:62433/api/timesheet/GetProjects',
        //data: { clientId: clientId },
        success: function (data) {
            alert(data.d);
        }

    });     

}