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
    public class PaisRepository
    {
        List<Pais> ObterTodos()
        {
            List<Pais> paises = new List<Pais>();
            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = "SELECT id, id_continente, nome FROM paises";
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            foreach (DataRow line in table.Rows)
            {
                Pais pais = new Pais()
                {
                    Id = Convert.ToInt32(line[0].ToString()),
                    Nome = line[1].ToString(),
                    IdContinente = Convert.ToInt32(line[2].ToString())

                };
                paises.Add(pais);
            }
            return paises;
        }

        public int Cadastrar(Pais pais)
        {
            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = @"INSERT INTO paises (nome) OUTPUT INSERTED.ID VALUES (@NOME)";
            command.Parameters.AddWithValue("@NOME", pais.Nome);
            int id = Convert.ToInt32(command.ExecuteScalar().ToString());
            return id;

        }

        public bool Alterar(Pais pais)
        {
            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = @"UPDATE paises SET nome = @NOME WHERE id = @ID";
            command.Parameters.AddWithValue("@NOME", pais.Nome);
            command.Parameters.AddWithValue("ID", pais.Id);
            return command.ExecuteNonQuery() == 1;
        }

        public Pais ObterPeloId(int id)
        {
            Pais pais = null;

            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = @"SELECT paises.nome, continentes.nome, continentes.id FROM paises JOIN paises ON(paises.id_continente = continentes.id) WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);

            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());

            if (table.Rows.Count == 1)
            {
                pais.Id = id;
                pais.Nome = table.Rows[0][0].ToString();
                pais.IdContinente = Convert.ToInt32(table.Rows[0][1].ToString());
            }
            return pais;
        }
    }
}