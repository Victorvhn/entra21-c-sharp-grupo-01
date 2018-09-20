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
                data: 'Id',
                bSortable: false,
                width: "20%",
            }
        ]
    });



});