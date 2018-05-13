var url = new URL(window.location.href);
var userVal = url.searchParams.get("user");
var user = url.searchParams.get("user");
user = user.split("~0/");
if (user.length > 0) {
    user[0] = atob(user[0]);
    user[1] = atob(user[1]);
    user[2] = atob(user[2]);
}

$(document).on('click', '#lnkNew', function () {
    window.location = "time.html?user=" + userVal;
});

$(document).on('click', '#lnkDataEntry', function () {
    window.location = "DataEntities.html?user=" + userVal;
});

$(document).on('click', '#lnkPending', function () {
    window.location = "Approval.html?user=" + userVal;
});