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
    public class PontoTuristicoController : Controller
    {
        // GET: PontoTuristico
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Cadastro()
        {
            
            ViewBag.PontoTuristico = new PontoTuristico();
            return View();
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            PontoTuristico PontoTuristico = new PontosTuristicosRepository().ObterPeloId(id);
            ViewBag.PontoTuristico = PontoTuristico;
           
            return View();
        }


        [HttpGet]
        public ActionResult Excluir(int id)
        {
            bool apagado = new PontosTuristicosRepository().Excluir(id);
            return null;
        }

        [HttpPost]
        public ActionResult Store(PontoTuristicoString pontoTuristico)
        {
            PontoTuristico pontoTuristicoModel = new PontoTuristico()
            {
                Nome = pontoTuristico.Nome.ToString()
            };
            int identificador = new PontosTuristicosRepository().Cadastrar(pontoTuristicoModel);
            //return RedirectToAction("Editar", new { id = identificador });
            return null;
            
        }
        [HttpPost]
        public ActionResult Update(PontoTuristico PontoTuristico)
        {
            bool alterado = new PontosTuristicosRepository().Alterar(PontoTuristico);
            return null;
        }

        public ActionResult ObterTodosPorJSON()
        {
            string start = Request.QueryString["start"];
            string length = Request.QueryString["length"];

            List<PontoTuristico> pontosturisticos = new PontosTuristicosRepository().ObterTodosParaJSON(start, length);

            return Content(JsonConvert.SerializeObject(new { data = pontosturisticos }));
        }
        [HttpGet]
        public ActionResult ObterTodosPorJSONSelect2()
        {
            List<PontoTuristico> pontosturisticos = new PontosTuristicosRepository().ObterTodosParaSelect();

            var x = new Object[pontosturisticos.Count];
            int i = 0;
            foreach (var pontoturistico in pontosturisticos)
            {
                x[i] = new { id = pontoturistico.Id, text = pontoturistico.Nome, idEndereco = pontoturistico.IdEndereco };
                i++;
            }
            return Content(JsonConvert.SerializeObject(new { results = x }));
        }
    }
}