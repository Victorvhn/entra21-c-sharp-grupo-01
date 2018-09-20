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
        [HttpGet]
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

            return Content(JsonConvert.SerializeObject(idioma));
        }

        [HttpGet]
        public ActionResult Excluir(int id)
        {
            bool apagado = new IdiomaRepository().Excluir(id);

            int sucesso = 0;
            if (apagado == true)
            {
                sucesso = 1;
            }
            else
            {
                sucesso = 0;
            }

            return Content(JsonConvert.SerializeObject(sucesso));
        }

        [HttpPost]
        public ActionResult Store(Idioma idioma)
        {
            Idioma idiomaModel = new Idioma();
            idiomaModel.Nome = idioma.Nome.ToString();
            int identificador = new IdiomaRepository().Cadastrar(idiomaModel);
            return Content(JsonConvert.SerializeObject(new { id = identificador }));
        }

        [HttpGet]
        public ActionResult ObterTodosPorJSON()
        {
            string start = Request.QueryString["start"];
            string length = Request.QueryString["length"];
            string draw = Request.QueryString["draw"];
            string search = '%' + Request.QueryString["search[value]"] + '%';
            string orderColumn = Request.QueryString["order[0][column]"];
            string orderDir = Request.QueryString["order[0][dir]"];
            orderColumn = "nome";

            IdiomaRepository repository = new IdiomaRepository();

            List<Idioma> idiomas = repository.ObterTodosParaJSON(start, length, search, orderColumn, orderDir);

            int countEstados = repository.ContabilizarEstados();
            int countFiltered = repository.ContabilizarEstadosFiltradas(search);

            return Content(JsonConvert.SerializeObject(new
            {
                data = idiomas,
                draw = draw,
                recordsTotal = countEstados,
                recordsFiltered = countFiltered
            }));
        }

        [HttpPost]
        public ActionResult Update(Idioma idioma)
        {
            bool alterado = new IdiomaRepository().Alterar(idioma);
            int sucesso = 0;

            if (alterado == true)
            {
                sucesso = 1;
            }
            else
            {
                sucesso = 0;
            }

            return Content(JsonConvert.SerializeObject(sucesso));
        }

        [HttpGet]
        public ActionResult ObterTodosPorJSONSelect2()
        {
            List<Idioma> idiomas = new IdiomaRepository().ObterTodosParaSelect();

            var x = new object[idiomas.Count];
            int i = 0;
            foreach (var idioma in idiomas)
            {
                x[i] = new { id = idioma.Id, text = idioma.Nome,  };
                i++;
            }
            return Content(JsonConvert.SerializeObject(new { results = x }));
        }

        [HttpGet]
        public ActionResult ModalCadastro()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ModalEditar()
        {
            return View();
        }
    }
}