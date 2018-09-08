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
                    return "<a class='btn btn-outline-info' id='botao-editar-viagem' data-id='" + row.Id + "'>Editar</a>" +
                        "<a class='btn btn-outline-danger ml-1' id='botao-excluir-viagem' data-id='" + row.Id + "'>Desativar</a>";
                }
            }
        ]
    });
});

$('#botao-modal-cadastrar-viagem').on('click', function () {
    limparCampos();
    $('#viagem-modal-cadastro').modal('show');
});

$('table').on('click', '#botao-excluir-viagem', function () {
    var id = $(this).data('id');
    $.ajax({
        url: '/Viagem/Excluir?id=' + id,
        method: 'get',
        success: function (data) {
            var result = JSON.parse(data);
            if (result == 1) {
                new PNotify({
                    title: 'Desativado!',
                    text: 'Viagem desativada com sucesso',
                    type: 'success'
                });

                $('#table-viagens').DataTable().ajax.reload();

            } else {
                new PNotify({
                    title: 'Erro!',
                    text: 'Erro ao desativar viagem',
                    type: 'error'
                });
            }
        }
    });
});

function limparCampos() {
    
}