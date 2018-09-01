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
    public class EstadoRepository
    {
        public List<Estado> ObterTodos()
        {
            List<Estado> estados = new List<Estado>();
            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = "SELECT id, nome FROM estados";
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            foreach (DataRow line in table.Rows)
            {
                Estado estado = new Estado()
                {
                    Id = Convert.ToInt32(line[0].ToString()),
                    Nome = line[2].ToString()
                };
                estados.Add(estado);
            }
            return estados;
        }

        public List<Estado> ObterTodosParaSelect()
        {
            List<Estado> estados = new List<Estado>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = "SELECT id, nome FROM estados ORDER BY nome";
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            foreach (DataRow line in table.Rows)
            {
                Estado estado = new Estado()
                {
                    Id = Convert.ToInt32(line[0].ToString()),
                    Nome = line[1].ToString(),
                };
                estados.Add(estado);
            }
            return estados;
        }

        public List<Estado> ObterTodosParaJSON(string start, string length)
        {
            List<Estado> estados = new List<Estado>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT id, nome FROM estados ORDER BY nome OFFSET " +
                start + "ROWS FETCH NEXT" + length + "ROWS ONLY";
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            foreach(DataRow line in table.Rows)
            {
                Estado estado = new Estado()
                {
                    Id = Convert.ToInt32(line[0].ToString()),
                    Nome = line[2].ToString()

                };
                estados.Add(estado);
            }
            return estados;
        }
        public int Cadastrar(Estado estado)
        {
            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = @"INSERT INTO estados (nome)
            OUTPUT INSERTED.ID 
            VALUES(@NOME)";

            command.Parameters.AddWithValue("@NOME", estado.Nome);
            int id = Convert.ToInt32(command.ExecuteScalar().ToString());
            return id;
        }

        public bool Alterar(Estado estado)
        {
            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = @"UPDATE estados SET nome = @Nome WHERE id = @ID";

            command.Parameters.AddWithValue("@NOME", estado.Nome);
            command.Parameters.AddWithValue("@ID", estado.Id);
            return command.ExecuteNonQuery() == 1;
        }

        public bool Excluir(int id)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"DELETE FROM estado WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            return command.ExecuteNonQuery() == 1;

        }


        public Estado ObterPeloId(int id)
        {
            Estado estado = null;

            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT estados.nome FROM estados 
            JOIN estados ON (estados.id_pais = estados.id
            WHERE id =@ID)";
            command.Parameters.AddWithValue("@ID", id);

            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());

            if (table.Rows.Count == 1)
            {
                estado = new Estado();
                estado.Id = id;
                estado.Nome = table.Rows[0][0].ToString();
            }
            return estado;
        }
    }
}
