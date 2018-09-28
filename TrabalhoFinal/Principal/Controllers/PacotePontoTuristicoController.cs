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
    public class PacotePontoTuristicoController : Controller
    {
        // GET: PacotePontoTuristico
        [HttpPost]
        public JsonResult GetStrings()
        {
            return Json(new
            {
                sucesso = Resources.Resource.Sucesso,
                desativado = Resources.Resource.Desativado,
                cadastrado = Resources.Resource.CadastradoSucesso,
                alterado = Resources.Resource.AlteradoSucesso,
                desativadoSucesso = Resources.Resource.DesativadoSucesso,
                selecionePT = Resources.Resource.SelecionePontoTuristico,
                selecionePacote = Resources.Resource.SelecionePacote
            });
        }
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
            
            ViewBag.PacotePontoTuristico = new PacotePontoTuristico();
            return View();
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            PacotePontoTuristico pacotePontoTuristicos = new PacotePontosTuristicosRepository().ObterPeloId(id);
            ViewBag.PacotePontoTuristico = pacotePontoTuristicos;
            return  Content(JsonConvert.SerializeObject(pacotePontoTuristicos));
        }


        [HttpGet]
        public ActionResult Excluir(int id)
        {
            bool apagar = new PacotePontosTuristicosRepository().Excluir(id);
            int sucesso = 0;
            if (apagar == true)
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
        public ActionResult Store(PacotePontoTuristico pacotePontosTuristicos)
        {
            int identificador = new PacotePontosTuristicosRepository().Cadastro(pacotePontosTuristicos);
            return Content(JsonConvert.SerializeObject(new {id = identificador}));
        }

        [HttpPost]
        public ActionResult Update(PacotePontoTuristico pacotePontosTuristicos)
        {
            bool alterado = new PacotePontosTuristicosRepository().Alterar(pacotePontosTuristicos);

            int sucesso = 0;

            if (alterado == true)
            {
                sucesso = 1;
            }
            else
            {
                sucesso = 0;
            }
            return Content(JsonConvert.SerializeObject(sucesso));
        }

        [HttpGet]
        public ActionResult ObterTodosPorJSON()
        {
            string start = Request.QueryString["start"];
            string length = Request.QueryString["length"];

            List<PacotePontoTuristico> pacotePontoTuristicos = new PacotePontosTuristicosRepository().ObterTodosPorJSON(start, length);

            return Content(JsonConvert.SerializeObject(new
            {
                data = pacotePontoTuristicos
            }));
        }

        [HttpGet]
        public ActionResult ObterTodosPorJSONParaSelect2()
        {
            List<PacotePontoTuristico> pacotePontoTuristicos = new PacotePontosTuristicosRepository().ObterTodosParaSelect();

            var x = new object[pacotePontoTuristicos.Count];
            int i = 0;
            foreach (var pacotePontoTuristico in pacotePontoTuristicos)
            {
                x[i] = new { id = pacotePontoTuristico.Id, idP = pacotePontoTuristico.IdPacote, idPT = pacotePontoTuristico.IdPontoTuristico };
                i++;
            }
            return Content(JsonConvert.SerializeObject(new { results = x }));
        }

        [HttpGet]
        public ActionResult ModalCadastro()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ModalEditar()
        {
            return View();
        }
    }
}