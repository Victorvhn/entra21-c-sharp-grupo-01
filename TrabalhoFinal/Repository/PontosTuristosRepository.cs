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
        public List<Pacote> ObterTodosPacotesPeloIdPacote(int idPacote)
        {
            List<Pacote> pacotes = new List<Pacote>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT pt.id, p.id, p.nome, p.valor, id_endereco, pt.nome,  FROM pontos_turisticos pt JOIN
                                  pacotes p ON(pt.id = p.id_pacote) WHERE pt.id_pacote = @ID_PACOTE";

            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            foreach (DataRow line in table.Rows)
            {
                Pacote pacote = new Pacote()
                {
                    Id = Convert.ToInt32(line["p.id"].ToString()),
                    Nome = line["p.nome"].ToString()

                };
                pacotes.Add(pacote);

            }
            return pacotes;
        }

        public List<PontoTuristico> ObterTodosParaSelect()
        {
            List<PontoTuristico> pontosTuristicos = new List<PontoTuristico>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = "SELECT id, nome FROM pontos_turisticos";
            DataTable tabela = new DataTable();
            tabela.Load(command.ExecuteReader());
            foreach(DataRow linha in tabela.Rows)
            {
                PontoTuristico pontoTuristico = new PontoTuristico()
                {
                    Id = Convert.ToInt32(linha[0].ToString()),
                    Nome = linha[1].ToString()
                };
                pontosTuristicos.Add(pontoTuristico);
            }
            return pontosTuristicos;
        }
        public int Cadastrar(PontoTuristico pontoturistico)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = "INSERT INTO pontos_turisticos (id_endereco,nome)OUTPUT INSERTED.ID VALUES(@ID_ENDERECO,@NOME)";
            command.Parameters.AddWithValue("@NOME", pontoturistico.Nome);
            command.Parameters.AddWithValue("@ID_ENDERECO", pontoturistico.IdEndereco);
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
                pontoTuristico.Pacotes = new PacotePontosTuristicosRepository().ObterTodosPontosTuristicosPeloIdPacote(pontoTuristico.Id);


            }
            return pontoTuristico;
        }
        public bool Alterar(PontoTuristico pontoturisco)
        {
            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = "UPDATE pontos_turisticos SET id_endereco = @ID_ENDERECO,nome = @NOME WHERE id = @ID";
            command.Parameters.AddWithValue("@iD_ENDERECO",pontoturisco.IdEndereco);
            command.Parameters.AddWithValue("@NOME", pontoturisco.Nome);
            command.Parameters.AddWithValue("@ID", pontoturisco.Id);
            return command.ExecuteNonQuery() == 1;
        }
        public List<PontoTuristico> ObterTodosParaJSON(string start, string length)
        {
            List<PontoTuristico> pontosturisticos = new List<PontoTuristico>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = "SELECT id,id_endereco,nome FROM pontos_turisticos ORDER BY nome OFFSET " + start + " ROWS FETCH NEXT " + length + " ROWS ONLY ";
            DataTable tabela = new DataTable();
            tabela.Load(command.ExecuteReader());
            foreach (DataRow linha in tabela.Rows)
            {
                PontoTuristico pontoturistico = new PontoTuristico()
                {
                    Id = Convert.ToInt32(linha[0].ToString()),
                    IdEndereco = Convert.ToInt32(linha[1].ToString()),
                    Nome = linha[2].ToString()
                };
                pontosturisticos.Add(pontoturistico);
            }
            return pontosturisticos;
        }

    }
}
