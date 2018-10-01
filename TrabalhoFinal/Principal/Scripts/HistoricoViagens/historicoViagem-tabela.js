$(function () {
    //Preenche DataTable
    $('#historico-viagem-tabela').DataTable({
        ajax: "/HistoricoViagem/ObterTodosPorJSON",
        order: [[1, "asc"]],
        columns: [
            {
                data: "Id",
                bSortable: false,
                width: "10%",
                target: 0
            },
            {
                data: "Pacote.Nome",
                bSortable: true,
                width: "40%",
                target: 1
            },
            {
                data: "DataPadraoBR",
                bSortable: true,
                width: "30%",
                target: 2
            },
            {
                data: null,
                bSortable: false,
                width: "20%",
                render: function (data, type, row) {
                    return "<a style='font-size: 24px;' class='btn fa fa-edit botao-editar-historico-viagem' data-id='" + row.Id + "' ></a>" +
                        "<a style='font-size: 24px;' class='btn fa fa-trash ml-1 botao-excluir-historico-viagem' data-id='" + row.Id + "' href='#' ></a>";
                }
            }
        ]
    });


    //Abre modal de cadastro
    $('#botao-modal-cadastrar-historico-viagem').on("click", function () {
        limparCampos();
        $("#historico-viagem-modal-cadastro").modal('show');
    });

    //Validação modal cadastro
    $('#form-modal-cadastro-historico-viagem').validate({
        errorClass: "form-control-danger",
        validClass: "form-control-success",
        highlight: function (element) {
            jQuery(element).closest('.form-group').addClass('has-error');
            jQuery(element).addClass("form-control-danger");
            jQuery(element).removeClass("form-control-success");
        },
        unhighlight: function (element) {
            jQuery(element).closest('.form-group').removeClass('has-error');
            jQuery(element).removeClass("form-control-danger");
            jQuery(element).addClass("form-control-success");
        },
        errorPlacement: function (error, element) {
            $(element).parent().append(error[0])
        },

        rules: {
            'historicoViagem.IdPacote': {
                required: true
            },
            'historicoViagem.Data': {
                required: true,
                date: true
            }
        },
        messages: {
            'historicoViagem.IdPacote': {
                required: STRINGS.selecionePacote
            },
            'historicoViagem.Data': {
                required: STRINGS.informeData,
                date: STRINGS.dataValida
            }
        }

    });

    //Salvar modal cadastro
    $('#botao-salvar-modal-cadastrar-historico-viagem').on('click', function () {
        if ($('#form-modal-cadastro-historico-viagem').valid()) {
            var PacoteVar = $("#select-cadastro-historico-viagem-idPacote").text();
            $.ajax({
                url: '/HistoricoViagem/Store',
                method: 'post',
                data: {
                    data: $('#campo-cadastro-historico-viagem-data').val(),
                    idPacote: $('#select-cadastro-historico-viagem-idPacote :selected').val()
                },
                success: function (data) {
                    var resultado = JSON.parse(data);
                    $('#historico-viagem-modal-cadastro').modal('hide');
                    $('#historico-viagem-tabela').DataTable().ajax.reload();
                    $(function () {
                        new PNotify({
                            title: STRINGS.sucesso,
                            text: PacoteVar + " " + STRINGS.cadastro,
                            type: 'success'
                        });
                    });
                    limparCampos();
                }
            });
        }
    });

    //Botao editar
    $('table').on('click', '.botao-editar-historico-viagem', function () {
        var id = $(this).data('id');
        $.ajax({
            url: '/HistoricoViagem/Editar?id=' + id,
            method: 'get',
            success: function (resultado) {
                var data = JSON.parse(resultado);
                $('#campo-editar-historico-viagem-id').val(data.Id);
                console.log(resultado);
                $('#select-editar-historico-viagem-idPacote').append(new Option(data.Pacote.Nome, data.IdPacote, false, false)).val(data.IdPacote).trigger('change');
                $('#campo-editar-historico-viagem-data').val(data.DataPadraoBR);

                $('#historico-viagem-modal-editar').modal('show');
            }
        });
    });

    // Validação editar
    $('#form-modal-ediatar-historico-viagem').validate({
        errorClass: 'form-control-danger',
        validClass: 'form-control-sucess',
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
            'HistoricoViagem.IdPacote': {
                required: true
            },
            'HistoricoViagem.Data': {
                required: true,
                date: true
            }
        },
        messages: {
            'historicoViagem.IdPacote': {
                required: STRINGS.selecionePacote
            },
            'historicoViagem.Data': {
                required: STRINGS.informeData,
                date: STRINGS.dataValida
            }
        }

    });

    //Update modal editar
    $('#botao-salvar-modal-editar-historico-viagem').on('click', function () {
        if ($('#form-modal-ediatar-historico-viagem').valid()) {
            $.ajax({
                url: '/HistoricoViagem/Update',
                method: 'post',
                dataType: 'json',
                data: {
                    id: $('#campo-editar-historico-viagem-id').val(),
                    idPacote: $('#select-editar-historico-viagem-idPacote').val(),
                    data: $('#campo-editar-historico-viagem-data').val()
                },
                success: function (data) {
                    var resultado = JSON.parse(data);
                    if (resultado == 1) {
                        $('#historico-viagem-tabela').DataTable().ajax.reload();
                        $(function () {
                            new PNotify({
                                title: STRINGS.sucesso,
                                text: STRINGS.alterado,
                                type: 'info'
                            });
                        });
                        $('#historico-viagem-modal-editar').modal('hide');
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
    $('table').on('click', '.botao-excluir-historico-viagem', function () {
        var id = $(this).data('id');
        swal({
            title: STRINGS.voceTemCerteza,
            text: STRINGS.voceIraDesativarRegistro,
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: STRINGS.simDesativar,
            cancelButtonText: STRINGS.naoCancelar,
            closeOnConfirm: false,
            closeOnCancel: false
        },
            function (isConfirm) {
                if (isConfirm) {
                    swal(STRINGS.desativado, STRINGS.voceDesativouRegistro, "success");
                    $.ajax({
                        url: 'HistoricoViagem/Excluir?id=' + id,
                        method: 'get',
                        success: function (data) {
                            var resultado = JSON.parse(data);
                            if (resultado == 1) {
                                new PNotify({
                                    title: STRINGS.desativado,
                                    text: STRINGS.desativadoSucesso,
                                    type: 'success'
                                });

                                $('#historico-viagem-tabela').DataTable().ajax.reload();

                            } else {
                                new PNotify({
                                    title: 'Erro!',
                                    text: 'Erro ao desativar viagem',
                                    type: 'error'
                                });
                            }
                        }
                    });
                } else {
                    swal(STRINGS.cancelado, STRINGS.seuArquivoEstaSalvo, "error");
                }
            });
    });

    
    function limparCampos() {
        $('#select-cadastro-historico-viagem-idPacote').val('').trigger('change');
        $("#campo-cadastro-historico-viagem-data").val(null);            
    }
});