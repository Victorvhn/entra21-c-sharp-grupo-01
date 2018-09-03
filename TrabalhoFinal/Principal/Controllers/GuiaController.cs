﻿using Model;
using Newtonsoft.Json;
using Principal.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Principal.Controllers
{
    public class GuiaController : Controller
    {
        // GET: Guia

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Cadastro()
        {

            ViewBag.Guia = new Guia();
            return View();
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            Guia guia = new GuiaRepository().ObterPeloId(id);
            ViewBag.Guia = guia;

            return Content(JsonConvert.SerializeObject(guia));
        }

        [HttpGet]
        public ActionResult Excluir(int id)
        {
            bool apagado = new GuiaRepository().Excluir(id);
            return Content(JsonConvert.SerializeObject(apagado));
        }

        [HttpPost]
        public ActionResult Store(GuiaString guia, EnderecoString endereco)
        {

            Endereco enderecoModel = new Endereco();
            enderecoModel.Cep = endereco.Cep.ToString();
            enderecoModel.Logradouro = endereco.Logradouro.ToString();
            enderecoModel.Numero = Convert.ToInt16(endereco.Numero.ToString());
            enderecoModel.Referencia = endereco.Referencia.ToString();
            enderecoModel.Complemento = endereco.Complemento.ToString();

            int codigoEndereco = new EnderecoRepository().Cadastrar(enderecoModel);

            Guia guiaModel = new Guia();
            guiaModel.IdEndereco = codigoEndereco;
            guiaModel.Nome = guia.Nome.ToString();
            guiaModel.Sobrenome = guia.Sobrenome.ToString();
            guiaModel.DataNascimento = Convert.ToDateTime(guia.DataNascimento.Replace("/", "-").ToString());
            guiaModel.Sexo = guia.Sexo.ToString();
            guiaModel.Rg = guia.Rg.ToString();
            guiaModel.Cpf = guia.Cpf.ToString();
            guiaModel.CarteiraTrabalho = guia.CarteiraTrabalho.ToString();
            guiaModel.CategoriaHabilitacao = guia.CategoriaHabilitacao.ToString();
            guiaModel.Salario = Convert.ToDouble(guia.Salario.ToString());
            guiaModel.Rank = Convert.ToByte(guia.Rank.ToString());

            int identificador = new GuiaRepository().Cadastrar(guiaModel);
            return Content(JsonConvert.SerializeObject(new { id = identificador }));
        }

        [HttpPost]
        public ActionResult Update(Guia guia)
        {
            bool alterado = new GuiaRepository().Alterar(guia);
            return Content(JsonConvert.SerializeObject(new {id = alterado }));
        }

        [HttpGet]
        public ActionResult ObterTodosPorJSON()
        {
            string start = Request.QueryString["start"];
            string length = Request.QueryString["length"];

            List<Guia> guias = new GuiaRepository().ObterTodosParaJSON(start, length);

            return Content(JsonConvert.SerializeObject(new
            {
                data = guias
            }));

        }

        [HttpGet]
        public ActionResult ModalCadastro()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ModalEditar()
        {
            return View();
        }
    }
}