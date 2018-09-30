$(function () {
    //Abre modal de cadastro
    $('#criar-conta').on("click", function () {
        $("#modal-criar-conta").modal('show');
    });

    $('#cadastrar-agora').on('click', function () {
        $.ajax({
            url: '/Login/Store',
            method: 'post',
            data: {
                email: $('#cadastro-email').val(),
                senha: $('#cadastro-senha').val()
            },
            success: function (data) {
                var result = JSON.parse(data);
                limparCampos();
                $('#modal-criar-conta').modal('hide');
                $('#modal-criar-turista').modal('show');
                $('#campo-id-invisivel').val(result.id);
            }
        });
    });

    $('#cadastrar-agora-turista').on('click', function () {
        $.ajax({
            url: '/Login/StoreTurista',
            method: 'post',
            data: {
                idLogin: $('#campo-id-invisivel').val(),
                nome: $('#cadastro-nome-turista').val(),
                sobrenome: $('#cadastro-sobrenome-turista').val(),
                cpf: $('#cadastro-cpf-turista').val(),
                rg: $('#cadastro-rg-turista').val(),
                dataNascimento: $('#cadastro-data-nascimento').val(),
                sexo: $('input[type=radio][name=sexo]:checked').val()
            },
            success: function () {
                limparCamposTurista1();
                $('#modal-criar-turista').modal('hide');
                $(function () {
                    new PNotify({
                        title: 'Sucesso',
                        text: 'Você acaba de se cadastrar, favor entre para cadastrar seu endereço',
                        type: 'success'
                    });
                });
            }
        });
    });

    function limparCampos() {
        $('#cadastro-email').val("");
        $('#cadastro-senha').val("");
    }

    function limparCamposTurista1() {
        $('#campo-id-invisivel').val("");
        $('#cadastro-nome-turista').val("");
        $('#cadastro-sobrenome-turista').val("");
        $('#cadastro-cpf-turista').val("");
        $('#cadastro-rg-turista').val("");
        $('#cadastro-data-nascimento').val("");
        $('input[type=radio][name=sexo]:checked').val("");
    }
});