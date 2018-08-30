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
                        "<a class='btn btn-outline-danger ml-1' value='" + row.Id + "'>Excluir</a>";

                }
            }
        ]
    });
});



$("#botao-modal-cadastrar-guia").on("click", function () {
    limparCampos();
    $("#guia-modal-cadastro").modal('show');
});

$("#salvar-modal-cadastrar-guia").on("click", function () {
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
            $.pnotify({
                title: 'Sticky Success',
                text: 'Sticky success... I\'m not even gonna make a joke.',
                type: 'success',
                hide: false
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