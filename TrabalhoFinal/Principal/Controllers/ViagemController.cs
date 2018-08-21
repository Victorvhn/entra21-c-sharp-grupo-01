using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Principal.Content
{
    public class ViagemController : Controller
    {
        // GET: Viagem
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Cadastro()
        {
            ViewBag.Viagem = new Viagem();
            return View();
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            Viagem viajens = new ViagensRepository().ObterPeloId(id);
            ViewBag.Viagem = viajens;
            return View();
        }

        [HttpGet]
        public ActionResult Excluir(int id)
        {
            bool apagar = new ViagensRepository().Excluir(id);
            return View();
        }

        [HttpGet]
        public ActionResult Store(Viagem viagens)
        {
            int identificador = new ViagensRepository().Cadastrar(viagens);
            return RedirectToAction("Editar", new { id = identificador });
        }
    }
}