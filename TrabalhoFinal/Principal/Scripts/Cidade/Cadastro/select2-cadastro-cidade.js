$(document).ready(function () {
    $('#select-modal-cadastro-cidade').select2({
        ajax: {
            url: '/Estado/ObterTodosPorJSONToSelect2',
            dataType: 'json'
        }
    });
});

$(document).ready(function () {
    $('#select-modal-editar-cidade').select2({
        ajax: {
            url: '/Estado/ObterTodosPorJSONToSelect2',
            dataType: 'json'
        }
    });
});