$(function () {
    $('#idioma-tabela').DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": "/Idioma/ObterTodosPorJSON",
        "columns": [
            { "data": "id" },
            { "data": "nome" },
            {
                data: null,
                render: function (data, type, row) {
                    return "<a class='btn btn-outline-info' id='botao-editar-idioma' data-id='" + row.Id + "'>Editar</a>" +
                        "<a class='btn btn-outline-danger ml-1' id='botao-excluir-idioma' data-id='" + row.Id + "' href='#' > Excluir</a>";
                }
            }
        ]
    });
});
$('#botao-modal-cadastrar-idioma').on("click", function () {
    limparCampos();
    $("#idioma-modal-cadastro").modal('show');
})

$("#botao-salvar-modal-cadastro-idioma").on('click', function () {
    var nomeVar = $("#select-cadastro-idioma").val();
    $.ajax({
        url: '/Idioma/Store',
        method: 'post',
        data: {
            nome: $('#select-cadastro-idioma').val()
        },
        success: function (data) {
            var resultado = JSON.parse(data);
            limparCampos();
            $('#idioma-modal-cadastro').modal('hide');
            $('#table-idiomas').DataTable().ajax.reload();
            $(function () {
                new PNotify({
                    title: 'Sucesso!',
                    text: nomeVar + 'cadastro com sucesso',
                    type: 'success'
                });
            });

        }
    });
});

function limparCampos() {
    $("#select-cadastro-idioma").prop('selectIndex', -1);
    $("#select-editar-idioma").prop('selectIndex', -1);
    
}