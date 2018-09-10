$(function () {
    $('#table-cidade').DataTable({
        processing: true,
        serverSide: true,
        ajax: '/PontoTuristico/ObterTodosPorJSON',
        columns: [
            { data: 'Id' },
            { data: 'Endereco.Nome' },
            { data: 'Nome' },
            {
                data: null,
                render: function (data, type, row) {
                    return "<a class='btn btn-outline-info' id='botao-editar-pontoturistico' data-id='" + row.Id + "'>Editar</a>" +
                        "<a class='btn btn-outline-danger ml-1' id='botao-excluir-pontoturistico' data-id='" + row.Id + "' data-nome='" + row.Nome + "'>Desativar</a>";
                }
            }
        ]
    });
});

$('#botao-modal-cadastrar-pontoturistico').on('click', function () {
    limparCamposPontoTuristicoCadastro();
    $('#cidade-modal-pontoturistico').modal('show');
});

$('#botao-salvar-modal-cadastrar-pontoturistico').on('click', function () {
    var nomeVar = $('#campo-cadastro-pontoturistico-nome').val();
    $.ajax({
        url: '/PontoTuristico/Store',
        method: 'post',
        data: {
            idEstado: $('#select-modal-cadastro-pontoturistico').val(),
            nome: $('#campo-cadastro-pontoturistico-nome').val()
        },
        success: function (data) {
            var resultado = JSON.parse(data);
            limparCamposPontoTuristicoCadastro();
            $('#cidade-modal-pontoturistico').modal('hide');
            $('#table-pontoturistico').DataTable().ajax.reload();
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

$('table').on('click', '#botao-editar-pontoturistico', function () {
    var id = $(this).data('id');
    $.ajax({
        url: '/PontoTuristico/Editar?id=' + id,
        method: 'get',
        success: function (resultado) {
            var data = JSON.parse(resultado);
            $('#campo-editar-pontoturistico-id').val(data.Id);
            $('#select-modal-editar-pontoturistico :selected').text(data.idEstado);
            $('#campo-editar-pontoturistico-nome').val(data.Nome);

            $('#pontoturistico-modal-editar').modal('show');
        }
    });
});

$('#botao-salvar-modal-editar-pontoturistico').on('click', function () {
    $.ajax({
        url: '/PontoTuristico/Update',
        method: 'Post',
        dataType: 'json',
        data: {
            id: $('#campo-editar-pontoturistico-id').val(),
            idEstado: $('#select-modal-editar-pontoturistico').val(),
            nome: $('#campo-editar-pontoturistico-nome').val()
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
                $('#table-pontoturistico').DataTable().ajax.reload();
                $('#pontoturistico-modal-editar').modal('hide');
                limparCamposCidadeEditar();
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

$('table').on('click', '#botao-excluir-pontoturistico', function () {
    var id = $(this).data('id');
    var nome = $(this).data('nome');
    $.ajax({
        url: 'PontoTuristico/Excluir?id=' + id,
        method: 'get',
        success: function (data) {
            var resultado = JSON.parse(data);
            if (resultado == 1) {
                new PNotify({
                    title: 'Desativado!',
                    text: nome + ' desativado com sucesso',
                    type: 'success'
                });

                $('#table-pontoturistico').DataTable().ajax.reload();

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

function limparCamposPontoTuristicoCadastro() {
    $('#select-modal-cadastro-pontoturistico').val('');
    $('#campo-cadastro-pontoturistico-nome').val('');
}

function limparCamposPontoTuristicoEditar() {

}