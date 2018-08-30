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
           
            return View();
        }

        [HttpGet]
        public ActionResult Excluir(int id)
        {
            bool apagado = new EstadoRepository().Excluir(id);
            return null;
        }

        [HttpGet]
        public ActionResult Store(EstadoString estado)
        {
            Estado estadoModel = new Estado()
            {

            };

            int identificador = new EstadoRepository().Cadastrar(estadoModel);
            return RedirectToAction("Editar", new { id = identificador });
        }

        [HttpGet]
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
    }
}