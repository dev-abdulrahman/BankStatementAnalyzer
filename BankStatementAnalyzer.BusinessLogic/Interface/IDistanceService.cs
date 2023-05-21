using BankStatementAnalyzer.Models;
using System.Linq;

namespace BankStatementAnalyzer.BusinessLogic.Interface
{
    public interface IDistanceService
    {
        bool GetServiceAvailability(IQueryable<Store> stores, double lat, double longitude);

        bool GetService(double lat, double longitude);
    }
}
