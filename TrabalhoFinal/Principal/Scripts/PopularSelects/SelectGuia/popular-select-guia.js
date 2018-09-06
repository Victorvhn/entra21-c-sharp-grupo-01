$(document).ready(function () {
    $('#select-cadastro-cidade-guia').select2({
        ajax: {
            url: '/Cidade/ObterTodosPorJSONParaSelect2',
            dataType: 'json'
        }
    });
});

$(document).ready(function () {
    $('#select-cadastro-guia-estado').select2({
        ajax: {
            url: '/Estado/ObterTodosPorJSONToSelect2',
            dataType: 'json'
        }
    });
});