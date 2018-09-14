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

        public string DataCompra { get; set; }

        public string IdPacote { get; set; }

        public string IdGuia { get; set; }

        public string DataHoraSaidaPadraoBR { get; set; }

        public string DataHoraVoltaPadraoBR { get; set; }

        public string DataHoraSaida { get; set; }

        public string DataHoraVolta { get; set; }

        public List<Turista> Turistas { get; set; }
    }
}