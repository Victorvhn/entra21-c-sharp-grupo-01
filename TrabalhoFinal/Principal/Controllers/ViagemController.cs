using Model;
using Newtonsoft.Json;
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
        public ActionResult Store(Viagem viagens)
        {
            int identificador = new ViagensRepository().Cadastrar(viagens);
            return RedirectToAction("Editar", new { id = identificador });
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