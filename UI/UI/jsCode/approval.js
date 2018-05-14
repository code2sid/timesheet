var apiURL = 'http://localhost:50792/api/timesheet';
function getPendingApprovals() {
    $("#imgloader").show();

    $.ajax(apiURL + "/GetPendingApprovals/", {
        type: "GET",
        contentType: "application/json",
    }).done(function (pendingApprovals) {
        var counter = 1;
        var results = '';
        for (var i = 0; i < pendingApprovals.length; i++) {
            results = '<tr>\
                            <td><input type="checkbox" class="chkRow" data-id=' + pendingApprovals[i].TimeSheetId + ' /></td>\
                            <td><span>' + pendingApprovals[i].WeekRange + '</span></td>\
                            <td><span>' + pendingApprovals[i].TotalHours + ' Hours</span></td>\
                            <td><span>' + pendingApprovals[i].UserName + '</span></td>\
                        </tr >';
            $("#tblresults").append(results);
            counter++;
        }

        $("#imgloader").hide();

    }).fail(function (xhr, status, error) {
        $("#imgloader").hide();
        alert("Could not reach the API: " + error);
    });

}


$(document).on('click', '#btnApprove', function () {
    var tsIds = [];
    $(".chkRow:checkbox:checked").each(function () {
        tsIds.push($(this).attr("data-id"));
    });
    $("#imgloader").show();

    $.ajax({
        type: "POST",
        url: apiURL + "/ApproveTimeSheets",
        contentType: 'application/json',
        type: 'POST',
        data: tsIds,
        dataType: "json",
        success: function (result) {
            if (result) {
                alert("TimeSheet Approved Successfully");
                getPendingApprovals();
            }
        },
        error: function (jqXHR, exception) {
            $("#imgloader").hide();
            alert(exception);
            alert("Could not reach the API: " + error);

        }
    });

    $("#imgloader").hide();
});
