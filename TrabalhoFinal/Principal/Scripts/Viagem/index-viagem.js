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

$('#botao-salvar-modal-cadastrar-viagem').on('click', function () {
    var nomeVar = $('#campo-cadastro-viagem').val();
    $.ajax({
        url: '/Viagem/Store',
        method: 'post',
        data: {
            idGuia: $('#select-cadastro-viagem-guia').val(),
            idPacote: $('#select-cadastro-viagem-pacote').val(),
            dataHoraSaidaPadraoBR: $('#campo-cadastro-data-saida-viagem').val(),
            dataHoraVoltaPadraoBR: $('#campo-cadastro-data-volta-viagem').val()
        },
        success: function (data) {
            var resultado = JSON.parse(data);
            limparCampos();
            $('#viagem-modal-cadastro').modal('hide');
            $('#table-viagem').DataTable().ajax.reload();
            $(function () {
                new PNotify({
                    title: 'Sucesso!',
                    text: nomeVar + 'cadastrado com sucesso',
                    type: 'success'
                });
            });
        }
    });
});

$('table').on('click', '#botao-editar-viagem', function () {
    var id = $(this).data('id');
    $.ajax({
        url: '/Viagem/Editar?id=' + id,
        method: 'get',
        success: function (resultado) {
            var data = JSON.parse(resultado);
            $('#select-cadastro-viagem-guia').val(data.idGuia);
            $('#select-cadastro-viagem-pacote').val(data.idPacote);
            $('#campo-cadastro-data-saida-viagem').val(data.dataHoraSaidaPadraoBR);
            $('#campo-cadastro-data-volta-viagem').val(data.dataHoraVoltaPadraoBR);
            $('#viagem-modal-editar').modal('show');


        }
    });
});

$('#botao-salvar-modal-editar-viagem').on('click', function () {
    $.ajax({
        url: '/Viagem/Update',
        method: 'post',
        dataType: 'json',
        data: {
            idGuia: $('#select-editar-viagem-guia').val(),
            idPacote: $('#select-editar-viagem-pacote').val(),
            dataHoraSaidaPadraoBR: $('#campo-editar-data-saida-viagem').val(),
            dataHoraVoltaPadraoBR: $('#campo-editar-data-volta-viagem').val()
        },
        success: function (data) {
            var resultado = JSON.parse(data);
            if (resultado == 1) {
                $(function () {
                    new PNotify({
                        title: 'Sucesso!',
                        text: 'Alterado com sucesso',
                        type: 'info'
                    });
                });
                $('#table-viagem').DataTable().ajax.reload();
                $('#viagem-modal-editar').modal('hide');
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
    $('.form-control').val();
}