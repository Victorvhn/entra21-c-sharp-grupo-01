using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Principal.Controllers
{
    public class TuristaController : Controller
    {
        // GET: Turista
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CadastroTurista()
        {
            return View();
        }
    }
}