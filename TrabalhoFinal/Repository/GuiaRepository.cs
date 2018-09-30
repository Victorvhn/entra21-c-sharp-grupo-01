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
            command.CommandText = @"SELECT g.login_ AS 'login', g.sexo AS 'sexo', g.senha AS 'senha', g.nome AS 'nome', g.sobrenome AS 'sobrenome', 
            g.numero_carteira_trabalho AS 'numero_carteira_trabalho', g.categoria_habilitacao AS 'categoria_habilitacao', 
            g.salario AS 'salario', g.cpf AS 'cpf', g.rg AS 'rg', g.data_nascimento AS 'data_nascimento', g.rank_ AS 'rank_', g.id_endereco AS 'id_endereco'
            e.id, e.cep AS 'cep', e.logradouro AS 'logradouro', e.numero AS 'numero', e.complemento AS 'complemento', e.referencia AS 'referencia', e.id_cidade AS 'id_cidade'
            FROM guias g
            JOIN enderecos e ON(e.id = g.id_endereco)";
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
            command.CommandText = @"SELECT g.id, g.nome, g.sobrenome, g.cpf, g.rank_, g.id_endereco, e.id, e.id_cidade, c.id, c.nome, c.id_estado, es.id, es.nome
            FROM guias g
            JOIN enderecos e ON (e.id = g.id_endereco)
            JOIN cidades c ON (c.id = e.id_cidade)
            JOIN estados es ON (es.id = c.id_estado)
            WHERE g.ativo = 1 AND ((g.nome LIKE @SEARCH) OR (g.sobrenome LIKE @SEARCH) OR (g.cpf LIKE @SEARCH))
            ORDER BY " + orderColumn + " " + orderDir +
            " OFFSET " + start + " ROWS FETCH NEXT " + length + " ROWS ONLY";

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
                guia.IdEndereco = Convert.ToInt32(linha[5].ToString());

                guia.Endereco = new Endereco();
                guia.Endereco.Id = Convert.ToInt32(linha[6].ToString());
                guia.Endereco.IdCidade = Convert.ToInt32(linha[7].ToString());

                guia.Endereco.Cidade = new Cidade();
                guia.Endereco.Cidade.Id = Convert.ToInt32(linha[8].ToString());
                guia.Endereco.Cidade.Nome = linha[9].ToString();
                guia.Endereco.Cidade.IdEstado = Convert.ToInt32(linha[10].ToString());

                guia.Endereco.Cidade.Estado = new Estado();
                guia.Endereco.Cidade.Estado.Id = Convert.ToInt32(linha[11].ToString());
                guia.Endereco.Cidade.Estado.Nome = linha[12].ToString();
                guias.Add(guia);
            };
            return guias;
        }

        public bool AlterarBasico(Guia guia)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = "UPDATE guias SET nome = @NOME, sobrenome = @SOBRENOME, data_nascimento = @DATANASCIMENTO" +
            "WHERE id = @ID";
            command.Parameters.AddWithValue("@NOME", guia.Nome);
            command.Parameters.AddWithValue("@SOBRENOME", guia.Sobrenome);
            command.Parameters.AddWithValue("@DATANASCIMENTO", guia.DataNascimento);
            command.Parameters.AddWithValue("@ID", guia.Id);

            return command.ExecuteNonQuery() == 1;
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
            // 

            command.CommandText = @"INSERT INTO guias (sexo, nome, sobrenome, numero_carteira_trabalho, categoria_habilitacao, salario, cpf, rg, data_nascimento, rank_, id_endereco) 
            OUTPUT INSERTED.ID
            VALUES (@SEXO, @NOME, @SOBRENOME, @NUMERO_CARTEIRA_TRABALHO, @CATEGORIA_HABILITACAO, @SALARIO, @CPF, @RG, @DATA_NASCIMENTO, @RANK_, @ID_ENDERECO)";
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
            command.Parameters.AddWithValue("@ID_ENDERECO", guia.Endereco.Id);

            var result = command.ExecuteScalar();

            int id = Convert.ToInt32(result.ToString());

            return id;

        }

        public int ContabilizarGuiasFiltrados(string search)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT COUNT(g.id)
            FROM guias g
            JOIN enderecos e ON (e.id = g.id_endereco)
            JOIN cidades c ON (c.id = e.id_cidade)
            JOIN estados es ON (es.id = c.id_estado)
            WHERE g.ativo = 1 AND ((g.nome LIKE @SEARCH) OR (g.sobrenome LIKE @SEARCH) OR (g.cpf LIKE @SEARCH))";
            command.Parameters.AddWithValue("@SEARCH", search);
            return Convert.ToInt32(command.ExecuteScalar().ToString());

        }

        public int ContabilizarGuias()
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT COUNT(id) FROM guias WHERE ativo = 1";
            return Convert.ToInt32(command.ExecuteScalar().ToString());

        }

        public bool Alterar(Guia guia)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"UPDATE guias
            SET sexo = @SEXO, nome = @NOME, sobrenome = @SOBRENOME, numero_carteira_trabalho = @NUMERO_CARTEIRA_TRABALHO, categoria_habilitacao = @CATEGORIA_HABILITACAO, 
            salario = @SALARIO, cpf = @CPF, rg = @RG, data_nascimento = @DATA_NASCIMENTO WHERE id = @ID";

            command.Parameters.AddWithValue("@ID", guia.Id);
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

            new EnderecoRepository().Alterar(guia.Endereco);

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
            command.CommandText = @"SELECT g.sexo, g.nome , g.sobrenome, g.numero_carteira_trabalho, g.categoria_habilitacao, g.salario, g.cpf, g.rg, g.data_nascimento, g.rank_, g.id_endereco,
            e.id, e.cep, e.logradouro, e.numero, e.complemento, e.referencia, e.id_cidade, 
            Cidades.id as cidades_id , Cidades.nome as cidades_nome, 
            Estados.id as estados_id, Estados.nome as estados_nome 
            FROM guias g
            INNER JOIN enderecos e ON (g.id_endereco = e.id) 
            INNER JOIN cidades Cidades ON (Cidades.id = e.id_cidade) 
            INNER JOIN estados Estados ON (Estados.id = Cidades.id_estado)
            WHERE g.id = @ID";
            command.Parameters.AddWithValue("@ID", id);

            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());

            if (table.Rows.Count == 1)
            {
                var row = table.Rows[0];

                guia = new Guia();
                guia.Id = id;
                guia.Nome = row.Field<string>("nome");
                guia.Sexo = row.Field<string>("sexo");
                guia.Sobrenome = row.Field<string>("sobrenome");
                guia.CarteiraTrabalho = row.Field<string>("numero_carteira_trabalho");
                guia.CategoriaHabilitacao = row.Field<string>("categoria_habilitacao");
                guia.Salario = row.Field<double>("salario");
                guia.Cpf = row.Field<string>("cpf");
                guia.Rg = row.Field<string>("rg");
                guia.DataNascimento = row.Field<DateTime>("data_nascimento");
                guia.Rank = row.Field<Int16>("rank_");
                guia.IdEndereco = row.Field<int>("id_endereco");
                guia.Endereco = new Endereco()
                {
                    Id = row.Field<int>("id_endereco"),
                    Cep = row.Field<string>("cep"),
                    Logradouro = row.Field<string>("logradouro"),
                    Numero = row.Field<short>("numero"),
                    Complemento = row.Field<string>("complemento"),
                    Referencia = row.Field<string>("referencia"),
                    IdCidade = row.Field<int>("id_cidade"),
                    Cidade = new Cidade
                    {
                        Id = row.Field<int>("cidades_id"),
                        Nome = row.Field<string>("cidades_nome"),
                        IdEstado = row.Field<int>("estados_id"),
                        Estado = new Estado
                        {
                            Id = row.Field<int>("estados_id"),
                            Nome = row.Field<string>("estados_nome"),
                        }
                    }
                };
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
