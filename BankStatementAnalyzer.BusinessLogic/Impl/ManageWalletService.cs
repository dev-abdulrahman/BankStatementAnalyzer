using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using System;
using System.Linq;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class ManageWalletService : IManageWalletService
    {
        private readonly ISystemSettingService systemSettingService;
        private readonly ICustomerService customerService;
        private readonly IWalletService walletService;

        public ManageWalletService(ISystemSettingService systemSettingService,
            ICustomerService customerService,
            IWalletService walletService)
        {
            this.walletService = walletService;
            this.customerService = customerService;
            this.systemSettingService = systemSettingService;
        }

        public Tuple<bool, string> AssignWalletPointsIfOrderDelivered(string friendUID, int companyId)
        {
            var points = systemSettingService.FindBy(x => x.SettingType == SettingType.ReferralPoints).FirstOrDefault();
            if (points != null)
            {
                Wallet wallet = new Wallet
                {
                    Amount = Convert.ToInt32(points.Value),
                    CustomerId = friendUID,
                    Transactiontype = Transactiontype.CR,
                    TransactionDate = DateTime.Now,
                    TransactionDetail = "Refferal Points"
                };

                walletService.Add(wallet);
                walletService.Save();

                return new Tuple<bool, string>(true, "Successfully added points.");
            }

            return new Tuple<bool, string>(false, "Points not configured.");
        }

        public decimal GetWalletPoints(string customerUID)
        {
            var sysVal = systemSettingService.FindBy(x => x.SettingType == SettingType.WalletPointValue).FirstOrDefault();

            var onePointToRs = sysVal == null ? 1 : Convert.ToDecimal(sysVal.Value);

            var value = walletService.FindBy(x => x.CustomerId == customerUID).ToList();

            var response = value.Count() > 0 ? value.Sum(x => x.Amount) : 0;

            return (response * onePointToRs);
        }
    }
}
