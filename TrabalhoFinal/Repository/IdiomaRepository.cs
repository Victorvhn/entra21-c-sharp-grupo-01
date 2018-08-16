using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Principal;
using Principal.Database;
using System.Data;

namespace Principal.Repository
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

        public Idioma ObterPeloId(int id)
        {
            Idioma idioma = null;

            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = @"SELECT nome, id_guia FROM idiomas WHERE id = @ID";
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
    }
}