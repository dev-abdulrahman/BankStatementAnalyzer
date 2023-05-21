using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class TimeSlotRepository : GenericRepository<BankStatementAnalyzerContext, TimeSlot>, ITimeSlotRepository
    {
        public TimeSlotRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}