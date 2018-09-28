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
     public class PacotePontosTuristicosRepository
    {
        public List<PacotePontoTuristico> ObterTodosPorJSON(string start, string length)
        {
            List<PacotePontoTuristico> pacotePontoTuristicos = new List<PacotePontoTuristico>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT ppt.id, p.id,p.nome, pt.id, pt.nome
            FROM pacotes_pontos_turisticos ppt
            INNER JOIN pacotes p ON (p.id = ppt.id_pacote)
            INNER JOIN pontos_turisticos pt ON (pt.id = ppt.id_ponto_turistico)
            WHERE ppt.ativo = 1
            ORDER BY p.nome OFFSET " + start + " ROWS FETCH NEXT " + length + " ROWS ONLY ";

            DataTable tabela = new DataTable();
            tabela.Load(command.ExecuteReader());
            foreach (DataRow linha in tabela.Rows)
            {
                PacotePontoTuristico pacotePontoTuristico = new PacotePontoTuristico();
                pacotePontoTuristico.Id = Convert.ToInt32(linha[0].ToString());
                pacotePontoTuristico.Pacote = new Pacote();
                pacotePontoTuristico.Pacote.Id = Convert.ToInt32(linha[1].ToString());
                pacotePontoTuristico.Pacote.Nome = linha[2].ToString();
                pacotePontoTuristico.PontoTuristico = new PontoTuristico();
                pacotePontoTuristico.PontoTuristico.Id = Convert.ToInt32(linha[3].ToString());
                pacotePontoTuristico.PontoTuristico.Nome = linha[4].ToString();
                pacotePontoTuristicos.Add(pacotePontoTuristico);
            }
            return pacotePontoTuristicos;
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
            command.CommandText = @"UPDATE pacotes_pontos_turisticos SET ativo = 0 WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            return command.ExecuteNonQuery() == 1;

        }

        public PacotePontoTuristico ObterPeloId(int id)
        {
            PacotePontoTuristico pacotePontoTuristico = null;
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT ppt.id, p.id, p.nome, pt.id, pt.nome
            FROM pacotes_pontos_turisticos ppt
            JOIN pacotes p ON (p.id = ppt.id_pacote)
            JOIN pontos_turisticos pt ON (pt.id = ppt.id ) WHERE ppt.id = @ID"; 
            command.Parameters.AddWithValue("@ID", id);
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            if (table.Rows.Count == 1)
            {
                pacotePontoTuristico = new PacotePontoTuristico();
                pacotePontoTuristico.Id = Convert.ToInt32(table.Rows[0][0].ToString());
                pacotePontoTuristico.Pacote = new Pacote();
                pacotePontoTuristico.Pacote.Id = Convert.ToInt32(table.Rows[0][1].ToString());
                pacotePontoTuristico.Pacote.Nome = table.Rows[0][2].ToString();
                pacotePontoTuristico.PontoTuristico = new PontoTuristico();
                pacotePontoTuristico.PontoTuristico.Id = Convert.ToInt32(table.Rows[0][3].ToString());
                pacotePontoTuristico.PontoTuristico.Nome = table.Rows[0][4].ToString();
            }
            return pacotePontoTuristico;
        }


        public List<PacotePontoTuristico> ObterTodosParaSelect()
        {
            List<PacotePontoTuristico> pacotePontoTuristicos = new List<PacotePontoTuristico>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = "SELECT id, id_pacote, id_ponto_turistico FROM pacotes_pontos_turisticos WHERE ativo = 1";
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            foreach (DataRow line in table.Rows)
            {
                PacotePontoTuristico pacotePontoTuristico = new PacotePontoTuristico()
                {
                    Id = Convert.ToInt32(line[0].ToString()),
                    IdPacote = Convert.ToInt32(line[1].ToString()),
                    IdPontoTuristico = Convert.ToInt32(line[2].ToString())
                };
                pacotePontoTuristicos.Add(pacotePontoTuristico);
            }
            return pacotePontoTuristicos;
        } 
        

        

    }
}