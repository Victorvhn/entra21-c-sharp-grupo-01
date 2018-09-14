﻿$(document).ready(function () {
    $('#select-destino-pacote-user').select2({
        placeholder: "Selecione um Destino",
        ajax: {
            url: '/Estado/ObterTodosPorJSONToSelect2',
            dataType: 'json'
        }
    });
});