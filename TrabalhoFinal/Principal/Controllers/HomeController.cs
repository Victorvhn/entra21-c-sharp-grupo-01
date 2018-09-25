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
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.UsuarioNome = ((Guia)Session["usuarioLogado"]).Nome;
            ViewBag.UsuarioSobrenome = ((Guia)Session["usuarioLogado"]).Sobrenome;
            ViewBag.UsuarioPrivilegio = ((Guia)Session["usuarioLogado"]).Login.Privilegio;
            return View();
        }
    }
}