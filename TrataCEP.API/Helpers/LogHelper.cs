using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace TrataCEP.API.Helpers
{
    public class LogHelper
    {
        private readonly IConfiguration _configuration;
        public LogHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void LogFile(Exception ex, string function)
        {
            DateTime date = DateTime.Now;
            string path = _configuration.GetSection("LogsFile").ToString();
            string logFormat = "FUNCTION: {0} | DATA: {1} | EXCEPTION: {2}{3}";
            System.IO.File.AppendAllText(path, String.Format(logFormat, function, date, ex.Message.ToString(), Environment.NewLine));

        }
    }
}
