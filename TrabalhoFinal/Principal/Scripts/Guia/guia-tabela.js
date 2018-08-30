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


$("#botao-modal-editar-guia").on("click", function () {
    if ($("#large-Modal").length === 0) {
        $.ajax({
            url: "/Guia/Modal",
            method: "get",
            success: function (data) {
                $("body").append(data);
                $("#large-Modal").modal('show');
            }
        });
    } else {
        $("#large-Modal").modal('show');
        limparCampos();
    }

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
    });
});

function limparCampos() {
    $("#nome").val("");
    $("#idade").val("");
    $("#salario").val("");
}