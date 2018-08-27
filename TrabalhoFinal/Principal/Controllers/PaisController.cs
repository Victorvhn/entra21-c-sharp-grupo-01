using System;
using Model;
using Repository;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Principal.Models;

namespace Principal.Controllers
{
    public class PaisController : Controller
    {
        // GET: Paises
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Cadastro()
        {

            ViewBag.Pais = new Pais();
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
        public ActionResult Store(PaisString pais)
        {
            Pais paisModel = new Pais()
            {
                IdContinente = Convert.ToInt32(pais.IdContinente.ToString()),
                Nome = pais.Nome.ToString()
            };

            int identificador = new PaisRepository().Cadastrar(paisModel);
            //return RedirectToAction("Editar", new { id = identificador });
            return null;
        }

        [HttpGet]
        public ActionResult ObterTodosPorJSON()
        {
            string start = Request.QueryString["start"];
            string length = Request.QueryString["length"];

            List<Pais> paises = new PaisRepository().ObterTodosParaJSON(start, length);

            return Content(JsonConvert.SerializeObject(new
            {
                data = paises
            }));
        }
    }
}