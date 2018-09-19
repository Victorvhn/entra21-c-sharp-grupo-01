$(function () {
    $('#table-viagemTurista').DataTable({
        processing: true,
        serverSide: true,
        ajax: '/ViagemTurista/ObterTodosPorJSON',
        columns: [
            { data: 'Id' },
            { data: 'Turista.Nome' },
            { data: 'Viagem.Nome' },
            { data: 'Valor' },
            {
                data: null,
                render: function (data, type, row) {
                    return '<a class="btn btn-outline-info" id="botao-editar-viagemTurista" data-id="' + row.Id + '" data-toggle="modal" data-target="#viagemTurista-modal-editar">Editar</a>' +
                        "<a class='btn btn-outline-danger ml-1' id='botao-excluir-viagemTurista' data-id='" + row.Id + "'>Desativar</a>";
                }

            }
        ]
    });


    $('#botao-modal-cadastrar-viagemTurista').on('click', function () {
        limparCampos();
        $('#viagemTurista-modal-cadastro').modal('show');

    });

    $('#botao-salvar-modal-cadastrar-viagemTurista').on('click', function () {
        var nomeVar = $('#campo-cadastro-viagemTurista').val();
        $.ajax({
            url: '/ViagemTurista/Store',
            method: 'post',
            data: {
                idTurista: $('#select-cadastro-viagemTurista-turista').val(),
                idViagem: $('#select-cadastro-viagemTurista-viagem').val()
            },
            success: function (data) {
                var resultado = JSON.parse(data);
                limparCampos();
                $('#viagemTurista-modal-cadastro').modal('hide');
                $('#table-viagemTurista').DataTable().ajax.reload();
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

    $('table').on('click', '#botao-editar-viagemTurista', function () {
        var id = $(this).data('id');
        $.ajax({
            url: '/ViagemTurista/Editar?id= ' + id,
            method: 'get',
            success: function (resultado) {
                var data = JSON.parse(resultado);
                $('#campo-editar-viagem-id').val(data.Id);
                $('#select-modal-editar-turista :selected').text(data.idTurista);
                $('#select-modal-editar-viagem :selected').text(data.idViagem);

                $('#viagem-modal-editar').modal('show');
            }


        });

    });

    $('#botao-salvar-modal-editar-viagem').on('click', function () {
        $.ajax({
            url: '/ViagemTurista/Update',
            method: 'post',
            dataType: 'json',
            data: {
                idTurista: $('#select-modal-editar-turista').val(),
                idViagem: $('#select-modal-editar-viagem').val()
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
                    $('#viagemTurista-modal-editar').modal('hide');
                    limparCampos();
                } else {
                    new PNotify({
                        title: 'Erro!',
                        text: 'Erro ao alterar',
                        type: 'Info'
                    });
                }
            }
        });
    });

    $('table').on('click', '#botao-excluir-viagemTurista', function () {
        var id = $(this).data('id');
        $.ajax({
            url: '/ViagemTurista/Excluir?=' + id,
            method: 'get',
            success: function (data) {
                var result = JSON.parse(data);
                if (result == 1) {
                    new PNotify({
                        title: 'Desativado!',
                        text: 'Viagem desativada com sucesso',
                        type: 'success'
                    });

                    $('#table-viagemTurista').DataTable().ajax.reload();

                } else {
                    new PNotify({
                        title: 'Erro!',
                        text: 'Erro ao desativar viagem',
                        type: 'Error'
                    });
                }
            }
        });
    });
});