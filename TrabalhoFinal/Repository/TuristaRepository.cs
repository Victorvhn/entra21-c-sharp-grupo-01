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
            List<Turista> turistas = new List<Turista>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = "";
            DataTable tabela = new DataTable();
            tabela.Load(command.ExecuteReader());
            foreach (DataRow linha in tabela.Rows)
            {
                Turista turista = new Turista()
                {
                    Id=
                }
            }
                return turistas;
        }





    }
}


