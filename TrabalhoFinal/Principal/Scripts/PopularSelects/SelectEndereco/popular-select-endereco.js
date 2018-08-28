$(document).ready(function () {
    $('#select-cadastro-endereco-cidade').select2({
        ajax: {
            url: '/Endereco/ObterTodosPorJSONToSelect2',
            datatype: 'json',

        }
    });
});