using Model;
using Newtonsoft.Json;
using Principal.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Principal.Controllers
{
    public class EstadoController : Controller
    {
        // GET: Estados        
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Cadastro()
        {
          
            ViewBag.Estado = new Estado();
            return View();
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            Estado estado = new EstadoRepository().ObterPeloId(id);
            ViewBag.Estado = estado;

            return Content(JsonConvert.SerializeObject(estado));
        }

        [HttpGet]
        public ActionResult Excluir(int id)
        {
            bool apagado = new EstadoRepository().Excluir(id);

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

            /*if (apagado == true)
            {
                sucesso = 1;
                return View(sucesso);
            }
            else
            {
                sucesso = 0;
                return View(sucesso);
            }*/
        }

        [HttpPost]
        public ActionResult Store(EstadoString estado)
        {
            Estado estadoModel = new Estado()
            {
                Nome = estado.Nome.ToString()
            };

            int identificador = new EstadoRepository().Cadastrar(estadoModel);
            return Content(JsonConvert.SerializeObject(new { id = identificador }));
        }

        [HttpPost]
        public ActionResult Update(Estado estado)
        {
            bool alterado = new EstadoRepository().Alterar(estado);
            return null;
        }

        [HttpGet]
        public ActionResult ObterTodosPorJSON()
        {
            string start = Request.QueryString["start"];
            string length = Request.QueryString["length"];

            List<Estado> estados = new EstadoRepository().ObterTodosParaJSON(start, length);

            return Content(JsonConvert.SerializeObject(new
            {

                data = estados
            }));
        }

        [HttpGet]
        public ActionResult ObterTodosPorJSONToSelect2()
        {
            List<Estado> estados = new EstadoRepository().ObterTodosParaSelect();

            var x = new object[estados.Count];
            int i = 0;
            foreach (var estado in estados)
            {
                x[i] = new { id = estado.Id, text = estado.Nome };
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