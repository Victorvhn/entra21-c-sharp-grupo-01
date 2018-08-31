$(document).ready(function () {
    $('#select-cadastro-cidade-idestado').select2({
        ajax: {
            url: '/Estado/ObterTodosPorJSONToSelect2',
            dataType: 'json',

        }
    });
});