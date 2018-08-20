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
    public class PacoteTuristicoRepository
    {
        public List<PontoTuristico> ObterTodosPontosTuristicosPeloIdPacote(int idPacote)
        {
            List<PontoTuristico> pontosTuristicos = new List<PontoTuristico>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT pt.id, pt.nome FROM pacotes_pontos_turisticos ppt 
                                JOIN pontos_turisticos pt ON(pt.id = ppt.id_ponto_turistico)
                                WHERE ppt.id_pacote = @ID_PACOTE";
            DataTable tabela = new DataTable();
            tabela.Load(command.ExecuteReader());
            foreach (DataRow linha in tabela.Rows)
            {
                PontoTuristico pacotePontoturistico = new PontoTuristico()
                {
                    Id = Convert.ToInt32(linha[0].ToString()),
                    Nome = linha[1].ToString()
                };
                pontosTuristicos.Add(pacotePontoturistico);
            }
            return pontosTuristicos;
        }

        public int Cadastro(PacotePontoTuristico pacotePontoTuristico)
        {
            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = @"INSERT INTO pacotes_pontos_turisticos (id_ponto_turistico, id_pacote) OUTPUT INSERTED.ID VALUES (@ID_PONTO_TURISTICO, @ID_PACOTE)";
            command.Parameters.AddWithValue("@ID_PONTO_TURISTICO", pacotePontoTuristico.IdPontoTuristico);
            command.Parameters.AddWithValue("@ID_PACOTE", pacotePontoTuristico.IdPacote);

            int id = Convert.ToInt32(command.ExecuteScalar().ToString());
            return id;

        }

        public bool Alterar(PacotePontoTuristico pacotePontoTuristico)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"UPDATE pacotes_pontos_turisticos SET id_ponto_turistico = @ID_PONTO_TURISTICO, id_pacote = @ID_PACOTE WHERE id = @ID  ";

            command.Parameters.AddWithValue("@ID_PONTO_TURISTICO", pacotePontoTuristico.IdPontoTuristico);
            command.Parameters.AddWithValue("@ID_PACOTE", pacotePontoTuristico.IdPacote);
            command.Parameters.AddWithValue("@ID", pacotePontoTuristico.Id);
            return command.ExecuteNonQuery() == 1;
        }

        public bool Excluir(int id)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"DELETE FROM pacotes_pontos_turisticos WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            return command.ExecuteNonQuery() == 1;

        }

        public PacotePontoTuristico ObterPeloId(int id)
        {
            PacotePontoTuristico pacoteTuristico = null;
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT pontos_turisticos.nome, pontos_turisticos.nome, pacotes.nome, pacotes.id, id_ponto_turistico,id_pacote FROM pacotes_pontos_turisticos JOIN pontos_turisticos on pacotes_pontos_turisticos(pacotes_pontos_turisticos.id_ponto_turistico = pontos_turisticos.id)
            JOIN pacotes_pontos_turisticos ON(pacotes_pontos_turisticos.id_pacote = pacotes.id WHERE id = @ID)"; 
            command.Parameters.AddWithValue("ID", id);
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            if (table.Rows.Count == 1)
            {
                pacoteTuristico = new PacotePontoTuristico();
                pacoteTuristico.Id = id;
                pacoteTuristico.IdPontoTuristico = Convert.ToInt32(table.Rows[0][0]);
                pacoteTuristico.IdPacote = Convert.ToInt32(table.Rows[0][1]);
            }
            return pacoteTuristico;
        }
    }
}