using Model;
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

        public List<TuristaPacote> ObterTodosPorJSON(string start, string length, string search, string orderColumn, string orderDir)
        {
            List<TuristaPacote> turistasPacotes = new List<TuristaPacote>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT tp.id, t.nome, p.nome, p.valor, tp.status_do_pedido
FROM turistas_pacotes tp 
INNER JOIN turistas t ON (t.id = tp.id_turista)
INNER JOIN pacotes p ON (p.id = tp.id_pacote)
WHERE t.ativo = 1 AND (tp.id LIKE @SEARCH) OR (t.nome LIKE @SEARCH) OR (p.nome LIKE @SEARCH) OR (p.valor LIKE @SEARCH) OR (tp.status_do_pedido LIKE @SEARCH) 
ORDER BY " + orderColumn + " " + orderDir +  
" OFFSET " + start + " ROWS FETCH NEXT " + length + " ROWS ONLY ";

            command.Parameters.AddWithValue("@SEARCH", search);
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());

            foreach (DataRow line in table.Rows)
            {
                TuristaPacote turistaPacote = new TuristaPacote();
                turistaPacote.Id = Convert.ToInt32(line[0].ToString());
                turistaPacote.Turista = new Turista();
                turistaPacote.Turista.Nome = line[1].ToString();
                turistaPacote.Pacote = new Pacote();
                turistaPacote.Pacote.Nome = line[2].ToString();
                turistaPacote.Pacote.Valor = Convert.ToDouble(line[3].ToString());
                turistaPacote.StatusPedido = line[4].ToString();
                turistasPacotes.Add(turistaPacote);
            }
            return turistasPacotes;
        }

        public int ContabilizarPacotesAguardandoFiltrado(string search)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT COUNT(tp.id)
            FROM turistas_pacotes tp 
            INNER JOIN turistas t ON (t.id = tp.id_turista)
            INNER JOIN pacotes p ON (p.id = tp.id_pacote) 
            WHERE t.ativo = 1 AND (tp.id LIKE @SEARCH) OR (t.nome LIKE @SEARCH) OR (p.nome LIKE @SEARCH) OR (p.valor LIKE @SEARCH) OR (tp.status_do_pedido LIKE @SEARCH)";

            command.Parameters.AddWithValue("@SEARCH", search);
            return Convert.ToInt32(command.ExecuteScalar().ToString());
        }

        public int ContabilizarPacotesAguardando()
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT COUNT(id) FROM turistas_pacotes WHERE ativo = 1";
            return Convert.ToInt32(command.ExecuteScalar().ToString());
        }

        public int Cadastro(TuristaPacote turistaPacoteModel)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"INSERT INTO turistas_pacotes(id_pacote, id_turista, status_do_pedido, data_requisicao) OUTPUT INSERTED.ID VALUES (@IDPACOTE, @IDTURISTA, @STATUS, GETDATE())";
            command.Parameters.AddWithValue("@IDPACOTE", turistaPacoteModel.IdPacote);
            command.Parameters.AddWithValue("@IDTURISTA", turistaPacoteModel.IdTurista);
            command.Parameters.AddWithValue("@STATUS", turistaPacoteModel.StatusPedido);

            return Convert.ToInt32(command.ExecuteScalar().ToString());
        }

        public bool Excluir(int id)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"UPDATE turistas_pacotes SET status_do_pedido = 'Aguardando Pagamento' WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            return command.ExecuteNonQuery() == 1;
        }
    }
}
