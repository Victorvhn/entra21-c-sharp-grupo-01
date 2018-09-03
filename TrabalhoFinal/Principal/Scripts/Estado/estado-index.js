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
                    return "<a class='btn btn-outline-info' id='botao-editar-guia' data-id='" + row.Id + "'>Editar</a>" +
                        "<a class='btn btn-outline-danger ml-1' id='botao-excluir-guia' data-id='" + row.Id + "' href='#' >Excluir</a>";
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
            $(function () {
                new PNotify({
                    title: 'Sucesso!',
                    text: nomeVar + ' cadastrado com sucesso',
                    type: 'success'
                });
            });
            $('#estado-modal-cadastro').DataTable().ajax.reload();
        }
    });
});

function limparCampos() {
    $('#campo-cadastro-estado-nome')
}