$(function () {

    //Preencher DataTable
    $('#table-pacotes-confimacao').DataTable({
        ajax: '/ConfirmarPacotes/ObterTodosPorJSON',
        columns: [
            { data: 'Id' },
            { data: 'Turista.Nome' },
            { data: 'Pacote.Nome' },
            { data: 'Pacote.Valor' },
            { data: 'StatusPedido' },
            {
                data: null,
                render: function (row) {
                    return "<a class='btn btn-outline-warning botao-confirmar-compra-admin' data-id='" + row.Id + "'>Confirmar Pedido</a>";
                }
            }
        ]
    });



});