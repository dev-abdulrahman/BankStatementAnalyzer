using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.WebUI.Common.logger;
using BankStatementAnalyzer.WebUI.Filters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankStatementAnalyzer.WebUI.Controllers
{
    [RouteAuthorize]
    //[OutputCache(NoStore = true, Duration = 0)]
    public class ProductController : BaseController
    {
        private readonly IProductService productService;
        private readonly ILoggerFacade<BaseController> logger;
        private readonly IFileMasterService fileMasterService;
        private readonly IStyleClassService styleClassService;
        private readonly IStyleTraitService styleTraitService;
        private readonly IStyleTraitValueService styleTraitValueService;
        private readonly IStyleTraitValueProductService styleTraitValueProductService;
        private readonly IUnitOfMeasureService unitOfMeasureService;
        private readonly ICategoryService categoryService;
        private readonly ISubCategoryService subCategoryService;

        public ProductController(IUserMasterService userMasterService,
            IProductService productService,
            ILoggerFacade<BaseController> logger,
            IFileMasterService fileMasterService,
            IStyleClassService styleClassService,
            IStyleTraitService styleTraitService,
            IStyleTraitValueService styleTraitValueService,
            IStyleTraitValueProductService styleTraitValueProductService,
            IUnitOfMeasureService unitOfMeasureService,
            ICategoryService categoryService,
            ISubCategoryService subCategoryService) : base(userMasterService, logger)
        {
            this.productService = productService;
            this.logger = logger;
            this.fileMasterService = fileMasterService;
            this.styleClassService = styleClassService;
            this.styleTraitService = styleTraitService;
            this.styleTraitValueService = styleTraitValueService;
            this.styleTraitValueProductService = styleTraitValueProductService;
            this.unitOfMeasureService = unitOfMeasureService;
            this.categoryService = categoryService;
            this.subCategoryService = subCategoryService;
        }

        // GET: Product
        public ActionResult Index()
        {
            
            return View(productService.GetAll().ToList());
        }

        [HttpGet]
        public ActionResult Manage(int id = 0)
        {
            
            Product product = new Product();
            if (id > 0)
            {
                Load((int)id);

                product = productService.FindBy(x => x.ID == id).FirstOrDefault();

                mapVariantDDValues(product);

                MapCategoryDDValues(product);

                return View(product);
            }

            Load();

            product.ID = (int)id;

            return View(product);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Manage(Product product)
        {
            if (!ModelState.IsValid)
            {
                Load();

                TempData["warning-message"] = "Please input correct and valid data.";

                return View(product);
            }

            
            if (product.ID == 0 && productService.FindBy(x => x.Name == product.Name).FirstOrDefault() != null)
            {
                Load();

                TempData["warning-message"] = "Product with same name already exist.";

                return View(product);
            }

            var response = MapProduct(product);

            if (response.Item1)
            {
                TempData["success-message"] = response.Item2;

                return RedirectToAction("Manage", new { id = product.ID });
            }
            else
            {
                Load();

                TempData["error-message"] = response.Item2;

                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Category(Product product)
        {
            var response = MapCategory(product);

            if (response.Item1)
            {
                TempData["success-message"] = response.Item2;

                Load();

                return RedirectToAction("Manage", new { id = product.ID });
            }
            else
            {
                Load();

                TempData["error-message"] = response.Item2;

                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Discount(Product product)
        {
            var response = MapDiscount(product);

            if (response.Item1)
            {
                TempData["success-message"] = response.Item2;

                Load();

                return RedirectToAction("Manage", new { id = product.ID });
            }
            else
            {
                Load();

                TempData["error-message"] = response.Item2;

                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Images(Product product)
        {
            List<HttpPostedFileBase> files = new List<HttpPostedFileBase>();
            int i = 0;
            foreach (var item in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[i];
                files.Add(file);
                i++;
            }

            var response = MapImage(files, product);
            if (response.Item1)
            {
                Load();

                TempData["success-message"] = response.Item2;

                return RedirectToAction("Manage", new { id = product.ID });
            }
            else
            {
                Load();

                TempData["error-message"] = response.Item2;

                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult ProductVariant(Product product)
        {
            var response = MapProductVariant(product);

            if (response.Item1)
            {
                TempData["success-message"] = response.Item2;

                Load();

                return RedirectToAction("Manage", new { id = product.ID });
            }
            else
            {
                Load();

                TempData["error-message"] = response.Item2;

                return View("Error");
            }
        }

        [AllowAnonymous]
        public JsonResult DDStyleTraitByStyleClassId(int id)
        {
            return Json(this.styleTraitService.FindBy(x => x.StyleClassId == id).ToList(), JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public JsonResult DDStyleTraitValueByStyleTraitId(int id)
        {
            return Json(this.styleTraitValueService.FindBy(x => x.StyleTraitId == id).ToList(), JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public JsonResult DDSubcategoryByCategoryId(int id)
        {
            return Json(this.subCategoryService.FindBy(x => x.CategoryID == id).ToList(), JsonRequestBehavior.AllowGet);
        }

        private void mapVariantDDValues(Product product)
        {
            var prodVariants = styleTraitValueProductService.FindBy(x => x.ProductId == product.ID).ToList();

            foreach (var item in prodVariants)
            {
                product.ListStyleVariantValue.Add(item.StyleTraitValueId.ToString());
            }

            var id = prodVariants.Count() > 0 ? prodVariants.FirstOrDefault().StyleTraitValueId : 0;
            if (id > 0)
            {
                var traitId = styleTraitValueService.FindBy(x => x.ID == id).FirstOrDefault().StyleTraitId;
                var classId = styleTraitService.FindBy(x => x.ID == traitId).FirstOrDefault().StyleClassId;

                product.DdStyleClass = classId;
                product.DdStyleTrait = traitId;
            }
        }

        private void MapCategoryDDValues(Product product)
        {
            var subcategory = subCategoryService.FindBy(x => x.ID == product.SubCategoryId).FirstOrDefault();
            if (subcategory != null)
            {
                var cat = categoryService.FindBy(x => x.ID == subcategory.CategoryID).FirstOrDefault();
                product.CategoryId = cat.ID;
            }
        }

        private Tuple<bool, string> MapProduct(Product product)
        {

            try
            {
                if (product.ID == 0)
                {
                    product.CreatedBy = UserName;

                    productService.Add(product);
                    productService.Save();

                    return new Tuple<bool, string>(true, "Product added successfully.");
                }
                else
                {
                    var model = productService.FindBy(x => x.ID == product.ID).FirstOrDefault();

                    model.Name = product.Name;
                    model.Title = product.Title;
                    model.ShortTitle = product.ShortTitle;
                    model.Code = product.Code;
                    model.SKU = product.SKU;
                    model.Price = product.Price;
                    model.ActivateOn = product.ActivateOn;
                    model.DeactivateOn = product.DeactivateOn;
                    model.Description = product.Description;
                    model.UnitOfMeasureId = product.UnitOfMeasureId;
                    model.Liters = product.Liters;
                    model.UpdatedBy = UserName;
                    model.IsReturnAble = product.IsReturnAble;
                    model.UpdatedDate = DateTime.Now;

                    productService.Edit(model);
                    productService.Save();

                    return new Tuple<bool, string>(true, "Product edited successfully.");
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);

                return new Tuple<bool, string>(false, "Something went wrong kindly contact system adminstrator.");
            }
        }

        private Tuple<bool, string> MapImage(List<HttpPostedFileBase> files, Product product)
        {
            
            var model = productService.FindBy(x => x.ID == product.ID).Include("Files").FirstOrDefault();

            try
            {
                if (files.Count() > 0)
                {
                    foreach (var file in files)
                    {
                        if (file != null)
                        {
                            var filePath = ConfigurationManager.AppSettings["UploadPath"];

                            filePath = HttpContext.Server.MapPath(filePath);
                            var guid = Guid.NewGuid();
                            var fileName = $"{guid.ToString()}_{DateTime.Now.ToString("ddmmyyyhhss")}.{Path.GetExtension(file.FileName)}";
                            var path = fileMasterService.Upload(file, filePath, fileName);

                            BankStatementAnalyzer.Models.File document = new BankStatementAnalyzer.Models.File
                            {
                                Path = fileName,
                                Name = fileName,
                                EntityName = "Product",
                                Extension = Path.GetExtension(file.FileName),
                                CreatedDate = DateTime.Now,
                                CreatedBy = "UserName",
                            };

                            model.Files.Add(document);
                        }
                    }

                    productService.Edit(model);
                    productService.Save();
                }

                return new Tuple<bool, string>(true, "Image uploaded successfully.");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);

                return new Tuple<bool, string>(false, "Something went wrong kindly contact system adminstrator.");
            }
        }

        private Tuple<bool, string> MapDiscount(Product product)
        {
            try
            {
                var model = productService.FindBy(x => x.ID == product.ID).FirstOrDefault();

                model.DiscountType = product.DiscountType;
                model.SpecialPrice = product.SpecialPrice;
                model.StartDate = product.StartDate;
                model.EndDate = product.EndDate;
                model.UpdatedBy = "";
                model.UpdatedDate = DateTime.Now;

                productService.Edit(model);
                productService.Save();

                return new Tuple<bool, string>(true, "Discount added successfully.");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);

                return new Tuple<bool, string>(false, "Something went wrong kindly contact system adminstrator.");
            }
        }

        private Tuple<bool, string> MapCategory(Product product)
        {
            try
            {
                
                var model = productService.FindBy(x => x.ID == product.ID).FirstOrDefault();

                model.SubCategoryId = product.DdSubCategory;

                model.UpdatedBy = "";
                model.UpdatedDate = DateTime.Now;

                productService.Edit(model);
                productService.Save();

                return new Tuple<bool, string>(true, "Category added successfully.");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);

                return new Tuple<bool, string>(false, "Something went wrong kindly contact system adminstrator.");
            }
        }

        private Tuple<bool, string> MapProductVariant(Product product)
        {
            try
            {
                foreach (var styleTraitValue in styleTraitValueProductService.FindBy(x => x.ProductId == product.ID).ToList())
                {
                    styleTraitValueProductService.Delete(styleTraitValue);
                    styleTraitValueProductService.Save();
                }

                foreach (var item in product.ListStyleVariantValue)
                {
                    var id = Convert.ToInt32(item);

                    StyleTraitValueProduct styleTraitValueProduct = new StyleTraitValueProduct
                    {
                        StyleTraitValueId = id,
                        ProductId = product.ID
                    };

                    styleTraitValueProductService.Add(styleTraitValueProduct);
                    styleTraitValueProductService.Save();
                }

                return new Tuple<bool, string>(true, "Product Variant added successfully.");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);

                return new Tuple<bool, string>(false, "Something went wrong kindly contact system adminstrator.");
            }
        }

        private void Load(int productId = 0)
        {
            ViewBag.StyleClass = styleClassService
                                .GetAll()
                                .Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Name })
                                .ToList();

            ViewBag.StyleTrait = styleTraitService
                                .GetAll()
                                .Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Name })
                                .ToList();

            ViewBag.UOM = unitOfMeasureService
                         .GetAll()
                         .OrderBy(x => x.Name)
                         .Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Name })
                         .ToList();

            ViewBag.Category = categoryService
                              .GetAll()
                              .Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Name })
                              .ToList();

            StyleTraitValue styleTraitValue = new StyleTraitValue();
            if (productId > 0)
            {
                var dbval = styleTraitValueProductService.FindBy(x => x.ProductId == productId).FirstOrDefault();
                styleTraitValue = dbval != null ? styleTraitValueService.FindBy(x => x.ID == dbval.StyleTraitValueId).FirstOrDefault() : null;
            }

            if (styleTraitValue != null)
            {
                ViewBag.StyleTraitValue = styleTraitValueService
                                    .GetAll()
                                    .Where(x => x.StyleTraitId == styleTraitValue.StyleTraitId)
                                    .Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Value })
                                    .ToList();
            }
            else
            {
                ViewBag.StyleTraitValue = styleTraitValueService
                                  .GetAll()
                                  .Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Value })
                                  .Take(0);
            }

        }
    }
}