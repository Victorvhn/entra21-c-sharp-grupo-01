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

        [HttpPost]
        public JsonResult GetStrings()
        {
            return Json(new
            {   
                sucesso = Resources.Resource.Sucesso,
                desativado = Resources.Resource.Desativado,
                logPreenchido = Resources.Resource.LogradouroPreenchido,
                logDeveConter = Resources.Resource.LogradouroDeveConter,
                cidadePreenchido = Resources.Resource.CidadePreenchido,
                numPreenchido = Resources.Resource.NumeroPreenchido,
                numSomenteDig = Resources.Resource.NumeroDigitos,
                cepPreenchido = Resources.Resource.CEPPreenchido,
                cepDeveConter = Resources.Resource.CEPDeveConter,
                endereco = Resources.Resource.Endereco,
                enderecoCadastrado = Resources.Resource.EnderecoCadastradoSucesso,
                enderecoAlterado = Resources.Resource.EnderecoEditadoSucesso,
                enderecoDesativado = Resources.Resource.EnderecoDesativadoSucesso,
                editar = Resources.Resource.Editar,
                desativar = Resources.Resource.Desativar
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
        public ActionResult Store(EnderecoString endereco)
        {
            Endereco enderecoModel = new Endereco();

            enderecoModel.Cep = endereco.Cep.Replace("-", "").ToString();
            enderecoModel.Logradouro = endereco.Logradouro.ToString();
            enderecoModel.Numero = Convert.ToInt16(endereco.Numero);
            if (endereco.Complemento != null)
            {
                enderecoModel.Complemento = endereco.Complemento.ToString();
            }
            if (endereco.Referencia != null)
            {
                enderecoModel.Referencia = endereco.Referencia.ToString();
            }
            enderecoModel.IdCidade = Convert.ToInt32(endereco.IdCidade.ToString());


            int identificador = new EnderecoRepository().Cadastrar(enderecoModel);
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
                x[i] = new { id = endereco.Id, text = endereco.Cidade.Nome +  endereco.Logradouro +  endereco.Numero };
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