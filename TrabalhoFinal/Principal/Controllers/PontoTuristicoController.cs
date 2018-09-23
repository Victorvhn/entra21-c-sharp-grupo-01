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
            PontoTuristico pontoTuristico = new PontosTuristicosRepository().ObterPeloId(id);
            return Content(JsonConvert.SerializeObject(pontoTuristico));
        }


        [HttpGet]
        public ActionResult Excluir(int id)
        {
            bool apagado = new PontosTuristicosRepository().Excluir(id);
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
        public ActionResult Store(PontoTuristico pontosTuristicos)
        {
            int identificador = new PontosTuristicosRepository().Cadastrar(pontosTuristicos);
            return Content(JsonConvert.SerializeObject(new { id = identificador }));

        }


        [HttpPost]
        public ActionResult Update(PontoTuristico PontoTuristico)
        {
            bool alterado = new PontosTuristicosRepository().Alterar(PontoTuristico);
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

        public ActionResult ObterTodosPorJSON()
        {
            string[] colunasNomes = new string[2];           
            colunasNomes[0] = "pt.nome";
            colunasNomes[1] = "e.nome";
            string start = Request.QueryString["start"];
            string length = Request.QueryString["length"];
            string draw = Request.QueryString["draw"];
            string search = '%' + Request.QueryString["search[value]"] + '%';
            string orderColumn = Request.QueryString["order[0][column]"];
            string orderDir = Request.QueryString["order[0][dir]"];
            orderColumn = colunasNomes[Convert.ToInt32(orderColumn)];

            PontosTuristicosRepository repository = new PontosTuristicosRepository();

            List<PontoTuristico> pontosturisticos = repository.ObterTodosParaJSON(start, length, search, orderColumn, orderDir);

            int countPontoTuristico = repository.ContabilizarPontosTuristicos();
            int countFiltered = repository.ContabilizarPontosTuristicosFiltrados(search);

            return Content(JsonConvert.SerializeObject(new
            {
                data = pontosturisticos,
                draw = draw,
                recordsTotal = countPontoTuristico,
                recordsFiltered = countFiltered
            }));
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
