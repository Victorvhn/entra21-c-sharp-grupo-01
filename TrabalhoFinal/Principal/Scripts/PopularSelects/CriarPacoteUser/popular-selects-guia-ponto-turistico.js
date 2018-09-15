﻿$(document).ready(function () {
     //SELECT GUIAS
    $('#select-guia-pacote-user').select2({
        placeholder: "Selecione um Guia",
        ajax: {
            url: '/Guia/ObterTodosParaSelect2',
            dataType: 'json'
        }
    });

     //SELECT PONTOS TURISTICOS
     $('#select-pontos-turisticos-cadastro-user').select2({
         placeholder: "Selecione um Ponto Turístico",
        ajax: {
            url: '/PontoTuristico/ObterTodosPorJSONSelect2',
            dataType: 'json'
        }
    });
});