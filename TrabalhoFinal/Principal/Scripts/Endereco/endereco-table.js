﻿$(function () {
    //Preenche DataTable
    $('#table-endereco').DataTable({
        ajax: '/Endereco/ObterTodosPorJSON',
        "order": [[2, "asc"]],
        columns: [
            {
                data: 'Id',
                bSortable: false,
                width: "10%",
                target: 0
            },
            {
                data: 'Cep',
                bSortable: true,
                width: "15%",
                target: 1
            },
            {
                data: 'Logradouro',
                bSortable: true,
                width: "30%",
                target: 2
            },
            {
                data: 'Cidade.Nome',
                bSortable: true,
                width: "25%",
                target: 3
            },
            {
                data: null,
                bSortable: false,
                width: "20%",
                target: 4,
                render: function (data, type, row) {
                    return '<a class="btn btn-outline-info botao-editar-endereco" data-id="' + row.Id + '" data-toggle="modal" data-target="#endereco-modal-editar">Editar</a>' +
                        '<a class="btn btn-outline-danger ml-1 botao-excluir-endereco" data-id="' + row.Id + '">Desativar</a>';
                }
            }
        ]
    });

    //Abre modal de cadastro
    $('#botao-modal-cadastrar-endereco').on('click', function () {
        limparCamposEnderecoCadastro();
        $('#endereco-modal-cadastro').modal('show');
    });

    //Validação modal Cadastro
    function init() {
        $('#form-modal-cadastro-endereco').validate({
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
                'endereco.Cep': {
                    required: true,
                    validacep: true
                },
                'endereco.Logradouro': {
                    required: true,
                    rangelength: [6, 30]
                },
                'endereco.Numero': {
                    required: true,
                    digits:true
                },                                    
                'endereco.IdCidade': {
                    required: true
                }
            },
            messages: {
                'endereco.Cep': {
                    required: STRINGS.cepPreenchido
                    
                },
                'endereco.Logradouro': {
                    required: STRINGS.logPreenchido,
                    rangelength: STRINGS.logDeveConter
                },
                'endereco.Numero': {
                    required: STRINGS.numPreenchido,
                    digits: STRINGS.numSomenteDig
                },            
                'endereco.IdCidade': {
                    required: STRINGS.cidadePreenchido
                }
            }

        });

    }


    function limpa_formulário_cep() {
        // Limpa valores do formulário de cep.
        $('##campo-cadastro-endereco-logradouro').val('');
       
    }

    //valida cep
    jQuery.validator.addMethod("validacep", function (value, element) {

        //Nova variável "cep" somente com dígitos.
        var cep = value;

        //Verifica se campo cep possui valor informado.


        //Expressão regular para validar o CEP.
        var validacep = /^[0-9]{8}$/;

        //Valida o formato do CEP.
        if (validacep.test(cep)) {

            //Preenche os campos com "..." enquanto consulta webservice.
            $('#campo-cadastro-endereco-logradouro').val("...");
            

            //Consulta o webservice viacep.com.br/
            $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?", function (dados) {

                if (!("erro" in dados)) {
                    //Atualiza os campos com os valores da consulta.
                    $('#campo-cadastro-endereco-logradouro').val(dados.logradouro);
                   
                } //end if.
                else {
                    //CEP pesquisado não foi encontrado.
                    limpa_formulário_cep();

                    return false;
                }
            });
        } //end if.
        else {
            //cep é inválido.
            limpa_formulário_cep();

            return false;
        }

        return true;

    }, "Por favor, digite um CEP válido");

    $(document).ready(init);

    //Salvar Modal Cadastro
    $('#botao-salvar-modal-cadastrar-endereco').on('click', function () {
        if ($('#form-modal-cadastro-endereco').valid()) {
            $.ajax({
                url: '/Endereco/Store',
                method: 'post',
                data: {
                    cep: $('#campo-cadastro-endereco-cep').val(),
                    logradouro: $('#campo-cadastro-endereco-logradouro').val(),
                    numero: $('#campo-cadastro-endereco-numero').val(),
                    complemento: $('#campo-cadastro-endereco-complemento').val(),
                    referencia: $('#campo-cadastro-endereco-referencia').val(),
                    idCidade: $('#select-cadastro-endereco-cidade').val(),
                },
                success: function (data) {
                    var resultado = JSON.parse(data);
                    $('#table-endereco').DataTable().ajax.reload();
                    $('#endereco-modal-cadastro').modal('hide');
                    $(function () {
                        new PNotify({
                            title: STRINGS.sucesso,
                            text: STRINGS.enderecoCadastrado,
                            type: 'success'
                        });
                    });
                    limparCamposEnderecoCadastro();
                }
            });
        }
    });

    //Botão Editar
    $('table').on('click', '.botao-editar-endereco', function () {
        var id = $(this).data('id');
        $.ajax({
            url: '/Endereco/Editar?id=' + id,
            method: 'get',
            success: function (resultado) {
                var data = JSON.parse(resultado);
                $('#campo-editar-endereco-cep').val(data.Cep);
                $('#campo-editar-endereco-id').val(data.Id);
                $('#campo-editar-endereco-logradouro').val(data.Logradouro);
                $('#campo-editar-endereco-numero').val(data.Numero);
                $('#campo-editar-endereco-complemento').val(data.Complemento);
                $('#campo-editar-endereco-referencia').val(data.Referencia);
                $('#select-editar-endereco-cidade').append(new Option(data.Cidade.Nome, data.IdCidade, false, false)).val(data.IdCidade).trigger('change');

                $('#endereco-modal-editar').modal('show');
            }
        });
    });

    //Validação Editar
    function init() {
        $('#form-modal-editar-endereco').validate({
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
                'endereco.Cep': {
                    required: true,
                    validacep: true
                },
                'endereco.Logradouro': {
                    required: true,
                    rangelength: [6, 30]
                },
                'endereco.Numero': {
                    required: true
                },
                'endereco.IdCidade': {
                    required: true
                }
            },
            messages: {
                'endereco.Cep': {
                    required: STRINGS.cepPreenchido
                    
                },
                'endereco.Logradouro': {
                    required: STRINGS.logPreenchido,
                    rangelength: STRINGS.logDeveConter
                },
                'endereco.Numero': {
                    required: STRINGS.numPreenchido,
                    digits: STRINGS.numSomenteDig
                },
                'endereco.IdCidade': {
                    required: STRINGS.cidadePreenchido
                }
            }
        });
    }

    function limpa_formulário_cep() {
        // Limpa valores do formulário de cep.
        $('#campo-editar-endereco-logradouro').val('');

    }

    //valida cep
    jQuery.validator.addMethod("validacep", function (value, element) {

        //Nova variável "cep" somente com dígitos.
        var cep = value;

        //Verifica se campo cep possui valor informado.


        //Expressão regular para validar o CEP.
        var validacep = /^[0-9]{8}$/;

        //Valida o formato do CEP.
        if (validacep.test(cep)) {

            //Preenche os campos com "..." enquanto consulta webservice.
            $('#campo-editar-endereco-logradouro').val("...");


            //Consulta o webservice viacep.com.br/
            $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?", function (dados) {

                if (!("erro" in dados)) {
                    //Atualiza os campos com os valores da consulta.
                    $('#campo-editar-endereco-logradouro').val(dados.logradouro);

                } //end if.
                else {
                    //CEP pesquisado não foi encontrado.
                    limpa_formulário_cep();

                    return false;
                }
            });
        } //end if.
        else {
            //cep é inválido.
            limpa_formulário_cep();

            return false;
        }

        return true;

    }, "Por favor, digite um CEP válido");

    $(document).ready(init);
    //Update Modal Editar
    $('#botao-salvar-modal-editar-endereco').on('click', function () {
        if ($('#form-modal-editar-endereco').valid()) {
            $.ajax({
                url: '/Endereco/Update',
                method: 'post',
                dataType: 'json',
                data: {
                    id: $('#campo-editar-endereco-id').val(),
                    cep: $('#campo-editar-endereco-cep').val(),
                    logradouro: $('#campo-editar-endereco-logradouro').val(),
                    numero: $('#campo-editar-endereco-numero').val(),
                    complemento: $('#campo-editar-endereco-complemento').val(),
                    referencia: $('#campo-editar-endereco-referencia').val(),
                    idCidade: $('#select-editar-endereco-cidade').val()
                },
                success: function (data) {
                    var resultado = JSON.parse(data);
                    if (resultado == 1) {
                        $(function () {
                            new PNotify({
                                title: STRINGS.sucesso,
                                text: STRINGS.enderecoAlterado,
                                type: 'info'
                            });
                        });
                        $('#table-endereco').DataTable().ajax.reload();
                        $('#endereco-modal-editar').modal('hide');
                        limparCamposEnderecoEditar();
                    } else {
                        new PNotify({
                            title: 'Erro!',
                            text: 'Erro ao alterar',
                            type: 'error'
                        });
                    }
                }
            });
        }
    });

    //Desativar
    $('table').on('click', '.botao-excluir-endereco', function () {
        var id = $(this).data('id');
        $.ajax({
            url: '/Endereco/Excluir?id=' + id,
            method: 'get',
            success: function (data) {
                var resultado = JSON.parse(data);
                if (resultado == 1) {
                    new PNotify({
                        title: STRINGS.desativado,
                        text: STRINGS.enderecoDesativado,
                        type: 'success'
                    });
                    $('#table-endereco').DataTable().ajax.reload();
                } else {
                    new PNotify({
                        title: 'Erro!',
                        text: 'Erro ao desativar Endereço',
                        type: 'error'
                    });
                }
            }
        });
    });

    function limparCamposEnderecoCadastro() {
        $('#campo-cadastro-endereco-id').val('');
        $('#campo-cadastro-endereco-cep').val('');
        $('#campo-cadastro-endereco-logradouro').val('');
        $('#campo-cadastro-endereco-numero').val('');
        $('#campo-cadastro-endereco-complemento').val('');
        $('#campo-cadastro-endereco-referencia').val('');
        $('#select-cadastro-endereco-cidade').val('').trigger('change');
    }

    function limparCamposEnderecoEditar() {
        $('#campo-editar-endereco-id').val('');
        $('#campo-cadastro-endereco-cep').val('');
        $('#campo-editar-endereco-logradouro').val('');
        $('#campo-editar-endereco-numero').val('');
        $('#campo-editar-endereco-complemento').val('');
        $('#campo-editar-endereco-referencia').val('');
        $('#select-editar-endereco-cidade').val('').trigger('change');
    }
});