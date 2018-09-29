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
            command.CommandText = "SELECT id, id_endereco, login_, sexo, senha, nome, sobrenome, cpf, rg, data_nascimento";
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            foreach (DataRow linha in table.Rows)
            {
                Turista turistas = new Turista()
                {
                    Id = Convert.ToInt32(linha[0].ToString()),
                    IdEndereco = Convert.ToInt32(linha[1].ToString()),
                    Login_ = linha[2].ToString(),
                    Sexo = linha[3].ToString(),
                    Senha = linha[4].ToString(),
                    Nome = linha[5].ToString(),
                    Sobrenome = linha[6].ToString(),
                    Cpf = linha[7].ToString(),
                    Rg = linha[8].ToString(),
                    DataNascimento = Convert.ToDateTime(linha[9].ToString()),
                    
                };
                turista.Add(turistas);
            }
            return turista;
        }

        public Turista VerificarLogin(string email, string senha)
        {
            Turista turista = null;
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT t.id, t.id_login, t.id_endereco, t.nome, t.sobrenome, t.cpf, t.rg, t.sexo, t.data_nascimento, u.privilegio, u.email
            FROM turistas t
            JOIN logins u ON (u.id = t.id_login AND email = @EMAIL AND senha = @SENHA)";
            command.Parameters.AddWithValue("@EMAIL", email);
            command.Parameters.AddWithValue("@SENHA", senha);
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());

            if (table.Rows.Count == 1)
            {
                turista = new Turista();
                turista.Id = Convert.ToInt32(table.Rows[0][0].ToString());

                turista.Nome = table.Rows[0][3].ToString();
                turista.IdEndereco = Convert.ToInt32(table.Rows[0][2].ToString());
                turista.Sobrenome = table.Rows[0][4].ToString();
                turista.Cpf = table.Rows[0][5].ToString();
                turista.Rg = table.Rows[0][6].ToString();
                turista.Sexo = table.Rows[0][7].ToString();
                turista.DataNascimento = Convert.ToDateTime(table.Rows[0][8].ToString());

                turista.Login = new Login();
                turista.Login.Email = table.Rows[0][10].ToString();
                turista.Login.Id = Convert.ToInt32(table.Rows[0][1].ToString());
                turista.IdLogin = Convert.ToInt32(table.Rows[0][1].ToString());
                turista.Login.Privilegio = table.Rows[0][9].ToString();            
            }

            return turista;
        }

        public int Cadastrar(Turista turista)
        {
            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = @"INSERT INTO turistas (id_login, nome, sobrenome, cpf, rg, data_nascimento, sexo) 
            OUTPUT INSERTED.ID VALUES (@IDLOGIN, @NOME, @SOBRENOME, @CPF, @RG, @DATA_NASCIMENTO, @SEXO)";
            
            command.Parameters.AddWithValue("@IDLOGIN", turista.IdLogin);
            command.Parameters.AddWithValue("@NOME", turista.Nome);
            command.Parameters.AddWithValue("@SOBRENOME", turista.Sobrenome);
            command.Parameters.AddWithValue("@CPF", turista.Cpf);
            command.Parameters.AddWithValue("@RG", turista.Rg);
            command.Parameters.AddWithValue("@DATA_NASCIMENTO", turista.DataNascimento);
            command.Parameters.AddWithValue("@SEXO", turista.Sexo);
            int id = Convert.ToInt32(command.ExecuteScalar().ToString());
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
                turista.Sexo = table.Rows[0][1].ToString();
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



