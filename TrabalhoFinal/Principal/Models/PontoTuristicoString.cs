using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Principal.Models
{
    public class PontoTuristicoString
    {

        public string Id { get; set; }

        public string Endereco { get; set; }

        public string IdEndereco { get; set; }

        public string Nome { get; set; }

        public List<Pacote> Pacotes { get; set; }

    }
}