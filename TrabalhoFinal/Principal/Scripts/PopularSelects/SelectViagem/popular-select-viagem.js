$(document).ready(function () {
    $('#select-cadastro-viagem-pacote').select2({
        ajax: {
            url: '/Pacote/ObterTodosPorJSONToSelect2',
            dataType: 'json'
        }
    }).on('change', function () {
        $('#select-cadastro-viagem-guia').select2('open');
    });

    $('#select-cadastro-viagem-guia').select2({
        ajax: {
            url: '/Guia/ObterTodosParaSelect2',
            dataType: 'json'
        }
    });
});
$(document).ready(function () {
    $('#select-modal-editar-viagem-pacote').select2({
        ajax: {
            url: '/Pacote/ObterTodosPorJSONToSelect2',
            dataType: 'json'
        }
    });

    $('#select-modal-editar-viagem-guia').select2({
        ajax: {
            url: '/Guia/ObterTodosParaSelect2',
            dataType: 'json'
        }
    });
});