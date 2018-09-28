using Model;
using Newtonsoft.Json;
using Principal.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Principal.Controllers
{
    public class GuiaController : Controller
    {
        // GET: Guia
        [HttpPost]
        public JsonResult GetStrings()
        {
            return Json(new
            {
                nomePreenchido = Resources.Resource.NomePreenchido,
                nomeDeveConter = Resources.Resource.NomeDeveConter,
                sobrenomePreenchido = Resources.Resource.SobrenomePrenchido,
                sobrenemeDeveConter = Resources.Resource.SobrenomeDeveConter,
                cpfPreenchido = Resources.Resource.CpfPreenchido,
                cpfInvalido = Resources.Resource.CpfInvalido,
                dataNascimentoPreenchido = Resources.Resource.DataNascimentoPreenchido,
                dataNascimentoInvalida = Resources.Resource.DataNascimentoInvalido,
                sexoPreenchido = Resources.Resource.Sexo,
                cadastradoSucesso = Resources.Resource.CadastradoSucesso,
                carteiraTrabalhoPreenchido = Resources.Resource.CarteiraTrabalhoPreenchido,
                carteiraTrabalhoDigitos = Resources.Resource.CarteiraTrabalhoDigitos,
                salarioPreenchido = Resources.Resource.SalarioPreenchido,
                salarioNumber = Resources.Resource.SalarioDigitosDecimais,
                salariominimo = Resources.Resource.SalarioMinimo,
                salarioDeveSer = Resources.Resource.SalalarioDeveSer,
                categoriaHabilitacaoPreenchido = Resources.Resource.CarteiraTrabalhoPreenchido,
                estadoPreenchido = Resources.Resource.EstadoPreenchido,
                cidadePreenchido = Resources.Resource.EstadoPreenchido,
                cepPreenchido = Resources.Resource.CEPPreenchido,
                cepInvalido = Resources.Resource.CEPInvalido,
                numeroPrenchido = Resources.Resource.NumeroPreenchido,
                numeroDigitos = Resources.Resource.NumeroDigitos,
                sucesso = Resources.Resource.Sucesso,
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


            if(idTurista != -1)
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

            ViewBag.Guia = new Guia();
            return View();
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            Guia guia = new GuiaRepository().ObterPeloId(id);
            ViewBag.Guia = guia;

            return Content(JsonConvert.SerializeObject(guia));
        }

        [HttpGet]
        public ActionResult Excluir(int id)
        {
            
            bool apagado = new GuiaRepository().Excluir(id);
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
        public ActionResult Store(Guia guia)
        {
            guia.Endereco.Id = new EnderecoRepository().Cadastrar(guia.Endereco);

            int identificador = new GuiaRepository().Cadastrar(guia);
            
            return Content(JsonConvert.SerializeObject(new { id = identificador }));
        }

        [HttpPost]
        public ActionResult Update(Guia guia)
        {
            bool alterado = new GuiaRepository().Alterar(guia);
            return Content(JsonConvert.SerializeObject(new { success = alterado }));
        }

        [HttpGet]
        public ActionResult ObterTodosPorJSON()
        {
            string[] colunasNomes = new string[4];
            colunasNomes[0] = "g.id";
            colunasNomes[1] = "g.nome";
            colunasNomes[2] = "g.sobrenome";
            colunasNomes[3] = "g.cpf";
            string start = Request.QueryString["start"];
            string length = Request.QueryString["length"];
            string draw = Request.QueryString["draw"];
            string search = '%' + Request.QueryString["search[value]"] + '%';
            string orderColumn = Request.QueryString["order[0][column]"];
            string orderDir = Request.QueryString["order[0][dir]"];
            orderColumn = colunasNomes[Convert.ToInt32(orderColumn)];

            GuiaRepository repository = new GuiaRepository();

            List<Guia> guias = repository.ObterTodosParaJSON(start, length, search, orderColumn, orderDir);

            int countViagens = repository.ContabilizarGuias();
            int countFiltered = repository.ContabilizarGuiasFiltrados(search);

            return Content(JsonConvert.SerializeObject(new
            {
                data = guias,
                draw = draw,
                recordsTotal = countViagens,
                recordsFiltered = countFiltered
            }));

        }

        [HttpGet]
        public ActionResult ObterTodosParaSelect2()
        {
            List<Guia> guias = new GuiaRepository().ObterTodosParaSelect();

            var x = new object[guias.Count];
            int i = 0;
            foreach (var guia in guias)
            {
                x[i] = new { id = guia.Id, nome = guia.Nome, sobrenome = guia.Sobrenome, cpf = guia.Cpf, rank = guia.Rank };
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