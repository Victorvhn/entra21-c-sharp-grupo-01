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

            Viagem viajens = new ViagensRepository().ObterPeloId(id);
            ViewBag.Viagem = viajens;
            return View();
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

        [HttpGet]
        public ActionResult Store(ViagemString viagem, GuiaString guia, PacoteString pacote)
        {
            Pacote pacoteModel = new Pacote();
            pacoteModel.Nome = pacote.Nome.ToString();
            pacoteModel.Valor = Convert.ToDouble(pacote.Valor.ToString());

            int codigoPacote = new PacoteRepository().Cadastrar(pacoteModel);

            Guia guiaModel = new Guia();
            guiaModel.IdEndereco =Convert.ToInt32(guia.IdEndereco.ToString());
            guiaModel.Nome = guia.Nome.ToString();
            guiaModel.Sobrenome = guia.Sobrenome.ToString();
            guiaModel.DataNascimento = Convert.ToDateTime(guia.DataNascimento.Replace("/", "-").ToString());
            guiaModel.Sexo = guia.Sexo.ToString();
            guiaModel.Rg = guia.Rg.ToString();
            guiaModel.Cpf = guia.Cpf.ToString();
            guiaModel.CarteiraTrabalho = guia.CarteiraTrabalho.ToString();
            guiaModel.CategoriaHabilitacao = guia.CategoriaHabilitacao.ToString();
            guiaModel.Salario = Convert.ToDouble(guia.Salario.ToString());
            guiaModel.Rank = Convert.ToByte(guia.Rank.ToString());

            int codigoGuia = new GuiaRepository().Cadastrar(guiaModel);

            Viagem viagemModel = new Viagem();
            viagemModel.Data = Convert.ToDateTime(viagem.DataHoraSaidaPadraoBR.Replace("/", "-").ToString());
            viagemModel.Data = Convert.ToDateTime(viagem.DataHoraVoltaPadraoBR.Replace("/", "-").ToString());
            viagemModel.IdGuia = codigoGuia;
            viagemModel.IdPacote = codigoPacote;


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