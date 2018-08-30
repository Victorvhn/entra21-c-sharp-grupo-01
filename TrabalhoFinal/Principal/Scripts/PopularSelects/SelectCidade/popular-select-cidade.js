$(document).ready(function () {
    $('#select-cadastro-cidade-estado').select2({
        ajax: {
            url: '/Estado/ObterTodosPorSelect2',
            dataType: 'json',

        }
    });
});