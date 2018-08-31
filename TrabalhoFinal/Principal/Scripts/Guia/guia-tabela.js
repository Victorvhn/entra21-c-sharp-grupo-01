﻿
$(function () {
    $('#guia-tabela').DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": "/Guia/ObterTodosPorJSON",
        "columns": [
            { "data": "Id" },
            { "data": "Nome" },
            { "data": "Sobrenome" },
            { "data": "Cpf" },
            { "data": "Rank" },
            {
                data: null,
                render: function (data, type, row) {
                    return "<a class='btn btn-outline-info' value='" + row.Id + "'>Editar</a>" +
                        "<a class='btn btn-outline-danger ml-1' id='botao-excluir-guia' data-id='" + row.Id + "' href='#' >Excluir</a>";

                }
            }
        ]
    });
});

$("#botao-excluir-guia").on("click", function () {
    var idExcluir = $(this).data("id");
    $a.ajax({
        url: '/Guia/Excluir?id=' + idExcluir,
        method: 'get',
        success: function (data) {
            if (data == true) {
                new PNotify({
                    title: 'Desativado!',
                    text: 'Usuário desativado com sucesso',
                    type: 'success'
                });

                $('#guia-tabela').DataTable().ajax.reload();
            }
            else {
                new PNotify({
                    title: 'Erro!',
                    text: 'Erro ao desativar usuário',
                    type: 'error'
                });
            }
        }
    })
})

$("#botao-modal-cadastrar-guia").on("click", function () {
    limparCampos();
    $("#guia-modal-cadastro").modal('show');
});

$("#salvar-modal-cadastrar-guia").on("click", function () {
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