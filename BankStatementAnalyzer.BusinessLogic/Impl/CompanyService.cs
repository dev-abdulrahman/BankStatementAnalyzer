using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        public void Add(Company entity)
        {
            companyRepository.Add(entity);
        }

        public void Delete(Company entity)
        {
            companyRepository.Delete(entity);
        }

        public void Edit(Company entity)
        {
            companyRepository.Edit(entity);
        }

        public IQueryable<Company> FindBy(Expression<Func<Company, bool>> predicate)
        {
            return companyRepository.FindBy(predicate);
        }

        public IQueryable<Company> GetAll()
        {
            return companyRepository.GetAll();
        }

        public void Save()
        {
            companyRepository.Save();
        }
    }
}