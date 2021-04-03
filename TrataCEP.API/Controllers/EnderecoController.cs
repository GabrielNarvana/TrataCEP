using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using TrataCEP.API.Data.Entities;
using TrataCEP.API.Data.Repositories.Interfaces;
using System.IO;
using TrataCEP.API.Helpers;
using System.Text.Json;

namespace TrataCEP.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IEnderecoRepository _enderecoRepository;

        public EnderecoController(IConfiguration configuration, IEnderecoRepository enderecoRepository)
        {
            _configuration = configuration;
            _enderecoRepository = enderecoRepository;
        }

        [HttpGet]
        [Route("GetByCEP")]
        public IActionResult GetByCEP([FromBody] JsonElement requestBody)
        {
            try
            {
               string CEP = (requestBody.GetProperty("cep").ToString()).Replace("-", "");
               var retorno = _enderecoRepository.GetByCEP(CEP);
                if(retorno == null)
                {
                    return Ok();
                }
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [Route("Insert")]
        public IActionResult Insert([FromBody] JsonElement requestBody)
        {
            try
            {
                Endereco endereco = new Endereco {
                CEP = (requestBody.GetProperty("cep").ToString()).Replace("-", ""),
                Logradouro = requestBody.GetProperty("logradouro").ToString(),
                Complemento = requestBody.GetProperty("complemento").ToString(),
                Bairro = requestBody.GetProperty("bairro").ToString(),
                Localidade = requestBody.GetProperty("localidade").ToString(),
                UF = requestBody.GetProperty("uf").ToString(),
                IBGE = Int32.Parse(requestBody.GetProperty("ibge").ToString()),
                GIA = Int32.Parse(requestBody.GetProperty("gia").ToString()),
                DDD = Int32.Parse(requestBody.GetProperty("ddd").ToString()),
                Siafi = Int32.Parse(requestBody.GetProperty("siafi").ToString())
                };
              
                var retorno = _enderecoRepository.Insert(endereco);
                if(retorno == 0)
                {
                    return StatusCode(500, "Ocorreu um erro, veja o log gerado para mais detalhes");
                }
                return Ok(retorno);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
