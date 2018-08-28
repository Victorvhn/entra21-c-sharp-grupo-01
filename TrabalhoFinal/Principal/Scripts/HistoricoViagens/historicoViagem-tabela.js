$(function () {
    $('#historico-viagem-tabela').DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": "/HistoricoViagem/ObterTodosPorJSON",
        "columns": [
            { "data": "Id" },
            { "data": "IdPacote" },
            { "data": "Data"}
           
            

        ]
    });
});