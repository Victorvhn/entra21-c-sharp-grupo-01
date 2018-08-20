using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Pacote
    {
        public int Id { get; set; }

        public string  Nome { get; set; }

        public float Valor { get; set; }

        public byte PercentualMaximoDesconto { get; set; }

        public List<PontoTuristico> PontosTuristicos;

        public List<Guia> Guias { get; set; }
    }
}
