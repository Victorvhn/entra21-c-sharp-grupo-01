$(function () {
    $('#table-cidade').DataTable({
        processing: true,
        serverSide: true,
        ajax: '/Cidade/ObterTodosPorJSON',
        columns: [
            { data: 'Id' },
            { data: 'Estado.Nome' },
            { data: 'Nome' },
            {
                data: null,
                render: function (data, type, row) {
                    return "<a class='btn btn-outline-info' id='botao-editar-cidade' data-id='" + row.Id + "'>Editar</a>" +
                        "<a class='btn btn-outline-danger ml-1' id='botao-excluir-cidade' data-id='" + row.Id + "' data-nome='" + row.Nome + "'>Desativar</a>";
                }
            }
        ]
    });
});

$('#botao-modal-cadastrar-cidade').on('click', function () {
    limparCampos();
    $('#cidade-modal-cadastro').modal('show');
});

$('#botao-salvar-modal-cadastrar-cidade').on('click', function () {
    var nomeVar = $('#campo-cadastro-cidade-nome').val();
    $.ajax({
        url: 'Cidade/Store',
        method: 'post',
        data: {
            idEstado: $('#select-modal-cadastro-cidade').val(),
            nome: $('#campo-cadastro-cidade-nome').val()
        },
        success: function (data) {
            var resultado = JSON.parse(data);
            limparCampos();
            $('#cidade-modal-cadastro').modal('hide');
            $('#table-cidade').DataTable().ajax.reload();
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

function limparCampos() {
    $('#select-modal-cadastro-cidade').prop('selectedIndex', -1);
    $('#cadastro-cidade-campo-nome').val('');
}