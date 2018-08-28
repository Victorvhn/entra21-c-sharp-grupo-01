using Model;
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
            PontoTuristico PontoTuristico = new PontosTuristicosRepository().ObterPeloId(id);
            ViewBag.PontoTuristico = PontoTuristico;
           
            return View();
        }


        [HttpGet]
        public ActionResult Excluir(int id)
        {
            bool apagado = new PontosTuristicosRepository().Excluir(id);
            return null;
        }

        [HttpPost]
        public ActionResult Store(PontoTuristicoString pontoTuristico)
        {
            PontoTuristico pontoTuristicoModel = new PontoTuristico()
            {
                Nome = pontoTuristico.Nome.ToString()
            };
            int identificador = new PontosTuristicosRepository().Cadastrar(pontoTuristicoModel);
            //return RedirectToAction("Editar", new { id = identificador });
            return null;
            
        }
        [HttpPost]
        public ActionResult Update(PontoTuristico PontoTuristico)
        {
            bool alterado = new PontosTuristicosRepository().Alterar(PontoTuristico);
            return null;
        }

        public ActionResult ObterTodosPorJSON()
        {
            /*List<PontoTuristico> pontosTuristicos = new PontosTuristicosRepository().ObterTodos();
            return Content(JsonConvert.SerializeObject(pontosTuristicos));*/
            return null;
        }
    }
}