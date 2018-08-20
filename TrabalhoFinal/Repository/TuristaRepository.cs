using Model;
using Principal.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Repository
{
    public class TuristaRepository
    {

        public List<Turista> ObterTodos()
        {
            List<Turista> turista = new List<Turista>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = "SELECT id, login_, sexo, senha, nome, sobrenome, cpf, rg, data_nascimento";
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            foreach (DataRow linha in table.Rows)
            {
                Turista turistas = new Turista()
                {
                    Id = Convert.ToInt32(linha[0].ToString()),
                    Login_ = linha[1].ToString(),
                    Sexo = Convert.ToChar(linha[2].ToString()),
                    Senha = linha[3].ToString(),
                    Nome = linha[4].ToString(),
                    Sobrenome = linha[5].ToString(),
                    Cpf = linha[6].ToString(),
                    Rg = linha[7].ToString(),
                    DataNascimento = Convert.ToDateTime(linha[11].ToString()),
                    
                };
                turista.Add(turistas);
            }
            return turista;
        }
        public int Cadastrar(Turista turista)
        {
            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = @"INSERT INTO turistas (login_, sexo, senha, nome, sobrenome, cpf, rg, data_nascimento) OUTPUT INSERTED.ID VALUES (@LOGIN_, @SEXO, @SENHA, @NOME, @SOBRENOME, @CPF, @RG, @DATA_NASCIMENTO)";
            
            command.Parameters.AddWithValue("@LOGIN_", turista.Login_);
            command.Parameters.AddWithValue("@SEXO", turista.Sexo);
            command.Parameters.AddWithValue("@SENHA", turista.Senha);
            command.Parameters.AddWithValue("@NOME", turista.Nome);
            command.Parameters.AddWithValue("@SOBRENOME", turista.Sobrenome);
            command.Parameters.AddWithValue("@CPF", turista.Cpf);
            command.Parameters.AddWithValue("@RG", turista.Rg);
            command.Parameters.AddWithValue("@DATA_NASCIMENTO", turista.DataNascimento);
            int id = Convert.ToInt32(command.ExecuteReader().ToString());
            return id;
 



        }

        public bool Alterar(Turista turista)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"UPDATE turistas SET login_ = @LOGIN_, sexo = @SEXO, senha = @SENHA, nome = @NOME, sobrenome = @SOBRENOME, cpf = @CPF, rg = @RG, data_nascimento = @DATA_NASCIMENTO WHERE id = @ID";
            command.Parameters.AddWithValue("@LOGIN_", turista.Login_);
            command.Parameters.AddWithValue("@SEXO", turista.Sexo);
            command.Parameters.AddWithValue("@SENHA", turista.Senha);
            command.Parameters.AddWithValue("@NOME", turista.Nome);
            command.Parameters.AddWithValue("@SOBRENOME", turista.Sobrenome);
            command.Parameters.AddWithValue("@CPF", turista.Cpf);
            command.Parameters.AddWithValue("@RG", turista.Rg);
            command.Parameters.AddWithValue("@DATA_NASCIMENTO", turista.DataNascimento);
            return command.ExecuteNonQuery() == 1;

        }

        public bool Excluir(int id)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"DELETE FROM turistias WHERE id = @ID";
            command.Parameters.AddWithValue("ID", id);
            return command.ExecuteNonQuery() == 1;
        }

        public Turista ObterPeloId(int id)
        {
            Turista turista = null;
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT (login,sexo, senha, nome, sobrenome, cpf, rg, data_nascimento FROM turistas WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());

            if (table.Rows.Count == 1)
            {
                turista = new Turista();
                turista.Id = id;
                turista.Login_ = table.Rows[0][0].ToString();
                turista.Sexo = Convert.ToChar(table.Rows[0][1].ToString());
                turista.Senha = table.Rows[0][2].ToString();
                turista.Nome = table.Rows[0][3].ToString();
                turista.Sobrenome = table.Rows[0][4].ToString();   
                turista.Cpf = table.Rows[0][5].ToString();
                turista.Rg = table.Rows[0][6].ToString();
                turista.DataNascimento = Convert.ToDateTime(table.Rows[0][7]);

            }
            return turista;
        }
    }
}



