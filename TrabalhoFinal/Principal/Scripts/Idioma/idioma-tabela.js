$(function () {
    $('#table-idiomas').DataTable({
        processing: true,
        serverSide: true,
        ajax: "/Idioma/ObterTodosPorJSON",
        columns: [
            { data: "Id" },
            { data: "Nome" },
            {
                data: null,
                render: function (data, type, row) {
                    return "<a class='btn btn-outline-info' id='botao-editar-idioma' data-id='" + row.Id + "'>Editar</a>" +
                        "<a class='btn btn-outline-danger ml-1' id='botao-excluir-idioma' data-nome='" + row.Nome + "' data-id='" + row.Id + "' href='#' > Excluir</a>";
                }
            }
        ]
    });
});

$('#botao-modal-cadastrar-idioma').on("click", function () {
    limparCampos();
    $("#idioma-modal-cadastro").modal('show');
});

$("#botao-salvar-modal-cadastrar-idioma").on('click', function () {
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
                    text: nomeVar + ' cadastrado com sucesso',
                    type: 'success'
                });
            });


        }
    });
});

$('table').on('click', '#botao-editar-idioma', function () {
    var id = $(this).data('id');
    $.ajax({
        url: '/Idioma/Editar?id=' + id,
        success: function (resultado) {
            var data = JSON.parse(resultado);
            $('#campo-id-editar-idioma').val(data.Id);
            $('#select-editar-idioma').val(data.Nome);
            $('#idioma-modal-editar').modal('show');
        }
    });
});

$('#botao-salvar-modal-editar-idioma').on('click', function () {
    $.ajax({
        url: '/Idioma/Update',
        method: 'post',
        dataType: 'json',
        data: {
            id: $('#campo-id-editar-idioma').val(),
            nome: $('#select-editar-idioma').val()
        },
        success: function (data) {
            var resultado = JSON.parse(data);
            if (resultado == 1) {
                limparCampos();
                $('#idioma-modal-editar').modal('hide');
                $(function () {
                    new PNotify({
                        title: 'Sucesso!',
                        text: 'Alterado com sucesso',
                        type: 'info'
                    });
                });
                $('#table-idiomas').DataTable().ajax.reload();

            }
            else {
                new PNotify({
                    title: 'Erro!',
                    text: 'Erro ao alterar',
                    type: 'error'
                });
            }
        }
    });
});

$('table').on('click', '#botao-excluir-idioma', function () {
    var id = $(this).data('id');
    var nome = $(this).data('nome');
    $.ajax({
        url: '/Idioma/Excluir?id=' + id,
        method: 'get',
        success: function (data) {
            var resultado = JSON.parse(data);
            if (resultado == 1) {
                new PNotify({
                    title: 'Desativado!',
                    text: nome + ' desativado com sucesso',
                    type: 'success'
                });

                $('#table-idiomas').DataTable().ajax.reload();
            }
            else {
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
    $("#campo-cadastro-idioma").prop('');    
}