using Model;
using Newtonsoft.Json;
using Principal.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Security.Cryptography;

namespace Principal.Controllers
{
    public class LoginController : Controller
    {
        private string CriptografaSHA512(string valor)
        {
            var _stringHash = "";
            try
            {
                UnicodeEncoding _encode = new UnicodeEncoding();
                byte[] _hashBytes, _messageBytes = _encode.GetBytes(valor);

                SHA512Managed _sha512Manager = new SHA512Managed();

                _hashBytes = _sha512Manager.ComputeHash(_messageBytes);

                foreach (byte b in _hashBytes)
                {
                    _stringHash += String.Format("{0:x2}", b);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return _stringHash;
        }

        // GET: Login
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
        public ActionResult ModalCadastroTurista()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ModalCriarConta()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ModalCriarEndereco()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Store(LoginString login)
        {
            Login loginModel = new Login();
            loginModel.Email = login.Email;
            loginModel.Senha = CriptografaSHA512(login.Senha);

            int result = new LoginRepository().Cadastrar(loginModel);
            return Content(JsonConvert.SerializeObject(new { id = result }));
        }

        [HttpPost]
        public ActionResult StoreTurista(TuristaString turista)
       {
            Turista turistaModel = new Turista();
            turistaModel.IdLogin = Convert.ToInt32(turista.IdLogin.ToString());
            turistaModel.Nome = turista.Nome.ToString();
            turistaModel.Sobrenome = turista.Sobrenome.ToString();
            turistaModel.Cpf = turista.Cpf.ToString();
            turistaModel.Rg = turista.Rg.ToString();
            turistaModel.DataNascimento = Convert.ToDateTime(turista.DataNascimento.Replace("/", "-").ToString());
            turistaModel.Sexo = turista.Sexo.ToString();

            int identificador = new TuristaRepository().Cadastrar(turistaModel);
            return Content(JsonConvert.SerializeObject(new { id = identificador }));
        }

        [HttpPost]
        public ActionResult Index(string usuario, string senha)
        {
            var _senha = CriptografaSHA512(senha);
            try
            {
                Guia guia = new GuiaRepository().VerificarLogin(usuario, _senha);
                if (guia == null)
                {
                    Turista turista = new TuristaRepository().VerificarLogin(usuario, _senha);
                    if (turista == null)
                    {
                        return View();
                    }
                    else
                    {
                        Session.Add("usuarioLogado", turista);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    Session.Add("usuarioLogado", guia);
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return View();
            }
        }        

        public ActionResult Logout()
        {
            Session.Remove("usuarioLogado");
            return RedirectToAction("Index");
        }

        public ActionResult LockScreen()
        {
            return View();
        }
    }
}