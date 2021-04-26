using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace TrataCEP.API.Helpers
{
    public static class LogHelper
    {
        public static void LogFile(Exception ex, string function)
        {
            DateTime date = DateTime.Now;
            string path = "C:\\Logs\\ERROR_TrataCEP.txt";
            string logFormat = "FUNCTION: {0} | DATA: {1} | EXCEPTION: {2}{3}";
            System.IO.File.AppendAllText(path, String.Format(logFormat, function, date, ex.Message.ToString(), Environment.NewLine));        }
    }
}
