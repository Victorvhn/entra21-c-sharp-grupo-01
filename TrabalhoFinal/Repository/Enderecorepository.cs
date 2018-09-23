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
    public class EnderecoRepository
    {

        public List<Endereco> ObterTodosPorJSON()
        {
            List<Endereco> enderecos = new List<Endereco>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT e.id, e.cep, e.logradouro, e.numero, e.complemento, e.referencia, c.id, c.nome, es.id, es.nome FROM enderecos e 
            JOIN cidades c ON (c.id = e.id_cidade)
            JOIN estados es ON (es.id = c.id_estado )WHERE e.ativo = 1";

            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            foreach (DataRow line in table.Rows)
            {
                Endereco endereco = new Endereco();
                {
                    endereco.Id = Convert.ToInt32(line[0].ToString());
                    endereco.Cep = line[1].ToString();
                    endereco.Logradouro = line[2].ToString();
                    endereco.Numero = Convert.ToInt16(line[3].ToString());
                    endereco.Complemento = line[4].ToString();
                    endereco.Referencia = line[5].ToString();
                    endereco.Cidade = new Cidade();
                    endereco.Cidade.Id = Convert.ToInt32(line[6].ToString());
                    endereco.Cidade.Nome = line[7].ToString();
                    endereco.Cidade.Estado = new Estado();
                    endereco.Cidade.Estado.Id = Convert.ToInt32(line[8].ToString());
                    endereco.Cidade.Estado.Nome = line[9].ToString();

                };
                enderecos.Add(endereco);
            }
            return enderecos;
        }

        public List<Endereco> ObterTodosParaSelect()
        {
            List<Endereco> enderecos = new List<Endereco>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT e.id, e.cep, e.logradouro, e.numero, e.complemento, e.referencia, e.id_cidade FROM enderecos e 
            JOIN cidades c ON (c.id = e.id_cidade)
            JOIN estados es ON (e.id = c.id_estado)";
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            foreach (DataRow linha in table.Rows)
            {
                Endereco endereco = new Endereco()
                {
                    Id = Convert.ToInt32(linha[0].ToString()),
                    Cep = linha[1].ToString(),
                    Logradouro = linha[2].ToString(),
                    Numero = Convert.ToInt16(linha[3].ToString()),
                    Complemento = linha[4].ToString(),
                    Referencia = linha[5].ToString(),
                    IdCidade = Convert.ToInt32(linha[6].ToString()),
                };
                enderecos.Add(endereco);
            }
            return enderecos;
        }

        public List<Endereco> ObterTodosParaSelect2EmLine()
        {
            List<Endereco> enderecos = new List<Endereco>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT pontos_turisticos.nome, cidades.nome, enderecos.logradouro ,enderecos.numero FROM enderecos 
INNER JOIN pontos_turisticos ON pontos_turisticos.id = enderecos.id 
INNER JOIN cidades ON cidades.id = enderecos.id_cidade";
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            foreach (DataRow linha in table.Rows)
            {
                PontoTuristico pontoTuristico = new PontoTuristico();
                pontoTuristico.Nome = linha[0].ToString();

                Cidade cidade = new Cidade();
                cidade.Nome = linha[1].ToString();

                Endereco endereco = new Endereco();
                endereco.Logradouro = linha[2].ToString();
                endereco.Numero =  Convert.ToInt16(linha[3].ToString());
            }
            return enderecos;
        }

        public List<Endereco> ObterTodosParaJSON(string start, string length, string search, string orderColumn, string orderDir)
        {
            List<Endereco> enderecos = new List<Endereco>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT e.id AS 'id', e.id_cidade AS 'eidcidade' , e.cep AS 'cep', e.logradouro AS 'logradouro', 
                                    e.numero AS 'numero', e.complemento AS 'complemento', e.referencia AS 'referencia', 
                                    c.id AS 'cidadeid', c.nome AS 'cidadenome', es.id AS 'estadoid', es.nome AS 'nomeestado' FROM enderecos e 
                                    JOIN cidades c ON(c.id = e.id_cidade )
                                    JOIN estados es ON (es.id = c.id_estado ) 
                                    WHERE e.ativo = 1 AND ((e.cep LIKE @SEARCH) OR (e.logradouro LIKE @SEARCH) OR (c.nome LIKE @SEARCH))
                                    ORDER BY  "+ orderColumn + " " + orderDir + " OFFSET " + start + " ROWS FETCH NEXT " + length + " ROWS ONLY ";
            command.Parameters.AddWithValue("@SEARCH", search);
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());

            foreach (DataRow line in table.Rows)
            {
                Endereco endereco = new Endereco();
                endereco.Id =  Convert.ToInt32(table.Rows[0]["id"].ToString());
                endereco.IdCidade = Convert.ToInt32(table.Rows[0]["eidcidade"].ToString());
                endereco.Cep = table.Rows[0]["cep"].ToString();
                endereco.Logradouro = table.Rows[0]["logradouro"].ToString();
                endereco.Numero = Convert.ToInt16(table.Rows[0]["numero"].ToString());
                endereco.Complemento = table.Rows[0]["complemento"].ToString();
                endereco.Referencia = table.Rows[0]["referencia"].ToString();
                endereco.Cidade = new Cidade();
                endereco.Cidade.Id = Convert.ToInt32(table.Rows[0]["cidadeid"].ToString());
                endereco.Cidade.Nome = table.Rows[0]["cidadenome"].ToString();
                endereco.Cidade.Estado = new Estado();
                endereco.Cidade.Estado.Id = Convert.ToInt32(table.Rows[0]["estadoid"].ToString());
                endereco.Cidade.Estado.Nome = table.Rows[0]["nomeestado"].ToString();

                enderecos.Add(endereco);
            }
            return enderecos;
        }

        public int ContabilizarEnderecosFiltrados(string search)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT COUNT(e.id) FROM enderecos e
                                    JOIN cidades c ON (c.id = e.id_cidade )
                                    JOIN estados es ON (es.id = c.id_estado )
                                    WHERE e.ativo = 1 AND ((e.cep LIKE @SEARCH) OR (e.logradouro LIKE @SEARCH) OR (c.nome LIKE @SEARCH) )";
            command.Parameters.AddWithValue("@SEARCH", search);
            return Convert.ToInt32(command.ExecuteScalar().ToString());
        }

        public int Cadastrar(Endereco endereco)
        {
            string complemento = "";
            string complementoText = "";
            string referencia = "";
            string referenciaText = "";
            if (endereco.Complemento != null)
            {
                complemento = " complemento,";
                complementoText = " @COMPLEMENTO,";
            }
            else
            {
                complemento = "";
                complementoText = "";
            }

            if (endereco.Referencia != null)
            {
                referencia = " referencia,";
                referenciaText = " @REFERENCIA,";
            }
            else
            {
                referencia = "";
                referenciaText = "";
            }
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"INSERT INTO enderecos(cep, logradouro, numero," + complemento + "" + referencia + " id_cidade) OUTPUT INSERTED.ID VALUES (@CEP, @LOGRADOURO, @NUMERO," + complementoText + " " + referenciaText + " @ID_CIDADE)";
            command.Parameters.AddWithValue("@CEP", endereco.Cep);
            command.Parameters.AddWithValue("@LOGRADOURO", endereco.Logradouro);
            command.Parameters.AddWithValue("@NUMERO", endereco.Numero);
            if (endereco.Complemento != null)
            {
                command.Parameters.AddWithValue("@COMPLEMENTO", endereco.Complemento);
            }
            if (endereco.Referencia != null)
            {
                command.Parameters.AddWithValue("@REFERENCIA", endereco.Referencia);
            }
            command.Parameters.AddWithValue("@ID_CIDADE", endereco.IdCidade);
            int id = Convert.ToInt32(command.ExecuteScalar().ToString());
            return id;

        }

        public bool Alterar(Endereco endereco)
        {
            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = "UPDATE enderecos SET cep = @CEP, logradouro = @LOGRADOURO, numero = @NUMERO, complemento = @COMPLEMENTO, referencia = @REFERENCIA, id_cidade = @ID_CIDADE WHERE id = @ID";
            command.Parameters.AddWithValue("@CEP", endereco.Cep);
            command.Parameters.AddWithValue("@LOGRADOURO", endereco.Logradouro);
            command.Parameters.AddWithValue("@NUMERO", endereco.Numero);
            command.Parameters.AddWithValue("@COMPLEMENTO", endereco.Complemento);
            command.Parameters.AddWithValue("@REFERENCIA", endereco.Referencia);
            command.Parameters.AddWithValue("@ID_CIDADE", endereco.IdCidade);
            command.Parameters.AddWithValue("@ID", endereco.Id);
            return command.ExecuteNonQuery() == 1;

        }

        public bool Excluir(int id)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = "UPDATE enderecos SET ativo = 0 WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            return command.ExecuteNonQuery() == 1;
        }

        public Endereco ObterPeloId(int id)
        {
            Endereco endereco = null;
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT e.id_cidade AS 'eidcidade' , e.cep AS 'cep', e.logradouro AS 'logradouro', e.numero AS 'numero', e.complemento AS 'complemento', 
e.referencia AS 'referencia', c.id AS 'cidadeid', c.nome AS 'cidadenome', es.id AS 'estadoid', es.nome AS 'nomeestado' FROM enderecos e 
                                  JOIN cidades c ON(c.id = e.id_cidade )
                                  JOIN estados es ON (es.id = c.id_estado ) WHERE e.id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            if (table.Rows.Count == 1)
            {

                endereco = new Endereco();
                endereco.Id = id;
                endereco.IdCidade = Convert.ToInt32(table.Rows[0]["eidcidade"].ToString());
                endereco.Cep = table.Rows[0]["cep"].ToString();
                endereco.Logradouro = table.Rows[0]["logradouro"].ToString();
                endereco.Numero = Convert.ToInt16(table.Rows[0]["numero"].ToString());
                endereco.Complemento = table.Rows[0]["complemento"].ToString();
                endereco.Referencia = table.Rows[0]["referencia"].ToString();
                endereco.Cidade = new Cidade();
                endereco.Cidade.Id = Convert.ToInt32(table.Rows[0]["cidadeid"].ToString());
                endereco.Cidade.Nome = table.Rows[0]["cidadenome"].ToString();
                endereco.Cidade.Estado = new Estado();
                endereco.Cidade.Estado.Id = Convert.ToInt32(table.Rows[0]["estadoid"].ToString());
                endereco.Cidade.Estado.Nome = table.Rows[0]["nomeestado"].ToString();
                
            }
            return endereco;

        }

        public int ContabilizarEnderecos()
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = @"SELECT COUNT(id) FROM enderecos WHERE ativo = 1";
            return Convert.ToInt32(command.ExecuteScalar().ToString());
        }

    }
}