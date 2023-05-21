using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.WebUI.Common.logger;
using BankStatementAnalyzer.WebUI.Controllers;
using BankStatementAnalyzer.WebUI.Filters;
using BankStatementAnalyzer.WebUI.ViewModels;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Ecommerce.WebUI.Controllers
{
    [RouteAuthorize]
    //[OutputCache(NoStore = true, Duration = 0)]
    public class StyleTraitValuesController : BaseController
    {
        private readonly Lazy<IStyleTraitValueService> styleTraitValueService;
        private readonly Lazy<IStyleTraitService> styleTraitService;
        private readonly Lazy<IUserMasterService> userMasterService;

        public StyleTraitValuesController(Lazy<IStyleTraitValueService> styleTraitValueService,
            Lazy<IStyleTraitService> styleTraitService, Lazy<IUserMasterService> userMasterService,
            Lazy<ILoggerFacade<BaseController>> logger)
            : base(userMasterService.Value, logger.Value)
        {
            this.styleTraitValueService = styleTraitValueService;
            this.styleTraitService = styleTraitService;
            this.userMasterService = userMasterService;
        }

        // GET: StyleTraitValues
        public ActionResult Index()
        {
            
            var data = from stv in styleTraitValueService.Value.GetAll()
                       join st in styleTraitService.Value.GetAll() on stv.StyleTraitId equals st.ID
                       select new StyleTraitValues()
                       {
                           Id = stv.ID,
                           Description = stv.Description,
                           StyleTrait = st.Name,
                           Value = stv.Value
                       };

            return View(data.ToList());
        }

        // GET: StyleTraitValues/Create
        public ActionResult Create()
        {
            

            ViewBag.StyleTrait = styleTraitService.Value.GetAll();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StyleTraitValue styleTraitValue)
        {
            if (ModelState.IsValid)
            {
                styleTraitValueService.Value.Add(styleTraitValue);
                styleTraitValueService.Value.Save();
                return RedirectToAction("Index");
            }

            

            ViewBag.StyleTrait = styleTraitService.Value.GetAll();
            return View(styleTraitValue);
        }

        // GET: StyleTraitValues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            

            ViewBag.StyleTrait = styleTraitService.Value.GetAll();
            StyleTraitValue styleTraitValue = styleTraitValueService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            if (styleTraitValue == null)
            {
                return HttpNotFound();
            }
            return View(styleTraitValue);
        }

        // POST: StyleTraitValues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StyleTraitValue styleTraitValue)
        {
            if (ModelState.IsValid)
            {
                styleTraitValueService.Value.Edit(styleTraitValue);
                styleTraitValueService.Value.Save();
                return RedirectToAction("Index");
            }

            

            ViewBag.StyleTrait = styleTraitService.Value.GetAll();
            return View(styleTraitValue);
        }

        // GET: StyleTraitValues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            
            StyleTraitValue styleTraitValue = styleTraitValueService.Value.FindBy(x => x.ID == id).FirstOrDefault();
            if (styleTraitValue == null)
            {
                return HttpNotFound();
            }
            return View(styleTraitValue);
        }

        // POST: StyleTraitValues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            StyleTraitValue styleTraitValue = styleTraitValueService.Value.FindBy(x => x.ID == id).FirstOrDefault();
            styleTraitValueService.Value.Delete(styleTraitValue);
            styleTraitValueService.Value.Save();
            return RedirectToAction("Index");
        }
    }
}