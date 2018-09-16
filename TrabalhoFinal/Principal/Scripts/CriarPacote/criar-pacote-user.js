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
                Valor: $("#campo-valor-total-pacote-user").val()
            },
            success: function (data) {
                var resultado = JSON.parse(data);
                limparCamposCidadeCadastro();
                $('#modal-cadastro-pacote-user').modal('hide');
                if (resultado == 1) {
                    $(function () {
                        new PNotify({
                            title: 'Sucesso!',
                            text: 'Pacote cadastrado com sucesso',
                            type: 'success'
                        });
                    });
                } else {
                    $('#modal-cadastro-pacote-user').modal('hide');
                    $(function () {
                        new PNotify({
                            title: 'Sucesso!',
                            text: 'Erro ao cadastrar pacote',
                            type: 'erro'
                        });
                    });
                }
            }
        });
    });

    function gerarValorPacote() {
        var dataSaida = $('#campo-data-horario-saida-cadastro-user').val().replace('-', '').replace('T', '').replace(':', '').replace('-', '');
        var dataVolta = $('#campo-data-horario-retorno-cadastro-user').val().replace('-', '').replace('T', '').replace(':', '').replace('-', '');
        var local = $('#select-destino-pacote-user').val();
        var guia = Math.floor(Math.random() * (240 - 90 + 1) + 90);

        var dias = (dataVolta - dataSaida) / 10000;

        var valorDiario = dias * 170;
        var valorGuiaDiario = guia * dias;

        var precoViagem = 0;

        if (local == 1) {
            precoViagem = 1500;
        } else if (local == 2) {
            precoViagem = 1000;
        } else if (local == 3) {
            precoViagem = 1800;
        } else if (local == 4) {
            precoViagem = 1700;
        } else if (local == 5) {
            precoViagem = 2000;
        }

        var valorTotal = valorDiario + precoViagem + valorGuiaDiario;

        $('#campo-valor-total-pacote-user').html(valorTotal);
    };

    function limparCampos() {

    }
});