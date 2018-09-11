$('#form-modal-cadastro-pacote')({
    rules: {
        'Pacote.Nome': {

            required: true,
            rangelength: [2, 30]
        },
        'Pacote.Valor': {
            required: true
        },
        'Pacote.PercentualMaximoDesconto': {
            required: true
        },
    },
    messages: {
        'Pacote.Nome': {
            required: 'Este campo é obrigatório'
        },
        'Pacote.Valor': {
            required: 'Este campo é obrigatório'
        },
        'Pacote.PercentualMaximoDesconto': {
            required: 'Este campo é obrigatório'
        },
    }
});