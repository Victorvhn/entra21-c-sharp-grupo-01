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
            command.CommandText = "SELECT id, nome, id_guia FROM idiomas";
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            foreach (DataRow line in table.Rows)
            {
                Idioma idioma = new Idioma()
                {
                    Id = Convert.ToInt32(line[0].ToString()),
                    IdGuia = Convert.ToInt32(line[1].ToString()),
                    Nome = line[2].ToString(),
                };
                idiomas.Add(idioma);
            }
            return idiomas;
        }

        public List<Idioma> ObterTodosParaSelect()
        {

        }
        public int Cadastrar(Idioma idioma)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"INSERT INTO idiomas(nome, id_guia) OUTPUT INSERTED.ID VALUES (@NOME, @ID_GUIA)";
            command.Parameters.AddWithValue("@NOME", idioma.Nome);
            command.Parameters.AddWithValue("@ID_GUIA", idioma.IdGuia);
            int id = Convert.ToInt32(command.ExecuteScalar().ToString());
            return id;
        }

        public bool Alterar(Idioma idioma)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"UPDATE idiomas SET nome = @NOME, id_guia = @ID_GUIA WHERE id = @ID";
            
            command.Parameters.AddWithValue("@NOME", idioma.Nome);
            command.Parameters.AddWithValue("@ID_GUIA", idioma.IdGuia);
            command.Parameters.AddWithValue("@ID", idioma.Id);
            return command.ExecuteNonQuery() == 1;
        }

        public bool Excluir (int id)
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
            command.CommandText = @"SELECT guia.nome, idioma.nome, id_guia FROM idiomas JOIN idiomas ON(idiomas.id_guia = guias.id) WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            if (table.Rows.Count == 1)
            {
                idioma = new Idioma();
                idioma.Id = id;
                idioma.Nome = table.Rows[0][0].ToString();
                idioma.IdGuia = Convert.ToInt32(table.Rows[0][1].ToString());
            }
            return idioma;
        }

        public List<Idioma> ObterTodosParaJSON(string start, string length)
        {
            List<Idioma> idiomas = new List<Idioma>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = "SELECT id, nome, FROM idiomas ORDER BY nome OFFSET" + start + " ROWS FETCH NEXT " + length + " ROWS ONLY ";

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
    }
}
