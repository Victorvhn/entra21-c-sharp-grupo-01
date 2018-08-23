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

        [HttpGet]
        public ActionResult Cadastro()
        {
            
            ViewBag.HistoricoViagem = new HistoricoViagem();
            return View();
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            HistoricoViagem historicoViagem = new HistoricoViagemRepository().ObterPeloId(id);
            ViewBag.HistoricoViagem = historicoViagem;
           
            return View();
          
        }

        [HttpGet]
        public ActionResult Excluir(int id)
        {
            bool apagado = new HistoricoViagemRepository().Excluir(id);
            return null;
        }

        [HttpGet]
        public ActionResult Store(HistoricoViagem historicoViagem)
        {
            int identificador = new HistoricoViagemRepository().Cadastrar(historicoViagem);
            return RedirectToAction("Editar", new { id = identificador });
        }

        [HttpGet]
        public ActionResult Update(HistoricoViagem historicoViagem)
        {
            bool altertado = new HistoricoViagemRepository().Alterar(historicoViagem);
            return null;

        }
    }
}