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
        public List<Pais> ObterTodos()
        {
            List<Pais> paises = new List<Pais>();
            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = "SELECT p.id, p.id_continente, p.nome, c.nome FROM paises p JOIN continentes c ON (p.id_continente = c.id)";
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            foreach (DataRow line in table.Rows)
            {
                Pais pais = new Pais()
                {
                    Id = Convert.ToInt32(line[0].ToString()),
                    Nome = line[1].ToString(),
                    IdContinente = Convert.ToInt32(line[2].ToString()),
                    Continente = new Continente()
                    {
                        Id = Convert.ToInt32(line["p.id_continente"].ToString()),
                        Nome = line["c.nome"].ToString()
                    }
                };
                paises.Add(pais);
            }
            return paises;
        }

        public List<Pais> ObterTodosParaJSON(string start, string length)
        {
            List<Pais> paises = new List<Pais>();
            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = "SELECT p.id, p.id_continente, p.nome, c.nome FROM paises p JOIN continentes c ON (p.id_continente = c.id) ORDER BY p.nome OFFSET " + start +" ROWS FETCH NEXT " + length + " ROWS ONLY ";
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            foreach (DataRow line in table.Rows)
            {
                Pais pais = new Pais()
                {
                    Id = Convert.ToInt32(line["p.id"].ToString()),
                    Nome = line["p.nome"].ToString(),
                    IdContinente = Convert.ToInt32(line["p.id_continente"].ToString()),
                    Continente = new Continente()
                    {
                        Id = Convert.ToInt32(line["p.id_continente"].ToString()),
                        Nome = line["c.nome"].ToString()
                    }
                };
                paises.Add(pais);
            }
            return paises;
        }

        public int Cadastrar(Pais pais)
        {
            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = @"INSERT INTO paises (nome, id_continente) OUTPUT INSERTED.ID VALUES (@NOME, @ID_CONTINENTE)";
            command.Parameters.AddWithValue("@NOME", pais.Nome);
            command.Parameters.AddWithValue("ID_CONTINENTE", pais.IdContinente);
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

        public bool Excluir(int id)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"DELETE FROM paises WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            return command.ExecuteNonQuery() == 1;
        }

        public Pais ObterPeloId(int id)
        {
            Pais pais = null;

            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = @"SELECT p.id, p.id_continente, p.nome, c.nome FROM paises p JOIN continentes c ON (p.id_continente = c.id) WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);

            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());

            if (table.Rows.Count == 1)
            {
                pais = new Pais();
                pais.Id = id;
                pais.Nome = table.Rows[0][0].ToString();
                pais.IdContinente = Convert.ToInt32(table.Rows[0][1].ToString());               
            }
            return pais;
        }
    }
}