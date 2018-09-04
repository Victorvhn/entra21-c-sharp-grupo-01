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
    public class CidadeRepository
    {
        public List<Cidade> ObterTodos()
        {
            List<Cidade> cidades = new List<Cidade>();
            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = @"SELECT id, nome, id_estado FROM cidades";
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            foreach (DataRow line in table.Rows)
            {
                Cidade cidade = new Cidade()
                {
                    Id = Convert.ToInt32(line[0].ToString()),
                    IdEstado = Convert.ToInt32(line[1].ToString()),
                    Nome = line[2].ToString()
                };
                cidades.Add(cidade);
            }
            return cidades;
        }

        public List<Cidade> ObterTodosParaJSON(string start, string length)
        {
            List<Cidade> cidades = new List<Cidade>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT c.id, e.nome, c.nome, e.id FROM cidades c INNER JOIN estados e ON (e.id = c.id_estado) WHERE c.ativo = 1 ORDER BY c.nome 
                                    OFFSET " + start + " ROWS FETCH NEXT " + length + " ROWS ONLY ";

            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());

            foreach (DataRow line in table.Rows)
            {
                Cidade cidade = new Cidade()
                {
                    Id = Convert.ToInt32(line[0].ToString()),
                    IdEstado = Convert.ToInt32(line[3].ToString()),
                    Nome = line[2].ToString(),
                    Estado = new Estado()
                    {
                        Id = Convert.ToInt32(line[3].ToString()),
                        Nome = line[1].ToString()
                    }                
                };
                cidades.Add(cidade);
            }
            return cidades;
        }

        public List<Cidade> ObterTodosParaSelect()
        {
            List<Cidade> cidades = new List<Cidade>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = "SELECT id, nome, id_estado FROM cidades";
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            foreach (DataRow line in table.Rows)
            {
                Cidade cidade = new Cidade()
                {
                    Id = Convert.ToInt32(line[0].ToString()),
                    IdEstado = Convert.ToInt32(line[1].ToString()),
                    Nome = line[2].ToString()
                };
                cidades.Add(cidade);
            }
            return cidades;
        }

        public int Cadastrar(Cidade cidade)
        {
            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = @"INSERT INTO cidades (nome, id_estado) OUTPUT INSERTED.ID VALUES(@NOME, @ID_ESTADO)";
            command.Parameters.AddWithValue("@NOME", cidade.Nome);
            command.Parameters.AddWithValue("@ID_ESTADO", cidade.IdEstado);

            int id = Convert.ToInt32(command.ExecuteScalar().ToString());
            return id;
        }

        public bool Alterar(Cidade cidade)
        {
            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = @"UPDATE  cidades SET nome = @NOME, id_estado = @ID_ESTADO WHERE id = @ID";

            command.Parameters.AddWithValue("@NOME", cidade.Nome);
            command.Parameters.AddWithValue("@ID_ESTADO", cidade.IdEstado);
            command.Parameters.AddWithValue("@ID", cidade.Id);
            return command.ExecuteNonQuery() == 1;
        }

        public bool Excluir(int id)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"UPDATE cidades SET ativo = 0 WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            return command.ExecuteNonQuery() == 1;

        }

        public Cidade ObterPeloId(int id)
        {
            Cidade cidade = null;

            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = @"SELECT cidades.id, cidades.nome, cidades.id_estado, estados.nome FROM cidades 
JOIN estados ON (cidades.id_estado = estados.id) WHERE cidades.id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());

            if (table.Rows.Count == 1)
            {
                cidade = new Cidade();
                cidade.Id = id;
                cidade.Nome = table.Rows[0][1].ToString();
                cidade.IdEstado = Convert.ToInt32(table.Rows[0][2].ToString());
                cidade.Estado = new Estado();
                cidade.Estado.Nome = table.Rows[0][3].ToString();
                cidade.Estado.Id = Convert.ToInt32(table.Rows[0][2].ToString());

            }
            return cidade;
        }
    }
}
