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
        private readonly LogHelper _logHelper;

        public EnderecoRepository(IPostgressConnection postgresConnection, IConfiguration configuration, LogHelper logHelper)
        {
            _postgressConnection = postgresConnection;
            _configuration = configuration;
            _logHelper = logHelper;
        }

        public List<Endereco> GetAll()
        {
            var query = Task.Run(() => ReadResource("EnderecoSelectAll"));
            List<Endereco> endereco = new List<Endereco>();

            using (NpgsqlConnection conexao = _postgressConnection.CreateConnection())
            {
                try
                {
                    var command = new NpgsqlCommand(query.ToString(), conexao);
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            endereco.Add(
                                new Endereco
                                {
                                    CEP = reader["CEP"].ToString(),
                                    Logradouro = reader["Logradouro"].ToString(),
                                    Complemento = reader["Bairro"].ToString(),
                                    Bairro = reader["Bairro"].ToString(),
                                    Localidade = reader["Localidade"].ToString(),
                                    UF = reader["UF"].ToString(),
                                    IBGE = (int?)reader["IBGE"],
                                    GIA = (int?)reader["GIA"],
                                    DD = (int?)reader["DD"],
                                    Siafi = (int?)reader["Siafi"]
                                });
                            return endereco;
                        }
                    }
                    reader.Close();
                    return endereco;
                }
                catch (Exception ex)
                {
                    _logHelper.LogFile(ex, Process.GetCurrentProcess().ProcessName);
                    throw ex;
                }
            }
        }

        public Endereco GetByCEP(string CEP)
        {
            var query = Task.Run(() => ReadResource("EnderecoSelectByCEP"));
            Endereco endereco = new Endereco();

            using (NpgsqlConnection conexao = _postgressConnection.CreateConnection())
            {
                try
                {
                    var command = new NpgsqlCommand(query.ToString(), conexao);
                    command.CommandText = query.ToString();
                    command.Parameters.Add(new NpgsqlParameter("CEP", CEP));
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        endereco = new Endereco
                            {
                                CEP = reader["CEP"].ToString(),
                                Logradouro = reader["Logradouro"].ToString(),
                                Complemento = reader["Bairro"].ToString(),
                                Bairro = reader["Bairro"].ToString(),
                                Localidade = reader["Localidade"].ToString(),
                                UF = reader["UF"].ToString(),
                                IBGE = Int32.Parse(reader["IBGE"].ToString()),
                                GIA = Int32.Parse(reader["GIA"].ToString()),
                                DD = Int32.Parse(reader["DD"].ToString()),
                                Siafi = Int32.Parse(reader["Siafi"].ToString())
                            };
                            return endereco;
                    }
                    reader.Close();
                    return endereco;
                }
                catch (Exception ex)
                {
                    _logHelper.LogFile(ex, Process.GetCurrentProcess().ProcessName);
                    throw ex;
                }
            }
        }

        public int Insert(Endereco endereco)
        {
            var query = Task.Run(() => ReadResource("EnderecoInsert"));

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
                    command.Parameters.Add(new NpgsqlParameter("DD", endereco.DD));
                    command.Parameters.Add(new NpgsqlParameter("Siafi", endereco.Siafi));

                    int rows = command.ExecuteNonQuery();

                    return rows;
                }
                catch (Exception ex)
                {
                    _logHelper.LogFile(ex, Process.GetCurrentProcess().ProcessName);
                    throw ex;
                }
            }
        }
    }
}
