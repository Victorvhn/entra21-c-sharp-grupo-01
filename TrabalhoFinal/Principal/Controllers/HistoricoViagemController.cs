using Model;
using Newtonsoft.Json;
using Principal.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Principal.Controllers
{
    public class HistoricoViagemController : BaseController
    {
        // GET: HistoricoViagem
        [HttpPost]
        public JsonResult GetStrings()
        {
            return Json(new
            {
                sucesso = Resources.Resource.Sucesso,
                desativado = Resources.Resource.Desativado,
                cadastro = Resources.Resource.CadastradoSucesso,
                alterado = Resources.Resource.AlteradoSucesso,
                desativadoSucesso = Resources.Resource.DesativadoSucesso,
                selecionePacote = Resources.Resource.SelecionePacote,
                informeData = Resources.Resource.InformeData,
                dataValida = Resources.Resource.DataValida,
                voceTemCerteza = Resources.Resource.VoceTemCerteza,
                simDesativar = Resources.Resource.SimDesativar,
                naoCancelar = Resources.Resource.NaoCancelar,
                cancelado = Resources.Resource.Cancelado,
                seuArquivoEstaSalvo = Resources.Resource.SeuArquivoEstaSalvo,
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

            ViewBag.HistoricoViagem = new HistoricoViagem();
            return View();
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            HistoricoViagem historicoViagem = new HistoricoViagemRepository().ObterPeloId(id);
            ViewBag.HistoricoViagem = historicoViagem;

            return Content(JsonConvert.SerializeObject(historicoViagem));

        }

        [HttpGet]
        public ActionResult Excluir(int id)
        {
            bool apagado = new HistoricoViagemRepository().Excluir(id);

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
        public ActionResult Store(HistoricoViagemString historicoViagem)
        {
            HistoricoViagem historicoViagemModel = new HistoricoViagem();
            {
                historicoViagemModel.IdPacote = Convert.ToInt32(historicoViagem.IdPacote);
                historicoViagemModel.Data = Convert.ToDateTime(historicoViagem.Data.Replace("/", "-").ToString());
            }

            int identificador = new HistoricoViagemRepository().Cadastrar(historicoViagemModel);
            return Content(JsonConvert.SerializeObject(new { id = identificador }));
        }

        [HttpPost]
        public ActionResult Update(HistoricoViagem historicoViagem)
        {
            bool alterado = new HistoricoViagemRepository().Alterar(historicoViagem);

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
            orderColumn = colunasNomes[Convert.ToInt32(orderColumn)];

            HistoricoViagemRepository repository = new HistoricoViagemRepository();

            List<HistoricoViagem> historicoViagens = repository.ObterTodosParaJSON(start, length, search, orderColumn, orderDir);

            int countHistoricoViagens = repository.ContabilizarCidades();
            int countFiltered = repository.ContabilizarCidadesFiltradas(search);

            return Content(JsonConvert.SerializeObject(new
            {
                data = historicoViagens,
                draw = draw,
                recordsTotal = countHistoricoViagens,
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
