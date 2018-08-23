using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Principal.Models
{
    public class PacoteString
    {

        public string Id { get; set; }

        public string Nome { get; set; }

        public string Valor { get; set; }

        public string PercentualMaximoDesconto { get; set; }

        public List<PontoTuristico> PontosTuristicos;

        public List<Guia> Guias { get; set; }

    }
}