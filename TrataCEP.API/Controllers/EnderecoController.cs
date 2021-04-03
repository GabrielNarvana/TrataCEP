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

namespace TrataCEP.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly LogHelper _logHelper;

        public EnderecoController(IConfiguration configuration, IEnderecoRepository enderecoRepository, LogHelper logHelper)
        {
            _configuration = configuration;
            _enderecoRepository = enderecoRepository;
            _logHelper = logHelper;
        }

        [ActionName("GetById")]
        [HttpGet]
        public IActionResult GetByCEP([FromBody] string CEP)
        {
            try
            {
               var retorno = _enderecoRepository.GetByCEP(CEP);
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                _logHelper.LogFile(ex, Process.GetCurrentProcess().ProcessName);
                return StatusCode(500, ex);
            }
        }
            [ActionName("GetAll")]
            [HttpGet]
            public IActionResult GetAll()
            {
            try
            {
                var retorno = _enderecoRepository.GetAll();
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                _logHelper.LogFile(ex, Process.GetCurrentProcess().ProcessName);
                return StatusCode(500, ex);
            }
        }
        [ActionName("Insert")]
        [HttpPost]
        public IActionResult Insert([FromBody] JObject requestBody)
        {
            try
            {
               Endereco endereco = new Endereco {
                CEP = requestBody.SelectToken("CEP").ToString(),
                Logradouro = requestBody.SelectToken("Logradouro").ToString(),
                Complemento = requestBody.SelectToken("Complemento").ToString(),
                Bairro = requestBody.SelectToken("Bairro").ToString(),
                Localidade = requestBody.SelectToken("Localidade").ToString(),
                UF = requestBody.SelectToken("UF").ToString(),
                IBGE = Convert.ToInt32(requestBody.SelectToken("IBGE").ToString().Trim()),
                GIA = Convert.ToInt32(requestBody.SelectToken("GIA").ToString().Trim()),
                DD = Convert.ToInt32(requestBody.SelectToken("DD").ToString().Trim()),
                Siafi = Convert.ToInt32(requestBody.SelectToken("Siafi").ToString().Trim())
               };
              
                var retorno = _enderecoRepository.Insert(endereco);
                if (retorno != 0)
                {
                    return Ok(retorno);
                }

                else return StatusCode(500, "Ocorreu um erro, veja o log gerado para mais detalhes");
            }
            catch (Exception ex)
            {
                _logHelper.LogFile(ex, Process.GetCurrentProcess().ProcessName);
                return StatusCode(500, ex);
            }
        }
    }
}
