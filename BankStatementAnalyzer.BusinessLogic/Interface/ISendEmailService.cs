using BankStatementAnalyzer.Models;
using System;
using System.Threading.Tasks;

namespace BankStatementAnalyzer.BusinessLogic.Interface
{
    public interface ISendEmailService
    {
        Task<Tuple<bool, Exception>> SendAdminEmail(Customer user, Order order, DeliveryBoy deliveryBoy, string url, string subject, string vendorAddress = null);
    }
}