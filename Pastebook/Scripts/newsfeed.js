$(document).ready(function () {
    $('#btnPost').click(function () {
        var varContent = $.trim($('#content').val());
        var data = {
            content: varContent
        };
        if (varContent.length != 0) {
            $.ajax({
                url: '/Pastebook/SaveNewsFeedPost',
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

    function CheckResult(data) {
        if (data.Result == 1) {
            $('#newsFeedPartial').load('/Pastebook/NewsFeedPartial');
            $('#content').val('');
            $('#contentMessage').text('');
        }
        else {
            alert("Fail Inserting");
        }
    };
});