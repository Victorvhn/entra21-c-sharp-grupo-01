$(function () {
    //Preenche DataTable
    $('#pacote-tabela').DataTable({
        ajax: "/Pacote/ObterTodosPorJSON",
        order: [[1, "asc"]],
        columns: [
            {
                data: "Id",
                bSortable: false,
                width: "10%",
                target: 0
            },
            {
                data: "Nome",
                bSortable: true,
                width: "30%",
                target: 1
            },
            {
                data: "Valor",
                bSortable: true,
                width: "20%",
                target: 2
            },
            {
                data: "PercentualMaximoDesconto",
                bSortable: true,
                width: "20%",
                target: 3
            },
            {
                data: null,
                bSortable: false,
                width: "20%",
                render: function (data, type, row) {
                    return "<a class='btn btn-outline-info botao-editar-pacote' data-id='" + row.Id + "' data-nome='" + row.Nome + "'>Editar</a>" +
                        "<a class='btn btn-outline-danger ml-1 botao-excluir-pacote' data-id='" + row.Id + "' data-nome='" + row.Nome + "'>Desativar</a>";
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
                required: STRINGS.pacotePreenchido,
                rangelength: STRINGS.pacoteConter
            },
            'Pacote.Valor': {
                required: STRINGS.valorPreenchido,
                range: STRINGS.valorDeveSer,
                number: STRINGS.valorInteiro
            },
            'Pacote.PercentualMaximoDesconto': {
                required: STRINGS.percentualPreenchido,
                range: STRINGS.percentualDeveSer,
                number: STRINGS.percentualConter
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
                            title: STRINGS.sucesso,
                            text: nomeVar + " " + STRINGS.cadastrado,
                            type: 'success'
                        });
                    });
                }
            });
        }
    });

     //Botao editar
    $('table').on('click', '.botao-editar-pacote', function () {
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
                required: STRINGS.pacotePreenchido,
                rangelength: STRINGS.pacoteConter
            },
            'Pacote.Valor': {
                required: STRINGS.valorPreenchido,
                range: STRINGS.valorDeveSer,
                number: STRINGS.valorInteiro
            },
            'Pacote.PercentualMaximoDesconto': {
                required: STRINGS.percentualPreenchido,
                range: STRINGS.percentualDeveSer,
                number: STRINGS.percentualConter
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
                                title: STRINGS.sucesso,
                                text: STRINGS.alterado,
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
    $('table').on('click', '.botao-excluir-pacote', function () {
        var id = $(this).data('id');
        var nome = $(this).data('nome');
        $.ajax({
            url: 'Pacote/Excluir?id=' + id,
            method: 'get',
            success: function (data) {
                var resultado = JSON.parse(data);
                if (resultado == 1) {
                    new PNotify({
                        title: STRINGS.desativado,
                        text: nome + " " + STRINGS.desativadoSucesso,
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
        $('#campo-cadastro-percentualMaximoDesconto').val('');       
    }
});