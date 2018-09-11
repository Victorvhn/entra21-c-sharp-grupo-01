//$('#div-modal-cadastro-historico-viagem').validate({
//    rules: {
//        'historicoViagem.IdPacote': {
//            required: true
//        },
//        'historicoViagem.Data': {
//            required: true
//        },
//    },
//    messages: {
//        'historicoViagem.IdPacote': {
//            required: 'Este campo é obrigatório'
//        },
//        'historicoViagem.Data': {
//            required: 'Por favor insira uma data válida'
//        },
//    }
//});

$('#div-modal-cadastro-historico-viagem').validate({
    rules: {
        'select-cadastro-historico-viagem-idPacote': "This field is required."

    }
});