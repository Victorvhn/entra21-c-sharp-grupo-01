$(document).ready(function () {
    $('#select-modal-cadastro-cidade').select2({
        ajax: {
            url: '/Cidade/ObterTodosPorJSONParaSelect2',
            dataType: 'json'
        }
    });
});