$(function () {
    //Preenche DataTable
    $('#guia-tabela').DataTable({       
        ajax: "/Guia/ObterTodosPorJSON",
        order: [[1, "asc"]],
        columns: [
            {
                data: "Id",
                bSortable: false,
                width: "5%",
                target: 0
            },
            {
                data: "Nome",
                bSortable: true,
                width: "25%",
                target: 1
            },
            {
                data: "Sobrenome",
                bSortable: true,
                width: "25%",
                target: 2
            },
            {
                data: "Cpf",
                bSortable: true,
                width: "20%",
                target: 3
            },
            {
                data: "Rank",
                bSortable: true,
                width: "5%",
                target: 4
            },
            {
                data: null,
                bSortable: false,
                width: "20%",
                target: 5,
                render: function (data, type, row) {
                    return "<a class='btn fa fa-edit botao-editar-guia' data-id='" + row.Id + "' style='font-size: 24px;'></a>" +
                        "<a class='btn fa fa-trash ml-1 botao-excluir-guia' data-id='" + row.Id + "' style='font-size: 24px;'></a>";

                }
            }
        ]
    });

    //Abre modal de cadastro
    $("#botao-modal-cadastrar-guia").on("click", function () {
        limparCampos();
        $("#guia-modal-cadastro").modal('show');
    });

    //Desativar
    $('table').on('click', '.botao-excluir-guia', function () {
        var id = $(this).data('id');
        $.ajax({
            url: '/Guia/Excluir?id=' + id,
            method: 'get',
            success: function (data) {
                var result = JSON.parse(data)
                if (result == 1) {

                    new PNotify({
                        title: 'Desativado!',
                        text: 'Usuário desativado com sucesso',
                        type: 'success'
                    });
                    $('#guia-tabela').DataTable().ajax.reload();
                } else {

                    new PNotify({
                        title: 'Erro!',
                        text: 'Erro ao desativar usuário',
                        type: 'error'
                    });
                }
            }
        });
    });

    //Botao editar
    $('table').on('click', '.botao-editar-guia', function () {
        var id = $(this).data('id');
        $.ajax({
            url: 'Guia/Editar?id=' + id,
            success: function (result) {
                var data = JSON.parse(result);
                var cidade = data.Endereco.Cidade;
                var estado = cidade.Estado;
                $("#campo-editar-guia-id").val(id);
                $("#campo-editar-guia-endereco-id").val(data.Endereco.Id);
                
                $('#campo-editar-guia-nome').val(data.Nome);
                $('#campo-editar-guia-sobrenome').val(data.Sobrenome);
                $('#campo-editar-guia-rg').val(data.Rg);
                $('#campo-editar-guia-cpf').val(data.Cpf);
                $('#campo-editar-guia-data-nascimento').val(data.DataNascimento);
                $('#campo-editar-guia-sexo').val(data.Sexo);
                $('#campo-editar-guia-numero-carteira-trabalho').val(data.CarteiraTrabalho);
                $('#campo-editar-guia-salario').val(data.Salario);
                $('#campo-editar-guia-categoria-habilitacao').val(data.CategoriaHabilitacao);
                $('#campo-editar-guia-rank').val(data.Rank);
                $('#select-editar-guia-estado').append(new Option(estado.Nome, estado.Id, false, false)).val(estado.Id).trigger('change');
                $('#select-editar-guia-cidade').append(new Option(cidade.Nome, cidade.Id, false, false)).val(cidade.Id).trigger('change');
                $('#campo-cep-editar-guia').val(data.Endereco.Cep);
                $('#campo-logradouro-editar-guia').val(data.Endereco.Logradouro);
                $('#campo-numero-editar-guia').val(data.Endereco.Numero);
                $('#campo-complemento-guia-editar').val(data.Endereco.Complemento);
                $('#campo-referencia-guia-editar').val(data.Endereco.Referencia);
                $('#guia-modal-editar').modal("show");
            }
        });
    });

    //Update modal editar
    $("#botao-acao-editar-guia").on("click", function () {
        var nomeVar = $("#campo-editar-guia-nome").val();
        $.ajax({
            url: '/Guia/Update',
            method: 'post',
            dataType: 'json',
            data: {
                id: $("#campo-editar-guia-id").val(),
                nome: $("#campo-editar-guia-nome").val(),
                sobrenome: $("#campo-editar-guia-sobrenome").val(),
                rg: $("#campo-editar-guia-rg").val(),
                cpf: $("#campo-editar-guia-cpf").val(),
                dataNascimento: $("#campo-editar-guia-data-nascimento").val(),
                sexo: $("#campo-editar-guia-sexo").val(),
                carteiraTrabalho: $("#campo-editar-guia-numero-carteira-trabalho").val(),
                salario: $("#campo-editar-guia-salario").val(),
                categoriaHabilitacao: $("#campo-editar-guia-categoria-habilitacao").val(),
                rank: $("#campo-editar-guia-rank").val(),

                endereco: {
                    id: $("#campo-editar-guia-endereco-id").val(),
                    cidade: {
                        id: $("#select-editar-guia-cidade").val(),
                        estado: {
                            id: $("#select-editar-guia-estado").val()
                        }
                    },
                    cep: $("#campo-cep-editar-guia").val(),
                    numero: $("#campo-numero-editar-guia").val(),
                    referencia: $("#campo-referencia-guia-editar").val(),
                    logradouro: $("#campo-logradouro-editar-guia").val()
                }

            },
            success: function (data) {                
                limparCampos();
                $("#guia-modal-editar").modal('hide');
                $(function () {
                    new PNotify({
                        title: 'Sucesso!',
                        text: nomeVar + ' cadastrado com sucesso',
                        type: 'success'
                    });
                });
                $('#guia-tabela').DataTable().ajax.reload();
            }
        });
        limparCampos();
    });

    // Mascaras
    $(document).ready(function () {
        $("#campo-cadastro-guia-cpf").mask("999.999.999-99");
       
    });

    //Validação Modal Cadastro
    function init() {

        $('#form-modal-cadastro-guia').validate({
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
                'guia.Nome': {
                    required: true,
                    rangelength: [3, 30]
                },
                'guia.Sobrenome': {
                    required: true,
                    rangelength: [3, 30]
                },
                //'guia.Rg': {
                //    required: true,
                //    digits: true,
                //    minlength: [7],
                //    maxlength: [12]
                //},'
                'guia.Cpf': {
                    required: true,
                    verificaCPF: true
                },
                'guia.DataNascimento': {
                    required: true,                   
                    date: true
                },
                'sexo': {
                    required: true
                },
                'guia.CarteiraTrabalho': {
                    required: true,
                    digits: true
                },
                'guia.Salario': {
                    required: true,
                    number: true,
                    minlength: [3],
                    range: [954, 10000]                   
                },
                'guia.CategoriaHabilitacao': {
                    required: true,
                    rangelength:[1, 5]
                },               
                'cidade.IdEstado': {
                    required: true
                },
                'select-cadastro-guia-cidade': {
                    required: true
                },
                'endereco.Cep': {
                    required: true,
                    digits: true,
                    validacep:true
                                   
                },               
                'endereco.Numero': {  
                    required: true,
                    digits: true
                }
            },
            messages: {
                'guia.Nome': {
                    required: 'Nome deve ser preenchido.',
                    rangelength: 'Nome deve conter entre {0} a {1} caracteres.'
                },
                'guia.Sobrenome': {
                    required: 'Sobrenome deve ser preenchido.',
                    rangelength: 'Sobrenome deve conter entre {0} a {1} caracteres.'
                },
                //'guia.Rg': {
                //    required: 'RG deve ser preenchido.',
                //    digits: 'RG deve conter somente digitos.',
                //    minlength: 'RG deve conter no mínimo 7 dígitos.',
                //    maxlength: 'RG deve conter no máximo 12 digitos.'
                //},
                'guia.Cpf': {
                    required: 'CPF deve ser preenchido.',                    
                },
                'guia.DataNascimento': {
                    required: 'Data de Nascimento deve ser preenchido.',
                    date: 'Data de Nascimento Inválida.'
                },
                'sexo': {
                    required: 'Sexo deve ser preenchido.'
                },
                'guia.CarteiraTrabalho': {
                    required: 'Carteira de tabalho deve ser preenchido.',
                    digits: 'Carteira de trabalho deve conter somente digitos.'
                },
                'guia.Salario': {
                    required: 'Salario deve ser preenchido.',
                    number: 'Salario deve conter somente numeros inteiro e decimais.',
                    minlength: 'Salario deve conter no minimo 3 digitos.',
                    range: 'Salário deve ser entre R$ 954,00 e R$ 10.000,00'

                },
                'guia.CategoriaHabilitacao': {
                    required: 'Categoria da habilitação deve ser preenchido.',
                    rangelength: 'Carteira de habilitação deve conter entre {0} a {1} caracteres.'
                },               
                'cidade.IdEstado': {
                    required: 'Estado deve ser preenchido.'
                },
                'select-cadastro-guia-cidade': {
                    required: 'Cidade deve ser preenchido.'
                },
                'endereco.Cep': {
                    required: 'Cep deve ser preenchido.',                    
                    digits: 'Cep deve conter somente digitos',
                    validacep:'Cep inválido.'
                    
                },               
                'endereco.Numero': {    
                    required: 'Número deve ser preenchido.',
                    digits: 'Número deve conter apenas dígitos.'
                }
            }

        });
    }


    //verifica cpf válido
    jQuery.validator.addMethod("verificaCPF", function (value, element) {
        // tamanho do cpf
        if (value.length < 11) return false;
        // retira pontos, virgulas e traços
        value = value.replace('.', '');
        value = value.replace('.', '');
        cpf = value.replace('-', '');
        //  calcular cpf válido
        while (cpf.length < 11) cpf = "0" + cpf;
        var expReg = /^0+$|^1+$|^2+$|^3+$|^4+$|^5+$|^6+$|^7+$|^8+$|^9+$/;
        var a = [];
        var b = new Number;
        var c = 11;
        for (i = 0; i < 11; i++) {
            a[i] = cpf.charAt(i);
            if (i < 9) b += (a[i] * --c);
        }
        if ((x = b % 11) < 2) { a[9] = 0 } else { a[9] = 11 - x }
        b = 0;
        c = 11;
        for (y = 0; y < 10; y++) b += (a[y] * c--);
        if ((x = b % 11) < 2) { a[10] = 0; } else { a[10] = 11 - x; }
        if ((cpf.charAt(9) != a[9]) || (cpf.charAt(10) != a[10]) || cpf.match(expReg)) return false;
        return true;
    }, "Informe um CPF válido.");

    //Validar Data
    $.validator.addMethod("dateBR", function (value, element) {
        if (value.length != 10) return false;
        // verificando data
        var data = value;
        var dia = data.substr(0, 2);
        var barra1 = data.substr(2, 1);
        var mes = data.substr(3, 2);
        var barra2 = data.substr(5, 1);
        var ano = data.substr(6, 4);
        if (value == "") return true;
        if (data.length != 10 || barra1 != "/" || barra2 != "/" || isNaN(dia) || isNaN(mes) || isNaN(ano) || dia > 31 || mes > 12) return false;
        if ((mes == 4 || mes == 6 || mes == 9 || mes == 11) && dia == 31) return false;
        if (mes == 2 && (dia > 29 || (dia == 29 && ano % 4 != 0))) return false;
        if (ano < 1888) return false;
        return true;
    }, "Informe uma data válida");  // Mensagem padrão
    

    function limpa_formulário_cep() {
        // Limpa valores do formulário de cep.
        $('#campo-logradouro-cadastro-guia').val('');
        $('#campo-complemento-guia-cadastro').val('');
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
            $('#campo-logradouro-cadastro-guia').val("...");
            $('#campo-complemento-guia-cadastro').val("...");

            //Consulta o webservice viacep.com.br/
            $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?", function (dados) {

                if (!("erro" in dados)) {
                    //Atualiza os campos com os valores da consulta.
                    $('#campo-logradouro-cadastro-guia').val(dados.logradouro);
                    $('#campo-complemento-guia-cadastro').val(dados.bairro);
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
 
    //Salvar modal cadastro
    $("#botao-salvar-modal-cadastrar-guia").on("click", function () {
        var nomeVar = $("#campo-cadastro-guia-nome").val();

        if ($('#form-modal-cadastro-guia').valid()) {
            $.ajax({
                url: '/Guia/Store',
                method: 'post',
                data: {
                    nome: $("#campo-cadastro-guia-nome").val(),
                    sobrenome: $("#campo-cadastro-guia-sobrenome").val(),
                    datanascimento: $("#campo-cadastro-guia-data-nascimento").val(),
                    sexo: $('input[type=radio][name=sexo]:checked').val(),
                    rg: $("#campo-cadastro-guia-rg").val(),
                    cpf: $("#campo-cadastro-guia-cpf").val(),
                    carteiratrabalho: $("#campo-cadastro-guia-numero-carteira-trabalho").val(),
                    categoriahabilitacao: $("#campo-cadastro-guia-categoria-habilitacao").val(),
                    salario: $("#campo-cadastro-guia-salario").val(),
                    rank: $("#campo-cadastro-guia-rank").val(),
                    endereco: {
                        cidade: {
                            id: $("#select-cadastro-cidade-guia").val(),
                            estado: {
                                id: $("#select-cadastro-guia-estado").val()
                            }
                        },
                        cep: $("#campo-cep-cadastro-guia").val(),
                        numero: $("#campo-numero-cadastro-guia").val(),
                        referencia: $("#campo-referencia-guia-cadastro").val(),
                        logradouro: $("#campo-logradouro-cadastro-guia").val()
                    }
                },
                success: function (data) {
                    var resultado = JSON.parse(data);
                    $("#guia-modal-cadastro").modal('hide');
                    $('#guia-tabela').DataTable().ajax.reload();
                    $(function () {
                        new PNotify({
                            title: 'Sucesso!',
                            text: nomeVar + ' cadastrado com sucesso',
                            type: 'success'
                        });
                    });
                    limparCampos();
                }
            });
        }
    });
    

    function limparCampos() {
        $('#campo-cadastro-guia-nome').val('');
        $('#campo-cadastro-guia-sobrenome').val('');
        $('#campo-cadastro-guia-rg').val('');
        $('#campo-cadastro-guia-cpf').val('');
        $('#campo-cadastro-guia-data-nascimento').val(null);
        $('#campo-cadastro-guia-sexo').val('');
        $('#campo-cadastro-guia-numero-carteira-trabalho').val('');
        $('#campo-cadastro-guia-salario').val('');
        $("#campo-cadastro-guia-categoria-habilitacao").val('').trigger('change');
        $('#campo-cadastro-guia-rank').val('').trigger('change');
        $('#campo-referencia-guia-cadastro').val('');
        $('#campo-complemento-guia-cadastro').val('');
        $('#campo-numero-cadastro-guia').val('');
        $('#campo-logradouro-cadastro-guia').val('');
        $('#campo-cep-cadastro-guia').val('');
        $('#select-cadastro-cidade-guia').val('').trigger('change');
        $('#select-cadastro-guia-estado').val('').trigger('change');
    }
    
});