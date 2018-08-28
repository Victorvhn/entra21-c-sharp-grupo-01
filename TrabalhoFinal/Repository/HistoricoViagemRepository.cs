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

            command.CommandText = "SELECT h.id, p.id, p.id_pacote, h.data_, h.nome FROM pacotes p JOIN historico_de_viagens h ON (p.id_pacote = h.id)";
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

        

        public List<HistoricoViagem> ObterTodosParaJSON(string start, string length)
        {
            List<HistoricoViagem> historicoViagens = new List<HistoricoViagem>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT id, id_pacote, data_   FROM guias ORDER BY nome OFFSET " +
                start + " ROWS FETCH NEXT "
                + length + " ROWS ONLY  ";
            DataTable tabela = new DataTable();
            tabela.Load(command.ExecuteReader());
            foreach (DataRow linha in tabela.Rows)
            {
                HistoricoViagem historicoViagem = new HistoricoViagem()
                {
                    Id = Convert.ToInt32(linha[0].ToString()),
                    IdPacote = Convert.ToInt32(linha[1].ToString()),
                    Data = Convert.ToDateTime(linha[2].ToString().Replace("/", "-"))
                   

                };
                historicoViagens.Add(historicoViagem);
            }
            return historicoViagens;
        }


        public int Cadastrar(HistoricoViagem historicoViagem)
        {
            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = @"INSERT INTO historico_de_viagens (data_, id_pacote)
            OUTPUT INSERTED.ID VALUES (@DATA_, @ID_PACOTE)";
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

        public bool Excluir(int id)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"DELETE FROM historico_de_viagens WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            return command.ExecuteNonQuery() == 1;

        }

        public HistoricoViagem ObterPeloId(int id)
        {
            HistoricoViagem historicoViagem = null;

            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = @"SELECT p.id, p.id_pacote, h.data_, h.nome FROM pacotes p 
            JOIN historico_de_viagens h ON (p.id_pacote = h.id)
            WHERE id =@ID";
            command.Parameters.AddWithValue("@ID", id);

            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());

            if (table.Rows.Count == 1)
            {
                historicoViagem = new HistoricoViagem();
                historicoViagem.Id = id;
                historicoViagem.Data = Convert.ToDateTime(table.Rows[0][0].ToString());
                historicoViagem.IdPacote = Convert.ToInt32(table.Rows[0][1].ToString());
                historicoViagem.Pacote = new Pacote();
                historicoViagem.Pacote.Nome = table.Rows[0][2].ToString();
                historicoViagem.Pacote.Id = Convert.ToInt32(table.Rows[0][3].ToString());
            }
            return historicoViagem;
        }
    }
}
