$(document).ready(function () {
    $('#select-cadastro-pacote-ponto-turistico-pacote').select2({
        ajax: {
            url: '/Pacote/ObterTodosPorJSONToSelect2',
            dataType: 'json'
        }
        }).on('change', function () {
            $('select-cadastro-pacote-ponto-turistico-ponto-turistico').select2('open');
        });

    $('#select-cadastro-pacote-ponto-turistico-ponto-turistico').select2({
        ajax: {
            url: '/PontoTuristico/ObterTodosPorJSONSelect2',
            dataType: 'json'
        }
    });
});
$(document).ready(function () {
    $('select-editar-pacote-ponto-turistico-pacote').select2({
        ajax: {
            url: '/Pacote/ObterTodosPorJSONtoSelect2',
            dataType: 'json'
        }
    });

    $('select-editar-pacote-ponto-turisto-ponto-turistico').select2({
        ajax: {
            url: '/PontoTuristico/ObterTodosPorJSONSelect2',
            dataType: 'json'
        }
    });
});

