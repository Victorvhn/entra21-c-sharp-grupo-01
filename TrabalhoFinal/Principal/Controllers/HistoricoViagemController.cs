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
           
            return View();
          
        }

        [HttpGet]
        public ActionResult Excluir(int id)
        {
            bool apagado = new HistoricoViagemRepository().Excluir(id);
            return null;
        }

        [HttpPost]
        public ActionResult Store(HistoricoViagemString historicoViagem)
        {
            HistoricoViagem historicoViagemModel = new HistoricoViagem();
            {
                historicoViagemModel.IdPacote = Convert.ToInt32(historicoViagem.IdPacote.ToString());
                historicoViagemModel.Data = Convert.ToDateTime(historicoViagem.Data.Replace("/", "-").ToString());
            }

            int identificador = new HistoricoViagemRepository().Cadastrar(historicoViagemModel);
            return null;
        }

        [HttpPost]
        public ActionResult Update(HistoricoViagem historicoViagem)
        {
            bool altertado = new HistoricoViagemRepository().Alterar(historicoViagem);
            return null;

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
