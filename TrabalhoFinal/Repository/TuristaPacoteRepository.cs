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
    public class TuristaPacoteRepository
    {

        public List<TuristaPacote> ObterTodosPorJSON(string start, string length)
        {
            List<TuristaPacote> turistasPacotes = new List<TuristaPacote>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT tp.id, t.nome, p.nome, p.valor, tp.status_do_pedido
FROM turistas_pacotes tp 
INNER JOIN turistas t ON (t.id = tp.id_turista)
INNER JOIN pacotes p ON (p.id = tp.id_pacote)
ORDER BY tp.data_requisicao ASC OFFSET " + start + " ROWS FETCH NEXT " + length + " ROWS ONLY";

            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());

            foreach (DataRow line in table.Rows)
            {
                TuristaPacote turistaPacote = new TuristaPacote();
                turistaPacote.Id = Convert.ToInt32(line[0].ToString());
                turistaPacote.Turista.Nome = line[1].ToString();
                turistaPacote.Pacote.Nome = line[2].ToString();
                turistaPacote.Pacote.Valor = Convert.ToDouble(line[3].ToString());
                turistaPacote.StatusPedido = line[4].ToString();
                turistasPacotes.Add(turistaPacote);
            }
            return turistasPacotes;
        }

    }
}
