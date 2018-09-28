﻿$(document).ready(function () {
    $('#select-modal-cadastro-cidade').select2({
        placeholder: STRINGS.selecioneEstado,
        ajax: {
            url: '/Estado/ObterTodosPorJSONToSelect2',
            dataType: 'json'
        }
    }).on('change', function (e) {
        $('#form-modal-cadastro-cidade').valid()
    });
});

$(document).ready(function () {
    $('#select-modal-editar-cidade').select2({
        placeholder: STRINGS.selecioneEstado,
        ajax: {
            url: '/Estado/ObterTodosPorJSONToSelect2',
            dataType: 'json'
        }
    }).on('change', function (e) {
        $('#form-modal-editar-cidade').valid()
    });
});