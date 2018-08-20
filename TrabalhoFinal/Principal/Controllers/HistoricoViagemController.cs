using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Principal.Controllers
{
    public class HistoricoViagemController : Controller
    {
        // GET: HistoricoViagem
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Cadastro()
        {
            ViewBag.TituloPagina = "Historico de viagens Cadastro";
            ViewBag.HistoricoViagem = new HistoricoViagem();
            return View();
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            HistoricoViagem historicoViagem = new HistoricoViagemRepositorio().ObterPeloId(id);
            ViewBag.HistoricoViagem = historicoViagem;
            ViewBag.TituloPagina = "Historico de Viagens Editar";
            return View();
          
        }

        [HttpGet]
        public ActionResult Excluir(int id)
        {
            bool apagado = new HistoricoViagemRepositorio().Excluir(id);
            return null;
        }

        [HttpGet]
        public ActionResult Store(HistoricoViagem historicoViagem)
        {
            int identificador = new HistoricoViagemRepositorio().Cadastrar(historicoViagem);
            return RedirectToAction("Editar", new { id = identificador });
        }

        [HttpGet]
        public ActionResult Update(HistoricoViagem historicoViagem)
        {
            bool altertado = new HistoricoViagemRepositorio().Alterar(historicoViagem);
            return null;

        }
    }
}