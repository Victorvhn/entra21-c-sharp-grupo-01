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
        List<Viagem> ObterTodos()
        {
        List<Viagem> viagens = new List<Viagem>();
        SqlCommand command = new 
            Conexao().ObterConexao();

        command.CommandText = "SELECT id,data,pacote,id_pacote,guia,id_guia,data_horario_saida,data_horario_volta";
        DataTable table = new DataTable();
        table.Load(command.ExecuteReader());
        foreach (DataRow line in table.Rows)
        {
            Viagem viagem = new Viagem()
            {
                Id = Convert. ToInt32(line[0].ToString()),
                Data = Convert.ToDateTime(line[1].ToString()),
                IdPacote = Convert.ToInt32(line[3].ToString()),
                IdGuia = Convert.ToInt32(line[5].ToString()),
                DataHorarioSaida = Convert.ToDateTime(line[6].ToString()),
                DataHorarioVolta = Convert.ToDateTime(line[7].ToString())
            };
            viagens.Add(viagem);
        }
        return viagens;

        }

        public int Cadastrar(Viagem viagem)
        {
            SqlCommand command = new Conexao()
.ObterConexao();
            command.CommandText = "INSERT INTO viagens (data,data_horario_saida,data_horario_volta) OUTPUT INSERTED.ID VALUES(@DATA,@DATA_HORARIO_SAIDA,@DATA_HORARIO_VOLTA)";
            command.Parameters.AddWithValue("@DATA",viagem.Data);
            command.Parameters.AddWithValue("@DATA_HORARIO_SAIDA", viagem.DataHorarioSaida);
            command.Parameters.AddWithValue("@DATA_HORARIO_VOLTA", viagem.DataHorarioVolta);
            int id = Convert.ToInt32(command.ExecuteScalar().ToString());
            return id;
        }


        public bool Alterar(Viagem viagens)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = "UPDATE viagens SET data = @DATA, data_horario_saida = @DATA_HORARIO_SAIDA, data_horario_volta = @DATA_HORARIO_VOLTA WHERE id = @ID";
            command.Parameters.AddWithValue("@DATA", viagens.Data);
            command.Parameters.AddWithValue("@DATA_HORARIO_SAIDA", viagens.DataHorarioSaida);
            command.Parameters.AddWithValue("@DATA_HORARIO_VOLTA", viagens.DataHorarioVolta);
            command.Parameters.AddWithValue("@ID", viagens.Id);

            return command.ExecuteNonQuery() == 1;
        }

        public Viagem ObterPeloID(int id)
        {
            Viagem viagem = null;

            SqlCommand command = new Conexao().ObterConexao();
           
            command.CommandText = "SELECT data,data_horario_saida,data_horario_volta FROM viagens WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            if (table.Rows.Count == 1)
            {
                viagem.Id = id;
                viagem.Data =Convert.ToDateTime(table.Rows[0][0].ToString());
                viagem.DataHorarioSaida = Convert.ToDateTime(table.Rows[0][1].ToString());
                viagem.DataHorarioVolta = Convert.ToDateTime(table.Rows[0][2].ToString());

            }
            return viagem;
        }
    }
}
