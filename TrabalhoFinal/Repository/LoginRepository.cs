using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Principal.Database;

namespace Repository
{
    public class LoginRepository
    {
        public int Cadastrar(Login login)
        {
            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = @"INSERT INTO logins (email, senha, privilegio) 
            OUTPUT INSERTED.ID VALUES (@EMAIL, @SENHA, @PRIVILEGIO)";

            command.Parameters.AddWithValue("@EMAIL", login.Email);
            command.Parameters.AddWithValue("@SENHA", login.Senha);
            command.Parameters.AddWithValue("@PRIVILEGIO", "Usuário");

            int id = Convert.ToInt32(command.ExecuteScalar().ToString());
            return id;
        }

        public bool EditarEmail(Login login)
        {
            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = @"UPDATE logins SET email = @EMAIL WHERE id = @ID";
            command.Parameters.AddWithValue("@EMAIL", login.Email);
            command.Parameters.AddWithValue("@ID", login.Id);

            return command.ExecuteNonQuery() == 1;
        }

        public bool EditarSenha(Login login)
        {
            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = @"UPDATE logins SET senha = @SENHA WHERE id = @ID";
            command.Parameters.AddWithValue("@SENHA", login.Senha);
            command.Parameters.AddWithValue("@ID", login.Id);

            return command.ExecuteNonQuery() == 1;
        }
    }
}
