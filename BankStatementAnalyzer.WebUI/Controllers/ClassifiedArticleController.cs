using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.WebUI.Common.constants;
using BankStatementAnalyzer.WebUI.Filters;
using BankStatementAnalyzer.WebUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace BankStatementAnalyzer.WebUI.Controllers
{
    [RouteAuthorize]
    public class ClassifiedArticleController : Controller
    {
        private readonly Lazy<ICategoryService> categoryService;
        private readonly Lazy<ISubCategoryService> subCategoryService;
        private readonly Lazy<IClassifiedService> classifiedService;
        private readonly Lazy<IClassifiedArticleService> classifiedArticleService;
        private readonly Lazy<IClassifiedArticleImagesService> classifiedArticleImagesService;
        private readonly Lazy<IClassifiedArticleKeywordsService> classifiedArticleKeywordsService;
        private readonly Lazy<IClassifiedArticleCategoryService> classifiedArticleCategoryService;
        private readonly IFileMasterService fileMasterService;

        public ClassifiedArticleController(Lazy<ICategoryService> categoryService,
            Lazy<ISubCategoryService> subCategoryService,
            Lazy<IClassifiedService> classifiedService,
            Lazy<IClassifiedArticleService> classifiedArticleService,
            Lazy<IClassifiedArticleImagesService> classifiedArticleImagesService,
            Lazy<IClassifiedArticleKeywordsService> classifiedArticleKeywordsService,
            Lazy<IClassifiedArticleCategoryService> classifiedArticleCategoryService,
            IFileMasterService fileMasterService)
        {
            this.categoryService = categoryService;
            this.subCategoryService = subCategoryService;
            this.classifiedService = classifiedService;
            this.classifiedArticleService = classifiedArticleService;
            this.classifiedArticleImagesService = classifiedArticleImagesService;
            this.classifiedArticleKeywordsService = classifiedArticleKeywordsService;
            this.classifiedArticleCategoryService = classifiedArticleCategoryService;
            this.fileMasterService = fileMasterService;
        }

        public ActionResult Index()
        {
            var articleVM = from art in classifiedArticleService.Value.GetAll()
                            select new ClassifiedArticleVM()
                            {
                                Id = art.ID,
                                Name = art.Name,
                                Heading = art.Heading,
                                ShortDescription = art.ShortDescription
                            };
            return View(articleVM);
        }

        [HttpGet]
        public ActionResult Create()
        {
            Load(false);
            return View(new ClassifiedArticleVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClassifiedArticleVM articleVM)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;
                Load(articleVM.IsEditMode);
                return View(articleVM);
            }

            try
            {
                var isValid = classifiedService.Value.ValidateImageExtension(articleVM.Images);
                if (isValid)
                {
                    articleVM.Id = AddorUpdateArticle(articleVM);
                    AddorUpdateCategories(articleVM);
                    AddorUpdateImages(articleVM);
                    AddorUpdateKeywords(articleVM);

                    classifiedArticleCategoryService.Value.Save();
                    classifiedArticleImagesService.Value.Save();
                    classifiedArticleKeywordsService.Value.Save();
                }
                else
                {
                    TempData["warning-message"] = ConstantValue.EXTENSION_NOT_VALID;
                    Load(articleVM.IsEditMode);
                    return View(articleVM);
                }
            }
            catch (Exception ex)
            {
                TempData["error-message"] = ConstantValue.MASTER_ERROR_MESSAGE;
                Load(articleVM.IsEditMode);
                return View(articleVM);
            }

            if (!articleVM.IsEditMode)
            {
                TempData["success-message"] = ConstantValue.MASTER_CREATE_SUCCSESS_MESSAGE;
            }
            else
            {
                TempData["success-message"] = ConstantValue.MASTER_EDIT_SUCCSESS_MESSAGE;
            }
            return RedirectToAction("Index", "ClassifiedArticle");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ClassifiedArticleVM model = new ClassifiedArticleVM();
            model.Id = id;
            var classified = classifiedArticleService.Value.FindBy(x => x.ID == id).FirstOrDefault();
            if (classified != null)
            {
                model.Name = classified.Name;
                model.Heading = classified.Heading;
                model.SubHeading = classified.SubHeading;
                model.Address1 = classified.Address;
                model.ContactNo = classified.ContactNo;
                model.Availability = (ArticleAvailable) Convert.ToInt32(classified.Status);
                model.ShortDescription = classified.ShortDescription;
                model.Article = classified.Article;
            }

            var subCategory = classifiedArticleCategoryService.Value.FindBy(x => x.ClassifiedArticleId == id).ToList();
            if (subCategory.Any())
            {
                TempData["SelectedCateogory"] = subCategory;
                TempData.Keep();
            }

            var keywords = classifiedArticleKeywordsService.Value.FindBy(x => x.ClassifiedArticleId == id).ToList();
            if (keywords.Any())
            {
                model.Keywords = string.Join(",", keywords.Select(x => x.Keywords.ToString()));
            }

            model.IsEditMode = true;
            Load(model.IsEditMode);
            return View("Create", model);
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = classifiedArticleService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetArticle(int articleId)
        {
            var article = classifiedArticleService.Value.FindBy(x => x.ID == articleId).FirstOrDefault();
            if (article == null)
                return Content("<h2>Content Not found.<h2>");
            else
                return View("_ArticleView", article);
        }


        [HttpPost]
        public ActionResult Delete(ClassifiedArticle model)
        {
            var article = classifiedArticleService.Value.FindBy(x => x.ID == model.ID).FirstOrDefault();
            classifiedArticleService.Value.Delete(article);
            classifiedArticleService.Value.Save();

            var category = classifiedArticleCategoryService.Value.FindBy(x => x.ClassifiedArticleId == model.ID).ToList();
            foreach (var cat in category)
            {
                this.classifiedArticleCategoryService.Value.Delete(cat);
            }
            classifiedArticleCategoryService.Value.Save();

            var images = classifiedArticleImagesService.Value.FindBy(x => x.ClassifiedArticleId == model.ID).ToList();
            foreach (var img in images)
            {
                this.classifiedArticleImagesService.Value.Delete(img);
            }
            classifiedArticleImagesService.Value.Save();

            var keywords = classifiedArticleKeywordsService.Value.FindBy(x => x.ClassifiedArticleId == model.ID).ToList();
            foreach (var word in keywords)
            {
                this.classifiedArticleKeywordsService.Value.Delete(word);
            }
            classifiedArticleKeywordsService.Value.Save();

            TempData["success-message"] = ConstantValue.MASTER_DELETE_SUCCSESS_MESSAGE;
            return RedirectToAction("Index");
        }

        private int AddorUpdateArticle(ClassifiedArticleVM model)
        {
            var updateClassified = this.classifiedArticleService.Value.FindBy(x => x.ID == model.Id).FirstOrDefault();

            if (updateClassified != null && model.IsEditMode)
            {
                updateClassified.Name = model.Name;
                updateClassified.Address = model.Address1;
                updateClassified.Status = Convert.ToBoolean(model.Availability);
                updateClassified.ShortDescription = model.ShortDescription;
                updateClassified.Heading = model.Heading;
                updateClassified.SubHeading = model.SubHeading;
                updateClassified.ContactNo = model.ContactNo;
                updateClassified.Article = model.Article;

                classifiedArticleService.Value.Edit(updateClassified);
                classifiedArticleService.Value.Save();

                return updateClassified.ID;
            }
            else
            {
                ClassifiedArticle article = new ClassifiedArticle()
                {
                    Name = model.Name,
                    Address = model.Address1,
                    //Address2 = model.Address2,
                    Heading = model.Heading,
                    SubHeading = model.SubHeading,
                    Status = Convert.ToBoolean(model.Availability),
                    ShortDescription = model.ShortDescription,
                    ContactNo = model.ContactNo,
                    Article = model.Article
                };
                classifiedArticleService.Value.Add(article);
                classifiedArticleService.Value.Save();

                return article.ID;
            }
        }

        private void AddorUpdateImages(ClassifiedArticleVM model)
        {
            var filePath = ConfigurationManager.AppSettings["UploadArticleImage"];
            filePath = HttpContext.Server.MapPath(filePath);

            foreach (var image in model.Images)
            {
                if (image == null)
                    continue;

                var guid = Guid.NewGuid();
                var fileName = $"{guid.ToString()}_{DateTime.Now.ToString("ddmmyyyhhss")}{Path.GetExtension(image.FileName)}";
                var path = fileMasterService.Upload(image, filePath, fileName);

                var updateImage = this.classifiedArticleImagesService.Value.FindBy(x => x.ClassifiedArticleId == model.Id).ToList();
                if (updateImage.Any() && model.IsEditMode)
                {
                    foreach (var img in updateImage)
                    {
                        this.classifiedArticleImagesService.Value.Delete(img);
                    }
                    ClassifiedArticleImages articleImages = new ClassifiedArticleImages()
                    {
                        ClassifiedArticleId = Convert.ToInt32(model.Id),
                        Image = fileName
                    };
                    image.SaveAs(path);
                    this.classifiedArticleImagesService.Value.Add(articleImages);
                }
                else
                {
                    ClassifiedArticleImages articleImages = new ClassifiedArticleImages()
                    {
                        ClassifiedArticleId = Convert.ToInt32(model.Id),
                        Image = fileName
                    };
                    image.SaveAs(path);
                    this.classifiedArticleImagesService.Value.Add(articleImages);
                }
            }
        }

        private void AddorUpdateKeywords(ClassifiedArticleVM model)
        {
            var updateKeyword = this.classifiedArticleKeywordsService.Value.FindBy(x => x.ClassifiedArticleId == model.Id).ToList();
            var keywords = model.Keywords?.Split(',');

            if (updateKeyword.Any() && model.IsEditMode)
            {
                if (keywords != null)
                {
                    foreach (var record in updateKeyword)
                    {
                        this.classifiedArticleKeywordsService.Value.Delete(record);
                    }
                    AddKeywords(model.Id, keywords);
                }
            }
            else
            {
                if (keywords != null)
                {
                    AddKeywords(model.Id, keywords);
                }
            }
        }

        private void AddKeywords(int? articleId, string[] keywords)
        {
            foreach (var word in keywords)
            {
                ClassifiedArticleKeywords keywordsModel = new ClassifiedArticleKeywords()
                {
                    ClassifiedArticleId = Convert.ToInt32(articleId),
                    Keywords = word.Trim().ToLower()
                };
                this.classifiedArticleKeywordsService.Value.Add(keywordsModel);
            }
        }

        private void AddorUpdateCategories(ClassifiedArticleVM model)
        {
            var updateCategory = classifiedArticleCategoryService.Value.FindBy(x => x.ClassifiedArticleId == model.Id).ToList();

            if (updateCategory.Any() && model.IsEditMode)
            {
                foreach (var categoryId in model.SelectedSubCategory)
                {
                    var record = updateCategory.Where(x => x.CategoryId == categoryId).FirstOrDefault();
                    if (record != null)
                    {
                        record.ClassifiedArticleId = Convert.ToInt32(model.Id);
                        record.CategoryId = categoryId;
                        classifiedArticleCategoryService.Value.Edit(record);
                    }
                }
            }
            else
            {
                foreach (var categoryId in model.SelectedSubCategory)
                {
                    ClassifiedArticleCategory articleCategory = new ClassifiedArticleCategory()
                    {
                        ClassifiedArticleId = Convert.ToInt32(model.Id),
                        CategoryId = categoryId
                    };
                    classifiedArticleCategoryService.Value.Add(articleCategory);
                }
            }
        }

        private void Load(bool IsEditMode)
        {
            var categories = categoryService.Value.FindBy(x => x.IsSpecialCategory == true).ToList();
            ViewBag.subCategories = from cat in categories
                                    join sub in subCategoryService.Value.GetAll()
                                    on cat.ID equals sub.CategoryID
                                    select new SelectListItem()
                                    {
                                        Value = sub.ID.ToString(),
                                        Text = sub.Name.ToString()
                                    };
            if (IsEditMode)
            {
                var classifiedCategory = TempData.Peek("SelectedCateogory") as List<ClassifiedArticleCategory>;
                if (classifiedCategory != null && classifiedCategory.Any())
                {
                    foreach (var item in (IEnumerable<SelectListItem>)ViewBag.subCategories)
                    {
                        item.Selected = classifiedCategory.Where(x => x.CategoryId == Convert.ToInt32(item.Value)).Any();
                    }
                }
            }
        }

    }
}