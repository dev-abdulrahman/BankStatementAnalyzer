using BankStatementAnalyzer.BusinessLogic.Common;
using System.IO;
using System.Web;
using File = BankStatementAnalyzer.Models.File;

namespace BankStatementAnalyzer.BusinessLogic.Interface
{
    public interface IFileMasterService : IGenericService<File>
    {
        string Upload(HttpPostedFileBase file, string filePath, string fileName);

        string SaveFile(string inputFileName, string path, Stream stream);

        bool DeleteFile(string fullPath);

        byte[] ConvertToZip(byte[] stream, string filename);
    }
}