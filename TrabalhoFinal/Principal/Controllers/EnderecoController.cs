using Model;
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
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Excluir(int id)
        {
            return null;
        }

        [HttpPost]
        public ActionResult Cadastrar(Endereco endereco)
        {
            return null;
        }

        [HttpPost]
        public ActionResult Editar(Endereco endereco)
        {
            return null;
        }
    }
}