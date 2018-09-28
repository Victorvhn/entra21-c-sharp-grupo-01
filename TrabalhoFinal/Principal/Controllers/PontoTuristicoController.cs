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
        [HttpGet]
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

        [HttpGet]
        public ActionResult ObterTodosPorJSON()
        {
            string start = Request.QueryString["start"];
            string length = Request.QueryString["length"];
            string draw = Request.QueryString["draw"];
            string search = '%' + Request.QueryString["search[value]"] + '%';
            string orderColumn = Request.QueryString["order[0][column]"];
            string orderDir = Request.QueryString["order[0][dir]"];
            orderColumn = orderColumn == "1" ? "e.logradouro" : "pt.nome";

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
        public ActionResult ObterTodosPorJSONSelect2()
        {

            List<PontoTuristico> pontosturisticos = new PontosTuristicosRepository().ObterTodosParaSelect();
            var x = new Object[pontosturisticos.Count];
            int i = 0;
            foreach (var pontoturistico in pontosturisticos)
            {
                x[i] = new
                {
                    id = pontoturistico.Id,
                    text = pontoturistico.Nome,
                    idEndereco = pontoturistico.IdEndereco
                };
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
