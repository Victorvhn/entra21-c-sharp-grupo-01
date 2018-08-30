$(document).ready(function () {
    $('#select-cadastro-cidade-estado').select2({
        ajax: {
            url: '/Cidade/ObterTodosPorJSONToSelect2',
            dataType: 'json',
        }
    });
});