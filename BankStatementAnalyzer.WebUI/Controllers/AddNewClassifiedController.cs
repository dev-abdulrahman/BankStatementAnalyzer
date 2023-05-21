using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.WebUI.Common.constants;
using BankStatementAnalyzer.WebUI.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankStatementAnalyzer.WebUI.Controllers
{
    public class AddNewClassifiedController : Controller
    {
        private readonly Lazy<ICategoryService> categoryService;
        private readonly Lazy<IClassifiedService> classifiedService;
        private readonly Lazy<IClassifiedImagesService> classifiedImagesService;
        private readonly Lazy<IClassifiedCategoryService> classifiedCategoryService;
        private readonly Lazy<ISystemSettingService> systemSettingService;
        private readonly Lazy<IClassifiedKeywordsService> classifiedKeywordsService;
        private readonly Lazy<IClassifiedContactsService> classifiedContactsService;
        private readonly IFileMasterService fileMasterService;
        private readonly Lazy<ISubCategoryService> subcategoryService;

        public AddNewClassifiedController(Lazy<ICategoryService> categoryService, 
            Lazy<IClassifiedService> classifiedService,
            Lazy<IClassifiedImagesService> classifiedImagesService,
            Lazy<IClassifiedCategoryService> classifiedCategoryService,
            Lazy<ISystemSettingService> systemSettingService,
            Lazy<IClassifiedKeywordsService> classifiedKeywordsService,
            Lazy<IClassifiedContactsService> classifiedContactsService,
            IFileMasterService fileMasterService,
            Lazy<ISubCategoryService> subcategoryService)
        {
            this.categoryService = categoryService;
            this.classifiedService = classifiedService;
            this.classifiedImagesService = classifiedImagesService;
            this.classifiedCategoryService = classifiedCategoryService;
            this.systemSettingService = systemSettingService;
            this.classifiedKeywordsService = classifiedKeywordsService;
            this.classifiedContactsService = classifiedContactsService;
            this.fileMasterService = fileMasterService;
            this.subcategoryService = subcategoryService;
        }

        public ActionResult Index(ClassifiedIndexViewModel model)
        {
            Load(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddClassified(ClassifiedIndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;
                Load(model);
                return View("Index", model);
            }
            try
            {
                var isValid = classifiedService.Value.ValidateImageExtension(model.Images);
                if (isValid)
                {
                    model.ID = AddorUpdateClassified(model, model.ID);
                    AddorUpdateCategory(model, model.ID);
                    AddorUpdateImages(model, model.ID);
                    AddorUpdateKeywords(model, model.ID);
                    AddorUpdateContact(model, model.ID);

                    classifiedCategoryService.Value.Save();
                    classifiedImagesService.Value.Save();
                    classifiedKeywordsService.Value.Save();
                    classifiedContactsService.Value.Save();
                }
                else
                {
                    TempData["warning-message"] = ConstantValue.EXTENSION_NOT_VALID;
                    Load(model);
                    return View("Index", model);
                }
            }
            catch (Exception ex)
            {
                TempData["error-message"] = ConstantValue.MASTER_ERROR_MESSAGE;
                return RedirectToAction("Index", "AddNewClassified", model);
            }

            if (!model.IsEditMode)
            {
                TempData["success-message"] = ConstantValue.MASTER_CREATE_SUCCSESS_MESSAGE;
            }
            else
            {
                TempData["success-message"] = ConstantValue.MASTER_EDIT_SUCCSESS_MESSAGE;
            }
            return RedirectToAction("Index", "ClassifiedList");
        }

        private int AddorUpdateClassified(ClassifiedIndexViewModel model, int? classifiedID)
        {
            var updateClassified = this.classifiedService.Value.FindBy(x => x.ID == classifiedID).FirstOrDefault();

            if(updateClassified != null)
            {
                updateClassified.Name = model.Name;
                updateClassified.Address1 = model.Address1;
                updateClassified.Address2 = model.Address2;
                updateClassified.Latitude = model.Latitude;
                updateClassified.Longitude = model.Longitude;
                updateClassified.Availability = model.Availability;
                updateClassified.Description = model.Description;
                updateClassified.SpecialistIn = model.SpecialistIn;
                updateClassified.Location = model.Location;

                this.classifiedService.Value.Edit(updateClassified);
                classifiedService.Value.Save();

                return updateClassified.ID;
            }
            else
            {
                Classified classified = new Classified()
                {
                    Name = model.Name,
                    Address1 = model.Address1,
                    Address2 = model.Address2,
                    Latitude = model.Latitude,
                    Longitude = model.Longitude,
                    Availability = model.Availability,
                    Description = model.Description,
                    SpecialistIn = model.SpecialistIn,
                    Location = model.Location
                };
                classifiedService.Value.Add(classified);
                classifiedService.Value.Save();

                return classified.ID;
            }
        }

        private void AddorUpdateCategory(ClassifiedIndexViewModel model, int? classifiedID)
        {
            var updateCategory = this.classifiedCategoryService.Value.FindBy(x => x.ClassifiedId == classifiedID).ToList();

            if(updateCategory != null && updateCategory.Any())
            {
                foreach(var categoryId in model.SelectedCategory)
                {
                    var record = updateCategory.Where(x => x.CategoryId == categoryId).FirstOrDefault();
                    if(record != null)
                    {
                        record.ClassifiedId = Convert.ToInt32(classifiedID);
                        record.CategoryId = categoryId;
                        classifiedCategoryService.Value.Edit(record);
                    }
                }
            }
            else
            {
                foreach (var categoryId in model.SelectedCategory)
                {
                    ClassifiedCategory classifiedCategory = new ClassifiedCategory()
                    {
                        ClassifiedId = Convert.ToInt32(classifiedID),
                        CategoryId = categoryId
                    };
                    classifiedCategoryService.Value.Add(classifiedCategory);
                }
            }
        }

        private void AddorUpdateImages(ClassifiedIndexViewModel model, int? classifiedID)
        {
            var filePath = ConfigurationManager.AppSettings["UploadClassifiedImage"];
            filePath = HttpContext.Server.MapPath(filePath);

            foreach (var image in model.Images)
            {
                var guid = Guid.NewGuid();
                var fileName = $"{guid.ToString()}_{DateTime.Now.ToString("ddmmyyyhhss")}{Path.GetExtension(image.FileName)}";
                var path = fileMasterService.Upload(image, filePath, fileName);

                var updateImage = this.classifiedImagesService.Value.FindBy(x => x.ClassifiedId == classifiedID).ToList();
                if (updateImage.Any())
                {
                    foreach (var img in updateImage)
                    {
                        this.classifiedImagesService.Value.Delete(img);
                    }
                    ClassifiedImages classifiedImages = new ClassifiedImages()
                    {
                        ClassifiedId = Convert.ToInt32(classifiedID),
                        ImageUrl = fileName
                    };
                    image.SaveAs(path);
                    this.classifiedImagesService.Value.Add(classifiedImages);
                }
                else
                {
                    ClassifiedImages classifiedImages = new ClassifiedImages()
                    {
                        ClassifiedId = Convert.ToInt32(classifiedID),
                        ImageUrl = fileName
                    };
                    image.SaveAs(path);
                    this.classifiedImagesService.Value.Add(classifiedImages);
                }
            }
        }

        private void AddorUpdateKeywords(ClassifiedIndexViewModel model, int? classifiedID)
        {
            var updateKeyword = this.classifiedKeywordsService.Value.FindBy(x => x.ClassifiedId == classifiedID).ToList();
            var keywords = model.Keywords?.Split(',');

            if (updateKeyword.Any())
            {
                if (keywords != null)
                {
                    foreach (var record in updateKeyword)
                    {
                        this.classifiedKeywordsService.Value.Delete(record);
                    }
                    AddKeywords(classifiedID, keywords);
                }
            }
            else
            {
                if (keywords != null)
                {
                    AddKeywords(classifiedID, keywords);
                }
            }
        }

        private void AddorUpdateContact(ClassifiedIndexViewModel model, int? classifiedID)
        {
            var updateContact = this.classifiedContactsService.Value.FindBy(x => x.ClassifiedId == classifiedID).ToList();
            if (updateContact.Any() && !string.IsNullOrEmpty(model.ContactNo))
            {
                foreach(var item in updateContact)
                {
                    this.classifiedContactsService.Value.Delete(item);
                }
                AddContact(model, classifiedID);
            }
            else if(!string.IsNullOrEmpty(model.ContactNo))
            {
                AddContact(model, classifiedID);
            }
        }

        private void AddKeywords(int? classifiedID, string[] keywords)
        {
            foreach (var word in keywords)
            {
                ClassifiedKeywords keywordsModel = new ClassifiedKeywords()
                {
                    ClassifiedId = Convert.ToInt32(classifiedID),
                    Keyword = word.Trim().ToLower()
                };
                this.classifiedKeywordsService.Value.Add(keywordsModel);
            }
        }

        private void AddContact(ClassifiedIndexViewModel model, int? classifiedID)
        {
           CreateModel(model.ContactNo, classifiedID);
           if(!string.IsNullOrEmpty(model.OptionalContactNo))
            CreateModel(model.OptionalContactNo, classifiedID);
        }

        private void CreateModel(string contactNo, int? classifiedID)
        {
            ClassifiedContacts classifiedContacts = new ClassifiedContacts()
            {
                ClassifiedId = Convert.ToInt32(classifiedID),
                ContactNo = contactNo
            };
            this.classifiedContactsService.Value.Add(classifiedContacts);
        }

        //private bool ValidateImageExtension(ClassifiedIndexViewModel model)
        //{
        //    var extensionList = systemSettingService.Value.GetAll().Where(x => x.SettingTypeValue == "AllowedImageExtensions").FirstOrDefault();
        //    var allowedExtension = extensionList?.Value.ToLower().Split(';');
        //    bool status = true;
        //    foreach (var image in model.Images)
        //    {
        //        if (allowedExtension.Contains(Path.GetExtension(image.FileName).ToLower()))
        //        {
        //            continue;
        //        }
        //        else
        //        {
        //            status = false;
        //            break;
        //        }
        //    }
        //    return status;
        //}

        private void Load(ClassifiedIndexViewModel model)
        {
            ViewBag.categories = subcategoryService.Value.GetAll()?.Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Name.ToString()}).ToList();
            if (model.IsEditMode)
            {
                var classifiedCategory = TempData.Peek("SelectedCateogory") as List<ClassifiedCategory>;
                if (classifiedCategory != null && classifiedCategory.Any())
                {
                    foreach (var item in (IEnumerable<SelectListItem>)ViewBag.categories)
                    {
                        item.Selected = classifiedCategory.Where(x => x.CategoryId == Convert.ToInt32(item.Value)).Any();
                    }
                }
            }
        }
    }
}