﻿using Model;
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
        public List<PacotePontoTuristico> ObterTodos()
        {
            List<PacotePontoTuristico> pacotePontoTuristico = new List<PacotePontoTuristico>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = "SELECT id, id_ponto_turistico,id_pacote FROM pacotes_pontos_turisticos";
            DataTable tabela = new DataTable();
            tabela.Load(command.ExecuteReader());
            foreach (DataRow linha in tabela.Rows)
            {
                PacotePontoTuristico pacotePontoturistico = new PacotePontoTuristico()
                {
                    Id = Convert.ToInt32(linha[0].ToString()),
                    IdPontoTuristico = Convert.ToInt32(linha[1].ToString()),
                    IdPacote = Convert.ToInt32(linha[2].ToString())
                };
                pacotePontoTuristico.Add(pacotePontoturistico);
            }
            return pacotePontoTuristico;
        }

        public int Cadastro(PacotePontoTuristico poacotePontoTuristico)
        {
            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = @"INSERT INTO pacotes_pontos_turisticos (id_ponto_turistico, id_pacote) OUTPUT INSERTED.ID VALUES (@ID_PONTO_TURISTICO, @ID_PACOTE)";
            command.Parameters.AddWithValue("@ID_PONTO_TURISTICO", poacotePontoTuristico.IdPontoTuristico);
            command.Parameters.AddWithValue("@ID_PACOTE", poacotePontoTuristico.IdPacote);


            int id = Convert.ToInt32(command.ExecuteScalar().ToString());
            return id;

        }

        public bool Alterar(PacotePontoTuristico poacotePontoTuristico)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"UPDATE pacotes_pontos_turisticos SET id_ponto_turistico = @ID_PONTO_TURISTICO, id_pacote = @ID_PACOTE WHERE id = @ID  ";

            command.Parameters.AddWithValue("@ID_PONTO_TURISTICO", poacotePontoTuristico.IdPontoTuristico);
            command.Parameters.AddWithValue("@ID_PACOTE", poacotePontoTuristico.IdPacote);
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
            command.CommandText = @"SELECT id_ponto_turistico,id_pacote FROM pacotes_pontos_turisticos WHERE id = @ID";
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