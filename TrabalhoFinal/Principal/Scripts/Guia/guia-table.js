$(function () {
    $('#table-guia').DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": "/Guia/ObterTodosPorJSON",
        "columns": [
            { "data": "Id" },
            { "data": "Nome" },
            { "data": "Sobrenome" },
            { "data": "DataNascimento" },
            { "data": "Cpf" },
            { "data": "Rank" }
        ]
    });
});


