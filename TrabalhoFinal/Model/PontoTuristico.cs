using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PontoTuristico
    {
        public int Id { get; set; }

        public Endereco Endereco { get; set; }

        public int IdEndereco { get; set; }

        public string Nome { get; set; }
    }
}
