using BankStatementAnalyzer.BusinessLogic.Common;
using BankStatementAnalyzer.Models;
using System;

namespace BankStatementAnalyzer.BusinessLogic.Interface
{
    public interface IPackageService : IGenericService<Package>
    {
        Tuple<decimal, decimal> GetDiscount(decimal rate, decimal urgentRate, decimal discount, DiscountType discountType);
    }
}