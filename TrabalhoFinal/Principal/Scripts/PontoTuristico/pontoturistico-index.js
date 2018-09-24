$(function () {
    //Preenche DataTable
    $('#table-pontoturistico').DataTable({
        ajax: '/PontoTuristico/ObterTodosPorJSON',
        "order":[[2, "asc"]],
        columns: [
            {
                data: 'Id',
                bSortable: false,
                width: "10%",
                target: 0
            },
            {
                data: 'Endereco.Completo',
                bSortable: true,
                width: "30%%",
                target: 1
            },
            {
                data: 'Nome',
                bSortable: true,
                width: "40%",
                target: 2
            },
            {
                data: null,
                bSortable: false,
                width: "20%",
                render: function (data, type, row) {
                    return "<a class='btn btn-outline-info botao-editar-pontoturistico' data-id='" + row.Id + "'>Editar</a>" +
                        "<a class='btn btn-outline-danger ml-1 botao-excluir-pontoturistico' data-id='" + row.Id + "' data-nome='" + row.Nome + "'>Desativar</a>";
                }
            }
        ]
    });


    //Abre modal de cadastro
    $('#botao-modal-cadastrar-pontoturistico').on('click', function () {
        limparCamposPontoTuristicoCadastro();
        $('#pontoturistico-modal-cadastro').modal('show');
    });

    //Validacao modal cadastro
    $('#form-modal-cadastro-pontoturistico').validate({
        errorClass: "form-control-danger",
        validClass: "form-control-success",
        highlight: function (element) {
            jQuery(element).closet('.form-group').addClass('has-error');
        },
        unhighlight: function (element) {
            jQuery(element).closest('.form-group').removeClass('has-error');
        },
        errorPlacement: function (error, element) {
            $(element).parent().append(error[0])
        },

        rules: {
            'pontoturistico.IdEndereco': {
                required: true
            },
            'pontoturistico.Nome': {
                required: true,
                rangelength: [3, 30]
            }
        },
        messages: {
            'pontoturistico.IdEndereco': {
                required: 'Selecione um Ponto Turistico'
            },
            'pontoturistico.Nome': {
                required: 'ponto turistico deve ser preenchido.',
                rangelength: 'ponto turistico deve conter entre {0} a {1} caracteres'
            }
        }
    });

    //Salvar modal cadastro
    $('#botao-salvar-modal-cadastrar-pontoturistico').on('click', function () {
        if ($('#form-modal-cadastro-pontoturistico').valid()) {
            var nomeVar = $('#campo-cadastro-pontoturistico-nome').val();
            $.ajax({
                url: '/PontoTuristico/Store',
                method: 'post',
                data: {
                    idEndereco: $('#select-modal-cadastro-pontoturistico').val(),
                    nome: $('#campo-cadastro-pontoturistico-nome').val()
                },
                success: function (data) {
                    var resultado = JSON.parse(data);
                    limparCamposPontoTuristicoCadastro();
                    $('#pontoturistico-modal-cadastro').modal('hide');
                    $('#table-pontoturistico').DataTable().ajax.reload();
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
    $('table').on('click', '.botao-editar-pontoturistico', function () {
        var id = $(this).data('id');
        $.ajax({
            url: '/PontoTuristico/Editar?id=' + id,
            method: 'get',
            success: function (resultado) {
                var data = JSON.parse(resultado);
                $('#campo-editar-pontoturistico-id').val(data.Id);
                $('#select-modal-editar-pontoturistico').append(new Option(data.Endereco.Logradouro, data.IdEstado, false, false)).val(data.IdEstado).trigger('change');
                $('#campo-editar-pontoturistico-nome').val(data.Nome);

                $('#pontoturistico-modal-editar').modal('show');
            }
        });
    });

    //Update modal editar
    $('#botao-salvar-modal-editar-pontoturistico').on('click', function () {
        $.ajax({
            url: '/PontoTuristico/Update',
            method: 'Post',
            dataType: 'json',
            data: {
                id: $('#campo-editar-pontoturistico-id').val(),
                idEndereco: $('#select-modal-editar-pontoturistico').val(),
                nome: $('#campo-editar-pontoturistico-nome').val()
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
                    $('#table-pontoturistico').DataTable().ajax.reload();
                    $('#pontoturistico-modal-editar').modal('hide');
                    limparCamposPontoTuristicoEditar();
                } else {
                    new PNotify({
                        title: 'Erro!',
                        text: 'Erro ao alterar',
                        type: 'error'
                    });
                }
            }
        });
    });

    //Desativar
    $('table').on('click', '.botao-excluir-pontoturistico', function () {
        var id = $(this).data('id');
        $.ajax({
            url: '/PontoTuristico/Excluir?id=' + id,
            method: 'get',
            success: function (data) {
                var resultado = JSON.parse(data);
                if (resultado == 1) {
                    new PNotify({
                        title: 'Desativado!',
                        text: nome + ' desativado com sucesso',
                        type: 'success'
                    });
                    $('#table-pontoturistico').DataTable().ajax.reload();
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

    function limparCamposPontoTuristicoCadastro() {
        $('#select-modal-cadastro-pontoturistico').val('').trigger('change');
        $('#campo-cadastro-pontoturistico-nome').val('');
    }

    function limparCamposPontoTuristicoEditar() {
        $('#campo-editar-pontoturistico-id').val('');
        $('#select-modal-editar-pontoturistico').val('').trigger('change');
    }

});