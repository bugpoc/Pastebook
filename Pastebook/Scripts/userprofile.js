$(document).ready(function () {

    var hiddenUsername = $('#hiddenUsername').val();

    $('#btnUpdateAboutMe').click(function () {
        $('#myModal').modal('show');
    })

    $('#btnPost').click(function () {
        var varContent = $.trim(escapeHtml($('#txtAreacontent').val()));
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
                    CheckResultForPost(data);
                },
                error: function () {
                    //add error
                }
            });
        }
        else if (varContent.length>1000)
        {
            $('#contentMessage').text('The maximum content of post is 1000 characters only.');
        }
        else {
            $('#contentMessage').text('You cannot post an empty content.');
        }
    });

    $('#btnAboutMe').click(function () {
        var varAboutMe = $.trim(escapeHtml($('#txtAreaAboutMe').val()));
        var data = {
            aboutMe: varAboutMe,
            username: $('#hiddenUsername').val()
        };
        if (varAboutMe.length != 0 && varAboutMe.length <= 2000) {
            $.ajax({
                url: updateAboutMeUrl,
                data: data,
                type: 'GET',
                success: function (data) {
                    CheckResultForAboutMe(data);
                },
                error: function () {
                    //add error
                }
            });
        }
        else if (varAboutMe.length > 2000) {
            $('#aboutMeMessage').text('The maximum content of about me is 2000 characters only.');
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
                //add error
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
                    CheckResultForLikeAndComment(data);
                },
                error: function () {
                    //add error
                }
            });
        }
        else if (comment.length>1000) {
            $('#commentMessage').text('The maximum comment is 1000 characters only.');
        }
        else {
            $('#commentMessage').text('You cannot post a comment with an empty content.');
        }
    });

    $('#btnAddFriend').click(function () {
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
                //add error
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
                //add error
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
                //add error
            }

        });
    });

    $(document).on('click', ".showLikeModal", function () {
        var id = $(this).val();
        $('#likeModal_' + id).modal('show');
    });

    //http://stackoverflow.com/questions/14852090/jquery-check-for-file-extension-before-uploading
    $("#formUploadPicture").submit(function (submitEvent) {

        var imageSize = $('#imageType')[0].files[0].size;

        // get the file name, possibly with path (depends on browser)
        var filename = $("#imageType").val();

        // Use a regular expression to trim everything before final dot
        var extension = filename.replace(/^.*\./, '');

        // Iff there is no dot anywhere in filename, we would have extension == filename,
        // so we account for this possibility now
        if (extension == filename) {
            extension = '';
        }
        else {
            // if there is an extension, we convert to lower case
            // (N.B. this conversion will not effect the value of the extension
            // on the file upload.)
            extension = extension.toLowerCase();
        }


        if (imageSize > 2097152) {
            $('#messageImageType').text("Allowed file size is only 2 mb.");
            submitEvent.preventDefault();
        }
        else
        {
            switch (extension) {
                case 'jpg':
                case 'jpeg':
                case 'png':
                    break;

                default:
                    $('#messageImageType').text(".jpg, .jpeg and .png are the only allowed extension.");
                    // Cancel the form submission
                    submitEvent.preventDefault();
            }
        }
    });

    function CheckResultForAboutMe(data) {
        if (data.Result == 1) {
            $(location).attr('href', userLink + hiddenUsername);
        }
        else {
            //add fail inserting
        }
    };

    function CheckResultForPost(data) {
        if (data.Result == 1) {
            $('#timelinePartial').load(timelinePartialUrl + '?username=' + hiddenUsername);
            $('#txtAreacontent').val('');
            $('#contentMessage').text('');
        }
        else {
            //add fail inserting
        }
    };

    function CheckResultForLikeAndComment(data) {
        if (data.Result == 1) {
            $('#timelinePartial').load(timelinePartialUrl + '?username=' + hiddenUsername);
        }
        else {
            //add fail inserting
        }
    };

    function CheckResultForAddFriend(data) {
        if (data.Result == 1) {
            $('#addFriendPartial').load(addfriendPartialUrl + '?username=' + hiddenUsername);
        }
        else {
            //add fail inserting
        }
    };

    function CheckResultForRequest(data) {
        if (data.Result == 1) {
            $(location).attr('href', userLink + hiddenUsername);
        }
        else {
            //add fail inserting
        }
    };
});