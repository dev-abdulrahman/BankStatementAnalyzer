using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class ClassifiedService : IClassifiedService
    {
        private readonly IClassifiedRepository classifiedRepository;
        private readonly Lazy<ISystemSettingService> systemSettingService;

        public ClassifiedService(IClassifiedRepository classifiedRepository,
            Lazy<ISystemSettingService> systemSettingService)
        {
            this.classifiedRepository = classifiedRepository;
            this.systemSettingService = systemSettingService;
        }

        public void Add(Classified entity)
        {
            classifiedRepository.Add(entity);
        }

        public void Delete(Classified entity)
        {
            classifiedRepository.Delete(entity);
        }

        public void Edit(Classified entity)
        {
            classifiedRepository.Edit(entity);
        }

        public IQueryable<Classified> FindBy(Expression<Func<Classified, bool>> predicate)
        {
            return classifiedRepository.FindBy(predicate);
        }

        public IQueryable<Classified> GetAll()
        {
            return classifiedRepository.GetAll().Where(x => x.Status == true);
        }


        public void Save()
        {
            classifiedRepository.Save();
        }

        public bool ValidateImageExtension(HttpPostedFileBase[] model)
        {
            var extensionList = systemSettingService.Value.GetAll().Where(x => x.SettingTypeValue == "AllowedImageExtensions").FirstOrDefault();
            var allowedExtension = extensionList?.Value.ToLower().Split(';');
            bool status = true;
            foreach (var image in model)
            {
                if (image == null || allowedExtension.Contains(Path.GetExtension(image.FileName).ToLower()))
                {
                    continue;
                }
                else
                {
                    status = false;
                    break;
                }
            }
            return status;
        }
    }
}
