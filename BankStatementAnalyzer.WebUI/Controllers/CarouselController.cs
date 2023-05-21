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
using System.Web;
using System.Web.Mvc;

namespace BankStatementAnalyzer.WebUI.Controllers
{
    [RouteAuthorize]
    public class CarouselController : Controller
    {
        private readonly Lazy<ICarouselService> carouselService;
        private readonly Lazy<IClassifiedService> classifiedService;
        private readonly IFileMasterService fileMasterService;

        public CarouselController(Lazy<ICarouselService> carouselService,
            Lazy<IClassifiedService> classifiedService,
            IFileMasterService fileMasterService)
        {
            this.carouselService = carouselService;
            this.classifiedService = classifiedService;
            this.fileMasterService = fileMasterService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var carousels = carouselService.Value.GetAll().Select(x => new { x.ClassifiedId });
            var carousel = carousels.Distinct();
            var classified = classifiedService.Value.GetAll();

            var model = from car in carousel
                        join cl in classified
                        on car.ClassifiedId equals cl.ID 
                        select new CarouselViewModel()
                        {
                            ID = cl.ID,
                            Name = cl.Name,
                            ImageCount = carousels.Where(x => x.ClassifiedId == cl.ID).Count()
                        };

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            CarouselViewModel carousel = new CarouselViewModel();
            var classifieds = classifiedService.Value.GetAll();
            carousel.Classified = classifieds.Select(x => new SelectListItem { Text = x.Name, Value = x.ID.ToString() });
            return View(carousel);
        }

        [HttpPost]
        public ActionResult Create(CarouselViewModel carouselViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;
                return View(carouselViewModel);
            }

            var isValid = classifiedService.Value.ValidateImageExtension(carouselViewModel.Images);
            if (isValid)
            {
                var filePath = ConfigurationManager.AppSettings["UploadCarouselImage"];
                filePath = HttpContext.Server.MapPath(filePath);

                foreach (var img in carouselViewModel.Images)
                {
                    var guid = Guid.NewGuid();
                    Carousel carousel = new Carousel()
                    {
                        ClassifiedId = Convert.ToInt32(carouselViewModel.SelectedClassified),
                        ImageUrl = $"{guid.ToString()}_{DateTime.Now.ToString("ddmmyyyhhss")}{Path.GetExtension(img.FileName)}"
                    };
                    var path = fileMasterService.Upload(img, filePath, carousel.ImageUrl);
                    img.SaveAs(path);
                    carouselService.Value.Add(carousel);
                }
                try
                {
                    carouselService.Value.Save();
                }
                catch (Exception ex)
                {
                    TempData["error-message"] = ConstantValue.MASTER_ERROR_MESSAGE;
                    return View(carouselViewModel);
                }
                TempData["success-message"] = ConstantValue.MASTER_CREATE_SUCCSESS_MESSAGE;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["warning-message"] = ConstantValue.EXTENSION_NOT_VALID;
                return RedirectToAction("Index");
            }
        }

        //[HttpGet]
        //public ActionResult Edit(int id)
        //{
        //    CarouselViewModel carousel = new CarouselViewModel();
        //    var classifieds = classifiedService.Value.GetAll();
        //    carousel.Classified = classifieds.Select(x => new SelectListItem { Text = x.Name, Value = x.ID.ToString(), Selected = x.ID == id });
        //    return View(carousel);
        //}

        //[HttpPost]
        //public ActionResult Edit(CarouselViewModel carouselViewModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;
        //        return View(carouselViewModel);
        //    }

        //    var oldCategory = categoryService.Value.FindBy(x => x.ID == id).FirstOrDefault();

        //    if ((oldCategory.Name != category.Name) &&
        //        categoryService.Value.GetAll().Any(x => x.Name == category.Name))
        //    {
        //        TempData["warning-message"] = ConstantValue.MASTER_EDIT_DUPLICATE_RECORD_MESSAGE;

        //        return View(category);
        //    }

        //    oldCategory.Name = category.Name;
        //    oldCategory.Status = category.Status;
        //    oldCategory.UpdatedBy = UserName;
        //    oldCategory.UpdatedDate = DateTime.Now;

        //    categoryService.Value.Edit(oldCategory);
        //    categoryService.Value.Save();

        //    TempData["success-message"] = ConstantValue.MASTER_EDIT_SUCCSESS_MESSAGE;

        //    return RedirectToAction("Index");
        //}

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = classifiedService.Value.FindBy(x => x.ID == id).FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Classified classified, int id)
        {
            var oldCarousels = carouselService.Value.FindBy(x => x.ClassifiedId == id).ToList();

            foreach(var car in oldCarousels)
            {
                carouselService.Value.Delete(car);
            }
            carouselService.Value.Save();

            TempData["success-message"] = ConstantValue.MASTER_DELETE_SUCCSESS_MESSAGE;
            return RedirectToAction("Index");
        }

    }
}