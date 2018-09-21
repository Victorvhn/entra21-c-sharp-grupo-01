$(function () {
    //Preenche DataTable
    $('#guia-tabela').DataTable({
        processing: true,
        serverSide: true,
        ajax: "/Guia/ObterTodosPorJSON",
        columns: [
            { data: "Id" },
            { data: "Nome" },
            { data: "Sobrenome" },
            { data: "Cpf" },
            { data: "Rank" },
            {
                data: null,
                render: function (data, type, row) {
                    return "<a class='btn btn-outline-info botao-editar-guia' data-id='" + row.Id + "'>Editar</a>" +
                        "<a class='btn btn-outline-danger ml-1 botao-excluir-guia' data-id='" + row.Id + "' href='#' >Desativar</a>";

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
            ulr: 'Guia/Excluir?id=' + id,
            method: 'get',
            success: function (result) {
                var apagado = JSON.parse(result)
                if (apagado == 1) {

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
    $('table').on("click", ".botao-editar-guia", function () {
        var id = $(this).data('id');
        $.ajax({
            url: '/Guia/Editar?id=' + id,
            success: function (result) {
                var data = JSON.parse(result);
                $("#campo-editar-guia-nome").val(data.Nome);
                $("#campo-editar-guia-sobrenome").val(data.Sobrenome);
                $("#campo-editar-guia-rg").val(data.Rg);
                $("#campo-editar-guia-cpf").val(data.Cpf);
                $("#campo-editar-guia-data-nascimento").val(data.DataNascimento);
                $("#campo-editar-guia-sexo").val(data.Sexo);
                $("#campo-numero-carteira-trabalho").val(data.CarteiraTrabalho);
                $("#campo-editar-guia-salario").val(data.Salario);
                $("#campo-editar-guia-categoria-habilitacao").val(data.CategoriaHabilitacao);
                $("#campo-editar-guia-rank").val(data.Rank);

                $("#guia-modal-editar").modal("show");
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
                nome: $("#campo-editar-guia-nome").val(),
                sobrenome: $("#campo-editar-guia-sobrenome").val(),
                datanascimento: $("#campo-editar-guia-data-nascimento").val(),
                sexo: $("#campo-editar-guia-sexo").val(),
                rg: $("#campo-editar-guia-rg").val(),
                cpf: $("#campo-editar-guia-cpf").val(),
                carteiratrabalho: $("#campo-editar-guia-numero-carteira-trabalho").val(),
                categoriahabilitacao: $("#campo-editar-guia-categoria-habilitacao").val(),
                salario: $("#campo-editar-guia-salario").val(),
                rank: $("#campo-editar-guia-rank").val()
            },
            success: function (data) {
                var resultado = JSON.parse(data);
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
                'guia.Rg': {
                    required: true,
                    digits: true,
                    minlength: [7],
                    maxlength: [12]
                },
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
                'guia.Rank': {
                    required: true
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
                    validaCEP: true,
                   
                },
                'endereco.Logradouro': {
                    required: true,
                    rangelength:[3, 40]
                },
                'endereco.Numero': {
                    required: true,
                    digits: true
                },
                'endereco.Bairro': {
                    required: true,
                    rangelength:[4, 20]
                },
               
                
            },
            messages: {
                'guia.Nome': {
                    required: 'Nome deve ser preenchido.',
                    rangelength: 'Nome deve conter de  {0} a {1} caracteres.'
                },
                'guia.Sobrenome': {
                    required: 'Sobrenome deve ser preenchido.',
                    rangelength: 'Sobrenome deve conter de {0} a {1} caracteres.'
                },
                'guia.Rg': {
                    required: 'RG deve ser preenchido.',
                    digits: 'RG deve conter somente digitos.',
                    minlength: 'RG deve conter no mínimo 7 dígitos.',
                    maxlength: 'RG deve conter no máximo 12 dpigitos.'
                },
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
                    number: 'Salario deve conter somente nuemros inteiro e decimais.',
                    minlength: 'Salario deve conter no minimo 3 digitos.',
                    range: 'Salário deve ser entre R$ 954,00 e R$ 10.000,00'

                },
                'guia.CategoriaHabilitacao': {
                    required: 'Categoria da habilitação deve ser preenchido.',
                    rangelength: 'Carteira de habilitação deve conter de {0} a {1} caracteres.'
                },
                'guia.Rank': {
                    required: 'Rank deve ser preenchido.'
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
                    minlength: 'CEP deve conter no mínimo 8 dígitos.',
                    maxlength: 'CEP inválido.'
                },
                'endereco.Logradouro': {
                    required: 'Logradouro deve ser preenchido.',
                    rangelength: 'Logradouro deve conter de {0} a {1} caracteres.'
                },
                'endereco.Numero': {
                    required: 'Número deve ser preenchido.',
                    digits: 'Número deve conter apenas dígitos.'
                },
                'endereco.Bairro': {
                    required: 'Bairro deve ser preenchido.',
                    rangelength: 'Bairro deve conter de {0} a {1} caracteres.'
                }
                
            }

        });
    }


    jQuery.validator.addMethod("verificaCPF", function (value, element) {
        // tamnho do cpf
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

    jQuery.validator.addMethod("validaCEP", function (value, element) {
        return this.optional(element) || /^[0-9]{5}-[0-9]{3}$/.test(value);
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
                    sexo: $("#campo-cadastro-guia-sexo").val(),
                    rg: $("#campo-cadastro-guia-rg").val(),
                    cpf: $("#campo-cadastro-guia-cpf").val(),
                    carteiratrabalho: $("#campo-cadastro-guia-numero-carteira-trabalho").val(),
                    categoriahabilitacao: $("#campo-cadastro-guia-categoria-habilitacao").val(),
                    salario: $("#campo-cadastro-guia-salario").val(),
                    rank: $("#campo-cadastro-guia-rank").val()
                },
                success: function (data) {
                    var resultado = JSON.parse(data);
                    limparCampos();
                    $("#guia-modal-cadastro").modal('hide');
                    $('#guia-tabela').DataTable().ajax.reload();
                    $(function () {
                        new PNotify({
                            title: 'Sucesso!',
                            text: nomeVar + ' cadastrado com sucesso',
                            type: 'success'
                        });
                    });
                }
            });
        }
    });

    function limparCampos() {
        $("#campo-cadastro-guia-nome").val(""),
            $("#campo-cadastro-guia-sobrenome").val(""),
            $("#campo-cadastro-guia-rg").val(""),
            $("#campo-cadastro-guia-cpf").val(""),
            $("#campo-cadastro-guia-data-nascimento").val(""),
            $("#campo-cadastro-guia-sexo").val(""),
            $("#campo-cadastro-guia-numero-carteira-trabalho").val(""),
            $("#campo-cadastro-guia-salario").val(""),
            $("#campo-cadastro-guia-categoria-habilitacao").val(""),
            $("#campo-cadastro-guia-rank").val("")
    }
});