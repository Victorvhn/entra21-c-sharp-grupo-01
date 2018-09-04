$(function () {
    $('#table-cidade').DataTable({
        processing: true,
        serverSide: true,
        ajax: '/Cidade/ObterTodosPorJSON',
        columns: [
            { data: 'Id' },
            { data: 'Estado.Nome' },
            { data: 'Nome' },
            {
                data: null,
                render: function (data, type, row) {
                    return "<a class='btn btn-outline-info' id='botao-editar-cidade' data-id='" + row.Id + "'>Editar</a>" +
                        "<a class='btn btn-outline-danger ml-1' id='botao-excluir-cidade' data-id='" + row.Id + "' data-nome='" + row.Nome + "'>Desativar</a>";
                }
            }
        ]
    });
});