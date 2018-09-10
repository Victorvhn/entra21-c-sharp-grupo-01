using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Principal.Controllers
{
    public class CriarPacoteController : Controller
    {
        // GET: CriarPacote
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ModalCadastro()
        {
            return View();
        }
    }
}