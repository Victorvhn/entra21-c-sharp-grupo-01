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
    public class HistoricoViagemRepository
    {
        public List<HistoricoViagem> ObterTodos()
        {
            List<HistoricoViagem> historicoViagens = new List<HistoricoViagem>();
            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = @"SELECT h.id, p.id, p.id_pacote, h.data_, h.nome FROM pacotes p 
                                    JOIN historico_de_viagens h ON (p.id_pacote = h.id)";
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            foreach (DataRow line in table.Rows)
            {
                HistoricoViagem historicoViagem = new HistoricoViagem()
                {
                    Id = Convert.ToInt32(line[0].ToString()),
                    IdPacote = Convert.ToInt32(line[2].ToString()),
                    Data = Convert.ToDateTime(line[3].ToString()),
                    Pacote = new Pacote()
                    {
                        Id = Convert.ToInt32(line[1].ToString()),
                        Nome = line[4].ToString()
                    }

                };
                historicoViagens.Add(historicoViagem);
            }
            return historicoViagens;
        }

        public List<HistoricoViagem> ObterTodosParaJSON(string start, string length, string search, string orderColumn, string orderDir)
        {
            List<HistoricoViagem> historicoViagens = new List<HistoricoViagem>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT hv.id, p.id, hv.id_pacote, hv.data_, p.nome 
            FROM historico_de_viagens hv
            INNER JOIN pacotes p ON (p.id = hv.id_pacote) 
            WHERE hv.ativo = 1 AND ((hv.id LIKE @SEARCH) OR (p.nome LIKE @SEARCH) OR (hv.data_ LIKE @SEARCH))
            ORDER BY " + orderColumn + " " + orderDir + 
            " OFFSET " + start + " ROWS FETCH NEXT " + length + " ROWS ONLY";

            command.Parameters.AddWithValue("@SEARCH", search);
            DataTable tabela = new DataTable();
            tabela.Load(command.ExecuteReader());
            foreach (DataRow linha in tabela.Rows)
            {
                HistoricoViagem historicoViagem = new HistoricoViagem();


                historicoViagem.Id = Convert.ToInt32(linha[0].ToString());
                historicoViagem.IdPacote = Convert.ToInt32(linha[1].ToString());
                historicoViagem.Data = Convert.ToDateTime(linha[3].ToString());
                historicoViagem.Pacote = new Pacote()
                {
                    Id = Convert.ToInt32(linha[1].ToString()),
                    Nome = linha[4].ToString()
                };


                historicoViagens.Add(historicoViagem);
            }
            return historicoViagens;
        }

        public int Cadastrar(HistoricoViagem historicoViagem)
        {
            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = @"INSERT INTO historico_de_viagens (data_, id_pacote)
            OUTPUT INSERTED.ID VALUES(@DATA_, @ID_PACOTE)";
            command.Parameters.AddWithValue("@DATA_", historicoViagem.Data);
            command.Parameters.AddWithValue("@ID_PACOTE", historicoViagem.IdPacote);

            int id = Convert.ToInt32(command.ExecuteScalar().ToString());
            return id;
        }

        public bool Alterar(HistoricoViagem historicoViagem)
        {
            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = @"UPDATE historico_de_viagens SET data_ = @DATA_, id_pacote = @ID_PACOTE 
            WHERE id = @ID";
            command.Parameters.AddWithValue("@DATA_", historicoViagem.Data);
            command.Parameters.AddWithValue("@ID_PACOTE", historicoViagem.IdPacote);
            command.Parameters.AddWithValue("@ID", historicoViagem.Id);
            return command.ExecuteNonQuery() == 1;
        }

        public int ContabilizarCidadesFiltradas(string search)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT COUNT(hv.id)
            FROM historico_de_viagens hv
            INNER JOIN pacotes p ON (p.id = hv.id_pacote) 
            WHERE hv.ativo = 1 AND ((hv.id LIKE @SEARCH) OR (p.nome LIKE @SEARCH) OR (hv.data_ LIKE @SEARCH))";
            command.Parameters.AddWithValue("@SEARCH", search);
            return Convert.ToInt32(command.ExecuteScalar().ToString());
        }

        public int ContabilizarCidades()
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT COUNT(id) FROM historico_de_viagens WHERE ativo = 1";
            return Convert.ToInt32(command.ExecuteScalar().ToString());
        }

        public bool Excluir(int id)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"UPDATE historico_de_viagens SET ativo = 0 WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            return command.ExecuteNonQuery() == 1;

        }

        public HistoricoViagem ObterPeloId(int id)
        {
            HistoricoViagem historicoViagem = null;

            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = @"SELECT p.id, h.id_pacote, h.data_, p.nome FROM historico_de_viagens h 
            JOIN pacotes p ON (h.id_pacote = p.id)
            WHERE h.id =@ID";
            command.Parameters.AddWithValue("@ID", id);

            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());

            if (table.Rows.Count == 1)
            {
                historicoViagem = new HistoricoViagem()
                {
                    Id = id,
                    Data = Convert.ToDateTime(table.Rows[0][2].ToString()),
                    IdPacote = Convert.ToInt32(table.Rows[0][1].ToString()),
                    Pacote = new Pacote()
                    {
                        Nome = table.Rows[0][3].ToString(),
                        Id = Convert.ToInt32(table.Rows[0][1].ToString()),
                    }
                };
            }
            return historicoViagem;
        }
    }
}
