using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Guia
    {
        public string Login_ { get; set; }

        public string Sexo { get; set; }

        public int Id { get; set; }

        public Endereco Endereco { get; set; }

        public string Senha { get; set; }

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

        public byte Rank { get; set; }

        public List<Pacote> Pacotes { get; set; }
    }
}
