﻿using Model;
using Newtonsoft.Json;
using Principal.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Principal.Controllers
{
    public class EnderecoController : Controller
    {
        // GET: Endereco

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            
            ViewBag.Endereco = new Endereco();
            return View();
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            Endereco endereco = new EnderecoRepository().ObterPeloId(id);           
            ViewBag.Endereco = endereco;
            return View();
        }

        [HttpGet]
        public ActionResult Excluir(int id)
        {
            bool apagado = new EnderecoRepository().Excluir(id);
            return View();
        }

        [HttpPost]
        public ActionResult Store(EnderecoString endereco)
        {
            Endereco enderecoModel = new Endereco()
            {
                Cep = endereco.Cep.ToString(),
                Logradouro = endereco.Logradouro.ToString(),
                Numero = Convert.ToByte(endereco.Numero.ToString()),
                Referencia = endereco.Referencia.ToString(),
                Complemento = endereco.Complemento.ToString()
            };
            int identificador = new EnderecoRepository().Cadastrar(enderecoModel);
            return RedirectToAction("Editar", new { id = identificador });
            
        }

        [HttpGet]
        public ActionResult ObterTodosPorJSON()
        {
            List<Endereco> enderecos = new EnderecoRepository().ObterTodos();
            return Content(JsonConvert.SerializeObject(enderecos));
        }
    }
}