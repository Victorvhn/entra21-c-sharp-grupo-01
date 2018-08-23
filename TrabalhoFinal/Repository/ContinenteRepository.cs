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
    public class ContinenteRepository
    {
        public List<Continente> ObterTodos()
        {
            List<Continente> continentes = new List<Continente>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = "SELECT id, nome FROM continentes";
            DataTable tabela = new DataTable();
            tabela.Load(command.ExecuteReader());
            foreach (DataRow linha in tabela.Rows)
            {
                Continente continente = new Continente()
                {
                    Id = Convert.ToInt32(linha[0].ToString()),
                    Nome = linha[1].ToString()
                };
                continentes.Add(continente);
            }
            return continentes;
        }


        public int Cadastro(Continente continente)
        {
            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = @"INSERT INTO 
                continentes(nome) 
                OUTPUT INSERTED.ID 
                VALUES (@NOME)";
            command.Parameters.AddWithValue("@NOME", continente.Nome);

            int id = Convert.ToInt32(command.ExecuteScalar().ToString());
            return id;

        }

        public bool Alterar(Continente continente)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"UPDATE continentes SET nome = @NOME WHERE id = @ID";
            command.Parameters.AddWithValue("@NOME", continente.Nome);
            command.Parameters.AddWithValue("@ID", continente.Id);
            return command.ExecuteNonQuery() == 1;
        }

        public bool Excluir(int id)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"DELETE FROM continentes WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            return command.ExecuteNonQuery() == 1;

        }

        public Continente ObterPeloId(int id)
        {
            Continente continente = null;
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT id, nome FROM continentes WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            if (table.Rows.Count == 1)
            {
                continente = new Continente();
                continente.Id = id;
                continente.Nome = table.Rows[0][0].ToString();
            }
            return continente;

        }
    }
}