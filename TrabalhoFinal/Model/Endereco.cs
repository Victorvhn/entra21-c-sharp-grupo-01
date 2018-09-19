using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Endereco
    {
        public int Id { get; set; }

        public int IdCidade { get; set; }

        public Cidade Cidade { get; set; }

        public string Cep { get; set; }

        public string Logradouro { get; set; }

        public short Numero { get; set; }

        public string Complemento { get; set; }

        public string Referencia { get; set; }

       /* public string Completo
        {
            get
            {
                return Cidade.Estado.Nome + " " + Cidade.Nome + " " + Logradouro + " - " + Numero + " - " + Complemento + " - " + Referencia;
            }


        }*/
    }
}
