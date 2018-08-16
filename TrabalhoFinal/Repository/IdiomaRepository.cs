using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Principal;
using Principal.Database;

namespace Principal.Repository
{
    public class IdiomaRepository
    {
        public List<Idioma> ObterTodos()
        {
            List<Idioma> idiomas = new List<Idioma>();
            SqlCommand command = new BancoDados().ObterConexao();
            return idiomas;
        }
    }
}