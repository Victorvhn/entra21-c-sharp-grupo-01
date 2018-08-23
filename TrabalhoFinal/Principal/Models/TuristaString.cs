using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Principal.Models
{
    public class TuristaString
    {
        public string Login_ { get; set; }

        public string Sexo { get; set; }

        public string Id { get; set; }

        public string Endereco { get; set; }

        public string Senha { get; set; }

        public string Ativo { get; set; }

        public string Perfil { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string Cpf { get; set; }

        public string Rg { get; set; }

        public string DataNascimento { get; set; }

        public string IdEndereco { get; set; }

        public string Premium { get; set; }

        public List<Viagem> Viagens { get; set; }
    }
}