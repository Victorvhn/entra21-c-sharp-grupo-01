﻿$(document).ready(function () {
    $('#select-modal-cadastro-viagem').select2({
        ajax: {
            url: '/Viagem/ObterTodosPorJSONToSelect2',
            dataType: 'json'
        }
    });
});

$(document).ready(function () {
    $('#select-modal-editar-viagem').select2({
        ajax: {
            url: '/Viagem/ObterTodosPorJSONToSelect2',
            dataType: 'json'
        }
    });
});