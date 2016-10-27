$(document).ready(function () {
    $('.view-profile').click(function () {
        var username = $(this).val();
        $(location).attr('href', userLink + username)
    });
});