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
    class HistoricoViagenRepositorio
    {
        public List<HistoricoViagem> ObterTodos()
        {
            List<HistoricoViagem> historicoViagens = new List<HistoricoViagem>();
            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = "SELECT id, data, id_pacote FROM historico_de_viagens";
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            foreach (DataRow line in table.Rows)
            {
                HistoricoViagem historicoViagem = new HistoricoViagem()
                {
                    Id = Convert.ToInt32(line[0].ToString()),
                    IdPacote = Convert.ToInt32(line[1].ToString()),
                    Data = Convert.ToDateTime(line[2].ToString())
                };
                historicoViagens.Add(historicoViagem);
            }
            return historicoViagens;
        }

        public int Cadastrar(HistoricoViagem historicoViagem)
        {
            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = @"INSERT INTO historico_de_viagens (data, id_pacote)
            OUTPUT INSERTED.ID VALUES ()@DATA, @ID_PACOTE";
            command.Parameters.AddWithValue("@DATA", historicoViagem.Data);
            command.Parameters.AddWithValue("@ID_PACOTE", historicoViagem.IdPacote);

            int id = Convert.ToInt32(command.ExecuteScalar().ToString());
            return id;
        }

        public bool Alterar(HistoricoViagem historicoViagem)
        {
            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = @"UPDATE historico_de_viagens SET data = @DATA, id_pacote = @ID_PACOTE 
            WHERE id = @ID";
            command.Parameters.AddWithValue("@DATA", historicoViagem.Data);
            command.Parameters.AddWithValue("@ID_PACOTE", historicoViagem.IdPacote);
            command.Parameters.AddWithValue("@ID", historicoViagem.Id);
            return command.ExecuteNonQuery() == 1;
        }

        public HistoricoViagem ObterPeloId(int id)
        {
            HistoricoViagem historicoViagem = null;

            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = @"SELECT historico_de_viagens.Data, id_pacote, pacotes.nome FROM historico_de_viagens
            JOIN historico_de_viagens ON (historico_de_viagens.id_pacote = pacotes.id)
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
                historicoViagem.Pacote.Id = Convert.ToInt32(table.Rows.ToString());
            }
            return historicoViagem;
        }
    }
}
