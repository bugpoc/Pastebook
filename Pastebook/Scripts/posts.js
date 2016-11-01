$(document).ready(function () {

    var postID = $('#txtPostID').val();

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
                CheckResult(data);
            },
            error: function () {
                $('#messageError').text('Failed to like/comment to the post.');
                $('#errorModal').modal('show');
            }

        });
    });

    $(document).on('click', ".btnComment", function () {
        var id = $(this).val();
        var comment = $.trim(escapeHtml($('#' + id).val()));

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
                    CheckResult(data);
                },
                error: function () {
                    $('#messageError').text('Failed to like/comment to the post.');
                    $('#errorModal').modal('show');
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
            $(location).attr('href', postUrl + postID);
        }
        else {
            $('#messageError').text('Failed to like/comment to the post.');
            $('#errorModal').modal('show');
        }
    };
});