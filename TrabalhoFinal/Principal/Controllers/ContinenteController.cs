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
    public class ContinenteController : Controller
    {
        // GET: Continentes

        [HttpGet]
        public ActionResult Table()
        {

            return View();
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
           
            ViewBag.Continente = new Continente();
            return View();
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {                     
            Continente continente = new ContinenteRepository().ObterPeloId(id);
            ViewBag.Continente = continente;
            return View();
        }

        [HttpGet]
        public ActionResult Excluir(int id)
        {
           bool apagado = new ContinenteRepository().Excluir(id);
            return null;
        }

        [HttpPost]
        public ActionResult Store(ContinenteString continente)
        {

            Continente continenteModel = new Continente()
            {
                Nome = continente.Nome.ToString(),
            };


            int identificador = new ContinenteRepository().Cadastro(continenteModel);
            //return RedirectToAction("Editar", new { id = identificador });
            return null;
        }

        [HttpPost]
        public ActionResult Update(Continente continente)
        {
           bool alterado = new ContinenteRepository().Alterar(continente);

            return null;
        }

        [HttpGet]
        public ActionResult ObterTodosPorJSON()
        {
            string start = Request.QueryString["start"];
            string length = Request.QueryString["length"];
            
            List<Continente> continentes = new ContinenteRepository().ObterTodos(start, length);
            
            return Content(JsonConvert.SerializeObject(new
            {
                data = continentes
            }));
        }
    }
}