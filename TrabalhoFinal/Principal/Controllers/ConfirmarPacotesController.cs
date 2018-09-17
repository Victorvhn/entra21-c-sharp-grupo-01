using Model;
using Newtonsoft.Json;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Principal.Controllers
{
    public class ConfirmarPacotesController : Controller
    {
        // GET: ConfirmarPacotes
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ObterTodosPorJSON()
        {
            string start = Request.QueryString["start"];
            string lengh = Request.QueryString["length"];
            string search = Request.QueryString["search[value]"];

            List<TuristaPacote> turistasPacotes = new TuristaPacoteRepository().ObterTodosPorJSON(start, lengh, search);
            return Content(JsonConvert.SerializeObject(new { data = turistasPacotes }));
        }
    }
}