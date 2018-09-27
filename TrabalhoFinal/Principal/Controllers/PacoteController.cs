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
        [HttpPost]
        public JsonResult GetStrings()
        {
            return Json(new
            {
                sucesso = Resources.Resource.Sucesso,
                desativado = Resources.Resource.Desativado,
                cadastrado = Resources.Resource.CadastradoSucesso,
                alterado = Resources.Resource.AlteradoSucesso,
                desativadoSucesso = Resources.Resource.DesativadoSucesso,
                percentualPreenchido = Resources.Resource.PercentualDescontoPreenchido,
                percentualConter = Resources.Resource.PercentualDescontoDeveConter,
                percentualDeveSer = Resources.Resource.PercentualDescontoDeveSer,
                valorPreenchido = Resources.Resource.ValorPreenchido,
                valorDeveSer = Resources.Resource.ValorDeveSer,
                valorInteiro = Resources.Resource.ValorInteros,
                pacotePreenchido = Resources.Resource.PacotePreenchido,
                pacoteConter = Resources.Resource.PacoteDeveConter
            });
        }
        [HttpGet]
        public ActionResult Index()
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
            return Content(JsonConvert.SerializeObject(pacotes));
        }

        [HttpGet]
        public ActionResult Excluir(int id)
        {
            bool apagado = new PacoteRepository().Excluir(id);

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
        public ActionResult Store(PacoteString pacote)
        {
            Pacote pacoteModel = new Pacote();
            {
                pacoteModel.Nome = pacote.Nome.ToString();
                pacoteModel.Valor = Convert.ToDouble(pacote.Valor.ToString());
                pacoteModel.PercentualMaximoDesconto = Convert.ToByte(pacote.PercentualMaximoDesconto.ToString());
            }

            int identificador = new PacoteRepository().Cadastrar(pacoteModel);
            return Content(JsonConvert.SerializeObject(new { id = identificador }));
        }

        [HttpPost]
        public ActionResult Update(Pacote pacote)
        {
            bool alterado = new PacoteRepository().Alterar(pacote);

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
        public ActionResult ObterTodosPorJSON()
        {
            string[] colunasNomes = new string[4];
            colunasNomes[0] = "id";
            colunasNomes[1] = "nome";
            colunasNomes[2] = "valor";
            colunasNomes[3] = "percentual_max_desconto";
            string start = Request.QueryString["start"];
            string length = Request.QueryString["length"];
            string draw = Request.QueryString["draw"];
            string search = '%' + Request.QueryString["search[value]"] + '%';
            string orderColumn = Request.QueryString["order[0][column]"];
            string orderDir = Request.QueryString["order[0][dir]"];
            orderColumn = colunasNomes[Convert.ToInt32(orderColumn)];

            PacoteRepository repository = new PacoteRepository();

            List<Pacote> pacotes = repository.ObterTodosParaJSON(start, length, search, orderColumn, orderDir);

            int countPacotes = repository.ContabilizarPacotes();
            int countFiltered = repository.ContabilizarPacotesFiltradas(search);

            return Content(JsonConvert.SerializeObject(new
            {
                data = pacotes,
                draw = draw,
                recordsTotal = countPacotes,
                recordsFiltered = countFiltered
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