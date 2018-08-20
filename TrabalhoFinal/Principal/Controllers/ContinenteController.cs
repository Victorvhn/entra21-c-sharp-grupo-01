using Model;
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
            return View();
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Excluir(int id)
        {
            return null;
        }

        [HttpPost]
        public ActionResult Cadastrar(Continente continente)
        {
            return null;
        }

        [HttpPost]
        public ActionResult Editar(Continente continente)
        {
            return null;
        }
    }
}