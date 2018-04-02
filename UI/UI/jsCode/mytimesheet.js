var apiURL = 'http://localhost:50792/api/timesheet';
var currentdt = new Date;

function onchange() {
    currentdt = new Date($("#datepicker").val());
    setWeek(currentdt);
}

function setWeek(selectedDate) {
    var curr = selectedDate == undefined ? new Date : selectedDate;
    var Monday = new Date(curr.setDate(curr.getDate() - curr.getDay() + 1));
    var Tuesday = new Date(curr.setDate(curr.getDate() - curr.getDay() + 2));
    var Wednesday = new Date(curr.setDate(curr.getDate() - curr.getDay() + 3));
    var Thursday = new Date(curr.setDate(curr.getDate() - curr.getDay() + 4));
    var Friday = new Date(curr.setDate(curr.getDate() - curr.getDay() + 5));
    var Saturday = new Date(curr.setDate(curr.getDate() - curr.getDay() + 6));
    var Sunday = new Date(curr.setDate(curr.getDate() - curr.getDay() + 7));

    const MONTH_NAMES = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];


    $("#MonDate").html(MONTH_NAMES[Monday.getMonth()] + " " + Monday.getDate());
    $("#TueDate").html(MONTH_NAMES[Tuesday.getMonth()] + " " + Tuesday.getDate());
    $("#WedDate").html(MONTH_NAMES[Wednesday.getMonth()] + " " + Wednesday.getDate());
    $("#ThuDate").html(MONTH_NAMES[Thursday.getMonth()] + " " + Thursday.getDate());
    $("#FriDate").html(MONTH_NAMES[Friday.getMonth()] + " " + Friday.getDate());
    $("#SatDate").html(MONTH_NAMES[Saturday.getMonth()] + " " + Saturday.getDate());
    $("#SunDate").html(MONTH_NAMES[Sunday.getMonth()] + " " + Sunday.getDate());

    $("#selectedWeek").html(MONTH_NAMES[Monday.getMonth()] + "-" + Monday.getDate() + "-" + Monday.getFullYear() + " to " +
        MONTH_NAMES[Sunday.getMonth()] + "-" + Sunday.getDate() + "-" + Sunday.getFullYear()
    );
}

function UpdateWeek(val) {
    var newDt = new Date(currentdt);
    newDt.setDate(newDt.getDate() + val);
    currentdt = new Date(newDt);
    setWeek(newDt);
}

function getProjects(userId) {

    $.ajax(apiURL + "/getprojects", {
        type: "GET",
        data: userId,
        contentType: "application/json",
    }).done(function (projects) {
        var projectOptions = '<option value="0">Select Project</option>';
        for (var i = 0; i < projects.length; i++) {
            projectOptions += '<option value="' + projects[i].Id + '">' + projects[i].Name + '</option>';
        }

        $('#projects').html(projectOptions);

    }).fail(function (xhr, status, error) {
        alert("Could not reach the API: " + error);
    });

}

$(document).on('change', '#projects', function () {
    $.ajax(apiURL + "/gettasks", {
        type: "GET",
        data: { projectId: this.value },
        contentType: "application/json",
    }).done(function (tasks) {
        var taskOptions = '<option value="0">Select Task</option>';
        for (var i = 0; i < tasks.length; i++) {
            taskOptions += '<option value="' + tasks[i].Id + '">' + tasks[i].Name + '</option>';
        }

        $('#tasks').html(taskOptions);

    }).fail(function (xhr, status, error) {
        alert("Could not reach the API: " + error);
    });
});

jQuery(function () {
    var counter = 1;
    jQuery('a.add-author').click(function (event) {
        event.preventDefault();

        var newRow = jQuery('<tr><td><input type="text" name="first_name' +
            counter + '"/></td><td><input type="text" name="last_name' +
            counter + '"/></td></tr>');
        counter++;
        jQuery('table.authors-list').append(newRow);

    });
});