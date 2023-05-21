using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class FileMasterRepository :
        GenericRepository<BankStatementAnalyzerContext, File>, IFileMasterRepository
    {
        public FileMasterRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}