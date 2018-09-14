$(function () {
    //Preenche DataTable
    $('#pacote-tabela').DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": "/Pacote/ObterTodosPorJSON",
        "columns": [
            { "data": "Id" },
            { "data": "Nome" },
            { "data": "Valor" },
            { "data": "PercentualMaximoDesconto" },
            {
                data: null,
                render: function (data, type, row) {
                    return "<a class='btn btn-outline-info' id='botao-editar-pacote' data-id='" + row.Id + "' data-nome='" + row.Nome + "'>Editar</a>" +
                        "<a class='btn btn-outline-danger ml-1' id='botao-excluir-pacote' data-id='" + row.Id + "' data-nome='" + row.Nome + "'>Desativar</a>";
                }
            }
        ]
    });

      //Abre modal de cadastro
    $('#botao-modal-cadastrar-pacote').on('click', function () {
        limparCampos();
        $('#pacote-modal-cadastro').modal('show');
    });

     //Validacao modal cadastro
    $('#form-modal-cadastro-pacote').validate({
        errorClass: "form-control-danger",
        validClass: "form-control-success",
        rules: {
            'Pacote.Nome': {
                required: true,
                rangelength: [3, 30]
            },
            'Pacote.Valor': {
                required: true,
                range: [500, 12000],
                number: true
            },
            'Pacote.PercentualMaximoDesconto': {
                required: true,
                range: [1, 100],
                number: true
            }
        },
        messages: {
            'Pacote.Nome': {
                required: 'Pacote deve ser preenchido.',
                rangelength: 'Pacote deve conter de {0} a {1} caracteres.'
            },
            'Pacote.Valor': {
                required: 'Valor deve ser preenchido.',
                range: 'Valor deve ser de R$ {0} a R$ {1}.',
                number: 'Valor deve conter números inteiros ou decimais'
            },
            'Pacote.PercentualMaximoDesconto': {
                required: 'Percentual maximo de desconto deve ser preenchido.',
                range: 'Percentual maximo de desconto deve ser de {0}% a {1}%.',
                number: 'Percentual maximo de desconto deve conter números inteiros ou decimais.'
            }
        }
    });

    //Salvar modal cadastro
    $('#botao-salvar-modal-cadastrar-pacote').on('click', function () {
        if ($('#form-modal-cadastro-pacote').valid()) {
            var nomeVar = $("#campo-cadastro-pacote-nome").val();
            $.ajax({
                url: '/Pacote/Store',
                method: 'post',
                data: {
                    nome: $('#campo-cadastro-pacote-nome').val(),
                    valor: $('#campo-cadastro-pacote-valor').val(),
                    percentualMaximoDesconto: $('#campo-cadastro-percentualMaximoDesconto').val()
                },
                success: function (data) {
                    var resultado = JSON.parse(data);
                    limparCampos();
                    $('#pacote-modal-cadastro').modal('hide');
                    $('#pacote-tabela').DataTable().ajax.reload();
                    $(function () {
                        new PNotify({
                            title: 'Sucesso!',
                            text: nomeVar + ' cadastrado com sucesso',
                            type: 'success'
                        });
                    });
                }
            });
        }
    });

     //Botao editar
    $('table').on('click', '#botao-editar-pacote', function () {
        var id = $(this).data('id');
        $.ajax({
            url: 'Pacote/Editar?id=' + id,
            success: function (resultado) {
                var data = JSON.parse(resultado);
                $('#campo-editar-pacote-id').val(data.Id);
                $('#campo-editar-pacote-nome').val(data.Nome);
                $('#campo-editar-pacote-valor').val(data.Valor);
                $('#campo-editar-pacote-percentualMaximoDesconto').val(data.PercentualMaximoDesconto);

                $('#pacote-modal-editar').modal('show');
            }
        });
    });

    // Validação editar
    $('#form-modal-editar-pacote').validate({
        errorClass: "form-control-danger",
        validClass: "form-control-success",
        rules: {
            'Pacote.Nome': {
                required: true,
                rangelength: [3, 30]
            },
            'Pacote.Valor': {
                required: true,
                range: [500, 12000],
                number: true
            },
            'Pacote.PercentualMaximoDesconto': {
                required: true,
                range: [1, 100],
                number: true
            }
        },
        messages: {
            'Pacote.Nome': {
                required: 'Pacote deve ser preenchido.',
                rangelength: 'Pacote deve conter de {0} a {1} caracteres.'
            },
            'Pacote.Valor': {
                required: 'Valor deve ser preenchido.',
                range: 'Valor deve ser de R$ {0} a R$ {1}.',
                number: 'Valor deve conter números inteiros ou decimais'
            },
            'Pacote.PercentualMaximoDesconto': {
                required: 'Percentual maximo de desconto deve ser preenchido.',
                range: 'Percentual maximo de desconto deve ser de {0}% a {1}%.',
                number: 'Percentual maximo de desconto deve conter números inteiros ou decimais.'
            }
        }
    });
    

    //Update modal editar
    $('#botao-salvar-modal-editar-pacote').on('click', function () {
        if ($('#form-modal-editar-pacote').valid()) {
            $.ajax({
                url: 'Pacote/Update',
                method: 'post',
                dataType: 'json',
                data: {
                    id: $('#campo-editar-pacote-id').val(),
                    nome: $('#campo-editar-pacote-nome').val(),
                    valor: $('#campo-editar-pacote-valor').val(),
                    percentualMaximoDesconto: $('#campo-editar-pacote-percentualMaximoDesconto').val()
                },
                success: function (data) {
                    var resultado = JSON.parse(data);
                    if (resultado == 1) {
                        $('#pacote-tabela').DataTable().ajax.reload();
                        $(function () {
                            new PNotify({
                                title: 'Sucesso!',
                                text: 'Alterado com sucesso',
                                type: 'info'
                            });
                        });
                        $('#pacote-modal-editar').modal('hide');
                        limparCampos();
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
    $('table').on('click', '#botao-excluir-pacote', function () {
        var id = $(this).data('id');
        var nome = $(this).data('nome');
        $.ajax({
            url: 'Pacote/Excluir?id=' + id,
            method: 'get',
            success: function (data) {
                var resultado = JSON.parse(data);
                if (resultado == 1) {
                    new PNotify({
                        title: 'Desativado!',
                        text: nome + ' desativado com sucesso',
                        type: 'success'
                    });

                    $('#pacote-tabela').DataTable().ajax.reload();

                } else {
                    new PNotify({
                        title: 'Erro!',
                        text: 'Erro ao desativar ' + nome,
                        type: 'error'
                    });
                }
            }
        });
    });


    function limparCampos() {
        $('#campo-cadastro-pacote-nome').val('');
        $('#campo-cadastro-pacote-valor').val('');
        $('#campo-cadastro-pacote-percentualMaximoDesconto').val('');
        $('#campo-editar-pacote-id').val('');
        $('#campo-editar-pacote-nome').val('');
        $('#campo-editar-pacote-valor').val('');
        $('#campo-editar-pacote-percentualMaximoMesconto').val('');
    }
});