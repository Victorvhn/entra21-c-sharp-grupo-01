using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Principal.Models
{
    public class PacotePontoTuristicoString
    {
        public string Nome { get; set; }
        public string DataHorarioSaida { get; set; }
        public string DataHorarioVolta { get; set; }
        public string IdGuia { get; set; }
        public string[] IdsPontosTuristicos { get; set; }
    }
}