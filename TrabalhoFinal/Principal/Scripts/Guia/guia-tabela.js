﻿$(function () {
    //Preenche DataTable
    $('#guia-tabela').DataTable({
        processing: true,
        serverSide: true,
        ajax: "/Guia/ObterTodosPorJSON",
        columns: [
            { data: "Id" },
            { data: "Nome" },
            { data: "Sobrenome" },
            { data: "Cpf" },
            { data: "Rank" },
            {
                data: null,
                render: function (data, type, row) {
                    return "<a class='btn btn-outline-info' id='botao-editar-guia' data-id='" + row.Id + "'>Editar</a>" +
                        "<a class='btn btn-outline-danger ml-1' id='botao-excluir-guia' data-id='" + row.Id + "' href='#' >Desativar</a>";

                }
            }
        ]
    });

    //Abre modal de cadastro
    $("#botao-modal-cadastrar-guia").on("click", function () {
        limparCampos();
        $("#guia-modal-cadastro").modal('show');
    });

    //Desativar
    $('table').on('click', '#botao-excluir-guia', function () {
        var id = $(this).data('id');
        $.ajax({
            ulr: 'Guia/Excluir?id=' + id,
            method: 'get',
            success: function (result) {
                var apagado = JSON.parse(result)
                if (apagado == 1) {

                    new PNotify({
                        title: 'Desativado!',
                        text: 'Usuário desativado com sucesso',
                        type: 'success'
                    });

                    $('#guia-tabela').DataTable().ajax.reload();

                } else {

                    new PNotify({
                        title: 'Erro!',
                        text: 'Erro ao desativar usuário',
                        type: 'error'
                    });
                }
            }
        });
    });

    //Botao editar
    $('table').on("click", "#botao-editar-guia", function () {
        var id = $(this).data('id');
        $.ajax({
            url: '/Guia/Editar?id=' + id,
            success: function (result) {
                var data = JSON.parse(result);
                $("#campo-editar-guia-nome").val(data.Nome);
                $("#campo-editar-guia-sobrenome").val(data.Sobrenome);
                $("#campo-editar-guia-rg").val(data.Rg);
                $("#campo-editar-guia-cpf").val(data.Cpf);
                $("#campo-editar-guia-data-nascimento").val(data.DataNascimento);
                $("#campo-editar-guia-sexo").val(data.Sexo);
                $("#campo-numero-carteira-trabalho").val(data.CarteiraTrabalho);
                $("#campo-editar-guia-salario").val(data.Salario);
                $("#campo-editar-guia-categoria-habilitacao").val(data.CategoriaHabilitacao);
                $("#campo-editar-guia-rank").val(data.Rank);

                $("#guia-modal-editar").modal("show");
            }
        });
    });

    //Update modal editar
    $("#botao-acao-editar-guia").on("click", function () {
        var nomeVar = $("#campo-editar-guia-nome").val();
        $.ajax({
            url: '/Guia/Update',
            method: 'post',
            dataType: 'json',
            data: {
                nome: $("#campo-editar-guia-nome").val(),
                sobrenome: $("#campo-editar-guia-sobrenome").val(),
                datanascimento: $("#campo-editar-guia-data-nascimento").val(),
                sexo: $("#campo-editar-guia-sexo").val(),
                rg: $("#campo-editar-guia-rg").val(),
                cpf: $("#campo-editar-guia-cpf").val(),
                carteiratrabalho: $("#campo-editar-guia-numero-carteira-trabalho").val(),
                categoriahabilitacao: $("#campo-editar-guia-categoria-habilitacao").val(),
                salario: $("#campo-editar-guia-salario").val(),
                rank: $("#campo-editar-guia-rank").val()
            },
            success: function (data) {
                var resultado = JSON.parse(data);
                limparCampos();
                $("#guia-modal-editar").modal('hide');
                $(function () {
                    new PNotify({
                        title: 'Sucesso!',
                        text: nomeVar + ' cadastrado com sucesso',
                        type: 'success'
                    });
                });
                $('#guia-tabela').DataTable().ajax.reload();
            }
        });
        limparCampos();
    });

    //Salvar modal cadastro
    $("#botao-salvar-modal-cadastrar-guia").on("click", function () {
        var nomeVar = $("#campo-cadastro-guia-nome").val();
        $.ajax({
            url: '/Guia/Store',
            method: 'post',
            data: {
                nome: $("#campo-cadastro-guia-nome").val(),
                sobrenome: $("#campo-cadastro-guia-sobrenome").val(),
                datanascimento: $("#campo-cadastro-guia-data-nascimento").val(),
                sexo: $("#campo-cadastro-guia-sexo").val(),
                rg: $("#campo-cadastro-guia-rg").val(),
                cpf: $("#campo-cadastro-guia-cpf").val(),
                carteiratrabalho: $("#campo-cadastro-guia-numero-carteira-trabalho").val(),
                categoriahabilitacao: $("#campo-cadastro-guia-categoria-habilitacao").val(),
                salario: $("#campo-cadastro-guia-salario").val(),
                rank: $("#campo-cadastro-guia-rank").val()
            },
            success: function (data) {
                var resultado = JSON.parse(data);
                limparCampos();
                $("#guia-modal-cadastro").modal('hide');
                $('#guia-tabela').DataTable().ajax.reload();
                $(function () {
                    new PNotify({
                        title: 'Sucesso!',
                        text: nomeVar + ' cadastrado com sucesso',
                        type: 'success'
                    });
                });
            }
        });
    });

    function limparCampos() {
        $("#campo-cadastro-guia-nome").val(""),
            $("#campo-cadastro-guia-sobrenome").val(""),
            $("#campo-cadastro-guia-rg").val(""),
            $("#campo-cadastro-guia-cpf").val(""),
            $("#campo-cadastro-guia-data-nascimento").val(""),
            $("#campo-cadastro-guia-sexo").val(""),
            $("#campo-cadastro-guia-numero-carteira-trabalho").val(""),
            $("#campo-cadastro-guia-salario").val(""),
            $("#campo-cadastro-guia-categoria-habilitacao").val(""),
            $("#campo-cadastro-guia-rank").val("")
    }
});