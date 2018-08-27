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
    public class EnderecoRepository
    {

        public List<Endereco> ObterTodos()
        {
            List<Endereco> enderecos = new List<Endereco>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = "SELECT id, cep, logradouro, numero, complemento, referencia FROM enderecos";

            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            foreach (DataRow line in table.Rows)
            {
                Endereco endereco = new Endereco()
                {
                    Id = Convert.ToInt32(line[0].ToString()),
                    Cep = line[1].ToString(),
                    Logradouro = line[2].ToString(),
                    Numero = Convert.ToInt16(line[3].ToString()),
                    Complemento = line[4].ToString(),
                    Referencia = line[5].ToString()


                };
                enderecos.Add(endereco);
            }
            return enderecos;
        }

        public List<Endereco> ObterTodosParaJSON(string start, string length)
        {
            List<Endereco> enderecos = new List<Endereco>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = "SELECT id, cep, logradouro, numero FROM enderecos ORDER BY logradouro OFFSET " + start + " ROWS FETCH NEXT " + length + " ROWS ONLY ";

            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());

            foreach (DataRow line in table.Rows)
            {
                Endereco endereco = new Endereco()
                {
                    Id = Convert.ToInt32(line[0].ToString()),
                    Cep = line[1].ToString(),
                    Logradouro = line[2].ToString(),
                    Numero = Convert.ToInt16(line[3].ToString()),
                };
                enderecos.Add(endereco);
            }
            return enderecos;
        }

        public int Cadastrar(Endereco endereco)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = "INSERT INTO enderecos(cep, logradouro, numero, complemento, referencia) OUTPUT INSERTED.ID VALUES (@CEP, @LOGRADOURO, @NUMERO, @COMPLEMENTO, @REFERENCIA)";
            command.Parameters.AddWithValue("@CEP", endereco.Cep);
            command.Parameters.AddWithValue("@LOGRADOURO", endereco.Logradouro);
            command.Parameters.AddWithValue("@NUMERO", endereco.Numero);
            command.Parameters.AddWithValue("@COMPLEMENTO", endereco.Complemento);
            command.Parameters.AddWithValue("@REFERENCIA", endereco.Referencia);
            int id = Convert.ToInt32(command.ExecuteScalar().ToString());
            return id;

        }

        public bool Alterar(Endereco endereco)
        {
            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = "UPDATE enderecos SET cep= @CEP,logradouro = @LOGRADOURO,numero = @NUMERO,complemento = @COMPLEMENTO,referencia = @REFERENCIA WHERE id = @ID";
            command.Parameters.AddWithValue("@CEP", endereco.Cep);
            command.Parameters.AddWithValue("@LOGRADOURO", endereco.Logradouro);
            command.Parameters.AddWithValue("@NUMERO", endereco.Numero);
            command.Parameters.AddWithValue("@COMPLEMENTO", endereco.Complemento);
            command.Parameters.AddWithValue("@REFERENCIA", endereco.Referencia);
            command.Parameters.AddWithValue("@ID", endereco.Id);
            return command.ExecuteNonQuery() == 1;

        }

        public bool Excluir(int Id)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = "DELETE FROM enderecos WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", Id);
            return command.ExecuteNonQuery() == 1;
        }

        public Endereco ObterPeloId(int id)
        {
            Endereco enderecos = null;
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = "SELECT id, cep, logradouro, numero, complemento, referencia FROM enderecos WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            if (table.Rows.Count == 1)
            {

                enderecos = new Endereco();
                enderecos.Id = Convert.ToInt32(table.Rows[0][0].ToString());
                enderecos.Cep = table.Rows[0][1].ToString();
                enderecos.Logradouro = table.Rows[0][2].ToString();
                enderecos.Numero = Convert.ToInt16(table.Rows[0][3].ToString());
                enderecos.Complemento = table.Rows[0][4].ToString();
                enderecos.Referencia = table.Rows[0][5].ToString();
                
            }
            return enderecos;

        }

    }
}
