$(function () {
    //Preenche DataTable
    $('#table-cidade').DataTable({
        ajax: '/Cidade/ObterTodosPorJSON',
        "order": [[1, "asc"]],
        columns: [

            {
                data: 'Id',
                bSortable: false,
                width: "10%"
            },
            {
                data: 'Estado.Nome',
                bSortable: true,
                width: "40%",
                target: 0
            },
            {
                data: 'Nome',
                bSortable: true,
                width: "30%",
                target: 1
            },
            {
                data: null,
                bSortable: false,
                width: "20%",
                render: function (data, type, row) {
                    return "<a style='font-size: 24px;' class='btn fa fa-edit botao-editar-cidade' data-id='" + row.Id + "'></a>" +
                        "<a style='font-size: 24px;' class='btn fa fa-trash ml-1 botao-excluir-cidade' data-id='" + row.Id + "' data-nome='" + row.Nome + "'></a>";
                }
            }
        ]
    });



    /*$.ajax({
        url: '/Cidade/ObterTodosPorJSON',
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $("#pai").append('<div class="card widget-statstic-card">\
                < div class= "card-header" >\
                <div class="card-header-left">\
                    <h5>Statistics</h5>\
                    <p class="p-t-10 m-b-0 text-c-blue">' + '</p>\
                </div>\
                                                    </div >\
                <div class="card-block">\
                    <i class="icofont icofont-presentation-alt st-icon bg-c-blue"></i>\

                    < div class= "text-left" >\
                    <h3 class="d-inline-block">5,456</h3>\
                    <i class="icofont icofont-long-arrow-up f-30 text-c-green"></i>\
                    <span class="f-right">23%</span>\
                    </div >\
                </div >\
                                                </div > ');
                                                }
        }

    });*/

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
                required: STRINGS.selecioneEstado
            },
            'cidade.Nome': {
                required: STRINGS.cidadePreenchido,
                rangelength: STRINGS.cidadeConter
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
                    $('#cidade-modal-cadastro').modal('hide');
                    $('#table-cidade').DataTable().ajax.reload();
                    $(function () {
                        new PNotify({
                            title: STRINGS.desativado,
                            text: nomeVar + " " + STRINGS.cadastrado,
                            type: 'success'
                        });
                    });
                    limparCamposCidadeCadastro();
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
                required: STRINGS.selecioneEstado
            },
            'cidade.Nome': {
                required: STRINGS.cidadePreenchido,
                rangelength: STRINGS.cidadeConter
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
                                title: STRINGS.desativado,
                                text: STRINGS.alterado,
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
                        title: STRINGS.desativado,
                        text: nome + " " + STRINGS.desativadoSucesso,
                        type: 'success'
                    });

                    $('#table-cidade').DataTable().ajax.reload();

                } else {
                    new PNotify({
                        title: 'Erro!',
                        text:  "erro ao desativar" + nome,
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

});