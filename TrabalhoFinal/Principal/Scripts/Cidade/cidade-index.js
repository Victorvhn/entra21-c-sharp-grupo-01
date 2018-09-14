﻿$(function () {
    //Preenche DataTable
    $('#table-cidade').DataTable({
        processing: true,
        serverSide: true,
        ajax: '/Cidade/ObterTodosPorJSON',
        columns: [
            { data: 'Id' },
            { data: 'Estado.Nome' },
            { data: 'Nome' },
            {
                data: null,
                render: function (data, type, row) {
                    return "<a class='btn btn-outline-info botao-editar-cidade' data-id='" + row.Id + "'>Editar</a>" +
                        "<a class='btn btn-outline-danger ml-1 botao-excluir-cidade' data-id='" + row.Id + "' data-nome='" + row.Nome + "'>Desativar</a>";
                }
            }
        ]
    });

    //Abre modal de cadastro
    $('#botao-modal-cadastrar-cidade').on('click', function () {
        limparCamposCidadeCadastro();
        $('#cidade-modal-cadastro').modal('show');
    });

    //Validacao modal cadastro
    $('#form-modal-cadastro-cidade').validate({
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
            'cidade.IdEstado': {
                required: true
            },
            'cidade.Nome': {
                required: true,
                rangelength: [3, 30]
            }
        },
        messages: {
            'cidade.IdEstado': {
                required: 'Selecione um Estado'
            },
            'cidade.Nome': {
                required: 'Cidade deve ser preenchido.',
                rangelength: 'Cidade deve conter de {0} a {1} caracteres'
            }
        }
    });

    //Salvar modal cadastro
    $('#botao-salvar-modal-cadastrar-cidade').on('click', function () {
        if ($('#form-modal-cadastro-cidade').valid()) {
            var nomeVar = $('#campo-cadastro-cidade-nome').val();
            $.ajax({
                url: '/Cidade/Store',
                method: 'post',
                data: {
                    idEstado: $('#select-modal-cadastro-cidade').val(),
                    nome: $('#campo-cadastro-cidade-nome').val()
                },
                success: function (data) {
                    var resultado = JSON.parse(data);
                    limparCamposCidadeCadastro();
                    $('#cidade-modal-cadastro').modal('hide');
                    $('#table-cidade').DataTable().ajax.reload();
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
    $('table').on('click', '.botao-editar-cidade', function () {
        var id = $(this).data('id');
        $.ajax({
            url: '/Cidade/Editar?id=' + id,
            method: 'get',
            success: function (resultado) {
                var data = JSON.parse(resultado);
                $('#campo-editar-cidade-id').val(data.Id);
                $('#select-modal-editar-cidade').append(new Option(data.Estado.Nome, data.IdEstado, false, false)).val(data.IdEstado).trigger('change');
                $('#campo-editar-cidade-nome').val(data.Nome);

                $('#cidade-modal-editar').modal('show');
            }
        });
    });


    // Validação editar
    $('#form-modal-editar-cidade').validate({
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
            'cidade.IdEstado': {
                required: true
            },
            'cidade.Nome': {
                required: true,
                rangelength: [3, 30]
            }
        },
        messages: {
            'cidade.IdEstado': {
                required: 'Selecione um Estado'
            },
            'cidade.Nome': {
                required: 'Cidade deve ser preenchido.',
                rangelength: 'Cidade deve conter de {0} a {1} caracteres'
            }
        }
    });


    //Update modal editar
    $('#botao-salvar-modal-editar-cidade').on('click', function () {
        if ($('#form-modal-editar-cidade').valid()) {
            $.ajax({
                url: '/Cidade/Update',
                method: 'Post',
                dataType: 'json',
                data: {
                    id: $('#campo-editar-cidade-id').val(),
                    idEstado: $('#select-modal-editar-cidade').val(),
                    nome: $('#campo-editar-cidade-nome').val()
                },
                success: function (data) {
                    var resultado = JSON.parse(data);
                    if (resultado == 1) {
                        $(function () {
                            new PNotify({
                                title: 'Sucesso!',
                                text: 'Alterado com sucesso',
                                type: 'info'
                            });
                        });
                        $('#table-cidade').DataTable().ajax.reload();
                        $('#cidade-modal-editar').modal('hide');
                        limparCamposCidadeEditar();
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
    $('table').on('click', '.botao-excluir-cidade', function () {
        var id = $(this).data('id');
        var nome = $(this).data('nome');
        $.ajax({
            url: 'Cidade/Excluir?id=' + id,
            method: 'get',
            success: function (data) {
                var resultado = JSON.parse(data);
                if (resultado == 1) {
                    new PNotify({
                        title: 'Desativado!',
                        text: nome + ' desativado com sucesso',
                        type: 'success'
                    });

                    $('#table-cidade').DataTable().ajax.reload();

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

    function limparCamposCidadeCadastro() {
        $('#select-modal-cadastro-cidade').val('').trigger('change');
        $('#campo-cadastro-cidade-nome').val('');
    }

    function limparCamposCidadeEditar() {
        $('#campo-editar-cidade-id').val('');
        $('#select-modal-editar-cidade').val('').trigger('change');
    }
});