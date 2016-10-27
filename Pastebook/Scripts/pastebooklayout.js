$(document).ready(function () {

    window.onload = reloadBadge;
    window.onload = UserInformation;

    setInterval(reloadBadge, 1000);

    function UserInformation()
    {
        var data = {
            username: $('#hiddenUsername').val()
        }

        $.ajax({
            url: userInformationUrl,
            type: 'GET',
            data: data,
            cache: false,
            success: function (data) {
                ChangeUserInformation(data);
            },
            error: function () {
                alert('Badge went wrong')
            }
        });
    }

    function reloadBadge() {
        $.ajax({
            url: getCountOfNotificationUrl,
            type: 'GET',
            success: function (data) {
                ChangeBadgeCount(data);
            },
            error: function () {
                //alert('Badge went wrong')
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
            },
            error: function () {
                alert('Notification went wrong')
            }
        });

        $.ajax({
            url: updateNotificationsUrl,
            type: 'GET',
            success: function (data) {
                LoadNotificationContent(data);
            },
            error: function () {
                alert('Badge went wrong')
            }
        });
    });

    function LoadNotificationContent(data) {
        if (data.Count > 0 || data.Result) {
            $('#menu1').load(notificationPartialUrl);
        }
    }

    function Check(data)
    {

    }

    function ChangeUserInformation(data) {
        $('.imageSource').attr('src', data.Source);
        $('.name').text(data.Name);
    }

    $('#searchUser').click(function () {
        $(location).attr('href', searchUrl + $('#txtName').val());
    });
});