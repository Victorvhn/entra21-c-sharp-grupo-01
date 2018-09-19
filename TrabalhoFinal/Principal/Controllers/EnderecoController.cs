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
    public class EnderecoController : Controller
    {
        // GET: Endereco

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {

            ViewBag.Endereco = new Endereco();
            return View();
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            Endereco endereco = new EnderecoRepository().ObterPeloId(id);
            ViewBag.Endereco = endereco;
            return View();
        }

        [HttpGet]
        public ActionResult Excluir(int id)
        {
            bool apagado = new EnderecoRepository().Excluir(id);
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
        public ActionResult Store(Endereco endereco)
        {   
            int identificador = new EnderecoRepository().Cadastrar(endereco);
            return Content(JsonConvert.SerializeObject(new { id = identificador }));

        }

        [HttpGet]
        public ActionResult ObterTodosPorJSON()
        {
            string start = Request.QueryString["start"];
            string length = Request.QueryString["length"];

            List<Endereco> enderecos = new EnderecoRepository().ObterTodosPorJSON();
            return Content(JsonConvert.SerializeObject(new
            {
                data = enderecos
            }));
        }

        public ActionResult Update(Endereco endereco)
        {
            bool alterado = new EnderecoRepository().Alterar(endereco);
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

        public ActionResult ObterTodosPorJSONToSelect2()
        {

            List<Endereco> enderecos = new EnderecoRepository().ObterTodosParaSelect();

            var x = new object[enderecos.Count];
            int i = 0;
            foreach (var endereco in enderecos)
            {
                x[i] = new { id = endereco.Id, cep = endereco.Cep, num = endereco.Numero, log = endereco.Logradouro, comp = endereco.Complemento, re = endereco.Referencia, eci = endereco.IdCidade };
                i++;
            }

            return Content(JsonConvert.SerializeObject(new { results = x }));
        }

        public ActionResult ModalCadastro()
        {
            return View();
        }

        public ActionResult ModalEditar()
        {
            return View();
        }
    }

}