using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Principal.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if(Session["usuarioLogado"] == null)
            {
                filterContext.Result = new RedirectResult(Url.Action("Index", "Login"));
            }
            base.OnActionExecuting(filterContext);
        }

        [HttpPost]
        public JsonResult GetDataTableStrings()
        {
            return Json(new
            {
                sEmptyTable = Resources.Resource.NenhumRegistroEncontrado,
                sInfo = Resources.Resource.MostrandoPagina + " " + " _PAGE_ " + Resources.Resource.de + " " + " _PAGES_ ",
                sInfoEmpty = Resources.Resource.NenhumRegistroDisponivel,
                sInfoFiltered = "(" + Resources.Resource.filtradosde + " _MAX_ "+ Resources.Resource.Cadastros+")",
                sInfoPostFix = "",
                sInfoThousands = ".",
                sLengthMenu = "<span>"+ Resources.Resource.Apresentar +"</span> _MENU_",
                sLoadingRecords = Resources.Resource.Carregando,
                sProcessing = Resources.Resource.Processando,
                sZeroRecords = Resources.Resource.NenhumRegistroEncontrado,
                sSearch = Resources.Resource.Buscar,
                oPaginate = new {
                sNext = Principal.Resources.Resource.Proximo,
                    sPrevious = Resources.Resource.Anterior,
                    sFirst = Resources.Resource.Primeiro,
                    sLast = Resources.Resource.Ultimo
                },
                oAria = new {
                    sSortAscending = Resources.Resource.OrdenarAscendentes,
                    sSortDescending = Resources.Resource.OrdenarDescendentes
                }
            });
        }

        public ActionResult ChangeIdiom(String lang)
        {
            if (lang != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lang);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);

                HttpCookie cookie = new HttpCookie("Language");
                cookie.Value = lang;
                Response.Cookies.Add(cookie);
            }

            return RedirectToAction("Index", "Home");
        }

        public static List<CultureInfo> Idioms
        {
            get
            {
                return new List<CultureInfo>
                {
                    CultureInfo.GetCultureInfo("pt-BR"),
                    CultureInfo.GetCultureInfo("en-US"),
                    CultureInfo.GetCultureInfo("de-DE")
                };
            }
        }

        public static CultureInfo CurrentIdiom
        {
            get
            {
                return Thread.CurrentThread.CurrentCulture;
            }
        }

    }
}