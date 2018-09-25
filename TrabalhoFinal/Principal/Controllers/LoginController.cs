using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Principal.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string usuario, string senha)
        {
            try
            {
                Guia guia = new GuiaRepository().VerificarLogin(usuario, senha);
                if (guia == null)
                {
                    Turista turista = new TuristaRepository().VerificarLogin(usuario, senha);
                    if (turista == null)
                    {
                        return View();
                    }
                    else
                    {
                        Session.Add("usuarioLogado", turista);
                        return RedirectToAction("Index", "HomeTurista");
                    }
                }
                else
                {
                    Session.Add("usuarioLogado", guia);
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session.Remove("usuarioLogado");
            return RedirectToAction("Index");
        }

        public ActionResult LockScreen()
        {
            return View();
        }
    }
}