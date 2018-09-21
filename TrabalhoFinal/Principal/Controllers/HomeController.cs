using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Principal.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Principal
        public ActionResult Index()
        {
            ViewBag.UsuarioNome = ((Guia)Session["usuarioLogado"]).Nome;
            return View();
        }

    }
}