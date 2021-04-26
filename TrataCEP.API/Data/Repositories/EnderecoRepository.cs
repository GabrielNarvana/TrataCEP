using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TrataCEP.API.Data.Entities;
using TrataCEP.API.Data.Repositories.Interfaces;
using TrataCEP.API.Helpers;
using TrataCEP.API.Infra;
using TrataCEP.API.Infra.Interface;

namespace TrataCEP.API.Data.Repositories
{
    public class EnderecoRepository : DatabaseReaderBase, IEnderecoRepository
    {
        private readonly IPostgressConnection _postgressConnection;
        private readonly IConfiguration _configuration;
     
        public EnderecoRepository(IPostgressConnection postgresConnection, IConfiguration configuration)
        {
            _postgressConnection = postgresConnection;
            _configuration = configuration;
        }

        public Endereco GetByCEP(string CEP)
        {
            var query = Task.Run(() => ReadResource("EnderecoSelectByCEP")).Result;
            using (NpgsqlConnection conexao = _postgressConnection.CreateConnection())
            {
                try
                {
                    var command = new NpgsqlCommand(query.ToString(), conexao);
                    command.CommandText = query.ToString();
                    command.Parameters.Add(new NpgsqlParameter("Cep", CEP));
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        Endereco endereco = new Endereco
                        {
                            Id = Int32.Parse(reader["id"].ToString()),
                            DataCriacao = DateTime.Parse(reader["datacriacao"].ToString()),
                            CEP = reader["CEP"].ToString(),
                            Logradouro = reader["logradouro"].ToString(),
                            Complemento = reader["complemento"].ToString(),
                            Bairro = reader["bairro"].ToString(),
                            Localidade = reader["localidade"].ToString(),
                            UF = reader["uf"].ToString(),
                            IBGE = Int32.Parse(reader["ibge"].ToString()),
                            GIA = Int32.Parse(reader["gia"].ToString()),
                            DDD = Int32.Parse(reader["ddd"].ToString()),
                            Siafi = Int32.Parse(reader["siafi"].ToString())
                        };

                        reader.Close();
                        return endereco;
                    }
                    else return null;
                }
                catch (Exception ex)
                {
                    LogHelper.LogFile(ex, Process.GetCurrentProcess().ProcessName);
                    throw ex;
                }
            }
        }

        public int Insert(Endereco endereco)
        {
            var query = Task.Run(() => ReadResource("EnderecoInsert")).Result;
            using (NpgsqlConnection conexao = _postgressConnection.CreateConnection())
            {
                try
                {
                    var command = new NpgsqlCommand(query.ToString(), conexao);

                    command.CommandText = query.ToString();
                    command.Parameters.Add(new NpgsqlParameter("DataCriacao", DateTime.Now));
                    command.Parameters.Add(new NpgsqlParameter("CEP", endereco.CEP));
                    command.Parameters.Add(new NpgsqlParameter("Logradouro", endereco.Logradouro));
                    command.Parameters.Add(new NpgsqlParameter("Complemento", endereco.Complemento));
                    command.Parameters.Add(new NpgsqlParameter("Bairro", endereco.Bairro));
                    command.Parameters.Add(new NpgsqlParameter("Localidade", endereco.Localidade));
                    command.Parameters.Add(new NpgsqlParameter("UF", endereco.UF));
                    command.Parameters.Add(new NpgsqlParameter("IBGE", endereco.IBGE));
                    command.Parameters.Add(new NpgsqlParameter("GIA", endereco.GIA));
                    command.Parameters.Add(new NpgsqlParameter("DDD", endereco.DDD));
                    command.Parameters.Add(new NpgsqlParameter("Siafi", endereco.Siafi));

                    int Id = (int)command.ExecuteScalar();
                    if (Id != 0)
                    {
                        return Id;
                    }
                    else 
                        return 0;
                }
                catch (Exception ex)
                {
                    LogHelper.LogFile(ex, Process.GetCurrentProcess().ProcessName);
                    throw ex;
                }
            }
        }
    }
}
