using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrataCEP.API.Data.Entities;

namespace TrataCEP.API.Data.Repositories.Interfaces
{
    public interface IEnderecoRepository
    {
        Endereco GetByCEP(string CEP);
        int Insert(Endereco endereco);
    }
}
