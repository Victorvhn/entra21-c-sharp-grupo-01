$(document).ready(function () {
    $('#select-cadastro-pontoturistico-endereco').select2({
        ajax: {
            url: '/PontoTuristico/ObterTodosPorJSONSelect2',
            dataType: 'json',
        }
    });

});