using Rabbit.Uniquators;
using System;
using System.Configuration;
using System.IO;

namespace ReadyIds
{
    class Program
    {
        static void Main(string[] args)
        {
            var idBase = (IdBase)Enum.Parse(typeof(IdBase), ConfigurationManager.AppSettings["IdBase"]);
            var startFrom = int.Parse(ConfigurationManager.AppSettings["StartFrom"]);
            var idLengthMax = int.Parse(ConfigurationManager.AppSettings["IdLengthMax"]);

            var idLength = 0;
            var count = (UInt64)startFrom;
            StreamWriter streamWriter = null;

            while (idLength <= idLengthMax)
            {
                var newId = NumberToString.Convert(count, idBase);
                count += 1;

                if (idLength != newId.Length)
                {
                    if (streamWriter != null)
                    {
                        streamWriter.Flush();
                        streamWriter.Dispose();
                    }

                    idLength = newId.Length;
                    if (newId.Length > idLength)
                    {
                        Console.WriteLine("Done for id length " + idLength);
                    }

                    var fileName = string.Format("{0}_{1}.txt", idBase, idLength);
                    streamWriter = new StreamWriter(new FileStream(fileName, FileMode.Create));
                }

                if (streamWriter != null)
                {
                    streamWriter.WriteLine(newId);
                }
            }

            Console.WriteLine("Done");
        }
    }
}
