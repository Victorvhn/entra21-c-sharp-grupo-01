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
    public class PacoteController : Controller
    {
        // GET: Pacote
        public ActionResult Tabela()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            ViewBag.Pacote = new Pacote();
            return View();
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            Pacote pacotes = new PacoteRepository().ObterPeloId(id);

            ViewBag.Pacote = pacotes;
            return View();
        }

        [HttpGet]
        public ActionResult Excluir(int id)
        {
            bool apagado = new PacoteRepository().Excluir(id);
            return null;
        }

        [HttpPost]
        public ActionResult Store(PacoteString pacote)
        {
            Pacote pacoteModel = new Pacote();
            {
                pacoteModel.Nome = pacote.Nome.ToString();
                pacoteModel.Valor = Convert.ToDouble(pacote.Valor.ToString());
                pacoteModel.PercentualMaximoDesconto = Convert.ToByte(pacote.PercentualMaximoDesconto.ToString());
            }

            int identificador = new PacoteRepository().Cadastrar(pacoteModel);
            return null;
        }

        [HttpGet]
        public ActionResult ObterTodosPorJSON()
        {
            string start = Request.QueryString["start"];
            string length = Request.QueryString["length"];

            List<Pacote> pacotes = new PacoteRepository().ObterTodosParaJSON(start, length);

            return Content(JsonConvert.SerializeObject(new
            {
                data = pacotes
            }));

        }

        [HttpGet]
        public ActionResult ObterTodosPorJSONToSelect2()
        {

            List<Pacote> pacotes = new PacoteRepository().ObterTodosParaSelect();

            var x = new object[pacotes.Count];
            int i = 0;
            foreach (var pacote in pacotes)
            {
                x[i] = new { id = pacote.Id, text = pacote.Nome };
                i++;
            }


            return Content(JsonConvert.SerializeObject(new { results = x }));

        }
    }
}