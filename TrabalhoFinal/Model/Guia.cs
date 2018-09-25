using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Guia
    {

        public string Sexo { get; set; }

        public int Id { get; set; }

        public Endereco Endereco { get; set; }

        public int IdLogin { get; set; }

        public Login Login { get; set; }


        public char Ativo { get; set; }

        public int IdEndereco { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string CarteiraTrabalho { get; set; }

        public string CategoriaHabilitacao { get; set; }

        public double Salario { get; set; }

        public string Cpf { get; set; }

        public string Rg { get; set; }
        

        public DateTime DataNascimento { get; set; }

        public string DataNascimentoPadraoBR { get { return string.Format("{0:dd/MM/yyyy H:mm}", DataNascimentoPadraoBR); } }

        public byte Rank { get; set; }

        public List<Pacote> Pacotes { get; set; }
    }
}
