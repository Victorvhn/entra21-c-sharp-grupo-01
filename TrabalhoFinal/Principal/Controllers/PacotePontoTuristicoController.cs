using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Principal.Controllers
{
    public class PacotePontoTuristicoController : Controller
    {
        // GET: PacotePontoTuristico
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Cadastro()
        {
            
            ViewBag.PacotePontoTuristico = new PacotePontoTuristico();
            return View();
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            //PacotePontoTuristico pacotePontoTuristico = new PacotePontosTuristicosRepository().ObterPeloId(id);
            //ViewBag.PacotePontoTuristico = pacotePontoTuristico;
          

            return View();
        }


        [HttpGet]
        public ActionResult Excluir(int id)
        {
            //bool apagado = new PacotePontosTuristicosRepository().Excluir(id);

            return null;
        }

        [HttpPost]
        public ActionResult Store(PacotePontoTuristico pacotePontosTuristicos)
        {
            //int identificador = new PacotePontosTuristicosRepository().Cadastrar(pacotePontoTuristico);
            //return RedirectToAction("Editar", new object { id = identificador });
            return null;
        }
        [HttpPost]
        public ActionResult Update(PacotePontoTuristico pacotePontosTuristicos)
        {
            //bool alterado = new PacotePontosTuristicosRepository().Alterar(pacotePontosTuristico);

            return null;
        }
    }
}