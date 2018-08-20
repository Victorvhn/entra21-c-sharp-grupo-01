using Model;
using Principal.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PacoteRepository
    {
        public List<Pacote> ObterTodos()
        {
            List<Pacote> pacotes = new List<Pacote>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = "SELECT id, nome, valor, percentual_max_desconto FROM pacotes";
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            foreach (DataRow line in table.Rows)
            {
                Pacote pacote = new Pacote()
                {
                    Id = Convert.ToInt32(line[0].ToString()),
                    Nome = line[1].ToString(),
                    Valor = Convert.ToSingle(line[2].ToString()),
                    PercentualMaximoDesconto = Convert.ToByte(line[3].ToString())
                };
                pacotes.Add(pacote);
            }
            return pacotes;
        }
        public int Cadastrar(Pacote pacote)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = "INSERT INT pacotes(nome, valor, percentual_max_desconto) OUTPUT INSERTED.ID VALUES(@NOME,@VALOR,@PERCENTUAL_MAX_DESCONTO)";
            command.Parameters.AddWithValue("@NOME", pacote.Nome);
            command.Parameters.AddWithValue("@VALOR", pacote.Valor);
            command.Parameters.AddWithValue("@PERCENTUAL_MAX_DESCONTO", pacote.PercentualMaximoDesconto);
            int id = Convert.ToInt32(command.ExecuteScalar().ToString());
            return id;
        }
        public bool Excluir(int id)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = "DELETE FROM pacotes WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            return command.ExecuteNonQuery() == 1;
        }
        public Pacote ObterPeloId(int id)
        {
            Pacote pacote = null;
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = "SELECT nome, valor,percentual_max_desconto FROM pacotes WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            if(table.Rows.Count == 1)
            {
                pacote = new Pacote();
                pacote.Id = id;
                pacote.Nome = table.Rows[0][0].ToString();
                pacote.Valor = Convert.ToSingle(table.Rows[0][1].ToString());
                pacote.PercentualMaximoDesconto = Convert.ToByte(table.Rows[0][2].ToString());
            }
            return pacote;
        }
        public bool Alterar(Pacote pacote)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = "UPDATE pacotes SET nome = @NOME, valor = @VALOR, percentual_max_desconto = @PERCENTUAL_MAX_DESCONTO WHERE id = @ID";
            command.Parameters.AddWithValue("@NOME", pacote.Nome);
            command.Parameters.AddWithValue("@VALOR",pacote.Valor);
            command.Parameters.AddWithValue("@PERCENTUAL_MAX_DESCONTO", pacote.PercentualMaximoDesconto);
            command.Parameters.AddWithValue("@ID", pacote.Id);
            return command.ExecuteNonQuery() == 1;



        }
    }
    
}
