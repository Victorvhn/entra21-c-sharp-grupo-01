using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Principal.Controllers
{
    public class IdiomaController : Controller
    {
        // GET: Idioma
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Cadastro()
        {
            ViewBag.TituloPagina = "Idioma Cadastro";
            ViewBag.Idioma = new Idioma();
            return View();
        }
        [HttpGet]
        public ActionResult Editar(int id)
        {
            Idioma idioma = new IdiomaRepository().ObterPeloId(id);
            ViewBag.Idioma = idioma;
            ViewBag.TituloPagina = "Idioma Editar";
            return View();
        }

     

        [HttpPost]
        public ActionResult Store(Idioma idioma)
        {
            int identificador = new IdiomaRepository().Cadastrar(idioma);
            return null;
        }
        [HttpPost]
        public ActionResult Update(Idioma idioma)
        {
            bool alterado = new IdiomaRepository().Alterar(idioma);
            return null;
        }
    }
}