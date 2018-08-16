using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TLP.Models
{
    public class Endereco
    {
        public int Id { get; set; }

        public string Cep { get; set; }

        public string Logradouro { get; set; }

        public short Numero { get; set; }

        public string Complemento { get; set; }

        public string Referencia { get; set; }
    }
}