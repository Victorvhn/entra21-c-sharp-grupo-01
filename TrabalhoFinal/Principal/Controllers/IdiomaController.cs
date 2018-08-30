using Model;
using Newtonsoft.Json;
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

            ViewBag.Idioma = new Idioma();
            return View();
        }
        [HttpGet]
        public ActionResult Editar(int id)
        {
            Idioma idioma = new IdiomaRepository().ObterPeloId(id);
            ViewBag.Idioma = idioma;

            return View();
        }



        [HttpPost]
        public ActionResult Store(Idioma idioma)
        {
            int identificador = new IdiomaRepository().Cadastrar(idioma);
            return null;
        }

        public ActionResult ObterTodosPorJSON()
        {
            string start = Request.QueryString["start"];
            string length = Request.QueryString["length"];

            List<Idioma> idiomas = new IdiomaRepository().ObterTodosParaJSON(start, length);
            return Content(JsonConvert.SerializeObject(new
            {
                data = idiomas
            }));
        }
        [HttpPost]
        public ActionResult Update(Idioma idioma)
        {
            bool alterado = new IdiomaRepository().Alterar(idioma);
            return null;
        }

        [HttpGet]
        public ActionResult ObterTodosPorJSONSelect2()
        {
            List<Idioma> idiomas = new IdiomaRepository().ObterTodosParaSelect();

            var x = new object[idiomas.Count];
            int i = 0;
            foreach (var idioma in idiomas)
            {
                x[i] = new { id = idioma.Id, text = idioma.Nome, idGuia = idioma.IdGuia };
                i++;
            }
            return Content(JsonConvert.SerializeObject(new { results = x }));
        }
    }
}