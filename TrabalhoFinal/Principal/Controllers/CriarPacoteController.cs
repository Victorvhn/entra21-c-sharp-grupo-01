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
    public class CriarPacoteController : Controller
    {
        // GET: CriarPacote
        [HttpGet]
        public ActionResult Index()
        {
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
    }
}