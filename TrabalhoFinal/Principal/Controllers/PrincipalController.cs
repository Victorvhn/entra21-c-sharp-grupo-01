using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Principal.Controllers
{
    public class PrincipalController : Controller
    {
        // GET: Principal
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CadastroEndereco()
        {
            return View();
        }

        public ActionResult CadastroTurista()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CadastroGuia()
        {
            ViewBag.TituloPagina = "Guia Cadastro";
            ViewBag.Guia = new Guia();
            return View();
        }

       /* Não ta funcionando Guia repositorio pq precisa da referencia e nao tem como adicionar rever em sala 
        [HttpGet]
        public ActionResult EditarGuia(int id)
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
            if (ModelState.IsValid)
            {
                guia.Cpf = guia.Cpf.Replace(".", "").Replace("-", "");
                int identificador = new GuiaRepositorio().Cadastrar(guia);
                return RedirectToAction("Editar", new { id = identificador });
            }

            ViewBag.Guia = guia;
            return View("Guia Cadastro");
        }

        [HttpPost]
        public ActionResult Update(Guia guia)
        {
            bool alterado = new GuiaRepositorio().Alterar(guia);
            return null;
        }
        */
    }
}