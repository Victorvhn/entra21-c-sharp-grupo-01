using Model;
using Newtonsoft.Json;
using Principal.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Principal.Content
{
    public class ViagemController : Controller
    {
        // GET: Viagem
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Cadastro()
        {
            ViewBag.Viagem = new Viagem();
            return View();
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {

            Viagem viagem = new ViagensRepository().ObterPeloId(id);


            return Content(JsonConvert.SerializeObject(viagem));
        }

        [HttpGet]
        public ActionResult Excluir(int id)
        {
            bool apagar = new ViagensRepository().Excluir(id);

            int sucesso = 0;
            if (apagar == true)
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
        public ActionResult Store(ViagemString viagem)
        {

            Viagem viagemModel = new Viagem();
            viagemModel.IdPacote = Convert.ToInt32(viagem.IdPacote.ToString());
            viagemModel.IdGuia = Convert.ToInt32(viagem.IdGuia.ToString());
            viagemModel.DataHorarioSaida = Convert.ToDateTime(viagem.DataHoraSaidaPadraoBR.Replace("/", "-").ToString());
            viagemModel.DataHorarioVolta = Convert.ToDateTime(viagem.DataHoraVoltaPadraoBR.Replace("/", "-").ToString());

            int identificador = new ViagensRepository().Cadastrar(viagemModel);
            return Content(JsonConvert.SerializeObject(new { id = identificador }));
        }

        [HttpGet]
        public ActionResult ObterTodosPorJSON()
        {
            string start = Request.QueryString["start"];
            string length = Request.QueryString["length"];

            List<Viagem> viagens = new ViagensRepository().ObterTodosPorJSON(start, length);

            return Content(JsonConvert.SerializeObject(new
            {
                data = viagens
            }));
        }

        public ActionResult ObterTodosPorJSONParaSelect2()
        {
            List<Viagem> viagens = new ViagensRepository().ObterTodosParaSelect();

            var x = new object[viagens.Count];
            int i = 0;
            foreach (var viagem in viagens)
            {
                x[i] = new { id = viagem.Id, dataS = viagem.DataHorarioSaida, dataV = viagem.DataHorarioVolta, idGuia = viagem.IdGuia, idPacote = viagem.IdPacote };
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