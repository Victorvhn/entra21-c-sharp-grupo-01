$(document).ready(function () {
    $('#select-cadastro-idioma-idguia').select2({
        ajax: {
            url: '/Guia/ObterTodosPorSelect20',
            dataType: 'json',
        }
    });
});