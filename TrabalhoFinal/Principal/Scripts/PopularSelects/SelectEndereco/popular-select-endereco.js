$(document).ready(function () {
    $('#select-cadastro-endereco-cidade').select2({
        ajax: {
            url: '/Cidade/ObterTodosPorJSONParaSelect2',
            datatype: 'json',

        }
    });
});