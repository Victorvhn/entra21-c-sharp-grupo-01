$(function () {

    //Preencher DataTable
    $('#table-pacotes-confimacao').DataTable({
        ajax: '/ConfirmarPacotes/ObterTodosPorJSON',
        order: [[4, "asc"]],
        columns: [
            {
                data: 'Id',
                bSortable: false,
                width: "10%",
                target: 0
            },
            {
                data: 'Turista.Nome',
                bSortable: true,
                width: "20%",
                target: 1
            },
            {
                data: 'Pacote.Nome',
                bSortable: true,
                width: "20%",
                target: 2
            },
            {
                data: 'Pacote.Valor',
                bSortable: true,
                width: "10%",
                target: 3
            },
            {
                data: 'StatusPedido',
                bSortable: true,
                width: "20%",
                target: 4
            },
            {
                data: null,
                bSortable: false,
                width: "20%",
                render: function (data, type, row) {
                    return "<a style='font-size: 24px;' class='btn fa fa-trash botao-excluir-confirmacao' data-id='" + row.Id + "'></a>" +
                        "<a style='font-size: 24px;' class='btn fa fa-check ml-1 botao-aceitar-confirmacao' data-id='" + row.Id + "'></a>";
                }
            }
        ]
    });

    $('table').on('click', '.botao-aceitar-confirmacao', function () {
        var id = $(this).data('id');
        $.ajax({
            url: '/CriarPacote/Confirmar?id=' + id,
            method: 'get',
            success: function (data) {
                var resultado = JSON.parse(data);
                if (resultado == 1) {
                    new PNotify({
                        title: 'Confirmado!',
                        text: 'Confirmado com sucesso',
                        type: 'success'
                    });

                    $('#table-pacotes-confimacao').DataTable().ajax.reload();
                }
            }
        });
    });

});