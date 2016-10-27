$(document).ready(function () {

    var hiddenUsername = $('#hiddenUsername').val();

    $('#btnUpdateAboutMe').click(function () {
        $('#myModal').modal('show');
    })

    $('#btnPost').click(function () {
        var varContent = $.trim($('#txtAreacontent').val());
        var data = {
            content: varContent,
            username: $('#hiddenUsername').val()
        };
        if (varContent.length != 0) {
            $.ajax({
                url: savePostUrl,
                data: data,
                type: 'GET',
                success: function (data) {
                    CheckResultForPost(data);
                },
                error: function () {
                    alert('Something went wrong')
                }
            });
        }
        else {
            $('#contentMessage').text('You cannot post an empty content.');
        }
    });

    $('#btnAboutMe').click(function () {
        var varAboutMe = $.trim($('#txtAreaAboutMe').val());
        var data = {
            aboutMe: varAboutMe,
            username: $('#hiddenUsername').val()
        };
        if (varAboutMe.length != 0) {
            $.ajax({
                url: updateAboutMeUrl,
                data: data,
                type: 'GET',
                success: function (data) {
                    CheckResultForAboutMe(data);
                },
                error: function () {
                    alert('Something went wrong')
                }

            });
        }
        else {
            $('#aboutMeMessage').text('You cannot update about me with an empty content.');
        }
    });

    $(document).on('click', ".btnLike", function () {
        var likeUnlike = $(this).attr('id');
        var data = {
            postID: $(this).val(),
            status: likeUnlike
        };
        $.ajax({
            url: likePostUrl,
            data: data,
            type: 'GET',
            success: function (data) {
                CheckResultForLikeAndComment(data);
            },
            error: function () {
                alert('Something went wrong')
            }

        });
    });

    $(document).on('click', ".btnComment", function () {
        var id = $(this).val();
        var comment = $('#' + id).val();

        var data = {
            postID: $(this).val(),
            content: comment
        };
        $.ajax({
            url: commentToPostUrl,
            data: data,
            type: 'GET',
            success: function (data) {
                CheckResultForLikeAndComment(data);
            },
            error: function () {
                alert('Something went wrong')
            }
        });
    });

    $('#btnAddFriend').click(function () {
        var varAboutMe = $.trim($('#txtAreaAboutMe').val());
        var data = {
            username: $('#hiddenUsername').val()
        };
        $.ajax({
            url: addFriendUrl,
            data: data,
            type: 'GET',
            success: function (data) {
                CheckResultForAddFriend(data);
            },
            error: function () {
                alert('Something went wrong')
            }

        });
    });

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
                alert('Something went wrong')
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
                alert('Something went wrong')
            }

        });
    });

    $(document).on('click', ".showLikeModal", function () {
        var id = $(this).val();
        $('#likeModal_' + id).modal('show');
    });

    function CheckResultForAboutMe(data) {
        if (data.Result == 1) {
            $(location).attr('href', userLink + hiddenUsername);
        }
        else {
            alert("Fail Inserting");
        }
    };

    function CheckResultForPost(data) {
        if (data.Result == 1) {
            $('#timelinePartial').load(timelinePartialUrl + '?username=' + hiddenUsername);
            $('#txtAreacontent').val('');
            $('#contentMessage').text('');
        }
        else {
            alert("Fail Inserting");
        }
    };

    function CheckResultForLikeAndComment(data) {
        if (data.Result == 1) {
            $('#timelinePartial').load(timelinePartialUrl + '?username=' + hiddenUsername);
        }
        else {
            alert("Fail Inserting");
        }
    };

    function CheckResultForAddFriend(data) {
        if (data.Result == 1) {
            $('#addFriendPartial').load(addfriendPartialUrl + '?username=' + hiddenUsername);
        }
        else {
            alert("Fail Inserting");
        }
    };

    function CheckResultForRequest(data) {
        if (data.Result == 1) {
            $(location).attr('href', userLink + hiddenUsername);
        }
        else {
            alert("Fail Inserting");
        }
    };
});