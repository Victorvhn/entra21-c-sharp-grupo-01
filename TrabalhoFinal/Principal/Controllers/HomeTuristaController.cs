using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Principal.Controllers
{
    public class HomeTuristaController : BaseController
    {
        // GET: HomeTurista
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.UsuarioNome = ((Turista)Session["usuarioLogado"]).Nome;
            ViewBag.UsuarioSobrenome = ((Turista)Session["usuarioLogado"]).Sobrenome;
            ViewBag.UsuarioPrivilegio = ((Turista)Session["usuarioLogado"]).Login.Privilegio;            
            return View();
        }
    }
}