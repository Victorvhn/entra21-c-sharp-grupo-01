using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    class ContinenteRepository
    {
        public List<Continente> ObterTodos()
        {
            List<Continente> continentes = new List<Continente>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = "SELECT id, nome FROM continente";
            DataTable tabela = new DataTable();
            tabela.Load(command.ExecuteReader());
            foreach (DataRow linha in tabela.Rows)
            {
                Continente continente = new Continente()
                {
                    Id = Convert.ToInt32(linha[0].ToString()),
                    Nome = linha[1].ToString()
                };
                continentes.Add(continente);
            }
            return continentes;
         }

        public int Cadastro(Continente continenteses)
        {
            SqlCommand command = new BancoDados().ObterConexao();

            command.CommandText = @"INSERT INTO continente(nome) OUTPUT INSERTED.ID VALUES (@NOME)";
            command.Parameters.AddWithValue("@NOME", continenteses.Nome);
            int id = Convert.ToInt32(command.ExecuteScalar().ToString());
            return id;

        }

        public bool Alterar(Continente continente)
        {
            SqlCommand command = new BancoDados().ObterConexao();
            command.CommandText = @"UPDATE continentes SET nome = @NOME WHERE id = @ID";
            command.Parameters.AddWithValue("@NOME", continente.Nome);
            return command.ExecuteNonQuery() == 1;
        }

        public bool Excluir(int id)
        {
            SqlCommand command = new BancoDados().ObterConexao();
            command.CommandText = @"DELETE FROM continente WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            return command.ExecuteNonQuery() == 1;

        }

        public Continente ObterPeloId(int id)
        {
            Continente continente = null;
            SqlCommand command = new BancoDados().ObterConexao();
            command.CommandText = @"SELECT nome FROM continente WHERE id = @ID";
            command.Parameters.AddWithValue("ID", id);
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            if (table.Rows.Count == 1)
            {
                continente = new Continente();
                continente.Id = id;
                continente.Nome = table.Rows[0][0].ToString();
            }
            return continente;

        }
    }
}