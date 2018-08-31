$(document).ready(function () {
    $('#select-cadastro-ponto-turistico-endereco').select2({
        ajax: {
            url: '/Endereco/ObterTodosPorSelect2',
            dataType: 'json',
        }
    });
});
       