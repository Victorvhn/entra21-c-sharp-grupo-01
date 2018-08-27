$(function () {
    $('#table-paises').DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": "/Pais/ObterTodosPorJSON",
        "columns": [
            { "data": "Id" },
            { "data": "Continente.Nome" },
            { "data": "Nome" }
        ]
    });
});