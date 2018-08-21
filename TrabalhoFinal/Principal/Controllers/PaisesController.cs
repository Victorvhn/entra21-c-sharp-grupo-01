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

            ViewBag.Cidade = new Pais();
            return View();
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            Pais cidade = new PaisRepository().ObterPeloId(id);

            ViewBag.Cidade = cidade;
            return View();
        }


        [HttpGet]
        public ActionResult Excluir(int id)
        {
            //bool apagado = new PaisRepository().Excluir(id);
            return null;
        }

        [HttpPost]
        public ActionResult Store(Pais paises)
        {
            int identificador = new PaisRepository().Cadastrar(paises);
            return RedirectToAction("Editar", new { id = identificador });
        }
    }
}