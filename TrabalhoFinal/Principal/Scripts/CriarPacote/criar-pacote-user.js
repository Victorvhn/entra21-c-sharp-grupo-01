$(function () {


    //Abrir modal cadastro
    $('#cadastro-pacote-user').on('click', function () {
        limparCampos();
        $('#modal-cadastro-pacote-user').modal('show');
    });

    //Store 
    $("#botao-salvar-pacote-user").on("click", function () {
        $.ajax({
            "url": "/CriarPacote/Store",
            method: "post",
            data: {
                Nome: $("#cadastro-pacote-nome-user").val(),
                DataHorarioSaida: $("#campo-data-horario-saida-cadastro-user").val(),
                DataHorarioVolta: $("#campo-data-horario-retorno-cadastro-user").val(),
                IdGuia: $("#select-guia-pacote-user").val(),
                IdsPontosTuristicos: $("#select-pontos-turisticos-cadastro-user").val(),
            }
        })
    });



    function limparCampos() {

    }
});