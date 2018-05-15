var apiURL = 'http://localhost:50792/api/timesheet';
function getPendingApprovals() {
    $("#imgloader").show();

    var apprvalURL = apiURL + "/GetPendingApprovals?from={0}&to={1}";
    apprvalURL = apprvalURL.replace("{0}", $("#txtFrom").val()).replace("{1}", $("#txtTo").val());

    $.ajax(apprvalURL, {
        type: "GET",
        contentType: "application/json",
    }).done(function (pendingApprovals) {
        $(".removeRow").remove();
        var counter = 1;
        var results = '';
        for (var i = 0; i < pendingApprovals.length; i++) {
            results = '<tr class="removeRow">\
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
$(document).on('click', '#btnReset', function () {
    $("#txtFrom").val("");
    $("#txtTo").val("");
});

    $(document).on('click', '#btnSearch', function () {
    getPendingApprovals();
});


$(document).on('click', '#chkAll', function () {
    $(".chkRow").prop('checked', $(this).prop('checked'));
});

$(document).on('click', '#btnApprove', function () {
    var tsIds = [];
    $(".chkRow:checkbox:checked").each(function () {
        tsIds.push(parseInt($(this).attr("data-id")));
    });
    $("#imgloader").show();
    $.ajax({
        type: "POST",
        url: apiURL + "/ApproveTimeSheets",
        contentType: 'application/json',
        type: 'POST',
        data: JSON.stringify(tsIds),
        dataType: "json",
        success: function (result) {
            if (result) {
                alert("TimeSheet Approved Successfully");
                $(".removeRow").remove();
                getPendingApprovals();
            }
        },
        error: function (jqXHR, exception) {
            $("#imgloader").hide();
            alert("Could not reach the API: " + error);

        }
    });

    $("#imgloader").hide();
});
