$(document).ready(function () {
    $('#btnPost').click(function () {
        var varContent = $.trim($('#txtAreacontent').val());
        var data = {
            content: varContent,
            username: $('#hiddenUsername').val()
        };
        if (varContent.length != 0 && varContent.length <= 1000) {
            $.ajax({
                url: savePostUrl,
                data: data,
                type: 'GET',
                success: function (data) {
                    CheckResult(data);
                },
                error: function () {
                    $(location).attr('href', errorLink);
                }
            });
        }
        else if (varContent.length > 1000) {
            $('#contentMessage').text('The maximum content of post is 1000 characters only.');
        }
        else {
            $('#contentMessage').text('You cannot post an empty content.');
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
                $(location).attr('href', errorLink);
            }

        });
    });

    $(document).on('click', ".btnComment", function () {
        var id = $(this).val();
        var comment = $.trim($('#' + id).val());

        var data = {
            postID: $(this).val(),
            content: comment
        };
        if (comment.length > 0 && comment.length <= 1000) {
            $.ajax({
                url: commentToPostUrl,
                data: data,
                type: 'GET',
                success: function (data) {
                    CheckResultForLikeAndComment(data);
                },
                error: function () {
                    $(location).attr('href', errorLink);
                }
            });
        }
        else if (comment.length > 1000) {
            $('#commentMessage').text('The maximum comment is 1000 characters only.');
        }
        else {
            $('#commentMessage').text('You cannot post a comment with an empty content.');
        }
    });

    $(document).on('click', ".showLikeModal", function () {
        var id = $(this).val();
        $('#likeModal_' + id).modal('show');
    });

    function CheckResult(data) {
        if (data.Result == 1) {
            $('#newsFeedPartial').load(newsFeedPartialUrl);
            $('#txtAreacontent').val('');
            $('#contentMessage').text('');
        }
        else {
            alert("Fail Inserting");
        }
    };
});