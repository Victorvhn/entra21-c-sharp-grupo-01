using Model;
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

            return View();
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {

            return View();
        }


        [HttpGet]
        public ActionResult Escluir(int id)
        {

            return null;
        }

        [HttpPost]
        public ActionResult Store(Turista turista)
        {
            return null;
        }
    }
}