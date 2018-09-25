$(function () {
    //Preenche DataTable
    $('#table-endereco').DataTable({
        ajax: '/Endereco/ObterTodosPorJSON',
        "order": [[2, "asc"]],
        columns: [
            {
                data: 'Id',
                bSortable: false,
                width: "10%"
            },
            {
                data: 'Cep',
                bSortable: true,
                width: "15%",
                target: 0
            },
            {
                data: 'Logradouro',
                bSortable: true,
                width: "30%",
                target: 1
            },
            {
                data: 'Cidade.Nome',
                bSortable: true,
                width: "25%",
                target: 2
            },
            {
                data: null,
                bSortable: false,
                width: "20%",
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
     errorClass: 'form-control-danger',
        validClass: 'form-control-success',
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
                rangelength: [8, 8]
            },
            'endereco.Logradouro': {
                required: true,
                rangelength: [6, 30]
            },
            'endereco.Numero': {
                required: true,
                digits:true
            },                                    
            'endereco.IdCidade': {
                required: true
            }
        },
        messages: {
            'endereco.Cep': {
                required: STRINGS.cepPreenchido,
                rangelength: STRINGS.cepDeveConter
            },
            'endereco.Logradouro': {
                required: STRINGS.logPreenchido,
                rangelength: STRINGS.logDeveConter
            },
            'endereco.Numero': {
                required: STRINGS.numPreenchido,
                digits: STRINGS.numSomenteDig
            },            
            'endereco.IdCidade': {
                required: STRINGS.cidadePreenchido
            }
        }

    });
    //Salvar Modal Cadastro
    $('#botao-salvar-modal-cadastrar-endereco').on('click', function () {
        if ($('#form-modal-cadastro-endereco').valid()) {
            $.ajax({
                url: '/Endereco/Store',
                method: 'post',
                data: {
                    cep: $('#campo-cadastro-endereco-cep').val(),
                    logradouro: $('#campo-cadastro-endereco-logradouro').val(),
                    numero: $('#campo-cadastro-endereco-numero').val(),
                    complemento: $('#campo-cadastro-endereco-complemento').val(),
                    referencia: $('#campo-cadastro-endereco-referencia').val(),
                    idCidade: $('#select-cadastro-endereco-cidade').val(),
                },
                success: function (data) {
                    var resultado = JSON.parse(data);
                    $('#table-endereco').DataTable().ajax.reload();
                    $('#endereco-modal-cadastro').modal('hide');
                    $(function () {
                        new PNotify({
                            title: 'Sucesso!',
                            text: STRINGS.enderecoCadastrado,
                            type: 'success'
                        });
                    });
                    limparCamposEnderecoCadastro();
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
                $('#campo-editar-endereco-cep').val(data.Cep);
                $('#campo-editar-endereco-id').val(data.Id);
                $('#campo-editar-endereco-logradouro').val(data.Logradouro);
                $('#campo-editar-endereco-numero').val(data.Numero);
                $('#campo-editar-endereco-complemento').val(data.Complemento);
                $('#campo-editar-endereco-referencia').val(data.Referencia);
                $('#select-editar-endereco-cidade').append(new Option(data.Cidade.Nome, data.IdCidade, false, false)).val(data.IdCidade).trigger('change');

                $('#endereco-modal-editar').modal('show');
            }
        });
    });

    //Validação Editar
    $('#form-modal-editar-endereco').validate({
        errorClass: 'form-control-danger',
        validClass: 'form-control-success',
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
                rangelength: [8, 8]
            },
            'endereco.Logradouro': {
                required: true,
                rangelength: [6, 40]
            },
            'endereco.Numero': {
                required: true
            },
            'endereco.IdCidade': {
                required: true
            }
        },
        messages: {
            'endereco.Cep': {
                required: STRINGS.cepPreenchido,
                rangelength: STRINGS.cepDeveConter
            },
            'endereco.Logradouro': {
                required: STRINGS.logPreenchido,
                rangelength: STRINGS.logDeveConter
            },
            'endereco.Numero': {
                required: STRINGS.numPreenchido,
                digits: STRINGS.numSomenteDig
            },
            'endereco.IdCidade': {
                required: STRINGS.cidadePreenchido
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
                    id: $('#campo-editar-endereco-id').val(),
                    cep: $('#campo-editar-endereco-cep').val(),
                    logradouro: $('#campo-editar-endereco-logradouro').val(),
                    numero: $('#campo-editar-endereco-numero').val(),
                    complemento: $('#campo-editar-endereco-complemento').val(),
                    referencia: $('#campo-editar-endereco-referencia').val(),
                    idCidade: $('#select-editar-endereco-cidade').val()
                },
                success: function (data) {
                    var resultado = JSON.parse(data);
                    if (resultado == 1) {
                        $(function () {
                            new PNotify({
                                title: 'Sucesso!',
                                text: STRINGS.enderecoAlterado,
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
                        });
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
                        title: 'Desativado',
                        text: STRINGS.enderecoDesativado,
                        type: 'success'
                    });
                    $('#table-endereco').DataTable().ajax.reload();
                } else {
                    new PNotify({
                        title: 'Erro!',
                        text: 'Erro ao desativar Endereço',
                        type: 'error'
                    });
                }
            }
        });
    });

    function limparCamposEnderecoCadastro() {
        $('#campo-cadastro-endereco-id').val('');
        $('#campo-cadastro-endereco-cep').val('');
        $('#campo-cadastro-endereco-logradouro').val('');
        $('#campo-cadastro-endereco-numero').val('');
        $('#campo-cadastro-endereco-complemento').val('');
        $('#campo-cadastro-endereco-referencia').val('');
        $('#select-cadastro-endereco-cidade').val('').trigger('change');
    }

    function limparCamposEnderecoEditar() {
        $('#campo-editar-endereco-id').val('');
        $('#campo-cadastro-endereco-cep').val('');
        $('#campo-editar-endereco-logradouro').val('');
        $('#campo-editar-endereco-numero').val('');
        $('#campo-editar-endereco-complemento').val('');
        $('#campo-editar-endereco-referencia').val('');
        $('#select-editar-endereco-cidade').val('').trigger('change');
    }
});