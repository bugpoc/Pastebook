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
                //add error
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
                CheckResult(data);
            },
            error: function () {
                //add error
            }
        });
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
            //add fail inserting
        }
    };
});