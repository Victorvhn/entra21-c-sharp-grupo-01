$(function () {
    $('#table-viagens').DataTable({
        processing: true,
        serverSide: true,
        ajax: '/Viagem/ObterTodosPorJSON',
        columns: [
            { data: 'Id' },
            { data: 'Pacote.Nome' },
            { data: 'Guia.Nome' },
            { data: 'DataHoraSaidaPadraoBR' },
            { data: 'DataHoraVoltaPadraoBR' },
            {
                data: null,
                render: function (data, type, row) {
                    return "<a class='btn btn-outline-info' id='botao-editar-estado' data-id='" + row.Id + "'>Editar</a>" +
                        "<a class='btn btn-outline-danger ml-1' id='botao-excluir-estado' data-id='" + row.Id + "'>Desativar</a>";
                }
            }
        ]
    });
});

