$(function () {
    $('#table-pacote-ponto-turistico').DataTable({
        ajax: '/PacotePontoTuristico/ObterTodosPorJSON',
        columns: [
            { data: 'Id' },
            { data: 'Pacote.Nome' },
            { data: 'PontoTuristico.Nome' },
            {
                data: null,
                render: function (data, type, row) {
                    return '<a class="btn btn-outline-info id="botao-editar-pacote-ponto-turistico" data-id="' + row.Id + '" data-toggle="modal" data-target="#pacote-ponto-turistico-modal-editar">Editar</a>' +
                        '<a class="btn btn-outline-danger ml-1 id="botao-excluir-pacote-ponto-turistico" data-id="' + row.Id + '">Desativar</a>';
                }
            }
        ]
    });
});

$('#botao-modal-cadastrar-pacote-ponto-turistico').on('click', function () {
    limparCampos();
    $('#pacote-ponto-turistico-modal-cadastro').modal('show');
});

$('#botao-salvar-modal-cadastrar-pacote-ponto-turistico').on('click', function () {
    $.ajax({
        url: '/PacotePontoTuristico/Store',
        method: 'post',
        data: {
            idPacote: $('#select-cadastro-pacote-ponto-turistico-pacote').val(),
            idPontoTuristico: $('#select-cadastro-pacote-ponto-turistico-ponto-turistico').val(),
        },
        success: function (data) {
            var resultado = JSON.parse(data);
            limparCampos();
            $('#pacote-ponto-turistico-modal-cadastro').modal('hide');
            $('#table-pacote-ponto-turistico').DataTable().ajax.reload();
            $(function () {
                new PNotify({
                    title: 'Sucesso!',
                    text: 'Pacote Ponto Turistico Cadastrado com sucesso',
                    type: 'success'
                });
            });
        }
    });
});

$('table').on('click', '#botao-editar-pacote-ponto-turistico', function () {
    var id = $(this).data('id');
    $.ajax({
        url: '/PacotePontoTuristico/Editar?id=' + id,
        method: 'get',
        success: function (resultado) {
            var data = JSON.parse(resultado);
            $('#campo-editar-pacote-ponto-turistico-id').val(data.Id);
            $('#select-editar-pacote-ponto-turistico-pacote :selected').text(data.idPacote);
            $('#select-editar-pacote-ponto-turistco-ponto-turistico :selected').text(data.idPontoTuristico)

            $('#pacote-ponto-turistico-modal-editar').modal('show');
        }
    });
});

$('#botao-salvar-modal-editar-pacote-ponto-turistico').on('click', function () {
    $.ajax({
        url: '/PacotePontoTuristico/Update',
        method: 'post',
        dataType: 'json',
        data: {
            id: $('#campo-editar-pacote-ponto-turistico-id').val(),
            idPacote: $('#select-editar-pacote-ponto-turistico-pacote').val(),
            idPontoTuristico: $('#select-editar-pacote-ponto-turistco-ponto-turistico').val()
        },
        success: function (data) {
            var resultado = JSON.parse(data);
            if (resultado == 1) {
                $(function () {
                    $('#table-pacote-ponto-turistico').DataTable().ajax.reload();
                    new PNotify({
                        title: 'Sucesso!',
                        text: 'Alterado com sucesso',
                        type: 'info'
                    });
                });
                $('#pacote-ponto-turistico-modal-editar').modal('hide');
                limparCampoEditar();
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

$('table').on('click', '#botao-excluir-pacote-ponto-turistico', function () {
    var id = $(this).data('id');
    $.ajax({
        url: 'PacotePontoTuristico/Excluir?id=' + id,
        method: 'get',
        success: function (data) {
            var result = JSON.parse(data);
            if (result == 1) {
                new PNotify({
                    title: 'Desativado',
                    text: 'Pacote Ponto Turistico Desativado com sucesso',
                    type: 'success'
                });
                $('table-pacote-ponto-turistico').DataTable().ajax.reload();
            } else {
                new PNotify({
                    title: 'Erro!',
                    text: 'Erro ao desativar Pacote Ponto Turistico',
                    type: 'error'
                });
            }
        }
    });
});

function limparCampos() {
    $('#select-cadastro-pacote-ponto-turistico-pacote').val();
    $('#select-cadastro-pacote-ponto-turistico-ponto-turistico').val();
}

function limparCamposEditar() {
    $('#select-editar-pacote-ponto-turistico-pacote').val();
    $('#select-editar-pacote-ponto-turistco-ponto-turistico').val();
}