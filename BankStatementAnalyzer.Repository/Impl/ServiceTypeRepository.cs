using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class ServiceTypeRepository : GenericRepository<BankStatementAnalyzerContext, ServiceType>, IServiceTypeRepository
    {
        public ServiceTypeRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}