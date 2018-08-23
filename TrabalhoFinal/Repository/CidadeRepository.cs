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
    public class CidadeRepository
    {
        public List<Cidade> ObterTodos()
        {
            List<Cidade> cidades = new List<Cidade>();
            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = @"SELECT id, nome, id_estado FROM cidades";
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            foreach (DataRow line in table.Rows)
            {
                Cidade cidade = new Cidade()
                {
                    Id = Convert.ToInt32(line[0].ToString()),
                    IdEstado = Convert.ToInt32(line[1].ToString()),
                    Nome = line[2].ToString()
                };
                cidades.Add(cidade);
            }
            return cidades;
        }

        public int Cadastrar(Cidade cidade)
        {
            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = @"INSERT INTO  cidades (nome, id_estado
            OUTPUT INSERTED.ID(@NOME, @ID_ESTADO))";
            command.Parameters.AddWithValue("@NOME", cidade.Nome);
            command.Parameters.AddWithValue("@ID_ESTADO", cidade.IdEstado);

            int id = Convert.ToInt32(command.ExecuteScalar().ToString());
            return id;
        }

        public bool Alterar(Cidade cidade)
        {
            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = @"UPDATE  cidades SET nome = @NOME, id_estado = @ID_ESTADO WHERE id = @ID";

            command.Parameters.AddWithValue("@NOME", cidade.Nome);
            command.Parameters.AddWithValue("@ID_ESTADO", cidade.IdEstado);
            command.Parameters.AddWithValue("@ID", cidade.Id);
            return command.ExecuteNonQuery() == 1;
        }

        public bool Excluir(int id)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"DELETE FROM cidades WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            return command.ExecuteNonQuery() == 1;

        }

        public Cidade ObterPeloId(int id)
        {
            Cidade cidade = null;

            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = @"SELECT cidades.nome, id_estado, estados.nome FROM cidades  
            JOIN cidades ON (cidades.id_estado = estados.id
            WHERE id = @ID)";
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());

            if (table.Rows.Count == 1)
            {
                cidade = new Cidade();
                cidade.Id = id;
                cidade.Nome = table.Rows[0][0].ToString();
                cidade.IdEstado = Convert.ToInt32(table.Rows[0][1].ToString());
                cidade.Estado = new Estado();
                cidade.Estado.Nome = table.Rows[0][2].ToString();
                cidade.Estado.Id = Convert.ToInt32(table.Rows[0][3].ToString());

            }
            return cidade;
        }
    }
}
