using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;

namespace TrataCEP.API.Infra.Interface
{
    public interface IPostgressConnection
    {
        NpgsqlConnection CreateConnection();

    }
}
