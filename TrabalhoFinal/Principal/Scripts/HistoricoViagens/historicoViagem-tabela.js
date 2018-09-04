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
                    return "<a class='btn btn-outline-warning' id='botao-editar-historico-viagem' data-id='" + row.Id + "'>Editar</a>" +
                        "<a class='btn btn-outline-danger ml-1' id='botao-excluir-historico-viagem' data-id='" + row.Id + "' href='#' >Excluir</a>";
                }
            }
        ]
    });
});

$('table').on('click', '#botao-excluir-historico-viagem', function () {
    var id = $(this).data('id');
    $.ajax({
        url: 'HistoricoViagens/Excluir?id=' + id,
        method: 'get',
        success: function (result) {
            if (result === 1) {
                new PNotify({
                    title: 'Desativado!',
                    text: 'Desativado com sucesso',
                    type: 'Success'
                });

                $('#historicoViagem-tabela').DataTable().ajax.reload();

            } else {
                new PNotify({
                    title: 'Erro!',
                    text: 'Desativado com sucesso',
                    type: 'error'
                });
            }
        }
    });
});


$('#botao-modal-cadastrar-historicoViagens').on("click", function () {
    limparCampos();
    $("historicoViagens-modal-cadastro").modal('show');
});


$('table').on("Click", "#botao-editar-historicoViagens", function () {
    var id = $(this).data('id');
    $.ajax({
        url: '/HistoricoViagens/Editar?id' + id,
        success: function (result) {
            var data = JSON.parse(result);
            $('#select-editar-historico-viagem-idpacote').val(data.IdPacote);
            $('#campo-editar-historico-viagem-data').val(data.DataTime);

            $("#historicoViagens-modal-editar").modal("show");
        }
    });
});


$('#botao-acao-editar-historicoViagens').on("Click", function () {

    $.ajax({
        url: '/HistoricoViagens/Update',
        method: 'post',
        datatype: 'json',
        data: {
            idPacote: $('#select-editar-historico-viagem-idpacote').val(),
            data: $('#campo-editar-historico-viagem-data').val()
        },
        success: function (data) {
            var resultado = JSON.parse(data);
            limparCampos();
            $('#historicoViagens-modal-editar').modal('hide');
            $(function () {
                new PNotify({
                    title: 'Sucesso!',
                    text: idPacote.Var + 'Cadastrado com sucesso',
                    type: 'success'
                });
            });
            $('#historicoViagens-tabela').DataTable().ajax.reload();
        }
    });
    limparCampos();
});


$("#botao-salvar-modal-historicoViagens").on("Click", function () {
    var idPacoteVar = $("#select-cadastro-historicoViagens-idPacote").val();
    $.ajax({
        url: '/HistoricoViagens/Store',
        method: 'post',
        data: {
            idPacote: $("#select-cadastro-historicoViagens-idPacote").val(),
            data: $("#campo-cadastro-historicoViagens-data").val(),
        },
        success: function (data) {
            var resultado = JSON.parse(data);
            limparCampos();
            $("#historicoViagens-modal-cadastro").modal('hide');
            $(function () {
                new PNotify({
                    title: 'Sucesso!',
                    text: idPacoteVar + 'Cadastrado com sucesso',
                    type: 'success'
                });
            });

            $('#historicoViagem-tabela').DataTable().ajax.reload();
        }
    });
});


$("#botao-update-modal-editar-historicoViagens").on("Click", function () {
    var idPacoteVar = $("#select-editar-historicoViagens-idPacote").val();
    $.ajax({
        url: '/HistoricoViagens/Update',
        method: 'post',
        data{
            idPacote: $("#select-editar-historicoViagens-idPacote").val(),
            data: $("#campo-editar-historicoViagens-data").val()
        },
        success: function (data) {
            var resultado = JSON.parse(data);
            limparCampos();
            $("#historicoViagens-modal-editar").modal('hide');
            $(function () {
                new PNotify({
                    title: 'Sucesso!',
                    text: idPacoteVar + 'Cadastrado com sucesso',
                    type: 'success'
                });
            });
            $("#historicoViagem-tabela").DataTable().ajax.reload();
        }
    });
});

function limparCampos() {
    $("#select-cadastro-historicoViagens-idPacote").val(- 1);
    $("#campo-cadastro-historicoViagens-data").val("");
}