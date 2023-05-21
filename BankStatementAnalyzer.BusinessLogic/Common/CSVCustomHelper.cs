using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankStatementAnalyzer.BusinessLogic.Common
{
   public class CSVCustomHelper
    {
        public byte[] GetFileBytesByModel(IEnumerable<Object> model)
        {
            var fileBytes = Enumerable.Empty<byte>().ToArray();

            using (var ms = new MemoryStream())
            {
                using (var writer = new StreamWriter(ms))
                {
                    using (var csv = new CsvHelper.CsvWriter(writer))
                    {
                        csv.WriteRecords(model);
                    }
                }

                fileBytes = ms.ToArray();
            }

            return fileBytes;
        }
    }
}
