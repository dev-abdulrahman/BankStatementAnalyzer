using BankStatementAnalyzer.BusinessLogic.ViewModels;
using BankStatementAnalyzer.Models;
using System.Collections.Generic;

namespace BankStatementAnalyzer.BusinessLogic.Interface
{
    public interface ICartManageService
    {
        List<CartViewModel> Get(List<Order> cartItems, int customerID, int customerId, string url);
    }
}