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
    public class ViagensTuristasRepository
    {
        public List<Viagem> ObterTodasViagensPeloIdViagem(int idViagem)
        {
            List<Viagem> viagens = new List<Viagem>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT viagens.id, viagens.data, valor FROM viagens_turistas
                                  vt JOIN viagens ON (viagens.id = vt.id_viagem) WHERE vt.id_viagem = @ID_VIAGEM";
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            foreach (DataRow line in table.Rows)
            {
                Viagem viagensTuristas = new Viagem()
                {
                    Id = Convert.ToInt32(line[0].ToString()),
                    //Data = Convert.ToDateTime(line[1].ToString()),

                };
                viagens.Add(viagensTuristas);
            }
            return viagens;
        }

        public List<ViagemTurista> ObterTodosParaSelect()
        {
            List<ViagemTurista> viagensturista = new List<ViagemTurista>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = "SELECT id, valor, id_turista, id_viagem FROM viagens WHERE ativo = 1";
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            foreach (DataRow line in table.Rows)
            {
                ViagemTurista viagemTurista = new ViagemTurista()
                {
                    Id = Convert.ToInt32(line[0].ToString()),
                    Valor = Convert.ToInt32(line[1].ToString()),
                    IdTurista = Convert.ToInt32(line[3].ToString()),
                    IdViagem = Convert.ToInt32(line[4].ToString())
                };
                viagensturista.Add(viagemTurista);
            }
            return viagensturista;
        }


        public int Cadastro(ViagemTurista viagemTurista)
        {
            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = @"INSERT INTO viagens_turistas (valor, id_turista, id_viagem) OUTPUT INSERTED.ID VALUES (@VALOR, @ID_TURISTA, @ID_VIAGEM)";
            command.Parameters.AddWithValue("@VALOR", viagemTurista.Valor);
            command.Parameters.AddWithValue("@ID_TURISTA", viagemTurista.IdTurista);
            command.Parameters.AddWithValue("@ID_VIAGEM", viagemTurista.IdViagem);

            int id = Convert.ToInt32(command.ExecuteScalar().ToString());
            return id;

        }

        public bool Alterar(ViagemTurista viagemTurista)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"UPDATE viagens_turistas SET valor = @VALOR, id_turista = @ID_TURISTA, id_viagem = @ID_VIAGEM WHERE id = @ID";

            command.Parameters.AddWithValue("@VALOR", viagemTurista.Valor);
            command.Parameters.AddWithValue("@ID_TURISTA", viagemTurista.IdTurista);
            command.Parameters.AddWithValue("@ID_VIAGEM", viagemTurista.IdViagem);
            command.Parameters.AddWithValue("@ID", viagemTurista.Id);
            return command.ExecuteNonQuery() == 1;
        }

        public bool Excluir(int id)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"DELETA FROM viagens_turistas WHERE id = @ID;";
            command.Parameters.AddWithValue("@ID", id);
            return command.ExecuteNonQuery() == 1;
        }

        public ViagemTurista ObterPeloId(int id)
        {
            ViagemTurista viagemTurista = null;
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT turistas.nome, turistas.id, viagens.id, id_viagem, id_turista FROM viagens_turistas JOIN viagens (viagens_turistas.id_viagem = viagens.id)
             JOIN turistas ON (viagens_turistas.id_turista = turistas.id) WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            if (table.Rows.Count == 1)
            {
                viagemTurista = new ViagemTurista();
                viagemTurista.Id = id;
                viagemTurista.IdTurista = Convert.ToInt32(table.Rows[0][0]);
                viagemTurista.IdViagem = Convert.ToInt32(table.Rows[0][1]);
                viagemTurista.Valor = Convert.ToSingle(table.Rows[0][2]);
            }
            return viagemTurista;
        }
    }
}
