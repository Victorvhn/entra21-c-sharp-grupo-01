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
    public class PontoTuristicoController : BaseController
    {
        // GET: PontoTuristico
        [HttpGet]
        public ActionResult Index()
        {
            var idGuia = 0;
            var idTurista = 0;

            try
            {
                idGuia = ((Guia)Session["usuarioLogado"]).Id;
            }
            catch
            {
                idGuia = -1;
            }

            try
            {

                idTurista = ((Turista)Session["usuarioLogado"]).Id;
            }
            catch
            {
                idTurista = -1;
            }

            if (idTurista != -1)
            {
                return RedirectToAction("Index", "HomeTurista");
            }

            if (idGuia == -1)
            {
                if (idTurista != -1)
                {
                    ViewBag.UsuarioNome = ((Turista)Session["usuarioLogado"]).Nome;
                    ViewBag.UsuarioSobrenome = ((Turista)Session["usuarioLogado"]).Sobrenome;
                    ViewBag.UsuarioPrivilegio = ((Turista)Session["usuarioLogado"]).Login.Privilegio;
                }
                else
                {
                    return View();
                }
            }
            else
            {
                ViewBag.UsuarioNome = ((Guia)Session["usuarioLogado"]).Nome;
                ViewBag.UsuarioSobrenome = ((Guia)Session["usuarioLogado"]).Sobrenome;
                ViewBag.UsuarioPrivilegio = ((Guia)Session["usuarioLogado"]).Login.Privilegio;
            }
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
            string[] colunasNomes = new string[3];
            colunasNomes[0] = "hv.id";
            colunasNomes[1] = "p.nome";
            colunasNomes[2] = "hv.data_";
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
        public ActionResult ModalCadastro()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ModalEditar()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ObterTodosPorJSONSelect2()
        {

            List<PontoTuristico> pontoTuristicos = new PontosTuristicosRepository().ObterTodosParaSelect();

            var x = new object[pontoTuristicos.Count];
            int i = 0;
            foreach (var pontoTuristico in pontoTuristicos)
            {
                x[i] = new { id = pontoTuristico.Id, text = pontoTuristico.Nome, idEndereco = pontoTuristico.IdEndereco };
                i++;
            }

            return Content(JsonConvert.SerializeObject(new { results = x }));

        }
    }
}
