using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Principal.Controllers
{
    public class EstadoController : Controller
    {
        // GET: Estados        

        [HttpGet]
        public ActionResult Cadastro()
        {
          
            ViewBag.Estado = new Estado();
            return View();
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            Estado estado = new EstadoRepository().ObterPeloId(id);
            ViewBag.Estado = estado;
           
            return View();
        }

        [HttpGet]
        public ActionResult Excluir(int id)
        {
            bool apagado = new EstadoRepository().Excluir(id);
            return null;
        }

        [HttpGet]
        public ActionResult Store(Estado estado)
        {
            int identificador = new EstadoRepository().Cadastrar(estado);
            return RedirectToAction("Editar", new { id = identificador });
        }

        [HttpGet]
        public ActionResult Update(Estado estado)
        {
            bool alterado = new EstadoRepository().Alterar(estado);
            return null;
        }
    }
}