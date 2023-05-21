using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.WebUI.Common.constants;
using BankStatementAnalyzer.WebUI.Common.logger;
using BankStatementAnalyzer.WebUI.Filters;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankStatementAnalyzer.WebUI.Controllers
{
    [RouteAuthorize]
    //[OutputCache(NoStore = true, Duration = 0)]
    public class GalleryController : BaseController
    {
        private readonly Lazy<IImageCategoryService> imageCategoryService;
        private readonly Lazy<IGalleryService> galleryService;
        private readonly Lazy<IFileMasterService> fileMasterService;

        public GalleryController(Lazy<ILoggerFacade<BaseController>> logger,
           Lazy<IUserMasterService> userMasterService,
           Lazy<IImageCategoryService> imageCategoryService,
           Lazy<IGalleryService> galleryService,
           Lazy<IFileMasterService> fileMasterService) : base(userMasterService.Value, logger.Value)
        {
            this.imageCategoryService = imageCategoryService;
            this.galleryService = galleryService;
            this.fileMasterService = fileMasterService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            
            return View(galleryService.Value.GetAll().ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            Load();

            Gallery gallery = new Gallery();

            return View(gallery);
        }

        [HttpPost]
        public ActionResult Create(Gallery gallery, HttpPostedFileBase file)
        {
            ModelState["File"].Errors.Clear();
            if (!ModelState.IsValid)
            {
                Load();

                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;

                return View(gallery);
            }

            if (galleryService.Value.FindBy(x => x.Title == gallery.Title).FirstOrDefault() != null)
            {
                Load();

                TempData["warning-message"] = ConstantValue.MASTER_EDIT_DUPLICATE_RECORD_MESSAGE;

                return View(gallery);
            }

            if (file == null)
            {
                Load();

                TempData["warning-message"] = ConstantValue.FILE_REQUIRED;

                return View(gallery);
            }

            #region File upload
            var filePath = ConfigurationManager.AppSettings["galleryPath"];
            filePath = HttpContext.Server.MapPath(filePath);
            var guid = Guid.NewGuid();
            var fileName = $"{guid.ToString()}_{DateTime.Now.ToString("ddmmyyyhhss")}.{Path.GetExtension(file.FileName)}";
            var path = fileMasterService.Value.Upload(file, filePath, fileName);

            BankStatementAnalyzer.Models.File document = new BankStatementAnalyzer.Models.File
            {
                Path = fileName,
                Name = fileName,
                EntityName = "Gallery",
                Extension = Path.GetExtension(file.FileName),
                CreatedDate = DateTime.Now,
                CreatedBy = UserName
            };

            fileMasterService.Value.Add(document);
            fileMasterService.Value.Save();
            #endregion

            gallery.FileId = document.ID;
            gallery.CreatedBy = UserName;

            galleryService.Value.Add(gallery);
            galleryService.Value.Save();

            TempData["success-message"] = ConstantValue.MASTER_CREATE_SUCCSESS_MESSAGE;

            if (gallery.CreateAnother)
            {
                return RedirectToAction("Create");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Load();

            var model = galleryService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Gallery gallery, int id)
        {
            if (!ModelState.IsValid)
            {
                Load();

                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;

                return View(gallery);
            }

            var oldGallery = galleryService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            if ((oldGallery.Title != gallery.Title) &&
                imageCategoryService.Value.GetAll().Any(x => x.Name == gallery.Title))
            {
                Load();

                TempData["warning-message"] = ConstantValue.MASTER_EDIT_DUPLICATE_RECORD_MESSAGE;

                return View(gallery);
            }

            oldGallery.Title = gallery.Title;
            oldGallery.ImageCategoryId = gallery.ImageCategoryId;
            oldGallery.Status = gallery.Status;
            oldGallery.UpdatedBy = UserName;
            oldGallery.UpdatedDate = DateTime.Now;

            galleryService.Value.Edit(oldGallery);
            galleryService.Value.Save();

            TempData["success-message"] = ConstantValue.MASTER_EDIT_SUCCSESS_MESSAGE;

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = galleryService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(City city, int id)
        {
            var oldGallery = galleryService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            galleryService.Value.Delete(oldGallery);
            galleryService.Value.Save();

            #region delete existing image
            var filePath = ConfigurationManager.AppSettings["galleryPath"];
            filePath = HttpContext.Server.MapPath(filePath);

            var att = fileMasterService.Value.FindBy(x => x.ID == oldGallery.FileId).FirstOrDefault();

            fileMasterService.Value.DeleteFile(Path.Combine(filePath, att.Path));

            fileMasterService.Value.Delete(att);
            fileMasterService.Value.Save();
            #endregion

            TempData["success-message"] = ConstantValue.MASTER_DELETE_SUCCSESS_MESSAGE;

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditImage(int id)
        {
            var gallery = galleryService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            return View(gallery);
        }

        [HttpPost]
        public ActionResult EditImage(HttpPostedFileBase file, int attacthmentId, int id)
        {
            if (file == null)
            {
                TempData["warning-message"] = ConstantValue.FILE_REQUIRED;

                return RedirectToAction("EditImage", new { id });
            }

            var filePath = ConfigurationManager.AppSettings["galleryPath"];
            filePath = HttpContext.Server.MapPath(filePath);

            var guid = Guid.NewGuid();
            var fileName = $"{guid.ToString()}_{DateTime.Now.ToString("ddmmyyyhhss")}.{Path.GetExtension(file.FileName)}";
            var path = fileMasterService.Value.Upload(file, filePath, fileName);

            var att = fileMasterService.Value.FindBy(x => x.ID == attacthmentId).FirstOrDefault();

            #region delete existing image
            fileMasterService.Value.DeleteFile(Path.Combine(filePath, att.Path));
            #endregion

            att.Path = fileName;
            att.Name = fileName;
            att.UpdatedBy = UserName;
            att.UpdatedDate = DateTime.Now;

            fileMasterService.Value.Edit(att);
            fileMasterService.Value.Save();

            TempData["success-message"] = ConstantValue.IMAGE_UPLOAD_SUCCESSFULL;

            return RedirectToAction("Index");
        }

        private void Load()
        {
            ViewBag.ImageCatogies = imageCategoryService.Value
                                   .GetAll()
                                   .Where(x => x.Status == true)
                                   .Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Name })
                                   .ToList();
        }
    }
}