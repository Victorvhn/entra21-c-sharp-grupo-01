$(function () {
    //Preenche DataTable
    $('#table-viagens').DataTable({
        ajax: '/Viagem/ObterTodosPorJSON',
        columns: [
            { data: 'Id' },
            { data: 'Pacote.Nome' },
            { data: 'Guia.Nome' },
            { data: 'DataHoraSaidaPadraoBR' },
            { data: 'DataHoraVoltaPadraoBR' },
            {
                data: null,
                render: function (data, type, row) {
                    return '<a class="btn btn-outline-info botao-editar-viagem" data-id="' + row.Id + '" data-toggle="modal" data-target="#viagem-modal-editar">Editar</a>' +
                        "<a class='btn btn-outline-danger ml-1 botao-excluir-viagem' data-id='" + row.Id + "'>Desativar</a>";
                }
            }
        ]
    });

    //Abre modal de cadastro
    $('#botao-modal-cadastrar-viagem').on('click', function () {
        limparCampos();
        $('#viagem-modal-cadastro').modal('show');
    });

    //Validação modal cadastro
    $('#form-modal-cadastro-viagem').validate({
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
            'viagem.IdPacote': {
                required: true
            },
            'viagem.IdGuia': {
                required: true

            },
            'viagem.DataHorarioSaida': {
                required: true
            },
            'viagem.DataHorarioVolta': {
                required: true

            }
        },
        messages: {
            'viagem.IdPacote': {
                required: 'Selecione um pacote.'
            },
            'viagem.IdGuia': {
                required: 'Selecione um Guia.'

            },
            'viagem.DataHorarioSaida': {
                required: 'Informe a data de saida.',
                date: true
            },
            'viagem.DataHorarioVolta': {
                required: 'Informe a data da volta.',
                date: true

            }
        }

    });

    //Salvar modal cadastro
    $('#botao-salvar-modal-cadastrar-viagem').on('click', function () {
        if ($('#form-modal-cadastro-viagem').valid()) {
            $.ajax({
                url: '/Viagem/Store',
                method: 'post',
                data: {
                    idGuia: $('#select-cadastro-viagem-guia').val(),
                    idPacote: $('#select-cadastro-viagem-pacote').val(),
                    dataHoraSaidaPadraoBR: $('#campo-cadastro-data-saida-viagem').val(),
                    dataHoraVoltaPadraoBR: $('#campo-cadastro-data-volta-viagem').val()
                },
                success: function (data) {
                    var resultado = JSON.parse(data);
                    limparCampos();
                    $('#viagem-modal-cadastro').modal('hide');
                    $('#table-viagens').DataTable().ajax.reload();
                    $(function () {
                        new PNotify({
                            title: 'Sucesso!',
                            text: 'Viagem cadastrada com sucesso',
                            type: 'success'
                        });
                    });
                }
            });
        }
    });

    //Botao editar
    $('table').on('click', '.botao-editar-viagem', function () {
        var id = $(this).data('id');
        $.ajax({
            url: '/Viagem/Editar?id=' + id,
            method: 'get',
            success: function (resultado) {
                var data = JSON.parse(resultado);
                $('#campo-editar-viagem-id').val(data.Id);
                $('#select-modal-editar-viagem-guia :selected').text(data.idGuia);
                $('#select-modal-editar-viagem-pacote :selected').text(data.idPacote);
                $('#campo-editar-data-saida-viagem').val(data.dataHoraSaidaPadraoBR);
                $('#campo-editar-data-volta-viagem').val(data.dataHoraVoltaPadraoBR);

                $('#viagem-modal-editar').modal('show');

            }
        });
    });

    // Validação editar
    $('#form-modal-editar-viagem').validate({
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
            'viagem.IdPacote': {
                required: true
            },
            'viagem.IdGuia': {
                required: true

            },
            'viagem.DataHorarioSaida': {
                required: true
            },
            'viagem.DataHorarioVolta': {
                required: true

            }
        },
        messages: {
            'viagem.IdPacote': {
                required: 'Selecione um pacote.'
            },
            'viagem.IdGuia': {
                required: 'Selecione um Guia.'

            },
            'viagem.DataHorarioSaida': {
                required: 'Informe data de saida.',
                date: true
            },
            'viagem.DataHorarioVolta': {
                required: 'Informe a data da volta.',
                date: true

            }
        }

    });

    //Update modal editar
    $('#botao-salvar-modal-editar-viagem').on('click', function () {
        if ($('#form-modal-editar-viagem').valid()) {
            $.ajax({
                url: '/Viagem/Update',
                method: 'post',
                dataType: 'json',
                data: {
                    id: $('#campo-editar-viagem-id').val(),
                    idGuia: $('#select-modal-editar-viagem-guia').val(),
                    idPacote: $('#select-modal-editar-viagem-pacote').val(),
                    DataHorarioSaida: $('#campo-editar-data-saida-viagem').val(),
                    DataHorarioVolta: $('#campo-editar-data-volta-viagem').val()
                },
                success: function (data) {
                    var resultado = JSON.parse(data);
                    if (resultado == 1) {
                        $(function () {
                            $('#table-viagens').DataTable().ajax.reload();
                            new PNotify({
                                title: 'Sucesso!',
                                text: 'Alterado com sucesso',
                                type: 'info'
                            });
                        });
                        $('#viagem-modal-editar').modal('hide');
                        limparCampoEditar();
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
    $('table').on('click', '.botao-excluir-viagem', function () {
        var id = $(this).data('id');
        $.ajax({
            url: '/Viagem/Excluir?id=' + id,
            method: 'get',
            success: function (data) {
                var result = JSON.parse(data);
                if (result == 1) {
                    new PNotify({
                        title: 'Desativado!',
                        text: 'Viagem desativada com sucesso',
                        type: 'success'
                    });

                    $('#table-viagens').DataTable().ajax.reload();

                } else {
                    new PNotify({
                        title: 'Erro!',
                        text: 'Erro ao desativar viagem',
                        type: 'error'
                    });
                }
            }
        });
    });

    function limparCampos() {
        $('campo-cadastro-data-saida-viagem').val();
        $('#campo-cadastro-data-volta-viagem').val();
        $('#select-cadastro-viagem-guia').val();
        $('#select-cadastro-viagem-pacote').val();
    }

    function limparCampoEditar() {
        $('#select-modal-editar-viagem-guia').val(),
            $('#select-modal-editar-viagem-pacote').val(),
            $('#campo-editar-data-saida-viagem').val(),
            $('#campo-editar-data-volta-viagem').val()
    }
});