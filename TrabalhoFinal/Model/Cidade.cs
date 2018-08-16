using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Cidade
    {
        public int Id { get; set; }

        public Estado Estado { get; set; }

        public int IdEstado { get; set; }

        public string Nome { get; set; }

    }
}
