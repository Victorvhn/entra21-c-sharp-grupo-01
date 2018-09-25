using Model;
using Principal.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Repository
{
    public class GuiaRepository
    {
        public List<Guia> ObterTodos()
        {
            List<Guia> guias = new List<Guia>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT id, id_endereco, sexo, nome, sobrenome, numero_carteira_trabalho, 
            categoria_habilitacao, salario, cpf, rg, data_nascimento, rank_ FROM guias";
            DataTable tabela = new DataTable();
            tabela.Load(command.ExecuteReader());
            foreach (DataRow linha in tabela.Rows)
            {
                Guia guia = new Guia();
                guia.Id = Convert.ToInt32(linha[0].ToString());
                guia.IdEndereco = Convert.ToInt32(linha[1].ToString());
                guia.Sexo = linha[2].ToString();
                guia.Nome = linha[4].ToString();
                guia.Sobrenome = linha[5].ToString();
                guia.CarteiraTrabalho = linha[6].ToString();
                guia.CategoriaHabilitacao = linha[7].ToString();
                guia.Salario = Convert.ToSingle(linha[8].ToString());
                guia.Cpf = linha[9].ToString();
                guia.Rg = linha[10].ToString();
                guia.DataNascimento = Convert.ToDateTime(linha[11].ToString());
                guia.Rank = Convert.ToByte(linha[12].ToString());
                guias.Add(guia);
            }
            return guias;
        }

        public List<Guia> ObterTodosParaJSON(string start, string length, string search, string orderColumn, string orderDir)
        {
            List<Guia> guias = new List<Guia>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT id, nome, sobrenome, cpf, rank_ 
            FROM guias 
            WHERE ativo = 1 AND ((nome LIKE @SEARCH) OR (sobrenome LIKE @SEARCH) OR (cpf LIKE @SEARCH))
            ORDER BY " + orderColumn + " " + orderDir + 
            " OFFSET " + start + " ROWS FETCH NEXT " + length + " ROWS ONLY ";
            command.Parameters.AddWithValue("@SEARCH", search);
            DataTable tabela = new DataTable();
            tabela.Load(command.ExecuteReader());
            foreach (DataRow linha in tabela.Rows)
            {
                Guia guia = new Guia();
                guia.Id = Convert.ToInt32(linha[0].ToString());
                guia.Nome = linha[1].ToString();
                guia.Sobrenome = linha[2].ToString();
                guia.Cpf = linha[3].ToString();
                guia.Rank = Convert.ToByte(linha[4].ToString());
                guias.Add(guia);
            }
            return guias;
        }

        public List<Guia> ObterTodosParaSelect()
        {
            List<Guia> guias = new List<Guia>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = "SELECT id, nome FROM guias WHERE ativo = 1 ORDER BY nome";
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            foreach (DataRow line in table.Rows)
            {
                Guia guia = new Guia();
                guia.Id = Convert.ToInt32(line[0].ToString());
                guia.Nome = line[1].ToString();
                guias.Add(guia);
            }
            return guias;
        }

        public int Cadastrar(Guia guia)
        {
            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = @"INSERT INTO guias (sexo, nome, sobrenome, numero_carteira_trabalho, categoria_habilitacao, salario, cpf, rg, data_nascimento, rank_)
            OUTPUT INSERTED.ID
            VALUES (@SEXO, @NOME, @SOBRENOME, @NUMERO_CARTEIRA_TRABALHO, @CATEGORIA_HABILITACAO, @SALARIO, @CPF, @RG, @DATA_NASCIMENTO, @RANK_)";
            command.Parameters.AddWithValue("@SEXO", guia.Sexo);
            command.Parameters.AddWithValue("@NOME", guia.Nome);
            command.Parameters.AddWithValue("@SOBRENOME", guia.Sobrenome);
            command.Parameters.AddWithValue("@NUMERO_CARTEIRA_TRABALHO", guia.CarteiraTrabalho);
            command.Parameters.AddWithValue("@CATEGORIA_HABILITACAO", guia.CategoriaHabilitacao);
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
            SET id_endereco = @ID_ENDERECO, login_ = @LOGIN_, sexo = @SEXO, senha = @SENHA, nome = @NOME, sobrenome = @SOBRENOME, numero_carteira_trabalho = @NUMERO_CARTEIRA_TRABALHO, categoria_habilitacao = @CATEGORIA_HABILITACAO
            salario = @SALARIO, cpf = @CPF, rg = @RG, data_nascimento = @DATA_NASCIMENTO
            WHERE id = @ID";
            // TODO ajustar login
            command.Parameters.AddWithValue("@LOGIN_", "");
            //command.Parameters.AddWithValue("@LOGIN_", guia.Login_);
            command.Parameters.AddWithValue("@SEXO", guia.Sexo);
            // TODO ajustar login
            command.Parameters.AddWithValue("@SENHA", "");
            //command.Parameters.AddWithValue("@SENHA", guia.Senha);
            command.Parameters.AddWithValue("@NOME", guia.Nome);
            command.Parameters.AddWithValue("@SOBRENOME", guia.Sobrenome);
            command.Parameters.AddWithValue("@NUMERO_CARTEIRA_TRABALHO", guia.CarteiraTrabalho);
            command.Parameters.AddWithValue("@CATEGORIA_HABILITACAO", guia.CategoriaHabilitacao);
            command.Parameters.AddWithValue("@SALARIO", guia.Salario);
            command.Parameters.AddWithValue("@CPF", guia.Cpf);
            command.Parameters.AddWithValue("@RG", guia.Rg);
            command.Parameters.AddWithValue("@DATA_NASCIMENTO", guia.DataNascimento);
            command.Parameters.AddWithValue("@RANK_", guia.Rank);
            command.Parameters.AddWithValue("@ID_ENDERECO", guia.IdEndereco);
            return command.ExecuteNonQuery() == 1;
        }

        public bool Excluir(int id)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"UPDATE guias SET ativo = 0 WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            return command.ExecuteNonQuery() == 1;

        }

        public Guia ObterPeloId(int id)
        {
            Guia guia = null;
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT login_, sexo, senha, nome, sobrenome, numero_carteira_trabalho, categoria_habilitacao, salario, cpf, rg, data_nascimento, rank_, id_endereco
            FROM guias JOIN enderecos ON(guias.id_endereco = enderecos.id) WHERE guias.id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());

            if (table.Rows.Count == 1)
            {
                guia = new Guia();
                guia.Id = id;
                // TODO ajustar login
                //guia.Login_ = table.Rows[0][0].ToString();
                guia.Sexo = table.Rows[0][1].ToString();
                // TODO ajustar login
                //guia.Senha = table.Rows[0][2].ToString();
                guia.Nome = table.Rows[0][3].ToString();
                guia.Sobrenome = table.Rows[0][4].ToString();
                guia.CarteiraTrabalho = table.Rows[0][5].ToString();
                guia.CategoriaHabilitacao = table.Rows[0][6].ToString();
                guia.Salario = Convert.ToSingle(table.Rows[0][7]);
                guia.Cpf = table.Rows[0][8].ToString();
                guia.Rg = table.Rows[0][9].ToString();
                guia.DataNascimento = Convert.ToDateTime(table.Rows[0][10]);
                guia.Rank = Convert.ToByte(table.Rows[0][11].ToString());
                guia.IdEndereco = Convert.ToInt32(table.Rows[0][12].ToString());
            }
            return guia;
        }

        public Guia VerificarLogin(string email, string senha)
        {
            Guia guia = null;
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT g.id, g.id_login, g.sexo, g.nome, g.sobrenome, g.numero_carteira_trabalho, g.categoria_habilitacao, g.salario, g.cpf, g.rg, g.data_nascimento, g.rank_, g.id_endereco, u.email, u.privilegio
            FROM guias g
            JOIN logins u ON(u.id = g.id_login AND email = @EMAIL AND senha = @SENHA)";
            command.Parameters.AddWithValue("@EMAIL", email);
            command.Parameters.AddWithValue("@SENHA", senha);
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());

            if (table.Rows.Count == 1)
            {
                guia = new Guia();
                guia.Id = Convert.ToInt32(table.Rows[0]["id"].ToString());

                guia.Sexo = table.Rows[0]["sexo"].ToString();
                guia.Nome = table.Rows[0]["nome"].ToString();
                guia.Sobrenome = table.Rows[0]["sobrenome"].ToString();
                guia.CarteiraTrabalho = table.Rows[0]["numero_carteira_trabalho"].ToString();
                guia.CategoriaHabilitacao = table.Rows[0]["categoria_habilitacao"].ToString();
                guia.Salario = Convert.ToSingle(table.Rows[0]["salario"]);
                guia.Cpf = table.Rows[0]["cpf"].ToString();
                guia.Rg = table.Rows[0]["rg"].ToString();
                guia.DataNascimento = Convert.ToDateTime(table.Rows[0]["data_nascimento"]);
                guia.Rank = Convert.ToByte(table.Rows[0]["rank_"].ToString());
                guia.IdEndereco = Convert.ToInt32(table.Rows[0]["id_endereco"].ToString());

                guia.Login = new Login();
                guia.Login.Email = table.Rows[0]["email"].ToString();
                guia.Login.Id = Convert.ToInt32(table.Rows[0]["id_login"].ToString());
                guia.IdLogin = Convert.ToInt32(table.Rows[0]["id_login"].ToString());
                guia.Login.Privilegio = table.Rows[0]["privilegio"].ToString();
            }
            return guia;
        }
    }
}
