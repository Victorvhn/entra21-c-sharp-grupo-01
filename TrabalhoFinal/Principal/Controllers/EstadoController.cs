using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Principal.Controllers
{
    public class EstadoController : Controller
    {
        // GET: Estados
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Cadastro()
        {
            ViewBag.TituloPAgina = "Esdtado Cadastro";
            ViewBag.Estado = new Estado();
            return View();
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            Estado estado = new EstadoRepositorio().ObterPeloId();
        }
    }
}