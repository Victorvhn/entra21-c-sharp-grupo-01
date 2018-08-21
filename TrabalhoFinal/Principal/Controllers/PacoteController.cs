using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Principal.Controllers
{
    public class PacoteController : Controller
    {
        // GET: Pacote
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            ViewBag.Pacote = new Pacote();
            return View();
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            Pacote pacotes = new PacoteRepository().ObterPeloId(id);

            ViewBag.Pacote = pacotes;
            return View();
        }

        [HttpGet]
        public ActionResult Excluir(int id)
        {
            bool apagado = new PacoteRepository().Excluir(id);
            return null;
        }

        [HttpGet]
        public ActionResult Store(Pacote pacotes)
        {
            int identificador = new PacoteRepository().Cadastrar(pacotes);
            return RedirectToAction("Editar", new { id = identificador });
        }
    }
}