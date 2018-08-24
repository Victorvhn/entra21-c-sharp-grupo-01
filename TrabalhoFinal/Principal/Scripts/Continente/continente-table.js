$(function () {
    $('#table-continente').DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": "/Continente/ObterTodosPorJSON",
        "columns": [
            { "data": "Id" },
            { "data": "Nome" },
            { "data": "Id" }
        ]
    });
});


$("table-continente").on("click")