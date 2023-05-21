using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository walletRepository;

        public WalletService(IWalletRepository walletRepository)
        {
            this.walletRepository = walletRepository;
        }

        public void Add(Wallet entity)
        {
            walletRepository.Add(entity);
        }

        public void Delete(Wallet entity)
        {
            walletRepository.Delete(entity);
        }

        public void Edit(Wallet entity)
        {
            walletRepository.Edit(entity);
        }

        public IQueryable<Wallet> FindBy(Expression<Func<Wallet, bool>> predicate)
        {
            return walletRepository.FindBy(predicate);
        }

        public IQueryable<Wallet> GetAll()
        {
            return walletRepository.GetAll();
        }

        public void Save()
        {
            walletRepository.Save();
        }
    }
}