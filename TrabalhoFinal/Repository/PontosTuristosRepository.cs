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
        public List<Pacote> ObterTodosPacotesPeloIdPacote(int idPacote)
        {
            List<Pacote> pacotes = new List<Pacote>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT pt.id, p.id, p.nome, p.valor, id_endereco, pt.nome,  FROM pontos_turisticos pt JOIN
                                  pacotes p ON(pt.id = p.id_pacote) WHERE pt.id_pacote = @ID_PACOTE";

            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            foreach (DataRow line in table.Rows)
            {
                Pacote pacote = new Pacote()
                {
                    Id = Convert.ToInt32(line["p.id"].ToString()),
                    Nome = line["p.nome"].ToString()

                };
                pacotes.Add(pacote);

            }
            return pacotes;
        }

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
                    Nome = linha[1].ToString()
                };
                pontosTuristicos.Add(pontoTuristico);
            }
            return pontosTuristicos;
        }
        public int Cadastrar(PontoTuristico pontosturisticos)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = "INSERT INTO pontos_turisticos (id_endereco,nome)OUTPUT INSERTED.ID VALUES(@ID_ENDERECO,@NOME)";
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
            command.CommandText = @"SELECT pt.id,pt.nome AS 'ponto_turistico', pt.id_endereco, ed.logradouro AS 'logradouro', ed.numero AS 'numero', cidades.nome AS 'cidadenome', cidades.id AS 'id_cidade', cidades.id_estado AS 'idestado_cidade',estados.id AS 'idestado', estados.nome AS 'nome_estado' FROM pontos_turisticos pt
            JOIN enderecos ed ON(ed.id = pt.id_endereco) 
            JOIN cidades ci ON pontos_turisticos( ci.id =  pt.id_endereco)
            JOIN estados es ON pontos_turisticos (es.id = pt.id_endereco)
            WHERE pt.id = @ID";
            command.Parameters.AddWithValue("@ID", id);

            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            if (table.Rows.Count == 1)
            {
                pontoTuristico = new PontoTuristico();
                pontoTuristico.Id = id;
                pontoTuristico.Nome = table.Rows[0]["ponto_turistico"].ToString();
                pontoTuristico.IdEndereco = Convert.ToInt32(table.Rows[0][1].ToString());
                pontoTuristico.Endereco = new Endereco();
                pontoTuristico.Endereco.Id = Convert.ToInt32(table.Rows[0][1].ToString());
                pontoTuristico.Endereco.Logradouro = table.Rows[0]["logradouro"].ToString();
                pontoTuristico.Endereco.Numero = Convert.ToInt16(table.Rows[0]["numero"].ToString());
                pontoTuristico.Endereco.Complemento = table.Rows[0]["complemento"].ToString();
                pontoTuristico.Endereco.Referencia = table.Rows[0]["referencia"].ToString();
                pontoTuristico.Endereco.Cidade = new Cidade();
                pontoTuristico.Endereco.Cidade.Id = Convert.ToInt32(table.Rows[0]["id_cidade"].ToString());
                pontoTuristico.Endereco.Cidade.Nome = table.Rows[0]["cidadenome"].ToString();
                pontoTuristico.Endereco.Cidade.IdEstado = Convert.ToInt32(table.Rows[0]["idestado_cidade"].ToString());
                pontoTuristico.Endereco.Cidade.Estado = new Estado();
                pontoTuristico.Endereco.Cidade.Estado.Id = Convert.ToInt32(table.Rows[0]["idestado"].ToString());
                pontoTuristico.Endereco.Cidade.Estado.Nome = table.Rows[0]["nome_Estado"].ToString();
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
            command.CommandText = @"SELECT ponto_turistico.id,ponto_turistico.nome AS 'pontoturistico_nome', ponto_turistico.id_endereco, endereco.logradouro AS 'logradouro', endereco.numero AS 'numero', cidades.nome AS 'cidadenome', cidades.id AS 'id_cidade', cidades.id_estado AS 'idestado_cidade',estados.id AS 'idestado', estados.nome AS 'nome_estado' FROM pontos_turisticos pt
            INNER JOIN enderecos ed ON (enderecos.id = ponto_turistico.id)
            INNER JOIN cidades ci ON (cidades.id =  ponto_turistico.id)
            INNER JOIN estados es ON (estados.id = ponto_turistico.id) 
            ORDER BY p.nome OFFSET " + start + " ROWS FETCH NEXT " + length + " ROWS ONLY ";
            DataTable tabela = new DataTable();
            tabela.Load(command.ExecuteReader());
            foreach (DataRow linha in tabela.Rows)
            {
                PontoTuristico pontoturistico = new PontoTuristico()
                {
                    Id = Convert.ToInt32(linha[0].ToString()),
                    IdEndereco = Convert.ToInt32(linha[1].ToString()),
                    Nome = linha["pontoturistico_nome"].ToString(),
                    Endereco = new Endereco()
                    {
                        Logradouro = linha["logradouro"].ToString(),
                        Numero = Convert.ToInt16(linha["numero"].ToString()),
                        Complemento = linha["complemento"].ToString(),
                        Referencia = linha["referencia"].ToString(),
                        Cidade = new Cidade()
                        {
                            Id = Convert.ToInt32(linha["id_cidade"].ToString()),
                            Nome = linha["cidadenome"].ToString(),
                            IdEstado = Convert.ToInt32(linha["idestado_cidade"].ToString()),
                            Estado = new Estado()
                            {
                                Id = Convert.ToInt32(linha["idestado"].ToString()),
                                Nome = linha["nome_estado"].ToString(),
                            }
                        }   
                    }
                };
                pontosturisticos.Add(pontoturistico);
            }
            return pontosturisticos;
        }

    }
}
