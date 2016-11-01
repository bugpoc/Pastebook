$(document).ready(function () {

    window.onload = reloadBadge;

    setInterval(reloadBadge, 1000);

    function reloadBadge() {
        $.ajax({
            url: getCountOfNotificationUrl,
            type: 'GET',
            success: function (data) {
                ChangeBadgeCount(data);
            }
        });
    }

    function ChangeBadgeCount(data) {
        if (data.Count > 0)
        {
            $('#notifCount').text(data.Count);
        }
        else
        {
            $('#notifCount').text('')
        }
    }

    $('#viewNotifications').click(function () {
        $.ajax({
            url: getCountOfNotificationUrl,
            type: 'GET',
            success: function (data) {
                LoadNotificationContent(data);
            }
        });

        $.ajax({
            url: updateNotificationsUrl,
            type: 'GET',
            success: function (data) {
                LoadNotificationContent(data);
            }
        });
    });

    function LoadNotificationContent(data) {
        if (data.Count > 0 || data.Result) {
            $('#menu1').load(notificationPartialUrl);
        }
    }

    $('#searchUser').click(function () {
        $(location).attr('href', searchUrl + $('#txtName').val());
    });
});