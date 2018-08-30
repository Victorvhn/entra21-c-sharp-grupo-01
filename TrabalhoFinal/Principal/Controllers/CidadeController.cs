using Model;
using Newtonsoft.Json;
using Principal.Models;
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
        public ActionResult Store(CidadeString cidade)
        {
            Cidade cidadeModel = new Cidade()
            {
                Nome = cidade.Nome.ToString()
            };

            int identificador = new CidadeRepository().Cadastrar(cidadeModel);
            return RedirectToAction("Editar", new { id = identificador});
        }

        public ActionResult ObterTodosPorJSON()
        {
            string start = Request.QueryString["start"];
            string length = Request.QueryString["length"];

            List<Cidade> cidades = new CidadeRepository().ObterTodosParaJSON(start, length);
            return Content(JsonConvert.SerializeObject(new
            {
                data = cidades
            }));
        }

        public ActionResult Update(Cidade cidade)
        {
            bool alterado = new CidadeRepository().Alterar(cidade);
            return RedirectToAction("Index");


        }

        [HttpGet]
        public ActionResult ObterTodosPorJSONSelect2()
        {

            List<Cidade> cidades = new CidadeRepository().ObterTodosParaSelect();

            var x = new object[cidades.Count];
            int i = 0;
            foreach (var cidade in cidades)
            {
                x[i] = new { id = cidade.Id, text = cidade.Nome, idEstado = cidade.IdEstado };
                i++;
            }

            return Content(JsonConvert.SerializeObject(new { results = x }));
        }
    }
}