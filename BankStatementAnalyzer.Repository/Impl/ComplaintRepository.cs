using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class ComplaintRepository : GenericRepository<BankStatementAnalyzerContext, Complaint>, IComplaintRepository
    {
        public ComplaintRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}