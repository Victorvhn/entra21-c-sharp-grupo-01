$(function () {
    //Preenche DataTable
    $('#table-endereco').DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": "/Endereco/ObterTodosPorJSON",
        "columns": [
            { data: 'Id' },
            { data: 'Cep' },
            { data: 'Logradouro' },
            { data: 'Cidade.Nome' }
        ]
    });

    //Abre modal de cadastro
    $('#botao-modal-cadastrar-endereco').on('click', function () {
        limparCamposEnderecoCadastro();
        $('#endereco-modal-cadastro').modal('show');
    });

    //Validação modal Cadastro
    $('#form-modal-cadastro-endereco').validate({
        errorClass: "form-control-danger",
        validClass: "form-control-success",
        highlight: function (element) {
            jQuery(element).closest('.form-group').addClass('has.error');
        },
        errorPlacement: function (element) {
            jQuery(element).parent().append(error[0])
        },
        rules: {
            'endereco.Cep': {
                required: true,
                rangelength: [8]
            },
            'endereco.Lograduro': {
                required: true,
                rangelength: [6, 20]
            },
            'endereco.Numero': {
                required: true
            },
            'endereco.Complemento': {
                required: true
            },
            'endereco.Referencia': {
                required: true,
                rangelength: [5, 30]
            },
            'endereco.IdCidade': {
                required: true
            }
        },
        messages: {
            'endereco.Cep': {
                required: 'CEP deve ser preenchido',
                rangelength: 'CEP deve conter {8} caracteres'
            },
            'endereco.Logradouro': {
                required: 'Logradouro deve ser preenchido',
                rangelength: 'Logradouro deve conter entre {6} e {20} caracteres'
            },
            'endereco.Numero': {
                required: 'Número deve ser preenchido'
            },
            'endereco.Complemento': {
                required: 'Complemento deve ser preenchido'
            },
            'endereco.Referencia': {
                required: 'Referência deve ser preenchido',
                rangelength: 'Referência deve conter entre {5} e {30} caracteres'
            },
            'endereco.IdCidade': {
                required: 'Cidade deve ser preenchido'
            }
        }

    });
});