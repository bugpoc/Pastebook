$(document).ready(function () {
    $('#btnPost').click(function () {
        var varContent = $.trim($('#txtAreacontent').val());
        var data = {
            content: varContent,
            username: ""
        };
        if (varContent.length != 0) {
            $.ajax({
                url: '/Pastebook/SavePost',
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
            $('#txtAreacontent').val('');
            $('#contentMessage').text('');
        }
        else {
            alert("Fail Inserting");
        }
    };
});