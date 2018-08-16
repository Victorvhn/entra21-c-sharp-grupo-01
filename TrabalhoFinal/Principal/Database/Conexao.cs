﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Principal.Database
{
    public class Conexao
    {
        private static string connectionString;

        static Conexao()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public SqlCommand ObterConexao()
        {

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            return command;

        }
    }
}