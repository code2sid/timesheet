function getProjects(clientId) {

    $.ajax({
        type: 'GET',
        url: '/getProjects',
        data: { clientId: clientId },
        success: function (data) {
            alert(data.d);
        }

    });     

}