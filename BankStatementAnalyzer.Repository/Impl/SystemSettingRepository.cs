using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class SystemSettingRepository : GenericRepository<BankStatementAnalyzerContext, SystemSetting>, ISystemSettingRepository
    {
        public SystemSettingRepository(BankStatementAnalyzerContext ecommerceContext) : base(ecommerceContext)
        {

        }
    }
}