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
        public List<Viagem> ObterTodosPorJSON(string start, string length, string search, string orderColumn, string orderDir)
        {
            string whereSearch = "";

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = $"'%{search}%'";
                whereSearch = $" AND ((v.id LIKE {search}) OR (p.nome LIKE {search}) OR (g.nome LIKE {search}))";
            }

            List<Viagem> viagens = new List<Viagem>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = $@"SELECT v.id, p.id, p.nome, g.id, g.nome, v.data_horario_saida, v.data_horario_volta, v.id_pacote, v.id_guia
            FROM viagens v 
            INNER JOIN pacotes p ON (p.id = v.id_pacote)
            INNER JOIN guias g ON (g.id = v.id_guia)
            WHERE v.ativo = 1 {whereSearch}
            ORDER BY {orderColumn} {orderDir} OFFSET {start} ROWS FETCH NEXT {length} ROWS ONLY ";

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

        public List<Viagem> ObterTodosParaSelect()
        {
            List<Viagem> viagens = new List<Viagem>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = "SELECT id, data_horario_saida, data_horario_volta, id_guia, id_pacote FROM viagens WHERE ativo = 1";
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            foreach (DataRow line in table.Rows)
            {
                Viagem viagem = new Viagem()
                {
                    Id = Convert.ToInt32(line[0].ToString()),
                    DataHorarioSaida = Convert.ToDateTime(line[1].ToString()),
                    DataHorarioVolta = Convert.ToDateTime(line[2].ToString()),
                    IdGuia = Convert.ToInt32(line[3].ToString()),
                    IdPacote = Convert.ToInt32(line[4].ToString())
                };
                viagens.Add(viagem);
            }
            return viagens;
        }

        public int Cadastrar(Viagem viagem)
        {
            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = @"INSERT INTO viagens (id_pacote, id_guia, data_horario_saida, data_horario_volta)
            OUTPUT INSERTED.ID VALUES (@ID_PACOTES, @ID_GUIA, @DATA_HORARIO_SAIDA, @DATA_HORARIO_VOLTA)";
            command.Parameters.AddWithValue("@ID_PACOTES", viagem.IdPacote);
            command.Parameters.AddWithValue("@ID_GUIA", viagem.IdGuia);
            command.Parameters.AddWithValue("@DATA_HORARIO_SAIDA", viagem.DataHorarioSaida);
            command.Parameters.AddWithValue("@DATA_HORARIO_VOLTA", viagem.DataHorarioVolta);
            int id = Convert.ToInt32(command.ExecuteScalar().ToString());
            return id;
        }

         public int ContabilizarViagensFiltradas(string search)
         {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT COUNT(v.id)
            FROM viagens v
            INNER JOIN pacotes p ON (p.id = v.id_pacote)
            INNER JOIN guias g ON (g.id = v.id_guia)
            WHERE v.ativo = 1 AND ((v.id LIKE @SEARCH) OR (p.nome LIKE @SEARCH) OR (g.nome LIKE @SEARCH) OR (v.data_horario_saida LIKE @SEARCH) OR (v.data_horario_volta LIKE @SEARCH))";
            command.Parameters.AddWithValue("@SEARCH", search);
            return Convert.ToInt32(command.ExecuteScalar().ToString());
         }

         public int ContabilizarViagens()
         {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT COUNT(id) FROM viagens WHERE ativo = 1";
            return Convert.ToInt32(command.ExecuteScalar().ToString());

        }

        public bool Alterar(Viagem viagens)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = "UPDATE viagens SET data_horario_saida = @DATA_HORARIO_SAIDA, data_horario_volta = @DATA_HORARIO_VOLTA, id_guia = @ID_GUIA, id_pacote = @ID_PACOTE WHERE id = @ID";
            command.Parameters.AddWithValue("@DATA_HORARIO_SAIDA", viagens.DataHorarioSaida);
            command.Parameters.AddWithValue("@DATA_HORARIO_VOLTA", viagens.DataHorarioVolta);
            command.Parameters.AddWithValue("@ID_GUIA", viagens.IdGuia);
            command.Parameters.AddWithValue("@ID_PACOTE", viagens.IdPacote);
            command.Parameters.AddWithValue("@ID", viagens.Id);

            return command.ExecuteNonQuery() == 1;
        }

        public bool Excluir(int id)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"UPDATE viagens SET ativo = 0 WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            return command.ExecuteNonQuery() == 1;
        }

        public Viagem ObterPeloId(int id)
        {
            Viagem viagem = null;

            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = @"SELECT v.id_pacote, v.id_guia, v.data_horario_saida, v.data_horario_volta 
FROM viagens v 
INNER JOIN pacotes p ON (p.id = v.id_pacote)
INNER JOIN guias g ON (g.id = v.id_guia) WHERE v.id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            if (table.Rows.Count == 1)
            {
                viagem = new Viagem();
                viagem.Id = id;
                viagem.DataHorarioSaida = Convert.ToDateTime(table.Rows[0][2].ToString());
                viagem.DataHorarioVolta = Convert.ToDateTime(table.Rows[0][3].ToString());
                viagem.IdGuia = Convert.ToInt32(table.Rows[0][1].ToString());
                viagem.IdPacote = Convert.ToInt32(table.Rows[0][0].ToString());

            }
            return viagem;
        }
    }
}
