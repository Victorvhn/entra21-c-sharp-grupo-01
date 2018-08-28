$(document).ready(function () {
    $('#select-cadastro-historico-viagem-idpacote').select2({
        ajax: {
            url: '/Pacote/ObterTodosPorJSONToSelect2',
            dataType: 'json',
        }
    });

});