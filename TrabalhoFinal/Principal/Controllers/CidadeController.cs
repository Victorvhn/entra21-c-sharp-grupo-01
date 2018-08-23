using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Principal.Controllers
{
    public class CidadeController : Controller
    {
        // GET: Cidade
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Cadastro()
        {
            
            ViewBag.Cidade = new Cidade();
            return View();
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            Cidade cidade = new CidadeRepository().ObterPeloId(id);
           
            ViewBag.Cidade = cidade;
            return View();
        }


        [HttpGet]
        public ActionResult Excluir(int id)
        {
            bool apagado = new CidadeRepository().Excluir(id);
            return null;
        }

        [HttpPost]
        public ActionResult Store(Cidade cidade)
        {
            int identificador = new CidadeRepository().Cadastrar(cidade);
            return RedirectToAction("Editar", new { id = identificador});
        }
    }
}