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
    public class PontosTuristicosRepository
    {
        public List<PontoTuristico> ObterTodos()
        {
            List<PontoTuristico> pontosTuristicos = new List<PontoTuristico>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = "SELECT id, id_endereco, nome FROM pontosturisticos";

            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            foreach (DataRow line in table.Rows)
            {
                PontoTuristico pontoturistico = new PontoTuristico()
                {
                    Id = Convert.ToInt32(line[0].ToString()),
                    IdEndereco = Convert.ToInt32(line[1].ToString()),
                    Nome = line[2].ToString()

                };
                pontosTuristicos.Add(pontoturistico);

            }
            return pontosTuristicos;
        }
        public int Cadastrar(PontoTuristico pontoturistico)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = "INSERT INTO (id_endereco,nome)OUTPUT INSERTED.ID VALUES(@ID_ENDERECO,@NOME)";
            command.Parameters.AddWithValue("@ID_ENDERECO", pontoturistico.IdEndereco);
            command.Parameters.AddWithValue("@NOME", pontoturistico.Nome);
            int id = Convert.ToInt32(command.ExecuteScalar().ToString());
            return id;
        }
        public bool Excluir(int id)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = "DELETE FROM pontos_turisticos WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            return command.ExecuteNonQuery() == 1;

        }
        public PontoTuristico ObterPeloId(int id)
        {
            PontoTuristico pontoTuristico = null;
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT pontos_turisticos.nome, id_endereco,enderecos.id FROM pontos_turisticos JOIN enderecos ON(pontos_turisticos.id_endereco = enderecos.id) WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);

            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            if (table.Rows.Count == 1)
            {
                pontoTuristico = new PontoTuristico();
                pontoTuristico.Id = id;
                pontoTuristico.IdEndereco = Convert.ToInt32(table.Rows[0][0].ToString());
                pontoTuristico.Endereco = new Endereco();
                pontoTuristico.Endereco.Id = Convert.ToInt32(table.Rows[0][1].ToString());

            }
            return pontoTuristico;
        }
        public bool Alterar(PontoTuristico pontoturisco)
        {
            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = "UPDATE pontos_turisticos SET id_endereco = @ID_ENDERECO WHERE id = @ID";
            command.Parameters.AddWithValue("@id_endereco",pontoturisco.IdEndereco);
            command.Parameters.AddWithValue("@ID", pontoturisco.Id);
            return command.ExecuteNonQuery() == 1;
        }

    }
}
