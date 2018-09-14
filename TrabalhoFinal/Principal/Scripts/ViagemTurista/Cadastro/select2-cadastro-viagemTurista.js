$(document).ready(function () {
    $('#select-modal-editar-turista').select2({
        ajax: {
            url: '/ViagemTurista/ObterTodosPorJSONToSelect2',
            dataType: 'json'
        }
    });
});

$(document).ready(function () {
    $('#select-modal-editar-viagem').select2({
        ajax: {
            url: '/ViagemTurista/ObterTodosPorJSONToSelect2',
            dataType: 'json'
        }
    });
});
