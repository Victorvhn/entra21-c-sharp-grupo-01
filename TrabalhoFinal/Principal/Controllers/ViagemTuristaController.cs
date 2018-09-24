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
    public class ViagemTuristaController : Controller
    {
        // GET: ViagemTurista
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Cadastro()
        {

            ViewBag.ViagemTurista = new ViagemTurista();
            return View();
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {

            ViagemTurista viagemTurista = new ViagensTuristasRepository().ObterPeloId(id);
            ViewBag.ViagemTurista = viagemTurista;

            return View();
        }

        [HttpGet]
        public ActionResult Excluir(int id)
        {
            bool apagado = new ViagensTuristasRepository().Excluir(id);
            int sucesso = 0;
            if (apagado == true)
            {
                sucesso = 1;
            }
            else
            {
                sucesso = 0;
            }
            return Content(JsonConvert.SerializeObject(sucesso));
        }

        [HttpPost]
        public ActionResult Store(ViagemTurista viagemTurista)
        {
            int identificador = new ViagensTuristasRepository().Cadastro(viagemTurista);
            return null;
        }

        [HttpGet]
        public ActionResult ObterTodosPorJSONParaSelect2()
        {
            List<ViagemTurista> viagensturista = new ViagensTuristasRepository().ObterTodosParaSelect();

            var x = new object[viagensturista.Count];
            int i = 0;
            foreach (var viagemTurista in viagensturista)
            {
                x[i] = new { id = viagemTurista.Id, valor = viagemTurista.Valor, idGuia = viagemTurista.IdTurista, idPacote = viagemTurista.IdViagem };
                i++;
            }
            return Content(JsonConvert.SerializeObject(new { results = x }));
        }

        [HttpPost]
        public ActionResult Update(ViagemTurista viagemTurista)
        {
            bool alterado = new ViagensTuristasRepository().Alterar(viagemTurista);
            return null;
        }
    }
}