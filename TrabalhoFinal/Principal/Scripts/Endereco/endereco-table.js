$(function () {
    $('#table-endereco').DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": "/Endereco/ObterTodosPorJSON",
        "columns": [
            { "data": "Id" },
            { "data": "Cep" },
            { "data": "Logradouro" },
            { "data": "Numero" }
        ]
    });
});