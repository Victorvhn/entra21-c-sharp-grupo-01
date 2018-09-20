$(function () {
    //Preenche DataTable
    $('#table-idiomas').DataTable({
        ajax: "/Idioma/ObterTodosPorJSON",
        order: [[1, "asc"]],
        columns: [
            {
                data: "Id",
                bSortable: false,
                width: "10%"
            },
            {
                data: "Nome",
                bSortable: true,
                width: "70%"
            },
            {
                data: null,
                bSortable: false,
                width: "20%",
                render: function (data, type, row) {
                    return "<a class='btn btn-outline-info botao-editar-idioma' data-id='" + row.Id + "'>Editar</a>" +
                        "<a class='btn btn-outline-danger ml-1 botao-excluir-idioma' data-nome='" + row.Nome + "' data-id='" + row.Id + "' href='#' > Excluir</a>";
                }
            }
        ]
    });


    //Abre modal de cadastro
    $('#botao-modal-cadastrar-idioma').on("click", function () {
        limparCampos();
        $("#idioma-modal-cadastro").modal('show');
    });

    //Validação modal cadastro
    $('#form-modal-cadastro-idioma').validate({
        errorClass: 'form-control-danger',
        validClass: 'form-control-success',
        rules: {
            'idioma.Nome': {
                required: true,
                rangelength: [4, 20]
            }
        },
        messages: {
            'idioma.Nome': {
                required: 'Idioma deve ser preenchido,',
                rangelength: 'Idioma deve conter entre {0} a {1}'

            }
        }
    });

    //Salvar modal cadastro
    $("#botao-salvar-modal-cadastrar-idioma").on('click', function () {
        if ($('#form-modal-cadastro-idioma').valid()) {
            var nomeVar = $("#campo-cadastro-idioma").val();
            $.ajax({
                url: '/Idioma/Store',
                method: 'post',
                data: {
                    nome: $('#campo-cadastro-idioma').val()
                },
                success: function (data) {
                    var resultado = JSON.parse(data);
                    limparCampos();
                    $('#idioma-modal-cadastro').modal('hide');
                    $(function () {
                        new PNotify({
                            title: 'Sucesso!',
                            text: nomeVar + ' cadastrado com sucesso',
                            type: 'success'
                        });
                    });
                    $('#table-idiomas').DataTable().ajax.reload();



                }
            });
        }
    });

    //Botao editar
    $('table').on('click', '.botao-editar-idioma', function () {
        var id = $(this).data('id');
        $.ajax({
            url: '/Idioma/Editar?id=' + id,
            success: function (resultado) {
                var data = JSON.parse(resultado);
                $('#campo-id-editar-idioma').val(data.Id);
                $('#campo-editar-idioma').val(data.Nome);
                $('#idioma-modal-editar').modal('show');
            }
        });
    });

    // Validação editar
    $('#form-modal-editar-idioma').validate({
        errorClass: 'form-control-danger',
        validClass: 'form-control-success',
        rules: {
            'idioma.Nome': {
                required: true,
                rangelength: [4, 20]
            }
        },
        messages: {
            'idioma.Nome': {
                required: 'Idioma deve ser preenchido',
                rangelength: 'Idioma deve conter entre {0} a {1}'

            }
        }
    });


    //Update modal editar
    $('#botao-salvar-modal-editar-idioma').on('click', function () {
        if ($('#form-modal-editar-idioma').valid()) {
            $.ajax({
                url: '/Idioma/Update',
                method: 'post',
                dataType: 'json',
                data: {
                    id: $('#campo-id-editar-idioma').val(),
                    nome: $('#campo-editar-idioma').val()
                },
                success: function (data) {
                    var resultado = JSON.parse(data);
                    if (resultado == 1) {
                        limparCampos();
                        $('#idioma-modal-editar').modal('hide');
                        $(function () {
                            new PNotify({
                                title: 'Sucesso!',
                                text: 'Alterado com sucesso',
                                type: 'info'
                            });
                        });
                        $('#table-idiomas').DataTable().ajax.reload();

                    }
                    else {
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
    $('table').on('click', '.botao-excluir-idioma', function () {
        var id = $(this).data('id');
        var nome = $(this).data('nome');
        $.ajax({
            url: '/Idioma/Excluir?id=' + id,
            method: 'get',
            success: function (data) {
                var resultado = JSON.parse(data);
                if (resultado == 1) {
                    new PNotify({
                        title: 'Desativado!',
                        text: nome + ' desativado com sucesso',
                        type: 'success'
                    });

                    $('#table-idiomas').DataTable().ajax.reload();
                }
                else {
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
        $("#campo-cadastro-idioma").prop('');
    }

});