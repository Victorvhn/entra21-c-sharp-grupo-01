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
            command.CommandText = "SELECT e.id, e.cep, e.logradouro, e.numero, e.complemento, e.referencia, c.id, c.nome FROM enderecos e JOIN cidades c ON (e.id_cidade = c.id)";

            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            foreach (DataRow line in table.Rows)
            {
                Endereco endereco = new Endereco()
                {
                    Id = Convert.ToInt32(line["e.id"].ToString()),
                    Cep = line["e.cep"].ToString(),
                    Logradouro = line["e.logradouro"].ToString(),
                    Numero = Convert.ToInt16(line["e.numero"].ToString()),
                    Complemento = line["e.complemento"].ToString(),
                    Referencia = line["e.referencia"].ToString(),
                    Cidade = new Cidade(){

                        Id = Convert.ToInt32(line["c.id"].ToString()),
                        Nome = line["c.nome"].ToString()
                    },


                };
                enderecos.Add(endereco);
            }
            return enderecos;
        }

        public List<Endereco> ObterTodosParaSelect()
        {
            List<Endereco> enderecos = new List<Endereco>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = "SELECT id, cep, logradouro, numero, complemento, referencia, id_cidade FROM enderecos";
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


        public List<Endereco> ObterTodosParaJSON(string start, string length)
        {
            List<Endereco> enderecos = new List<Endereco>();
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = "SELECT e.id, c.id, e.cep, e.logradouro, c.nome FROM enderecos e JOIN cidades c ON(c.id = e.id_cidade) ORDER BY logradouro OFFSET " + start + " ROWS FETCH NEXT " + length + " ROWS ONLY ";

            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());

            foreach (DataRow line in table.Rows)
            {
                Endereco endereco = new Endereco()
                {
                    Id = Convert.ToInt32(line[0].ToString()),
                    IdCidade = Convert.ToInt32(line[1].ToString()),
                    Cep = line[2].ToString(),
                    Logradouro = line[3].ToString(),
                    Cidade = new Cidade()
                    {
                        Id = Convert.ToInt32(line[1].ToString()),
                        Nome = line[4].ToString()
                    }
                };
                enderecos.Add(endereco);
            }
            return enderecos;
        }

        public int Cadastrar(Endereco endereco)
        {
            SqlCommand command = new Conexao().ObterConexao();
            command.CommandText = "INSERT INTO enderecos(cep, logradouro, numero, complemento, referencia, id_cidade) OUTPUT INSERTED.ID VALUES (@CEP, @LOGRADOURO, @NUMERO, @COMPLEMENTO, @REFERENCIA, @ID_CIDADE)";
            command.Parameters.AddWithValue("@CEP", endereco.Cep);
            command.Parameters.AddWithValue("@LOGRADOURO", endereco.Logradouro);
            command.Parameters.AddWithValue("@NUMERO", endereco.Numero);
            command.Parameters.AddWithValue("@COMPLEMENTO", endereco.Complemento);
            command.Parameters.AddWithValue("@REFERENCIA", endereco.Referencia);
            command.Parameters.AddWithValue("@ID_CIDADE", endereco.IdCidade);
            int id = Convert.ToInt32(command.ExecuteScalar().ToString());
            return id;

        }

        public bool Alterar(Endereco endereco)
        {
            SqlCommand command = new Conexao().ObterConexao();

            command.CommandText = "UPDATE enderecos SET cep = @CEP,logradouro = @LOGRADOURO,numero = @NUMERO,complemento = @COMPLEMENTO,referencia = @REFERENCIA, id_cidade = @ID_CIDADE WHERE id = @ID";
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
            command.CommandText = "SELECT e.id_cidade, e.cep, e.logradouro, e.numero, e.complemento, e.referencia, c.id, c.nome FROM enderecos e JOIN cidades c ON(c.id = e.id_cidade) WHERE e.id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            if (table.Rows.Count == 1)
            {

                endereco = new Endereco();
                endereco.Id = id;
                endereco.IdCidade = Convert.ToInt32(table.Rows[0][1].ToString());
                endereco.Cep = table.Rows[0][2].ToString();
                endereco.Logradouro = table.Rows[0][3].ToString();
                endereco.Numero = Convert.ToInt16(table.Rows[0][4].ToString());
                endereco.Complemento = table.Rows[0][5].ToString();
                endereco.Referencia = table.Rows[0][6].ToString();
                endereco.Cidade = new Cidade();
                endereco.Cidade.Id = Convert.ToInt32(table.Rows[0][1].ToString());
                endereco.Cidade.Nome = table.Rows[0][8].ToString();
                
            }
            return endereco;

        }

    }
}