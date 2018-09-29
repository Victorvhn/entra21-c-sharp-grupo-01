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
    public class CriarPacoteController : BaseController
    {
        // GET: CriarPacote
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
                nomePreenchido = Resources.Resource.NomePreenchido,
                nomeConter = Resources.Resource.NomeDeveConter,
                selecioneDestino = Resources.Resource.SelecioneDestino,
                informeDataSaida = Resources.Resource.InformeDataSaida,
                dataInvalida = Resources.Resource.DataValida,
                informeDataVolta = Resources.Resource.InformeDataVolta,
                selecioneGuia = Resources.Resource.SelecioneGuia,
                selecionePontoTuristico = Resources.Resource.SelecionePontoTuristico
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

        [HttpPost]
        public ActionResult Store(PacotePontoTuristicoString pacotePontoTuristicoString, PacoteString pacoteString)
        {
            Pacote pacoteModel = new Pacote();
            pacoteModel.Nome = pacotePontoTuristicoString.Nome.ToString();
            pacoteModel.Valor = Convert.ToDouble(pacoteString.Valor.ToString());

            int codigoPacote = new PacoteRepository().Cadastrar(pacoteModel);

            PacotePontosTuristicosRepository pacotePontosTuristicosRepository = new PacotePontosTuristicosRepository();

            foreach (string idPontoTuristico in pacotePontoTuristicoString.IdsPontosTuristicos)
            {
                PacotePontoTuristico pacotePontoTuristico = new PacotePontoTuristico();
                pacotePontoTuristico.IdPacote = codigoPacote;
                pacotePontoTuristico.IdPontoTuristico = Convert.ToInt32(idPontoTuristico);
                pacotePontoTuristico.Id = pacotePontosTuristicosRepository.Cadastro(pacotePontoTuristico);
            }

            Viagem viagemModel = new Viagem();
            viagemModel.DataHorarioSaida = Convert.ToDateTime(pacotePontoTuristicoString.DataHorarioSaida);
            viagemModel.DataHorarioVolta = Convert.ToDateTime(pacotePontoTuristicoString.DataHorarioVolta);
            viagemModel.IdGuia = Convert.ToInt32(pacotePontoTuristicoString.IdGuia.ToString());
            viagemModel.IdPacote = codigoPacote;
            viagemModel.Id = new ViagensRepository().Cadastrar(viagemModel);

            return Content(JsonConvert.SerializeObject(new { id = viagemModel.Id }));
        }

        [HttpGet]
        public ActionResult ModalCadastro()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ObterTodosPorJSONParaCard()
        {
            List<Pacote> pacotes = new PacoteRepository().ObterTodosParaCard();

            return Content(JsonConvert.SerializeObject(new { data = pacotes }));
        }
    }
}