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
    public class IdiomaRepository
    {

        public List<Idioma> ObterTodos()
        {
            List<Idioma> idiomas = new List<Idioma>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = "SELECT id, nome FROM idiomas";
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            foreach (DataRow line in table.Rows)
            {
                Idioma idioma = new Idioma()
                {
                    Id = Convert.ToInt32(line[0].ToString()),
                    Nome = line[2].ToString(),
                };
                idiomas.Add(idioma);
            }
            return idiomas;
        }

        public List<Idioma> ObterTodosParaJSON(string start, string length, string search, string orderColumn, string orderDir)
        {
            List<Idioma> idiomas = new List<Idioma>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT id, nome FROM idiomas 
WHERE ativo = 1 AND ((nome LIKE @SEARCH) OR (id LIKE @SEARCH))
ORDER BY " + orderColumn + " " + orderDir +
" OFFSET " + start + " ROWS FETCH NEXT " + length + " ROWS ONLY ";

            command.Parameters.AddWithValue("@SEARCH", search);
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());

            foreach (DataRow line in table.Rows)
            {
                Idioma idioma = new Idioma()
                {
                    Id = Convert.ToInt32(line[0].ToString()),
                    Nome = line[1].ToString(),

                };
                idiomas.Add(idioma);
            }
            return idiomas;
        }

        public List<Idioma> ObterTodosParaSelect()
        {
            List<Idioma> idiomas = new List<Idioma>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = "SELECT id, nome FROM idiomas";
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            foreach (DataRow line in table.Rows)
            {
                Idioma idioma = new Idioma()
                {
                    Id = Convert.ToInt32(line["id"].ToString()),
                    Nome = line["nome"].ToString()
                };
                idiomas.Add(idioma);
            }
            return idiomas;
        }

        public int ContabilizarEstadosFiltradas(string search)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT COUNT(id) FROM idiomas WHERE ativo = 1 AND ((nome LIKE @SEARCH) OR (id LIKE @SEARCH))";
            command.Parameters.AddWithValue("@SEARCH", search);
            return Convert.ToInt32(command.ExecuteScalar().ToString());
        }

        public int ContabilizarEstados()
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT COUNT(id) FROM idiomas WHERE ativo = 1";           
            return Convert.ToInt32(command.ExecuteScalar().ToString());
        }

        public int Cadastrar(Idioma idioma)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"INSERT INTO idiomas (nome) OUTPUT INSERTED.ID VALUES (@NOME)";
            command.Parameters.AddWithValue("@NOME", idioma.Nome);
            int id = Convert.ToInt32(command.ExecuteScalar().ToString());
            return id;
        }

        public bool Alterar(Idioma idioma)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"UPDATE idiomas SET nome = @NOME WHERE id = @ID";

            command.Parameters.AddWithValue("@NOME", idioma.Nome);
            command.Parameters.AddWithValue("@ID", idioma.Id);
            return command.ExecuteNonQuery() == 1;
        }

        public bool Excluir(int id)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"DELETE FROM idiomas WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            return command.ExecuteNonQuery() == 1;
        }

        public Idioma ObterPeloId(int id)
        {
            Idioma idioma = null;
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT nome FROM idiomas WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            if (table.Rows.Count == 1)
            {
                idioma = new Idioma();
                idioma.Id = id;
                idioma.Nome = table.Rows[0][0].ToString();
            }
            return idioma;
        }
    }
}
