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
    limparCamposCidadeCadastro();
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
            limparCamposCidadeCadastro();
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

$('table').on('click', '#botao-editar-cidade', function () {
    var id = $(this).data('id');
    $.ajax({
        url: '/Cidade/Editar?id=' + id,
        method: 'get',
        success: function (resultado) {
            var data = JSON.parse(resultado);
            $('#campo-editar-cidade-id').val(data.Id);
            $('#select-modal-editar-cidade').val(data.idEstado);
            $('#campo-editar-cidade-nome').val(data.Nome);

            $('#cidade-modal-editar').modal('show');            
        }
    });
});

$('#botao-salvar-modal-editar-cidade').on('click', function () {
    $.ajax({
        ulr: '/Cidade/Update',
        method: 'post',
        dataType: 'json',
        data: {
            id: $('#campo-editar-cidade-id').val(),
            idEstado: $('#select-modal-editar-cidade').val(),
            nome: $('#campo-editar-cidade-nome').val()
        },
        success: function (data) {
            var resultado = JSON.parse(data);
            if (resultado == 1) {
                $('#table-cidade').DataTable.ajax.reload();
                $(function () {
                    new PNotify({
                        title: 'Sucesso!',
                        text: 'Alterado com sucesso',
                        type: 'info'
                    });
                });
                $('#cidade-modal-editar').modal('hide');
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

$('table').on('click', '#botao-excluir-cidade', function () {
    var id = $(this).data('id');
    var nome = $(this).data('nome');
    $.ajax({
        url: 'Cidade/Excluir?id=' + id,
        method: 'get',
        success: function (data) {
            var resultado = JSON.parse(data);
            if (resultado == 1) {
                new PNotify({
                    title: 'Desativado!',
                    text: nome + ' desativado com sucesso',
                    type: 'success'
                });

                $('#table-cidade').DataTable().ajax.reload();

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

function limparCamposCidadeCadastro() {
    $('#select-modal-cadastro-cidade').val('-1');
    $('#campo-cadastro-cidade-nome').val('');
}

function limparCamposCidadeEditar() {
    
}