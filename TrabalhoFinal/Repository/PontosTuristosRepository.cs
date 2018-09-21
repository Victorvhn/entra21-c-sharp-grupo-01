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
    public class PontosTuristicosRepository
    {
//        public List<Pacote> ObterTodosPacotesPeloIdPacote(int idPacote)
//        {
//            List<Pacote> pacotes = new List<Pacote>();
//            SqlCommand command = new Conexao().ObterConexao();
//            command.CommandText = @"SELECT pt.id, p.id, p.nome, p.valor, id_endereco, pt.nome,  FROM pontos_turisticos pt JOIN
//                                  pacotes p ON(pt.id = p.id_pacote) WHERE pt.id_pacote = @ID_PACOTE";

//            DataTable table = new DataTable();
//            table.Load(command.ExecuteReader());
//            foreach (DataRow line in table.Rows)
//            {
//                Pacote pacote = new Pacote()
//                {
//                    Id = Convert.ToInt32(line["p.id"].ToString()),
//                    Nome = line["p.nome"].ToString()

//                };
//                pacotes.Add(pacote);

//            }
//            return pacotes;
//        }

        public List<PontoTuristico> ObterTodosParaSelect()
        {
            List<PontoTuristico> pontosTuristicos = new List<PontoTuristico>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = "SELECT id, nome FROM pontos_turisticos";
            DataTable tabela = new DataTable();
            tabela.Load(command.ExecuteReader());
            foreach(DataRow linha in tabela.Rows)
            {
                PontoTuristico pontoTuristico = new PontoTuristico()
                {
                    Id = Convert.ToInt32(linha[0].ToString()),
                    Nome = linha[1].ToString(),
                    IdEndereco = Convert.ToInt32(linha[2].ToString())
                };
                pontosTuristicos.Add(pontoTuristico);
            }
            return pontosTuristicos;
        }
        public int Cadastrar(PontoTuristico pontosturisticos)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = "INSERT INTO pontos_turisticos(id_endereco,nome)OUTPUT INSERTED.ID VALUES(@ID_ENDERECO,@NOME)";
            command.Parameters.AddWithValue("@ID_ENDERECO", pontosturisticos.IdEndereco);
            command.Parameters.AddWithValue("@NOME", pontosturisticos.Nome);
            int id = Convert.ToInt32(command.ExecuteScalar().ToString());
            return id;
        }
        public bool Excluir(int id)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = "UPDATE pontos_turisticos SET ativo = 0 WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            return command.ExecuteNonQuery() == 1;

        }
        public PontoTuristico ObterPeloId(int id)
        {
            PontoTuristico pontoTuristico = null;
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT pt.id,pt.nome,e.logradouro,e.numero,c.id,c.nome,es.id,es.nome FROM pontos_turisticos
            JOIN endereco e ON(e.id = pt.id_endereco)
            JOIN cidades c ON (c.id = e.id_cidade)
            JOIN estados es ON (es.id = c.id_estado)
           
            WHERE pt.ativo = @ID";
            command.Parameters.AddWithValue("@ID", id);

            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            if (table.Rows.Count == 1)
            {
                pontoTuristico = new PontoTuristico();
                pontoTuristico.Id = id;
                pontoTuristico.Nome = table.Rows[0][0].ToString();
                pontoTuristico.Endereco = new Endereco();
                pontoTuristico.Endereco.Id = Convert.ToInt32(table.Rows[0][1].ToString());
                pontoTuristico.Endereco.Logradouro = table.Rows[0][2].ToString();
                pontoTuristico.Endereco.Numero = Convert.ToInt16(table.Rows[0][4].ToString());
                pontoTuristico.Endereco.Cidade = new Cidade();
                pontoTuristico.Endereco.Cidade.Id = Convert.ToInt32(table.Rows[0][5].ToString());
                pontoTuristico.Endereco.Cidade.Nome = table.Rows[0][6].ToString();
                pontoTuristico.Endereco.Cidade.Estado = new Estado();
                pontoTuristico.Endereco.Cidade.Estado.Id = Convert.ToInt32(table.Rows[0][7].ToString());
                pontoTuristico.Endereco.Cidade.Estado.Nome = table.Rows[0][8].ToString();
            }
            return pontoTuristico;
        }
        public bool Alterar(PontoTuristico pontoturisco)
        {
            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = "UPDATE pontos_turisticos SET id_endereco = @ID_ENDERECO,nome = @NOME WHERE id = @ID";
            command.Parameters.AddWithValue("@ID_ENDERECO",pontoturisco.IdEndereco);
            command.Parameters.AddWithValue("@NOME", pontoturisco.Nome);
            command.Parameters.AddWithValue("@ID", pontoturisco.Id);
            return command.ExecuteNonQuery() == 1;
        }
        public List<PontoTuristico> ObterTodosParaJSON(string start, string length)
        {
            List<PontoTuristico> pontosturisticos = new List<PontoTuristico>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT pt.id AS 'id',pt.nome AS 'ptnome',e.logradouro AS 'logradouro',e.numero AS 'numero',c.id AS 'cid',c.nome AS 'cnome',es.id AS 'esid',es.nome AS 'esnome' FROM pontos_turisticos
            JOIN enderecos e ON(e.id = pt.id_endereco)
            JOIN cidades c ON (c.id = e.id_cidade)
            JOIN estados es ON (es.id = c.id_estado)           
            WHERE pt.id = @ID
            ORDER BY logradouro OFFSET " + start + " ROWS FETCH NEXT " + length + " ROWS ONLY ";
            DataTable tabela = new DataTable();
            tabela.Load(command.ExecuteReader());
            foreach (DataRow linha in tabela.Rows)
            {
                PontoTuristico pontoturistico = new PontoTuristico();
                pontoturistico.Id = Convert.ToInt32(tabela.Rows[0]["id"].ToString());
                pontoturistico.Nome = tabela.Rows[0]["ptnome"].ToString();
                pontoturistico.Endereco = new Endereco();
                pontoturistico.Endereco.Id = Convert.ToInt32(tabela.Rows[0][1].ToString());
                pontoturistico.Endereco.Logradouro = tabela.Rows[0]["logradouro"].ToString();
                pontoturistico.Endereco.Numero = Convert.ToInt16(tabela.Rows[0]["numero"].ToString());
                pontoturistico.Endereco.Cidade = new Cidade();
                pontoturistico.Endereco.Cidade.Id = Convert.ToInt32(tabela.Rows[0]["cid"].ToString());
                pontoturistico.Endereco.Cidade.Nome = tabela.Rows[0]["cnome"].ToString();
                pontoturistico.Endereco.Cidade.Estado = new Estado();
                pontoturistico.Endereco.Cidade.Estado.Id = Convert.ToInt32(tabela.Rows[0]["esid"].ToString());
                pontoturistico.Endereco.Cidade.Estado.Nome = tabela.Rows[0]["esnome"].ToString();

                pontosturisticos.Add(pontoturistico);
            }
            return pontosturisticos;
        }

    }
}
