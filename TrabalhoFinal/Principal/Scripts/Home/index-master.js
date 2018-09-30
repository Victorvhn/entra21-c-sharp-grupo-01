$(function () {
    $('#botao-modal-configuracoes').on('click', function () {
        $('#Modal-tab').modal('show');
    });

    $('#botao-salvar-modal-configuracoes').on('click', function () {
        $.ajax({
            url: '/Home/AlterarDados',
            method: 'post',
            data: {
                email: $('#alterar-email').val(),
                senha: $('#alterar-senha').val(),
                nome: $('#configuracoes-nome').val(),
                sobrenome: $('#configuracoes-sobrenome').val(),
                dataNasciment: $('#configuracoes-data-nascimento').val(),
                cep: $('#configuracoes-cep').val()
            },
            success: function () {
                alert('tudo certo');
            }
        });
    });

});