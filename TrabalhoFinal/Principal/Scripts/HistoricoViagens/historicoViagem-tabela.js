$(function () {
    $('#historico-viagem-tabela').DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": "/HistoricoViagem/ObterTodosPorJSON",
        "columns": [
            { "data": "Id" },
            { "data": "Pacote.Nome" },
            { "data": "DataPadraoBR" },
            {
                data: null,
                render: function (data, type, row) {
                    return "<a class='btn btn-outline-warning' value='" + row.Id + "'>Editar</a>" +
                        "<a class='btn btn-outline-danger' value='" + row.Id + "'>Excluir</a>";
                }
            }
        ]
    });
});