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
        public ActionResult Editar()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Excluir()
        {
            return null;
        }

        [HttpPost]
        public ActionResult Cadastrar()
        {
            return null;
        }

        [HttpPost]
        public ActionResult Editar()
        {
            return null;
        }
    }
}