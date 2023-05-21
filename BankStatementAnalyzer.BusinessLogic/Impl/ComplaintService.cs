using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class ComplaintService : IComplaintService
    {
        private readonly IComplaintRepository complaintRepository;

        public ComplaintService(IComplaintRepository complaintRepository)
        {
            this.complaintRepository = complaintRepository;
        }

        public void Add(Complaint entity)
        {
            complaintRepository.Add(entity);
        }

        public void Delete(Complaint entity)
        {
            complaintRepository.Delete(entity);
        }

        public void Edit(Complaint entity)
        {
            complaintRepository.Delete(entity);
        }

        public IQueryable<Complaint> FindBy(Expression<Func<Complaint, bool>> predicate)
        {
            return complaintRepository.FindBy(predicate);
        }

        public IQueryable<Complaint> GetAll()
        {
            return complaintRepository.GetAll();
        }

        public void Save()
        {
            complaintRepository.Save();
        }
    }
}