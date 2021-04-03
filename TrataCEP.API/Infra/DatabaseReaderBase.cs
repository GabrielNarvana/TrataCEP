using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace TrataCEP.API.Infra
{
    public abstract class DatabaseReaderBase
    {
        protected string ReadResource(string name)
        {
            var assembly = Assembly.GetExecutingAssembly();
            string resourcePath = name;
            var format = $"TrataCEP.API.Data.Queries.{name}.sql";
            if (!name.StartsWith(nameof(format)))
            {
                resourcePath = assembly.GetManifestResourceNames()
                    .Single(str => str.EndsWith(format));
            }

            using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
        protected byte[] ReadResourceBytes(string name)
        {
            var assembly = Assembly.GetExecutingAssembly();
            string resourcePath = name;
            var format = $"TrataCEP.API.Data.Queries.{name}.sql";
            if (!name.StartsWith(nameof(format)))
            {
                resourcePath = assembly.GetManifestResourceNames()
                    .Single(str => str.EndsWith(format));
            }

            using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
            using (StreamReader reader = new StreamReader(stream))
            {
                using (var memoryStream = new MemoryStream())
                {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
                }
            }
        }
    }
}
