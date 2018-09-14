using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Viagem
    {
        public int Id { get; set; }

        public DateTime DataCompra { get; set; }

        public Pacote Pacote { get; set; }

        public int IdPacote { get; set; }

        public Guia Guia { get; set; }

        public int IdGuia { get; set; }

        public DateTime DataHorarioSaida { get; set; }

        public string DataHoraSaidaPadraoBR { get { return string.Format("{0:dd/MM/yyyy H:mm}", DataHorarioSaida); } }

        public DateTime DataHorarioVolta { get; set; }

        public string DataHoraVoltaPadraoBR { get { return string.Format("{0:dd/MM/yyyy H:mm}", DataHorarioVolta); } }

        public List<Turista> Turistas { get; set; }
    }
}
