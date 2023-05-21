using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Repository.Interface;
using System;
using BankStatementAnalyzer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class ActionService: IActionService
    {

         private readonly Lazy<IActionRepository> actionRepository;

        public ActionService(Lazy<IActionRepository> actionRepository)
        {
            this.actionRepository = actionRepository;
        }

        public void Add(Models.Action entity)
        {
            
            actionRepository.Value.Add(entity);
        }

        public void Delete(Models.Action entity)
        {
            actionRepository.Value.Delete(entity);
        }

        public void Edit(Models.Action entity)
        {
            actionRepository.Value.Edit(entity);
        }

        public IQueryable<Models.Action> FindBy(Expression<Func<Models.Action, bool>> predicate)
        {
            return actionRepository.Value.FindBy(predicate);
        }


        public IQueryable<Models.Action> GetAll()
        {
            return actionRepository.Value.GetAll();
        }

        public bool IsExist(string name)
        {
            return actionRepository.Value.GetAll().Where(x => x.Name == name).Any();
        }

        public void Save()
        {
             actionRepository.Value.Save();
        }
    }
}
