using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.WebUI.ViewModels.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BankStatementAnalyzer.WebUI.Controllers.api
{
    public class WalletController : ApiController
    {
        private readonly IWalletService walletService;
        private readonly ICompanyService companyService;
        private readonly ICustomerService customerService;

        public WalletController(IWalletService walletService,
            ICompanyService companyService,
            ICustomerService customerService)
        {
            this.walletService = walletService;
            this.companyService = companyService;
            this.customerService = customerService;
        }

        [HttpGet]
        [Route("api/Wallet/Get")]
        public HttpResponseMessage Get(string customerId, string companyKey)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { result = false, message = "uid cannot be null" });
            }

            var company = companyService.FindBy(x => x.Key == companyKey).FirstOrDefault();

            if (company == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { result = false, message = "Company details not found." });
            }

            if (customerService.FindBy(x => x.UID == customerId).FirstOrDefault() == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { result = false, message = "customer details not found." });
            }

            List<WalletVM> wallets = new List<WalletVM>();

            var dbWalletTransactions = walletService.FindBy(x => x.CustomerId == customerId && x.Status == true).ToList();

            foreach (var dbWalletTransaction in dbWalletTransactions)
            {
                WalletVM wallet = new WalletVM
                {
                    Amount = dbWalletTransaction.Amount,
                    Transactiontype = dbWalletTransaction.Transactiontype,
                    TransactionDetail = dbWalletTransaction.TransactionDetail,
                    TransactionDate = dbWalletTransaction.TransactionDate,
                };

                wallets.Add(wallet);
            }

            return Request.CreateResponse(HttpStatusCode.OK, wallets);
        }
    }
}