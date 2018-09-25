﻿function loadStrings(controller) {
    window.STRINGS = {};

    // Executa um ajax e busta no controler Base as strings
    $(document).ready(function () {
        $.ajax({
            url: '/' + controller + '/GetStrings/',
            type: 'POST',
            dataType: 'json',
            async: false,
            contentType: 'application/json',
            success: function (response) {
                if (response) {
                    // Armazena as strings na GLOBAL "STRINGS".
                    window.STRINGS = response;

                    for (var key in response) {
                        if ($.validator.messages[key]) {
                            $.validator.messages[key] = response[key];
                        }
                    }
                }
            }
        })
    })
}