using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Principal.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (string.IsNullOrWhiteSpace(Cookie.Get("Usuario")))
            {
                filterContext.Result = new RedirectResult(Url.Action("Index", "Login"));
            }
            base.OnActionExecuting(filterContext);
        }

        /*public void login()
        {
            Cookie.Set("UsuarioNome", usuario.Nome);
            Cookie.Set("UsuarioId", usuario.Id);
        }*/

        public void MinhaAction()
        {
            string nome = Cookie.Get("UsuarioNome");
        }

        public void Logout()
        {
            Cookie.Delete("UsuarioId");
        }
    }
}