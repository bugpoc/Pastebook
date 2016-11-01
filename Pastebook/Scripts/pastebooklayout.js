window.onload = UserInformation;

function UserInformation() {
    var data = {
        username: $('#hiddenSessionUsername').val()
    }

    $.ajax({
        url: userInformationUrl,
        type: 'GET',
        data: data,
        cache: false,
        success: function (data) {
            ChangeUserInformation(data);
        }
    });
}



function ChangeUserInformation(data) {
    $('.imageSource').attr('src', data.Source);
    $('.name').text(data.Name);
}

//http://stackoverflow.com/questions/1787322/htmlspecialchars-equivalent-in-javascript
function escapeHtml(text) {
    var map = {
        '&': '&amp;',
        '<': '&lt;',
        '>': '&gt;',
        '"': '&quot;',
        "'": '&#039;'
    };

    return text.replace(/[&<>"']/g, function (m) { return map[m]; });
}

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
        var name = $.trim($('#txtName').val());
        if (name.length > 0)
        {
            $(location).attr('href', searchUrl + name);
        }
    });
});