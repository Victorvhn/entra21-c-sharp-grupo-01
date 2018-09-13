"use strict";
$(document).ready(function () {
    $('#select-cadastro-historico-viagem-idPacote').select2({
        placeholder: "Selecione um Pacote",
        ajax: {
            url: '/Pacote/ObterTodosPorJSONToSelect2',
            dataType: 'json'
        }
    }).on('change', function (e) {
        $('#form-modal-cadastro-historico-viagem').valid()
    });
});

$(document).ready(function () {
    $('#select-editar-historico-viagem-idPacote').select2({
        placeholder: "Selecione um Pacote",
        ajax: {
            url: '/Pacote/ObterTodosPorJSONToSelect2',
            dataType: 'json'
        }
    }).on('change', function (e) {
        $('#form-modal-ediatar-historico-viagem').valid()
    });
});