﻿$(function () {

     $('#cadastro-turista-endereco-idEstado').select2({
         placeholder: "Selecione um estado",
         ajax: {
             url: '/Login/ObterEstadoSelect',
             dataType: 'json'
         }
     });

     $('#cadastro-turista-endereco-idCidade').select2({
         placeholder: "Selecione uma cidade",
         ajax: {
             url: '/Login/ObterCidadeSelect',
             dataType: 'json'
         }
     });
     
});