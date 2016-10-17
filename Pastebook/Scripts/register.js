﻿$(document).ready(function () {
    $('#txtUsername').on('blur', function () {
        var data = {
            username: $(this).val()
        };

        $($(this)).validate();

        if ($(this).valid()) {
            $.ajax({
                url: '/Account/CheckUsername',
                data: data,
                type: 'GET',
                success: function (data) {
                    CheckUsername(data);
                },
                error: function () {
                    alert('Something went wrong')
                }

            });
        }
        else {
            $('#msgForUsername').text('');
        }
    });

    $('#txtEmail').on('blur', function () {
        var data = {
            email: $(this).val()
        };

        $($(this)).validate();

        if ($(this).valid()) {
            $.ajax({
                url: '/Account/CheckEmail',
                data: data,
                type: 'GET',
                success: function (data) {
                    CheckEmail(data);
                },
                error: function () {
                    alert('Something went wrong')
                }

            });
        }
        else {
            $('#msgForEmail').text('');
        }
    });

    function CheckUsername(data) {
        if (data.Result == true) {
            $('#msgForUsername').text('Username already exists.');
        } else {
            $('#msgForUsername').text('');
        }
    };

    function CheckEmail(data) {
        if (data.Result == true) {
            $('#msgForEmail').text('Email Address already exists.');
        } else {
            $('#msgForEmail').text('');
        }
    };
});