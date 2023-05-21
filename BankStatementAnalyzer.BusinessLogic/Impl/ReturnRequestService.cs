using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class ReturnRequestService : IReturnRequestService
    {
        private readonly IReturnRequestRepository returnRequestRepository;

        public ReturnRequestService(IReturnRequestRepository returnRequestRepository)
        {
            this.returnRequestRepository = returnRequestRepository;
        }

        public void Add(ReturnRequest entity)
        {
            returnRequestRepository.Add(entity);
        }

        public void Delete(ReturnRequest entity)
        {
            returnRequestRepository.Delete(entity);
        }

        public void Edit(ReturnRequest entity)
        {
            returnRequestRepository.Edit(entity);
        }

        public IQueryable<ReturnRequest> FindBy(Expression<Func<ReturnRequest, bool>> predicate)
        {
            return returnRequestRepository.FindBy(predicate);
        }

        public IQueryable<ReturnRequest> GetAll()
        {
            return returnRequestRepository.GetAll();
        }

        public void Save()
        {
            returnRequestRepository.Save();
        }
    }
}