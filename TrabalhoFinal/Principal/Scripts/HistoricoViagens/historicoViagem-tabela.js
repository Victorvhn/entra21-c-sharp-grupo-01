$(function () {
    $('#historico-viagem-tabela').DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": "/HistoricoViagem/ObterTodosPorJSON",
        "columns": [
            { "data": "Id" },
            { "data": "Pacote.Nome" },
            { "data": "DataPadraoBR" },
            {
                data: null,
                render: function (data, type, row) {
                    return "<a class='btn btn-outline-warning' id='botao-editar-historico-viagem' >Editar</a>" +
                        "<a class='btn btn-outline-danger ml-1' id='botao-excluir-historico-viagem' data-id='" + row.Id + "' href='#' >Desativar</a>";
                }
            }
        ]
    });
});

$('#botao-modal-cadastrar-historico-viagem').on("click", function () {
    limparCampos();
    $("#historico-viagem-modal-cadastro").modal('show');
});

$('#botao-salvar-modal-cadastrar-historico-viagem').on('click', function () {
    var idPacoteVar = $("#select-cadastro-historico-viagem-idPacote").val();
    $.ajax({
        url: '/HistoricoViagem/Store',
        method: 'post',
        data: {
            idPacote: $('#select-cadastro-historico-viagem-idPacote').val(),
            data: $('#campo-cadastro-historico-viagem-data').val()
        },
        success: function (data) {
            var resultado = JSON.parse(data);
            limparCampos();
            $('#historico-viagem-modal-cadastro').modal('hide');
            $('#historico-viagem-tabela').DataTable().ajax.reload();
            $(function () {
                new PNotify({
                    title: 'Sucesso!',
                    text: idPacoteVar + ' cadastrado com sucesso',
                    type: 'success'
                });
            });
        }
    });
});

$('table').on('click', '#botao-editar-historico-viagem', function () {
    var id = $(this).data('id');
    $.ajax({
        url: 'HistoricoViagem/Editar?id=' + id,
        success: function (resultado) {
            var data = JSON.parse(resultado);
            $('#campo-editar-historico-viagem-id').val(data.Id);
            $('#select-editar-historico-viagem-idPacote').val(data.idPacote);
            $('#campo-editar-historico-viagem-idPacote').val(data.DateTime);

            $('#historico-viagem-modal-editar').modal('show');
        }
    });
});

$('#botao-salvar-modal-editar-historico-viagem').on('click', function () {
    $.ajax({
        url: 'HistoricoViagem/Update',
        method: 'post',
        dataType: 'json',
        data: {
            id: $('#campo-editar-estado-id').val(),
            idPacote: $('#select-editar-historico-viagem-idPacote').val(),
            data: $('#campo-editar-historico-viagem-data').val()
        },
        success: function (data) {
            var resultado = JSON.parse(data);
            if (resultado == 1) {
                $('#historico-viagem-tabela').DataTable().ajax.reload();
                $(function () {
                    new PNotify({
                        title: 'Sucesso!',
                        text: 'Alterado com sucesso',
                        type: 'success'
                    });
                });
                $('#historico-viagem-modal-editar').modal('hide');
                limparCampos();
            } else {
                new PNotify({
                    title: 'Erro!',
                    text: 'Erro ao alterar',
                    type: 'error'
                });
            }
        }
    });
});

$('table').on('click', '#botao-excluir-historico-viagem', function () {
    var id = $(this).data('id');
    $.ajax({
        url: 'HistoricoViagem/Excluir?id=' + id,
        method: 'get',
        success: function (resultado) {
            if (resultado == 1) {
                new PNotify({
                    title: 'Desativado!',
                    text: 'Viagem desativada com sucesso',
                    type: 'success'
                });

                $('#historico-viagem-tabela').DataTable().ajax.reload();

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
    $("#select-cadastro-historico-viagem-idPacote").prop('selectedIndex', - 1);  
    $("#campo-cadastro-historicoViagens-data").val("");
    $("#select-editar-historicoViagens-idPacote").prop('selectedIndex', - 1);
    $("#campo-editar-historicoViagens-data").val("");
}