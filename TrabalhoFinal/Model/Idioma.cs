using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Idioma
    {
        public int Id { get; set; }

        public Guia Guia { get; set; }

        public int IdGuia{ get; set; }

        public string Nome { get; set; }
    }
}
