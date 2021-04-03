using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrataCEP.API.Data.Entities
{
    public class Endereco
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string UF { get; set; }
        public Int32? IBGE { get; set; }
        public Int32? GIA { get; set; }
        public Int32? DDD { get; set; }
        public Int32? Siafi { get; set; }
    }
}
