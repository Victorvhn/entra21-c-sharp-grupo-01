$(function () {
    $('#table-estados').DataTable({
        processing: true,
        serverSide: true,
        ajax: '/Estado/ObterTodosPorJSON',
        columns: [
            { data: 'Id' },
            { data: 'Nome' },
            {
                data: null,
                render: function (data, type, row) {
                    return "<a class='btn btn-outline-info' id='botao-editar-estado' data-id='" + row.Id + "'>Editar</a>" +
                        "<a class='btn btn-outline-danger ml-1' id='botao-excluir-estado' data-id='" + row.Id + "' data-nome='" + row.Nome + "'>Desativar</a>";
                }
            }
        ]
    });
});

$('#botao-modal-cadastrar-estado').on('click', function () {
    limparCampos();
    $('#estado-modal-cadastro').modal('show');
});

$('#botao-salvar-modal-cadastrar-estado').on('click', function () {
    var nomeVar = $("#campo-cadastro-estado-nome").val();
    $.ajax({
        url: '/Estado/Store',
        method: 'post',
        data: {
            nome: $('#campo-cadastro-estado-nome').val()
        },
        success: function (data) {
            var resultado = JSON.parse(data);
            limparCampos();
            $('#estado-modal-cadastro').modal('hide');
            $('#table-estados').DataTable().ajax.reload();
            $(function () {
                new PNotify({
                    title: 'Sucesso!',
                    text: nomeVar + ' cadastrado com sucesso',
                    type: 'success'
                });
            });
        }
    });
});

$('table').on('click', '#botao-editar-estado', function () {
    var id = $(this).data('id');
    $.ajax({
        url: 'Estado/Editar?id=' + id,
        success: function (resultado) {
            var data = JSON.parse(resultado);
            $('#campo-editar-estado-id').val(data.Id);
            $('#campo-editar-estado-nome').val(data.Nome);

            $('#estado-modal-editar').modal('show');
        }
    });
});

$('#botao-salvar-modal-editar-estado').on('click', function () {
    $.ajax({
        url: 'Estado/Update',
        method: 'post',
        dataType: 'json',
        data: {
            id: $('#campo-editar-estado-id').val(),
            nome: $('#campo-editar-estado-nome').val()
        },
        success: function (data) {
            var resultado = JSON.parse(data);
            if (resultado == 1) {
                $('#table-estados').DataTable().ajax.reload();
                $(function () {
                    new PNotify({
                        title: 'Sucesso!',
                        text: 'Alterado com sucesso',
                        type: 'success'
                    });
                });
                $('#estado-modal-editar').modal('hide');
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

$('table').on('click', '#botao-excluir-estado', function () {
    var id = $(this).data('id');
    var nome = $(this).data('nome');
    $.ajax({
        url: 'Estado/Excluir?id=' + id,
        method: 'get',
        success: function (resultado) {
            if (resultado == 1) {
                new PNotify({
                    title: 'Desativado!',
                    text: nome + ' desativado com sucesso',
                    type: 'success'
                });

                $('#table-estados').DataTable().ajax.reload();

            } else {
                new PNotify({
                    title: 'Erro!',
                    text: 'Erro ao desativar ' + nome,
                    type: 'error'
                });
            }
        }
    });
});

function limparCampos() {
    $('#campo-cadastro-estado-nome').val('');
    $('#campo-editar-estado-id').val('');
    $('#campo-editar-estado-nome').val('');
}