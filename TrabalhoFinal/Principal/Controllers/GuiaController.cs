using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Principal.Controllers
{
    public class GuiaController : Controller
    {
        // GET: Guia
        
        [HttpGet]
        public ActionResult Cadastro()
        {
           
            ViewBag.Guia = new Guia();
            return View();
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            Guia guia = new GuiaRepository().ObterPeloId(id);
            ViewBag.Guia = guia;
            
            return View();
        }


        [HttpGet]
        public ActionResult Excluir(int id)
        {
            bool apagado = new GuiaRepository().Excluir(id);
            return null;
        }

        [HttpPost]
        public ActionResult Store(Guia guia)
        {
            /*if (ModelState.IsValid)
            {
                guia.Cpf = guia.Cpf.Replace(".", "").Replace("-", "");
            }*/
                int identificador = new GuiaRepository().Cadastrar(guia);
                /*return RedirectToAction("Editar", new { id = identificador });*/
                return null;

            /*ViewBag.Guia = guia;
            return View("Guia Cadastro");*/
        }


        [HttpPost]
        public ActionResult Update(Guia guia)
        {
            bool alterado = new GuiaRepository().Alterar(guia);
            return null;
        }
    }
}