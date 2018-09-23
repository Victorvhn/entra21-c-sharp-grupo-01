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
            return Content(JsonConvert.SerializeObject(endereco));
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

            string[] colunasNomes = new string[3];            
            colunasNomes[0] = "e.cep";
            colunasNomes[1] = "e.logradouro";
            colunasNomes[2] = "c.nome";
            string start = Request.QueryString["start"];
            string length = Request.QueryString["length"];
            string draw = Request.QueryString["draw"];
            string search = '%' + Request.QueryString["search[value]"] + '%';
            string orderColumn = Request.QueryString["order[0][column]"];
            string orderDir = Request.QueryString["order[0][dir]"];
            orderColumn = colunasNomes[Convert.ToInt32(orderColumn)];

            EnderecoRepository repository = new EnderecoRepository();

            List<Endereco> enderecos = repository.ObterTodosParaJSON(start, length, search, orderColumn, orderDir);

            int countEnderecos = repository.ContabilizarEnderecos();
            int countFiltered = repository.ContabilizarEnderecosFiltrados(search);

            return Content(JsonConvert.SerializeObject(new
            {
                data = enderecos,
                draw = draw,
                recordsTotal = countEnderecos,
                recordsFiltered = countFiltered
            }));
        }

        [HttpPost]
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

        [HttpGet]
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