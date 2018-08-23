using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Principal.Controllers
{
    public class EnderecoController : Controller
    {
        // GET: Endereco

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            
            ViewBag.Endereco = new Endereco();
            return View();
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            Endereco endereco = new EnderecoRepository().ObterPeloId(id);
           
            ViewBag.Endereco = endereco;
            return View();
        }

        [HttpGet]
        public ActionResult Excluir(int id)
        {
            bool apagado = new EnderecoRepository().Excluir(id);
            return null;
        }

        [HttpPost]
        public ActionResult Store(Endereco endereco)
        {
            int identificador = new EnderecoRepository().Cadastrar(endereco);
            return RedirectToAction("Editar", new { id = identificador });
        }
    }
}