$(document).ready(function () {
    $('#select-cadastro-viagem-pacote').select2({
        ajax: {
            url: '/Pacote/ObterTodosPorJSONToSelect2',
            dataType: 'json'
        }
    }).on('change', function () {
        $('#form-modal-cadastro-viagem').valid();
        $('#select-cadastro-viagem-guia').select2('open');

    });

    $('#select-cadastro-viagem-guia').select2({
        ajax: {
            url: '/Guia/ObterTodosParaSelect2',
            dataType: 'json'
        }
    }).on('change', function () {
        $('#form-modal-cadastro-viagem').valid();
    });
});

$(document).ready(function () {
    $('#select-modal-editar-viagem-pacote').select2({
        ajax: {
            url: '/Pacote/ObterTodosPorJSONToSelect2',
            dataType: 'json'
        }
    }).on('change', function () {
        $('#form-modal-editar-viagem').valid();
    });

    $('#select-modal-editar-viagem-guia').select2({
        ajax: {
            url: '/Guia/ObterTodosParaSelect2',
            dataType: 'json'
        }
    }).on('change', function () {
        $('#form-modal-editar-viagem').valid();
    });
});