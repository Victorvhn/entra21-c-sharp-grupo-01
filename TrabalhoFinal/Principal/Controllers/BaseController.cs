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
                sEmptyTable = "Nenhum registro encontrado",
                sInfo = "Mostrando página _PAGE_ de _PAGES_",
                sInfoEmpty = "Nenhum registro disponível",
                sInfoFiltered = "(Filtrados de _MAX_ registros)",
                sInfoPostFix = "",
                sInfoThousands = ".",
                sLengthMenu = "<span>Apresentar:</span> _MENU_",
                sLoadingRecords = "Carregando...",
                sProcessing = "Processando...",
                sZeroRecords = "Nenhum registro encontrado",
                sSearch = "Pesquisa:  ",
                oPaginate = new {
                sNext = "Próximo",
                    sPrevious = "Anterior",
                    sFirst = "Primeiro",
                    sLast = "Último"
                },
                oAria = new {
                    sSortAscending = ": Ordenar colunas de forma ascendente",
                    sSortDescending = ": Ordenar colunas de forma descendente"
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