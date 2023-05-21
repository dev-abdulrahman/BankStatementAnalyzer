using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.WebUI.Common.logger;
using BankStatementAnalyzer.WebUI.Controllers;
using BankStatementAnalyzer.WebUI.Filters;
using BankStatementAnalyzer.WebUI.ViewModels;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Ecommerce.WebUI.Controllers
{
    [RouteAuthorize]
    //[OutputCache(NoStore = true, Duration = 0)]
    public class StyleTraitsController : BaseController
    {
        private readonly Lazy<IStyleTraitService> styleTraitService;
        private readonly Lazy<IStyleClassService> styleClassService;
        private readonly Lazy<IUserMasterService> userMasterService;

        public StyleTraitsController(Lazy<IStyleTraitService> styleTraitService,
            Lazy<IStyleClassService> styleClassService, Lazy<IUserMasterService> userMasterService, Lazy<ILoggerFacade<BaseController>> logger)
            : base(userMasterService.Value, logger.Value)
        {
            this.styleTraitService = styleTraitService;
            this.styleClassService = styleClassService;
            this.userMasterService = userMasterService;
        }

        // GET: StyleTraits
        public ActionResult Index()
        {
            

            var data = from st in styleTraitService.Value.GetAll()
                       join sc in styleClassService.Value.GetAll() on st.StyleClassId equals sc.ID
                       select new StyleTraits()
                       {
                           StyleClass = sc.Name,
                           Name = st.Name,
                           Description = st.Description,
                           SortOrder = st.SortOrder,
                           Id = st.ID
                       };
            return View(data.ToList());
        }

        // GET: StyleTraits/Create
        public ActionResult Create()
        {

            ViewBag.StyleClass = styleClassService.Value.GetAll();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StyleTrait styleTrait)
        {

            if (ModelState.IsValid)
            {
                styleTraitService.Value.Add(styleTrait);
                styleTraitService.Value.Save();
                return RedirectToAction("Index");
            }
            
            ViewBag.StyleClass = styleClassService.Value.GetAll();
            return View(styleTrait);
        }

        // GET: StyleTraits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            StyleTrait styleTrait = styleTraitService.Value.FindBy(x => x.ID == id).FirstOrDefault();
            ViewBag.StyleClass = styleClassService.Value.GetAll();
            if (styleTrait == null)
            {
                return HttpNotFound();
            }
            return View(styleTrait);
        }

        // POST: StyleTraits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StyleTrait styleTrait)
        {
            if (ModelState.IsValid)
            {
                styleTraitService.Value.Edit(styleTrait);
                styleTraitService.Value.Save();
                return RedirectToAction("Index");
            }

            ViewBag.StyleClass = styleClassService.Value.GetAll();
            return View(styleTrait);
        }

        // GET: StyleTraits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var styleTrait = (from st in styleTraitService.Value.GetAll()
                              join sc in styleClassService.Value.GetAll() on st.StyleClassId equals sc.ID
                              where st.ID == id
                              select new StyleTraits()
                              {
                                  StyleClass = sc.Name,
                                  Name = st.Name,
                                  Description = st.Description,
                                  SortOrder = st.SortOrder,
                                  Id = st.ID
                              }).FirstOrDefault();

            if (styleTrait == null)
            {
                return HttpNotFound();
            }
            return View(styleTrait);
        }

        // POST: StyleTraits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            StyleTrait styleTrait = styleTraitService.Value.FindBy(x => x.ID == id).FirstOrDefault();
            styleTraitService.Value.Delete(styleTrait);
            styleTraitService.Value.Save();
            return RedirectToAction("Index");
        }
    }
}
