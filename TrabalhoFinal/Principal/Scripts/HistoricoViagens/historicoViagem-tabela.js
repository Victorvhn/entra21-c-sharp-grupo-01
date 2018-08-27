$(function () {
    $('#historicoViagem-tabela').DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": "/HistoricoViagem/ObterTodosPorJSON",
        "columns": [
            { "data": "Id" },
            { "data": "IdPacote" },            

        ]
    });
});