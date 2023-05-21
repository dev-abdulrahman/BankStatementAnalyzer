using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.WebUI.Common.constants;
using BankStatementAnalyzer.WebUI.ViewModels;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankStatementAnalyzer.WebUI.Controllers
{
    public class ClassifiedListController : Controller
    {
        private readonly Lazy<ICategoryService> categoryService;
        private readonly Lazy<IClassifiedService> classifiedService;
        private readonly Lazy<IClassifiedImagesService> classifiedImagesService;
        private readonly Lazy<IClassifiedCategoryService> classifiedCategoryService;
        private readonly Lazy<ISystemSettingService> systemSettingService;
        private readonly Lazy<IClassifiedKeywordsService> classifiedKeywordsService;
        private readonly Lazy<IClassifiedContactsService> classifiedContactsService;

        public ClassifiedListController(Lazy<ICategoryService> categoryService,
            Lazy<IClassifiedService> classifiedService,
            Lazy<IClassifiedImagesService> classifiedImagesService,
            Lazy<IClassifiedCategoryService> classifiedCategoryService,
            Lazy<ISystemSettingService> systemSettingService,
            Lazy<IClassifiedKeywordsService> classifiedKeywordsService,
            Lazy<IClassifiedContactsService> classifiedContactsService)
        {
            this.categoryService = categoryService;
            this.classifiedService = classifiedService;
            this.classifiedImagesService = classifiedImagesService;
            this.classifiedCategoryService = classifiedCategoryService;
            this.systemSettingService = systemSettingService;
            this.classifiedKeywordsService = classifiedKeywordsService;
            this.classifiedContactsService = classifiedContactsService;
        }

        public ActionResult Index()
        {
            var model = this.classifiedService.Value.GetAll().ToList();
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            ClassifiedIndexViewModel model = new ClassifiedIndexViewModel();
            model.ID = id;
            var classified = classifiedService.Value.FindBy(x => x.ID == id).FirstOrDefault();
            if(classified != null)
            {
                model.Name = classified.Name;
                model.Latitude = classified.Latitude;
                model.Longitude = classified.Longitude;
                model.Address1 = classified.Address1;
                model.Address2 = classified.Address2;
                model.Availability = classified.Availability;
                model.Description = classified.Description;
                model.SpecialistIn = classified.SpecialistIn;
                model.Location = classified.Location;
            }

            var category = classifiedCategoryService.Value.FindBy(x => x.ClassifiedId == id).ToList();
            if (category.Any())
            {
                TempData["SelectedCateogory"] = category;
                TempData.Keep();
            }

            var keywords = classifiedKeywordsService.Value.FindBy(x => x.ClassifiedId == id).ToList();
            if(keywords.Any())
            {
                model.Keywords = string.Join(",", keywords.Select(x => x.Keyword.ToString()));
            }

            var contacts = classifiedContactsService.Value.FindBy(x => x.ClassifiedId == id).ToList();
            if (contacts.Any())
            {
                model.ContactNo = contacts.Select(x => x.ContactNo).FirstOrDefault();
                if(contacts.Count() > 1)
                    model.OptionalContactNo = contacts.Select(x => x.ContactNo).LastOrDefault();
            }

            model.IsEditMode = true;
            return RedirectToAction("Index", "AddNewClassified", model);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = classifiedService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Classified model)
        {
            var classified = classifiedService.Value.FindBy(x => x.ID == model.ID).FirstOrDefault();
            classifiedService.Value.Delete(classified);
            classifiedService.Value.Save();

            var category = classifiedCategoryService.Value.FindBy(x => x.ClassifiedId == model.ID).ToList();
            foreach (var cat in category)
            {
                this.classifiedCategoryService.Value.Delete(cat);
            }
            classifiedCategoryService.Value.Save();

            var images = classifiedImagesService.Value.FindBy(x => x.ClassifiedId == model.ID).ToList();
            foreach (var img in images)
            {
                this.classifiedImagesService.Value.Delete(img);
            }
            classifiedImagesService.Value.Save();

            var keywords = classifiedKeywordsService.Value.FindBy(x => x.ClassifiedId == model.ID).ToList();
            foreach (var word in keywords)
            {
                this.classifiedKeywordsService.Value.Delete(word);
            }
            classifiedKeywordsService.Value.Save();

            TempData["success-message"] = ConstantValue.MASTER_DELETE_SUCCSESS_MESSAGE;
            return RedirectToAction("Index");
        }
    }
}