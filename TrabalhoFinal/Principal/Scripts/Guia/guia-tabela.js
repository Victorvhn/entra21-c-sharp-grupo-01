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
            { "data": "Rank" }
        ]
    });
});


$("#botao-modal-cadastrar-guia").on("click", function () {
    limparCampos();
    $("#guia-modal-cadastro").modal('show');
});

$("body").on("click", "#salvar-modal-cadastrar-guia", function () {
    $.ajax({
        url: '/Guia/Cadastro',
        method: 'post',
        data: {
            nome: $("#campo-cadastro-guia-nome").val(),
            idade: $("#campo-cadastro-guia-sobrenome").val(),
            rg: $("#campo-cadastro-guia-rg").val(),
            cpf: $("#campo-cadastro-guia-cpf").val(),
            data_nascimento: $("#campo-cadastro-guia-data-nascimento").val(),
            sexo: $("#campo-cadastro-guia-sexo").val(),
            numero_carteira_trabalho: $("#campo-cadastro-guia-numero-carteira-trabalho").val(),
            salario: $("#campo-cadastro-guia-salario").val(),
            categoria_habilitacao: $("#campo-cadastro-guia-categoria-habilitacao").val(),
            rank: $("#campo-cadastro-guia-rank").val()
        },
        success: function () {
            limparCampos()
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