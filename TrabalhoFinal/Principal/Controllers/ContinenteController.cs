using Model;
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
        public ActionResult Store(Continente continente)
        {
            int identificador = new ContinenteRepository().Cadastro(continente);
            return RedirectToAction("Editar", new { id = identificador });
            
        }

        [HttpPost]
        public ActionResult Update(Continente continente)
        {
           bool alterado = new ContinenteRepository().Alterar(continente);

            return null;
        }
    }
}