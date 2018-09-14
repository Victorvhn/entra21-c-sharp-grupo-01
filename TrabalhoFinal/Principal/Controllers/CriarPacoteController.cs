using Model;
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
        public ActionResult Store(PacoteString pacote, ViagemString viagem)
        {
            Pacote pacoteModel = new Pacote();
            pacoteModel.Nome = pacote.Nome.ToString();

            int codigoPacote = new PacoteRepository().Cadastrar(pacoteModel);

            Viagem viagemModel = new Viagem();
            viagemModel.DataHorarioSaida = Convert.ToDateTime(viagem.DataHoraSaidaPadraoBR.Replace("/", "-").ToString());
            viagemModel.DataHorarioVolta = Convert.ToDateTime(viagem.DataHoraVoltaPadraoBR.Replace("/", "-").ToString());
            viagemModel.IdGuia = Convert.ToInt32(viagem.IdGuia.ToString());
            viagemModel.IdPacote = Convert.ToInt32(codigoPacote.ToString());

            return null;
        }

        [HttpGet]
        public ActionResult ModalCadastro()
        {
            return View();
        }
    }
}