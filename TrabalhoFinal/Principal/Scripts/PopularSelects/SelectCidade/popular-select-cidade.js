$(document).ready(function () {
    $('#select-cadastro-cidade-idestado').select2({
        ajax: {
            url: '/Estado/ObterTodosPorSelect2',
            dataType: 'json',

        }
    });
});