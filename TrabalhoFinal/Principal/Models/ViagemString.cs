using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Principal.Models
{
    public class ViagemString
    {
        public string Id { get; set; }

        public string Data { get; set; }

        public string IdPacote { get; set; }

        public string IdGuia { get; set; }

        public string DataHorarioSaida { get; set; }

        public string DataHorarioVolta { get; set; }

        public List<Turista> Turistas { get; set; }
    }
}