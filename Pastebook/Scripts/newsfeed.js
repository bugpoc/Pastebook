$(document).ready(function () {
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
                    CheckResult(data);
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
                CheckResult(data);
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