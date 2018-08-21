using System;
using Model;
using Repository;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Principal.Controllers
{
    public class PaisesController : Controller
    {
        // GET: Paises
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Cadastro()
        {

            ViewBag.Cidade = new Paises();
            return View();
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            Paises cidade = new CidadeRepositorio().ObterPeloId(id);

            ViewBag.Cidade = cidade;
            return View();
        }


        [HttpGet]
        public ActionResult Excluir(int id)
        {
            bool apagado = new Paises().Excluir(id);
            return null;
        }

        [HttpPost]
        public ActionResult Store(Paises cidade)
        {
            int identificador = new CidadeRepositorio().Cadastrar(cidade);
            return RedirectToAction("Editar", new { id = identificador });
        }
    }
}