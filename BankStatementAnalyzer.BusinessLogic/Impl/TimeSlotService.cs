using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class TimeSlotService : ITimeSlotService
    {
        private readonly ITimeSlotRepository timeSlotRepository;

        public TimeSlotService(ITimeSlotRepository timeSlotRepository)
        {
            this.timeSlotRepository = timeSlotRepository;
        }

        public void Add(TimeSlot entity)
        {
            timeSlotRepository.Add(entity);
        }

        public void Delete(TimeSlot entity)
        {
            timeSlotRepository.Delete(entity);
        }

        public void Edit(TimeSlot entity)
        {
            timeSlotRepository.Edit(entity);
        }

        public IQueryable<TimeSlot> FindBy(Expression<Func<TimeSlot, bool>> predicate)
        {
            return timeSlotRepository.FindBy(predicate);
        }

        public IQueryable<TimeSlot> GetAll()
        {
            return timeSlotRepository.GetAll();
        }

        public void Save()
        {
            timeSlotRepository.Save();
        }
    }
}