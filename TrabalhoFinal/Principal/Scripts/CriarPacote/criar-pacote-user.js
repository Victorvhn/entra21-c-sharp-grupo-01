$(function () {
    //Abrir modal cadastro
    $('#cadastro-pacote-user').on('click', function () {
        limparCampos();
        $('#modal-cadastro-pacote-user').modal('show');
    });

    // Validação
    $('#form-modal-cadastro-pacote-usuario').validate({
        errorClass: 'form-control-danger',
        validClass: 'form-control-success',
        highlight: function (element) {
            jQuery(element).closest('.form-group').addClass('has-error');
        },
        unhighlight: function (element) {
            jQuery(element).closest('.form-group').removeClass('has-error');
        },
        errorPlacement: function (error, element) {
            $(element).parent().append(error[0])
        },

        rules: {
            'pacote.Nome': {
                required: true,
                rangelength: [3, 30]
            },
            'select-destino': {
                required: true
            },
            'campo-data-horario-saida-cadastro-user': {
                required: true,
                date: true
            },
            'campo-data-horario-retorno-cadastro-user': {
                required: true,
                date: true
            },
            'viagem.IdGuia': {
                required: true
            },
            'select-pontos-turisticos-cadastro-user': {
                required: true
            }

        },
        messages: {
            'pacote.Nome': {
                required: 'Nome deve ser preenchido.',
                rangelength: 'nome deve conter entre {0} a {1} caracteres'
            },
            'select-destino': {
                required: 'Selecione um destino.'
            },
            'campo-data-horario-saida-cadastro-user': {
                required: 'Informe a data de saída.',
                date: 'Esta data não é válida.'
            },
            'campo-data-horario-retorno-cadastro-user': {
                required: 'Informe a data de retorno.',
                date: 'Esta data não é válida.'
            },
            'viagem.IdGuia': {
                required: 'Selecione um guia.'
            },
            'select-pontos-turisticos-cadastro-user': {
                required: 'Selecione um ponto turistico.'
            }
        }
    });

    //Store 
    $("#botao-salvar-pacote-user").on("click", function () {
        if ($('#form-modal-cadastro-pacote-usuario').valid()) {
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
        }
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

    $("#botao-de-teste-card").on("click", function () {
        $.ajax ({
            url: "/CriarPacote/ObterTodosPorJSON",
            method: "get"
        }),
        success: function (result) {
            var resultado = JSON.parse(result);

            $('#principal-criar-pacote').append('<div class="col-lg-6 col-xl-3 col-md-6">\
                < div class= "card rounded-card user-card" >\
                <div class="card-block">\
                    <div class="user-content">\
                        <h4 class="">' + resultado.Nome + '</h4>\
                        <p class="m-b-0 text-muted">' + resultado.Valor + '</p>\
                    </div>\
                </div>\
                </div >\
            </div >');
          
        }
    });

    function limparCampos() {

    }
});