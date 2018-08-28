﻿using Model;
using Principal.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Principal.Controllers
{
    public class TuristaController : Controller
    {
        // GET: Turista
        public ActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public ActionResult Cadastro()
        {
           
            ViewBag.Turista = new Turista();
            return View();
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {

            Turista turista = new TuristaRepository().ObterPeloId(id);
            ViewBag.Turista = turista;
       
            return View();
        }


        [HttpGet]
        public ActionResult Excluir(int id)
        {
            bool apagado = new TuristaRepository().Excluir(id);
            return null;
        }

        [HttpPost]
        public ActionResult Store(TuristaString turista)
        {
            Turista turistaModel = new Turista()
            {
                Nome = turista.Nome.ToString(),
            };
            int identificador = new TuristaRepository().Cadastrar(turistaModel);
            return null;
        }
        
        [HttpPost]
        public ActionResult Update(Turista turista)
        {
            bool alterado = new TuristaRepository().Alterar(turista);
            return null;
        }
    }
}