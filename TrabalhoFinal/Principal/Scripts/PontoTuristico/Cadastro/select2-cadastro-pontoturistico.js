$(document).ready(function () {
    $('#select-cadastro-pontoturistico-endereco').select2({
        ajax: {
            url: '/Endereco/ObterTodosPorJSONSelect2',
            dataType: 'json',
        }
    });

});