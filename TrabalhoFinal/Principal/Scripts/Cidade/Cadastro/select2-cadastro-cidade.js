$(document).ready(function () {
    $('#select-cadastro-cidade-estado').select2({
        ajax: {
            url: '/Estado/ObterTodosPorJSONToSelect2',
            dataType: 'json',
        }
    });
});