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
            { data: 'Id'}
        ]
    });



});