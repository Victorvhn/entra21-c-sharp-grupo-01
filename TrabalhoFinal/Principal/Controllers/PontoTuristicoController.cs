using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Principal.Controllers
{
    public class PontoTuristicoController : Controller
    {
        // GET: PontoTuristico
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Cadastro()
        {
            ViewBag.TituloPagina = "Ponto Turistico Cadastro";
            ViewBag.PontoTuristico = new PontoTuristico();
            return View();
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            PontoTuristico PontoTuristico = new PontosTuristosRepository().ObterPeloId(id);
            ViewBag.PontoTuristico = PontoTuristico;
            ViewBag.TituloPagina = "Ponto Turistico Editar";
            return View();
        }


        [HttpGet]
        public ActionResult Excluir(int id)
        {
            bool apagado = new PontosTuristosRepository().Excluir(id);
            return null;
        }

        [HttpPost]
        public ActionResult Store(PontoTuristico PontoTuristico)
        {
            int identificador = new PontosTuristosRepository().Cadastrar(PontoTuristico);
            return null;
        }
        [HttpPost]
        public ActionResult Update(PontoTuristico PontoTuristico)
        {
            bool alterado = new PontosTuristosRepository().Alterar(PontoTuristico);
            return null;
        }
    }
}