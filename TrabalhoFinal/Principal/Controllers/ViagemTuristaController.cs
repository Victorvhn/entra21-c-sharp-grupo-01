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
            var idGuia = 0;
            var idTurista = 0;

            try
            {
                idGuia = ((Guia)Session["usuarioLogado"]).Id;
            }
            catch
            {
                idGuia = -1;
            }

            try
            {

                idTurista = ((Turista)Session["usuarioLogado"]).Id;
            }
            catch
            {
                idTurista = -1;
            }

            if (idTurista != -1)
            {
                return RedirectToAction("Index", "HomeTurista");
            }

            if (idGuia == -1)
            {
                if (idTurista != -1)
                {
                    ViewBag.UsuarioNome = ((Turista)Session["usuarioLogado"]).Nome;
                    ViewBag.UsuarioSobrenome = ((Turista)Session["usuarioLogado"]).Sobrenome;
                    ViewBag.UsuarioPrivilegio = ((Turista)Session["usuarioLogado"]).Login.Privilegio;
                }
                else
                {
                    return View();
                }
            }
            else
            {
                ViewBag.UsuarioNome = ((Guia)Session["usuarioLogado"]).Nome;
                ViewBag.UsuarioSobrenome = ((Guia)Session["usuarioLogado"]).Sobrenome;
                ViewBag.UsuarioPrivilegio = ((Guia)Session["usuarioLogado"]).Login.Privilegio;
            }
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