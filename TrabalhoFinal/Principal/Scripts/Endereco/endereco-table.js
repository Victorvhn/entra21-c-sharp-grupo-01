$(function () {
    //Preenche DataTable
    $('#table-endereco').DataTable({
        ajax: "/Endereco/ObterTodosPorJSON",
        columns: [
            { data: 'Id'},
            { data: 'Cep' },
            { data: 'Logradouro' },
            { data: 'Cidade.Nome' },
            {
                data: null,
                "bSortable": false, "width": "20%",
                render: function (data, type, row) {
                    return '<a class="btn btn-outline-info botao-editar-endereco" data-id="' + row.Id + '" data-toggle="modal" data-target="#endereco-modal-editar">Editar</a>' +
                        '<a class="btn btn-outline-danger ml-1 botao-excluir-endereco" data-id="' + row.Id + '">Desativar</a>';
                }
            }
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
        unhighlight: function (element) {
            jQuery(element).closest('.form-group').removeClass('has-error');
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
    //Salvar Modal Cadastro
    $('#botao-salvar-modal-cadastro-esdereco').on('click', function () {
        if ($('#form-modal-cadastro-endereco').valid()) {
            $.ajax({
                url: '/Endereco/Store',
                method: 'post',
                data: {
                    logradouro: $('campo-cadastro-endereco-logradouro').val(),
                    numero: $('campo-cadastro-endereco-numero').val(),
                    complemento: $('campo-cadastro-endereco-complemento').val(),
                    referencia: $('campo-cadastro-endereco-referencia').val(),
                    idCidade: $('#select-cadastro-endereco-cidade').val(),
                },
                success: function (data) {
                    var resultado = JSON.parse(data);
                    limparCamposEnderecoCadastro();
                    $('#endereco-modal-cadastro').modal('hide');
                    $('#table-endereco').DataTable().ajax.reload();
                    $(function () {
                        new PNotify({
                            title: 'Sucesso!',
                            text: 'Endereço Cadastrado com sucesso',
                            type: 'success'
                        });
                    });
                }
            });
        }
    });

    //Botão Editar
    $('table').on('click', '.botao-editar-endereco', function () {
        var id = $(this).data('id');
        $.ajax({
            url: '/Endereco/Editar?id=' + id,
            method: 'get',
            success: function (resultado) {
                var data = JSON.parse(resultado);
                $('campo-editar-endereco-id').val(data.Id);
                $('campo-editar-endereco-logradouro').val(data.Logradouro);
                $('campo-editar-endereco-numero').val(data.Numero);
                $('campo-editar-endereco-complemento').val(data.Complemento);
                $('campo-editar-endereco-referencia').val(data.Referencia);
                $('#select-editar-endereco-cidade').append(new Option(data.Cidade.Nome, data.IdCidade, false, false)).val(data.IdCidade).trigger('change');

                $('#endereco-modal-editar').modal('show');
            }
        });
    });

    //Validação Editar
    $('#form-modal-editar-endereco').validate({
        errorClass: "form-control-danger",
        validClass: "form-control-success",
        highlight: function (element) {
            jQuery(element).closest('.form-group').addClass('has-error');
        },
        unhighlight: function (element) {
            jQuery(element).closest('.form-group').removeClass('has-error');
        },
        errorPlacement: function (error, element) {
            $(element).parent().append(error[0])
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

    //Update Modal Editar
    $('#botao-salvar-modal-editar-endereco').on('click', function () {
        if ($('#form-modal-editar-endereco').valid()) {
            $.ajax({
                url: '/Endereco/Update',
                method: 'post',
                dataType: 'json',
                data: {
                    id: $('campo-editar-endereco-id').val(),
                    logradouro: $('campo-editar-endereco-logradouro').val(),
                    numero: $('campo-editar-endereco-numero').val(),
                    complemento: $('campo-editar-endereco-complemento').val(),
                    referencia: $('campo-editar-endereco-referencia').val(),
                    idCidade: $('#select-editar-endereco-cidade').val()
                },
                success: function (data) {
                    var resultado = JSON.parse(data);
                    if (resultado == 1) {
                        $(function () {
                            new PNotify({
                                title: 'Sucesso!',
                                text: 'Endereço alterado com sucesso',
                                type: 'info'
                            });
                        });
                        $('#table-endereco').DataTable().ajax.reload();
                        $('#endereco-modal-editar').modal('hide');
                        limparCamposEnderecoEditar();
                    } else {
                        new PNotify({
                            title: 'Erro!',
                            text: 'Erro ao alterar',
                            type: 'error'
                        })
                    }
                }
            });
        }
    });

    //Desativar
    $('table').on('click', '.botao-excluir-endereco', function () {
        var id = $(this).data('id');
        $.ajax({
            url: '/Endereco/Excluir?id=' + id,
            method: 'get',
            success: function (data) {
                var resultado = JSON.parse(data);
                if (resultado == 1) {
                    new PNotify({
                        title: 'Desativado!',
                        text: 'Endereço desativado com sucesso',
                        type: 'success'
                    });
                    $('#table-endereco').DataTable().ajax.reload();
                } else {
                    new PNotify({
                        title: 'Erro!',
                        text: 'Erro ao desativas Endereço',
                        type: 'error'
                    });
                }
            }
        });
    });

    function limparCamposEnderecoCadastro() {
        $('campo-cadastro-endereco-id').val('');
        $('campo-cadastro-endereco-logradouro').val('');
        $('campo-cadastro-endereco-numero').val('');
        $('campo-cadastro-endereco-complemento').val('');
        $('campo-cadastro-endereco-referencia').val('');
        $('#select-cadastro-endereco-cidade').val('').trigger('change');
    }

    function limparCamposEnderecoEditar() {
        $('campo-editar-endereco-id').val('');
        $('campo-editar-endereco-logradouro').val('');
        $('campo-editar-endereco-numero').val('');
        $('campo-editar-endereco-complemento').val('');
        $('campo-editar-endereco-referencia').val('');
        $('#select-editar-endereco-cidade').val('').trigger('change');
    }
});