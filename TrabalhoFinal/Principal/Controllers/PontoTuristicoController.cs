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
    public class PontoTuristicoController : Controller
    {
        // GET: PontoTuristico
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Cadastro()
        {
            
            ViewBag.PontoTuristico = new PontoTuristico();
            return View();
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            PontoTuristico pontoTuristico = new PontosTuristicosRepository().ObterPeloId(id);
            return Content(JsonConvert.SerializeObject(pontoTuristico));
        }


        [HttpGet]
        public ActionResult Excluir(int id)
        {
            bool apagado = new PontosTuristicosRepository().Excluir(id);
            int sucesso = 0;
            if (apagado == true)
            {
                sucesso = 1;
            }
            else
            {
                sucesso = 0;
            }
            return Content(JsonConvert.SerializeObject(sucesso));
        }

        [HttpPost]
        public ActionResult Store(PontoTuristicoString pontoTuristico)
        {
            PontoTuristico pontoTuristicoModel = new PontoTuristico()
            {
                Id = Convert.ToInt32(pontoTuristico.Id),
                Nome = pontoTuristico.Nome.ToString(),
                IdEndereco = Convert.ToInt32(pontoTuristico.IdEndereco)
            };
            int identificador = new PontosTuristicosRepository().Cadastrar(pontoTuristicoModel);
            return Content(JsonConvert.SerializeObject(pontoTuristicoModel));
            
        }
        [HttpPost]
        public ActionResult Update(PontoTuristico PontoTuristico)
        {
            bool alterado = new PontosTuristicosRepository().Alterar(PontoTuristico);
            int sucesso = 0;
            if (alterado == true)
            {
                sucesso = 1;
            }
            else
            {
                sucesso = 0;
            }
            return Content(JsonConvert.SerializeObject(sucesso));
        }

        public ActionResult ObterTodosPorJSON()
        {
            string start = Request.QueryString["start"];
            string length = Request.QueryString["length"];

            List<PontoTuristico> pontosturisticos = new PontosTuristicosRepository().ObterTodosParaJSON(start, length);

            return Content(JsonConvert.SerializeObject(new { data = pontosturisticos }));
        }
        [HttpGet]
        public ActionResult ObterTodosPorJSONSelect2()
        {
            List<PontoTuristico> pontosturisticos = new PontosTuristicosRepository().ObterTodosParaSelect();

            var x = new Object[pontosturisticos.Count];
            int i = 0;
            foreach (var pontoturistico in pontosturisticos)
            {
                x[i] = new { id = pontoturistico.Id, text = pontoturistico.Nome, idEndereco = pontoturistico.IdEndereco };
                i++;
            }
            return Content(JsonConvert.SerializeObject(new { results = x }));
        }
    }
}