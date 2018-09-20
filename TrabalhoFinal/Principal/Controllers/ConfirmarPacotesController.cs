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
    public class ConfirmarPacotesController : Controller
    {
        // GET: ConfirmarPacotes
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ObterTodosPorJSON()
        {
            string[] colunasNomes = new string[5];
            colunasNomes[0] = "tp.id";
            colunasNomes[1] = "t.nome";
            colunasNomes[2] = "p.nome";
            colunasNomes[3] = "p.valor";
            colunasNomes[4] = "tp.status_do_pedido";
            string start = Request.QueryString["start"];
            string length = Request.QueryString["length"];
            string draw = Request.QueryString["draw"];
            string search = '%' + Request.QueryString["search[value]"] + '%';
            string orderColumn = Request.QueryString["order[0][column]"];
            string orderDir = Request.QueryString["order[0][dir]"];
            orderColumn = colunasNomes[Convert.ToInt32(orderColumn)];

            TuristaPacoteRepository repository = new TuristaPacoteRepository();

            List<TuristaPacote> turistasPacotes = repository.ObterTodosPorJSON(start, length, search, orderColumn, orderDir);

            int countPacotesConfirmacoes = repository.ContabilizarPacotesAguardando();
            int countFiltered = repository.ContabilizarPacotesAguardandoFiltrado(search);

            return Content(JsonConvert.SerializeObject(new {
                data = turistasPacotes,
                draw = draw,
                recordsTotal = countPacotesConfirmacoes,
                recordsFiltered = countFiltered
            }));
        }
    }
}