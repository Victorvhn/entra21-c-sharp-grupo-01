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
            ViewBag.TituloPagina = "Guia Cadastro";
            ViewBag.Guia = new Guia();
            return View();
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            Guia guia = new GuiaRepositorio().ObterPeloId(id);
            ViewBag.Guia = guia;
            ViewBag.TituloPagina = "Guia Editar";
            return View();
        }


        [HttpGet]
        public ActionResult Excluir(int id)
        {
            bool apagado = new GuiaRepositorio().Excluir(id);
            return null;
        }

        [HttpPost]
        public ActionResult Store(Guia guia)
        {
            /*if (ModelState.IsValid)
            {
                guia.Cpf = guia.Cpf.Replace(".", "").Replace("-", "");
            }*/
                int identificador = new GuiaRepositorio().Cadastrar(guia);
                return RedirectToAction("Editar", new { id = identificador });

            /*ViewBag.Guia = guia;
            return View("Guia Cadastro");*/
        }


        [HttpPost]
        public ActionResult Update(Guia guia)
        {
            bool alterado = new GuiaRepositorio().Alterar(guia);
            return null;
        }
    }
}