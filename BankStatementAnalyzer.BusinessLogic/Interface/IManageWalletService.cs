using System;

namespace BankStatementAnalyzer.BusinessLogic.Interface
{
    public interface IManageWalletService
    {
        Tuple<bool, string> AssignWalletPointsIfOrderDelivered(string friendUID, int companyId);

        decimal GetWalletPoints(string customerUID);
    }
}