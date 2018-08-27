$(document).ready(function () {
    $('#select-cadastro-historico-viagem-idpacote').select2({
        ajax: {
            url: '/HistoricoViagem/ObterTodosPorJSONToSelect2',
            dataType: 'json',
        }
    });

});