using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class EmailRepository : GenericRepository<BankStatementAnalyzerContext, Email>, IEmailRepository
    {
        public EmailRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}