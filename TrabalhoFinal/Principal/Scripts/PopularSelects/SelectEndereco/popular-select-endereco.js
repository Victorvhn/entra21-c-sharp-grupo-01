$(document).ready(function () {
    $('#select-cadastro-endereco-cidade').select2({
        ajax: {
            url: '/Cidade/ObterTodosPorJSONParaSelect2',
            dataType: 'json',

        }
    });
});

$(document).ready(function () {
    $('#select-editar-endereco-cidade').select2({
        ajax: {
            url: '/Cidade/ObterTodosPorJSONParaSelect2',
            dataType: 'json'
        }
    })
})