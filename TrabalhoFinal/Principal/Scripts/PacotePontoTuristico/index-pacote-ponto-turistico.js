$(function () {
    //Preenche DataTable
    $('#table-pacote-ponto-turistico').DataTable({
        ajax: '/PacotePontoTuristico/ObterTodosPorJSON',
        columns: [
            { data: 'Id' },
            { data: 'Pacote.Nome' },
            { data: 'PontoTuristico.Nome' },
            {
                data: null,
                render: function (data, type, row) {
                    return '<a class="btn btn-outline-info botao-editar-pacote-ponto-turistico data-id="' + row.Id + '" data-toggle="modal" data-target="#pacote-ponto-turistico-modal-editar">Editar</a>' +
                        '<a class="btn btn-outline-danger ml-1 botao-excluir-pacote-ponto-turistico data-id="' + row.Id + '">Desativar</a>';
                }
            }
        ]
    });


    //Abre modal de cadastro
    $('#botao-modal-cadastrar-pacote-ponto-turistico').on('click', function () {
        limparCampos();
        $('#pacote-ponto-turistico-modal-cadastro').modal('show');
    });

    //Validação modal cadastro
    $('#form-modal-cadastro-pacote-ponto-turistico').validate({
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
            'pacotePontoTuristo.IdPacote': {
                required: true
            },
            'pacotePontoTuristico.IdPontoTuristico': {
                required: true
            }
        },
        messages: {
            'pacotePontoTuristo.IdPacote': {
                required: 'Selecione um pacote'
            },
            'pacotePontoTuristico.IdPontoTuristico': {
                required: 'Selecione um ponto turistico'
            }
        }
    });

    //Salvar modal cadastro
    $('#botao-salvar-modal-cadastrar-pacote-ponto-turistico').on('click', function () {
        if ($('#form-modal-cadastro-pacote-ponto-turistico').valid()) {
            $.ajax({
                url: '/PacotePontoTuristico/Store',
                method: 'post',
                data: {
                    idPacote: $('#select-cadastro-pacote-ponto-turistico-pacote').val(),
                    idPontoTuristico: $('#select-cadastro-pacote-ponto-turistico-ponto-turistico').val(),
                },
                success: function (data) {
                    var resultado = JSON.parse(data);
                    limparCampos();
                    $('#pacote-ponto-turistico-modal-cadastro').modal('hide');
                    $('#table-pacote-ponto-turistico').DataTable().ajax.reload();
                    $(function () {
                        new PNotify({
                            title: 'Sucesso!',
                            text: 'Pacote Ponto Turistico Cadastrado com sucesso',
                            type: 'success'
                        });
                    });
                }
            });
        }
    });


    //Botao editar
    $('table').on('click', '.botao-editar-pacote-ponto-turistico', function () {
        var id = $(this).data('id');
        $.ajax({
            url: '/PacotePontoTuristico/Editar?id=' + id,
            method: 'get',
            success: function (resultado) {
                var data = JSON.parse(resultado);
                $('#campo-editar-pacote-ponto-turistico-id').val(data.Id);
                $('#select-editar-pacote-ponto-turistico-pacote').append(new Option(data.Pacote.Nome, data.IdPacote, false, false)).val(data.idPacote).trigger('change');
                $('#select-editar-pacote-ponto-turistico-ponto-turistico').append(new Option(data.PontoTuristico.Nome, data.idPontoTuristico, false, false)).val(data.idPontoTuristico).trigger('change');

                $('#pacote-ponto-turistico-modal-editar').modal('show');
            }
        });
    });


    // Validação editar
    $('#form-modal-editar-pacote-ponto-turistico').validate({
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
            'pacotePontoTuristico.IdPacote': {
                required: true
            },
            'pacotePontoTuristico.IdPontoTuristico': {
                required: true
            }
        },
        messages: {
            'pacotePontoTuristico.IdPacote': {
                required: 'Selecione um pacote'
            },
            'pacotePontoTuristico.IdPontoTuristico': {
                required: 'Selecione um ponto turistico'
            }
        }
    });

    //Update modal editar
    $('#botao-salvar-modal-editar-pacote-ponto-turistico').on('click', function () {
        if ($('#form-modal-editar-pacote-ponto-turistico').valid()) {
            $.ajax({
                url: '/PacotePontoTuristico/Update',
                method: 'post',
                dataType: 'json',
                data: {
                    id: $('#campo-editar-pacote-ponto-turistico-id').val(),
                    idPacote: $('#select-editar-pacote-ponto-turistico-pacote').val(),
                    idPontoTuristico: $('#select-editar-pacote-ponto-turistico-ponto-turistico').val()
                },
                success: function (data) {
                    var resultado = JSON.parse(data);
                    if (resultado == 1) {
                        $(function () {
                            $('#table-pacote-ponto-turistico').DataTable().ajax.reload();
                            new PNotify({
                                title: 'Sucesso!',
                                text: 'Alterado com sucesso',
                                type: 'info'
                            });
                        });
                        $('#pacote-ponto-turistico-modal-editar').modal('hide');
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
    $('table').on('click', '.botao-excluir-pacote-ponto-turistico', function () {
        var id = $(this).data('id');
        $.ajax({
            url: '/PacotePontoTuristico/Excluir?id=' + id,
            method: 'get',
            success: function (data) {
                var result = JSON.parse(data);
                if (result == 1) {
                    new PNotify({
                        title: 'Desativado',
                        text: 'Pacote Ponto Turistico Desativado com sucesso',
                        type: 'success'
                    });
                    $('table-pacote-ponto-turistico').DataTable().ajax.reload();
                } else {
                    new PNotify({
                        title: 'Erro!',
                        text: 'Erro ao desativar Pacote Ponto Turistico',
                        type: 'error'
                    });
                }
            }
        });
    });

    function limparCampos() {
        $('#select-cadastro-pacote-ponto-turistico-pacote').val();
        $('#select-cadastro-pacote-ponto-turistico-ponto-turistico').val();
    }

    function limparCamposEditar() {
        $('#select-editar-pacote-ponto-turistico-pacote').val();
        $('#select-editar-pacote-ponto-turistco-ponto-turistico').val();
    }
});