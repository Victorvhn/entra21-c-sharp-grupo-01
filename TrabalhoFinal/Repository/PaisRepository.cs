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

            command.CommandText = "SELECT id, nome, id_continente FROM continentes";
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            foreach (DataRow line in table.Rows)
            {
                Pais pais = new Pais()
                {
                    Id = Convert.ToInt32(line[0].ToString()),
                    IdContinente = Convert.ToInt32(line[1].ToString()),
                    Nome = line[2].ToString(),
                };
                paises.Add(pais);
            }
            return paises;
        }

        public int Cadastrar(Pais pais)
        {
            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = @"INSERT INTO paises(nome, id_continente) OUTPUT INSERTED.ID VALUES (@NOME, @ID_CONTINENTE)";
            command.Parameters.AddWithValue("@NOME", pais.Nome);
            command.Parameters.AddWithValue("@ID_CONTINENTE", pais.IdContinente);

            int id = Convert.ToInt32(command.ExecuteScalar().ToString());
            return id;
        }

        public bool Alterar(Pais pais)
        {
            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = @"UPDATE paises SET nome = @NOME, id_continente = @ID_CONTINENTE WHERE id = @ID";

            command.Parameters.AddWithValue("@NOME", pais.Nome);
            command.Parameters.AddWithValue("@ID_CONTINENTE", pais.IdContinente);
            command.Parameters.AddWithValue("@ID", pais.Id);
            return command.ExecuteNonQuery() == 1;
        }

        public Pais ObterPeloId(int id)
        {
            Pais pais = null;

            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = @"SELECT paises.nome, id_continente, continentes.nome FROM paises JOIN continentes ON (paises.id_continente = guias.id) WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);

            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());

            if (table.Rows.Count == 1)
            {
                pais = new Pais();
                pais.Id = id;
                pais.Nome = table.Rows[0][0].ToString();
                pais.IdContinente = Convert.ToInt32(table.Rows[0][1].ToString());
                pais.Continente = new Continente();
                pais.Continente.Nome = table.Rows[0][2].ToString();
                pais.Continente.Id = Convert.ToInt32(table.Rows[0][3].ToString());
            }
            return pais;
        }
    }
}
