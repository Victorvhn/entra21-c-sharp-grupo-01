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
        [HttpPost]
        public JsonResult GetStrings()
        {
            return Json(new
            {
                sucesso = Resources.Resource.Sucesso,
                desativado = Resources.Resource.Desativado,
                selecioneEstado = Resources.Resource.SelecioneEstado,
                cidadePreenchido = Resources.Resource.CidadePreenchido,
                cidadeConter = Resources.Resource.CidadeDeveConterEntre,
                cadastrado = Resources.Resource.CadastradoSucesso,
                alterado = Resources.Resource.AlteradoSucesso,
                desativadoSucesso = Resources.Resource.DesativadoSucesso

            });
        }
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
            
            ViewBag.Cidade = new Cidade();
            return View();
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            Cidade cidade = new CidadeRepository().ObterPeloId(id);           
            ViewBag.Cidade = cidade;
            return Content(JsonConvert.SerializeObject(cidade));
        }

        [HttpGet]
        public ActionResult Excluir(int id)
        {
            bool apagado = new CidadeRepository().Excluir(id);

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
        public ActionResult Store(CidadeString cidade)
        {
            Cidade cidadeModel = new Cidade()
            {
                Id = Convert.ToInt32(cidade.Id),
                IdEstado = Convert.ToInt32(cidade.IdEstado),
                Nome = cidade.Nome.ToString()
            };

            int identificador = new CidadeRepository().Cadastrar(cidadeModel);
            return Content(JsonConvert.SerializeObject(new { id = identificador }));

        }

        [HttpPost]
        public ActionResult Update(Cidade cidade)
        {
            bool alterado = new CidadeRepository().Alterar(cidade);

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
            orderColumn = orderColumn == "1" ? "e.nome" : "c.nome";
     

            CidadeRepository repository = new CidadeRepository();

            List<Cidade> cidades = repository.ObterTodosParaJSON(start, length, search, orderColumn, orderDir);

            int countCidades = repository.ContabilizarCidades();
            int countFiltered = repository.ContabilizarCidadesFiltradas(search);

            return Content(JsonConvert.SerializeObject(new
            {
                data = cidades,
                draw = draw,
                recordsTotal = countCidades,
                recordsFiltered = countFiltered
            }));
        }

        [HttpGet]
        public ActionResult ObterTodosPorJSONParaSelect2()
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