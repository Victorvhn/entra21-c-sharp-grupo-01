using Model;
using Principal.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Principal.Controllers
{
    public class TuristaController : Controller
    {
        // GET: Turista
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
           
            ViewBag.Turista = new Turista();
            return View();
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {

            Turista turista = new TuristaRepository().ObterPeloId(id);
            ViewBag.Turista = turista;
       
            return View();
        }


        [HttpGet]
        public ActionResult Excluir(int id)
        {
            bool apagado = new TuristaRepository().Excluir(id);
            return null;
        }

        [HttpPost]
        public ActionResult Store(TuristaString turista)
        {
            Turista turistaModel = new Turista()
            {
                Nome = turista.Nome.ToString(),
            };
            int identificador = new TuristaRepository().Cadastrar(turistaModel);
            return null;
        }
        
        [HttpPost]
        public ActionResult Update(Turista turista)
        {
            bool alterado = new TuristaRepository().Alterar(turista);
            return null;
        }
    }
}