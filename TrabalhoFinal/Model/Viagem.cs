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

        public DateTime Data { get; set; }

        public int IdPacote { get; set; }

        public int IdGuia { get; set; }

        public DateTime DataHorarioSaida { get; set; }

        public DateTime DataHorarioVolta { get; set; }

        public List<ViagemTurista> ViagensTuristas;
    }
}
