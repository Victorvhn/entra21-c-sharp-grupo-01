using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Principal.Controllers
{
    public class ViagemTuristaController : Controller
    {
        // GET: ViagemTurista
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Cadastro()
        {
            ViewBag.TituloPagina = "Viagem Turista Cadastro";
            ViewBag.ViagemTurista = new ViagemTurista();
            return View();
        }
        [HttpGet]
        public ActionResult Editar(int id)
        {
            ViagemTurista viagemTurista = new ViagensTuristasRepository().ObterPeloId(id);
            //ViewBag.ViagemTurista = viagemTurista;
            ViewBag.TituloPagina = "Viagem Turista Editar";
            return View();
        }



        [HttpPost]
        public ActionResult Store(ViagemTurista viagemTurista)
        {
            int identificador = new ViagensTuristasRepository().Cadastro(viagemTurista);
            return null;
        }
        [HttpPost]
        public ActionResult Update(ViagemTurista viagemTurista)
        {
            bool alterado = new ViagensTuristasRepository().Alterar(viagemTurista);
            return null;
        }
    }
}