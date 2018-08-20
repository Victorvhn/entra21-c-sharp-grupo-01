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
    public class GuiaRepositorio
    {
        public List<Guia> ObterTodos()
        {
            List<Guia> guias = new List<Guia>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = "SELECT id, login_, sexo, senha, nome, sobrenome, numero_carteira_trabalho, categoria_habilitacao, salario, cpf, rg, data_nascimento, rank_ FROM guias";
            DataTable tabela = new DataTable();
            tabela.Load(command.ExecuteReader());
            foreach (DataRow linha in tabela.Rows)
            {
                Guia guia = new Guia()
                {
                    Id = Convert.ToInt32(linha[0].ToString()),
                    Login_ = linha[1].ToString(),
                    Sexo = Convert.ToChar(linha[2].ToString()),
                    Senha = linha[3].ToString(),
                    Nome = linha[4].ToString(),
                    Sobrenome = linha[5].ToString(),
                    CarteiraTrabalho = linha[6].ToString(),
                    CatagoriaHabilitacao = linha[7].ToString(),
                    Salario = Convert.ToSingle(linha[8].ToString()),
                    Cpf = linha[9].ToString(),
                    Rg = linha[10].ToString(),
                    DataNascimento = Convert.ToDateTime(linha[11].ToString()),
                    Rank = Convert.ToChar(linha[12].ToString())

                };
                guias.Add(guia);
            }
            return guias;
        }

        public int Cadastrar(Guia guia)
        {
            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = @"INSERT INTO guias (login_, sexo, senha, nome, sobrenome, numero_carteira_trabalho, categoria_habilitacao, salario, cpf, rg, data_nascimento, rank_)
            OUTPUT INSERTED.ID
            VALUES (@LOGIN_, @SEXO, @SENHA, @NOME, @SOBRENOME, @NUMERO_CARTEIRA_TRABALHO, @CATEGORIA_HABILITACAO, @SALARIO, @CPF, @RG, @DATA_NASCIMENTO, @RANK_)";
            command.Parameters.AddWithValue("@LOGIN_", guia.Login_);
            command.Parameters.AddWithValue("@SEXO", guia.Sexo);
            command.Parameters.AddWithValue("@SENHA", guia.Senha);
            command.Parameters.AddWithValue("@NOME", guia.Nome);
            command.Parameters.AddWithValue("@SOBRENOME", guia.Sobrenome);
            command.Parameters.AddWithValue("@NUMERO_CARTEIRA_TRABALHO", guia.CarteiraTrabalho);
            command.Parameters.AddWithValue("@CATEGORIA_HABILITACAO", guia.CatagoriaHabilitacao);
            command.Parameters.AddWithValue("@SALARIO", guia.Salario);
            command.Parameters.AddWithValue("@CPF", guia.Cpf);
            command.Parameters.AddWithValue("@RG", guia.Rg);
            command.Parameters.AddWithValue("@DATA_NASCIMENTO", guia.DataNascimento);
            command.Parameters.AddWithValue("@RANK_", guia.Rank);
            int id = Convert.ToInt32(command.ExecuteScalar().ToString());
            return id;

        }

        public bool Alterar(Guia guia)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"UPDATE guias
            SET login_ = @LOGIN_, sexo = @SEXO, senha = @SENHA, nome = @NOME, sobrenome = @SOBRENOME, numero_carteira_trabalho = @NUMERO_CARTEIRA_TRABALHO, categoria_habilitacao = @CATEGORIA_HABILITACAO
            salario = @SALARIO, cpf = @CPF, rg = @RG, data_nascimento = @DATA_NASCIMENTO
            WHERE id = @ID";
            command.Parameters.AddWithValue("@LOGIN_", guia.Login_);
            command.Parameters.AddWithValue("@SEXO", guia.Sexo);
            command.Parameters.AddWithValue("@SENHA", guia.Senha);
            command.Parameters.AddWithValue("@NOME", guia.Nome);
            command.Parameters.AddWithValue("@SOBRENOME", guia.Sobrenome);
            command.Parameters.AddWithValue("@NUMERO_CARTEIRA_TRABALHO", guia.CarteiraTrabalho);
            command.Parameters.AddWithValue("@CATEGORIA_HABILITACAO", guia.CatagoriaHabilitacao);
            command.Parameters.AddWithValue("@SALARIO", guia.Salario);
            command.Parameters.AddWithValue("@CPF", guia.Cpf);
            command.Parameters.AddWithValue("@RG", guia.Rg);
            command.Parameters.AddWithValue("@DATA_NASCIMENTO", guia.DataNascimento);
            command.Parameters.AddWithValue("@RANK_", guia.Rank);
            return command.ExecuteNonQuery() == 1;
        }

        public bool Excluir(int id)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"DELETE FROM guias WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            return command.ExecuteNonQuery() == 1;

        }

        public Guia ObterPeloId(int id)
        {
            Guia guia = null;
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT (login_, sexo, senha, nome, sobrenome, numero_carteira_trabalho, categoria_habilitacao, salario, cpf, rg, data_nascimento, rank_
            FROM guias WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());

            if(table.Rows.Count == 1)
            {
                guia = new Guia();
                guia.Id = id;
                guia.Login_ = table.Rows[0][0].ToString();
                guia.Sexo = Convert.ToChar(table.Rows[0][1].ToString());
                guia.Senha = table.Rows[0][2].ToString();
                guia.Nome = table.Rows[0][3].ToString();
                guia.Sobrenome = table.Rows[0][4].ToString();
                guia.CarteiraTrabalho = table.Rows[0][5].ToString();
                guia.CatagoriaHabilitacao = table.Rows[0][6].ToString();
                guia.Salario = Convert.ToSingle(table.Rows[0][7]);
                guia.Cpf = table.Rows[0][8].ToString();
                guia.Rg = table.Rows[0][9].ToString();
                guia.DataNascimento = Convert.ToDateTime(table.Rows[0][10]);
                guia.Rank = Convert.ToChar(table.Rows[0][11].ToString());
            }
            return guia;

        }
    }
}
