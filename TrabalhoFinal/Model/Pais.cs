using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Pais
    {
        public int Id { get; set; }

        public Continente Continente { get; set; }

        public int IdContinente { get; set; }

        public string Nome { get; set; }
    }
}
