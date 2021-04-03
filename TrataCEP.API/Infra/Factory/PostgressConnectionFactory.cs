using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrataCEP.API.Infra.Interface;
using Npgsql;

namespace TrataCEP.API.Infra.Factory
{
    public class PostgressConnectionFactory : IPostgressConnection
    {
        private readonly string _connectionString;
        public PostgressConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public NpgsqlConnection CreateConnection()
        {
            var conn = new NpgsqlConnection(_connectionString);
            conn.Open();
            return conn;
        }
    }
}
