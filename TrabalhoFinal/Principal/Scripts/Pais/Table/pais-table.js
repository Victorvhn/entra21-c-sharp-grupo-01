$(function () {
    $('#table-paises').DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": "/Pais/ObterTodosPorJSON",
        "columns": [
            { "data": "Id" },
            { "data": "Continente" },
            { "data": "Nome" }
        ]
    });
});