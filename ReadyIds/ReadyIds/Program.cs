using System;
using System.Configuration;
using System.IO;
using System.Text;
using Rabbit.Uniquators;

namespace ReadyIds
{
    class Program
    {
        static void Main(string[] args)
        {
            var idBase = (IdBase)Enum.Parse(typeof(IdBase), ConfigurationManager.AppSettings["IdBase"]);
            var startFrom = int.Parse(ConfigurationManager.AppSettings["StartFrom"]);
            var sb = new StringBuilder();

            const int idLengthMax = 10;
            var idLength = 0;
            var count = (UInt64)startFrom;

            while (idLength <= idLengthMax)
            {
                var newId = NumberToString.Convert(count, idBase);
                count += 1;

                if (sb.Length == 0)
                {
                    idLength = newId.Length;
                }

                if (newId.Length > idLength)
                {
                    File.WriteAllText(string.Format("{0}_{1}.txt", idBase, idLength), sb.ToString());
                    Console.WriteLine("Done for id length " + idLength);
                    idLength = newId.Length;
                    sb.Clear();
                }

                sb.AppendLine(newId);
            }

            Console.WriteLine("Done");
        }
    }
}
