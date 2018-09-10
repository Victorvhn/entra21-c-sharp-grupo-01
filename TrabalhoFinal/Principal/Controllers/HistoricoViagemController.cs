using Model;
using Newtonsoft.Json;
using Principal.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Principal.Controllers
{
    public class HistoricoViagemController : Controller
    {
        // GET: HistoricoViagem

        public ActionResult Index()
        {
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
            string start = Request.QueryString["start"];
            string length = Request.QueryString["length"];

            List<HistoricoViagem> historicoViagens = new HistoricoViagemRepository().ObterTodosParaJSON(start, length);

            return Content(JsonConvert.SerializeObject(new
            {
                data = historicoViagens
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
