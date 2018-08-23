﻿using Model;
using Principal.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Principal.Controllers
{
    public class GuiaController : Controller
    {
        // GET: Guia
        
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
            
            return View();
        }


        [HttpGet]
        public ActionResult Excluir(int id)
        {
            bool apagado = new GuiaRepository().Excluir(id);
            return null;
        }

        [HttpPost]
        public ActionResult Store(GuiaString guia)
        {
            
            
            Guia guiaModel = new Guia();
            {
                guiaModel.Nome = guia.Nome.ToString();
                guiaModel.Sobrenome = guia.Sobrenome.ToString();
                guiaModel.DataNascimento = Convert.ToDateTime(guia.DataNascimento.Replace("/","-").ToString());
                guiaModel.Sexo = Convert.ToChar(guia.Sexo.ToString());
                guiaModel.Rg = guia.Rg.ToString();
                guiaModel.Cpf = guia.Cpf.ToString();
                guiaModel.CarteiraTrabalho = guia.CarteiraTrabalho.ToString();
                guiaModel.CatagoriaHabilitacao = Convert.ToChar(guia.CatagoriaHabilitacao.ToString());
                guiaModel.Salario = Convert.ToDouble(guia.Salario.ToString());
                guiaModel.Rank = Convert.ToChar(guia.Rank.ToString());
            }

            int identificador = new GuiaRepository().Cadastrar(guiaModel);
            return null;
               
            

            
        }

        [HttpPost]
        public ActionResult Update(Guia guia)
        {
            bool alterado = new GuiaRepository().Alterar(guia);
            return null;
        }
    }
}