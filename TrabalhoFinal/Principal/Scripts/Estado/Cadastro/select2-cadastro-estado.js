$(document).ready(function () {
    $('#select-cadastro-estado-pais').select2({
        ajax: {
            url: '/Pais/ObterTodosPorJSONToSelect2',
            dataType: 'json',
        }
    });
});