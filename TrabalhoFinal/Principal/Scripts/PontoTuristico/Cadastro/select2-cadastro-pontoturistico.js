$(document).ready(function () {
    $('#select-modal-cadastro-pontoTuristico').select2({
        ajax: {
            url: '/Estado/ObterTodosPorJSONToSelect2',
            dataType: 'json'
        }
    });
});

$(document).ready(function () {
    $('#select-modal-editar-pontoTuristico').select2({
        ajax: {
            url: '/Estado/ObterTodosPorJSONToSelect2',
            dataType: 'json'
        }
    });
});