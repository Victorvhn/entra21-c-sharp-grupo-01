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
    public class ViagensRepository
    {
        public List<Viagem> ObterTodosPorJSON(string start, string length)
        {
            List<Viagem> viagens = new List<Viagem>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT v.id, p.id, p.nome, g.id, g.nome, v.data_horario_saida, v.data_horario_volta, v.id_pacote, v.id_guia
FROM viagens v 
INNER JOIN pacotes p ON (p.id = v.id_pacote)
INNER JOIN guias g ON (g.id = v.id_guia)
WHERE v.ativo = 1
ORDER BY p.nome OFFSET " + start + " ROWS FETCH NEXT " + length + " ROWS ONLY ";

            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());

            foreach (DataRow line in table.Rows)
            {
                Viagem viagem = new Viagem();
                viagem.Id = Convert.ToInt32(line[0].ToString());
                viagem.Pacote = new Pacote();
                viagem.Pacote.Id = Convert.ToInt32(line[1].ToString());
                viagem.Pacote.Nome = line[2].ToString();
                viagem.Guia = new Guia();
                viagem.Guia.Id = Convert.ToInt32(line[3].ToString());
                viagem.Guia.Nome = line[4].ToString();
                viagem.DataHorarioSaida = Convert.ToDateTime(line[5].ToString());
                viagem.DataHorarioVolta = Convert.ToDateTime(line[6].ToString());
                viagem.IdPacote = Convert.ToInt32(line[7].ToString());
                viagem.IdGuia = Convert.ToInt32(line[8].ToString());
                viagens.Add(viagem);
            }
            return viagens;
        }

        public int Cadastrar(Viagem viagem)
        {
            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = "INSERT INTO viagens (data,data_horario_saida,data_horario_volta,id_guia,id_pacote) OUTPUT INSERTED.ID VALUES(@DATA,@DATA_HORARIO_SAIDA,@DATA_HORARIO_VOLTA,@ID_GUIA,@ID_PACOTES)";
            command.Parameters.AddWithValue("@DATA", viagem.Data);
            command.Parameters.AddWithValue("@DATA_HORARIO_SAIDA", viagem.DataHorarioSaida);
            command.Parameters.AddWithValue("@DATA_HORARIO_VOLTA", viagem.DataHorarioVolta);
            command.Parameters.AddWithValue("@ID_GUIA", viagem.IdGuia);
            command.Parameters.AddWithValue("@ID_PACOTES", viagem.IdPacote);
            int id = Convert.ToInt32(command.ExecuteScalar().ToString());
            return id;
        }

        public bool Alterar(Viagem viagens)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = "UPDATE viagens SET data = @DATA, data_horario_saida = @DATA_HORARIO_SAIDA, data_horario_volta = @DATA_HORARIO_VOLTA,id_guia = @ID_GUIA,id_pacotes = @ID_PACOTES WHERE id = @ID";
            command.Parameters.AddWithValue("@DATA", viagens.Data);
            command.Parameters.AddWithValue("@DATA_HORARIO_SAIDA", viagens.DataHorarioSaida);
            command.Parameters.AddWithValue("@DATA_HORARIO_VOLTA", viagens.DataHorarioVolta);
            command.Parameters.AddWithValue("@ID_GUIA", viagens.IdGuia);
            command.Parameters.AddWithValue("@ID_PACOTES", viagens.IdPacote);
            command.Parameters.AddWithValue("@ID", viagens.Id);

            return command.ExecuteNonQuery() == 1;
        }

        public bool Excluir(int id)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"DELETE FROM viagem WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            return command.ExecuteNonQuery() == 1;
        }

        public Viagem ObterPeloId(int id)
        {
            Viagem viagem = null;

            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = @"SELECT guias.nome, pacotes.nome, pacotes.id, guias.id, data,data_horario_saida,data_horario_volta FROM viagens JOIN pacotes ON viagens(viagens.id_pacote = pacotes.id)
            JOIN guias ON viagens(viagens.id_guia = guias.id) WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            if (table.Rows.Count == 1)
            {
                viagem = new Viagem();
                viagem.Id = id;
                viagem.Data = Convert.ToDateTime(table.Rows[0][0].ToString());
                viagem.DataHorarioSaida = Convert.ToDateTime(table.Rows[0][1].ToString());
                viagem.DataHorarioVolta = Convert.ToDateTime(table.Rows[0][2].ToString());
                viagem.IdGuia = Convert.ToInt32(table.Rows[0][3].ToString());
                viagem.IdPacote = Convert.ToInt32(table.Rows[0][4].ToString());

            }
            return viagem;
        }
    }
}
