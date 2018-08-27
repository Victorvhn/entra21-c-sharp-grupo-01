$(function () {
    $('#pacote-tabela').DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": "/Pacote/ObterTodosPorJSON",
        "columns": [
            { "data": "Id" },
            { "data": "Nome" },
            { "data": "Valor" },
            { "data": "PercentualMaximoDesconto" }
            
        ]
    });
});


