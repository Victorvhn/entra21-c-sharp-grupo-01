$(function () {
    $('#table-pontosturisticos').DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": "/PontoTuristico/ObterTodosParaJSON",
        "columns": [
            { "data": "Id" },
            { "data": "IdEndereco" },
            { "data": "PontoTuristico.Nome" },
            { "data": "Nome" }
        ]
    })
})