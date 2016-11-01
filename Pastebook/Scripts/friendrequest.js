$(document).ready(function () {
    $(document).on('click', ".btnConfirm", function () {
        var data = {
            relationshipID: $(this).val(),
            status: "Confirm"
        };
        $.ajax({
            url: acceptRejectRequestUrl,
            data: data,
            type: 'GET',
            success: function (data) {
                CheckResultForRequest(data);
            },
            error: function () {
                $('#messageError').text('Failed to accept/reject this user.');
                $('#errorModal').modal('show');
            }

        });
    });

    $(document).on('click', ".btnReject", function () {
        var data = {
            relationshipID: $(this).val(),
            status: "Reject"
        };
        $.ajax({
            url: acceptRejectRequestUrl,
            data: data,
            type: 'GET',
            success: function (data) {
                CheckResultForRequest(data);
            },
            error: function () {
                $('#messageError').text('Failed to accept/reject this user.');
                $('#errorModal').modal('show');
            }

        });
    });

    $('.view-profile').click(function () {
        var username = $(this).val();
        $(location).attr('href', userLink + username)
    });

    function CheckResultForRequest(data) {
        if (data.Result == 1) {
            $('#friendRequestPartial').load(friendRequestPartialUrl);
        }
        else {
            $('#messageError').text('Failed to accept/reject this user.');
            $('#errorModal').modal('show');
        }
    };
});