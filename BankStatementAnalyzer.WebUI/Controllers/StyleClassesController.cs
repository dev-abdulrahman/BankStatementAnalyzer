using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.WebUI.Common.logger;
using BankStatementAnalyzer.WebUI.Filters;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace BankStatementAnalyzer.WebUI.Controllers
{
    [RouteAuthorize]
    //[OutputCache(NoStore = true, Duration = 0)]
    public class StyleClassesController : BaseController
    {
        private readonly Lazy<IStyleClassService> styleClassService;
        private readonly Lazy<IUserMasterService> userMasterService;

        public StyleClassesController(Lazy<IStyleClassService> styleClassService,
            Lazy<IUserMasterService> userMasterService,
            Lazy<ILoggerFacade<BaseController>> logger) : base(userMasterService.Value, logger.Value)
        {
            this.styleClassService = styleClassService;
            this.userMasterService = userMasterService;
        }

        // GET: StyleClasses
        public ActionResult Index()
        {
            
            return View(styleClassService.Value.GetAll().ToList());
        }

        // GET: StyleClasses/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StyleClass styleClass)
        {
            if (ModelState.IsValid)
            {
                styleClassService.Value.Add(styleClass);
                styleClassService.Value.Save();
                return RedirectToAction("Index");
            }

            return View(styleClass);
        }

        // GET: StyleClasses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            

            StyleClass styleClass = styleClassService.Value.FindBy(x => x.ID == id).FirstOrDefault();
            if (styleClass == null)
            {
                return HttpNotFound();
            }
            return View(styleClass);
        }

        // POST: StyleClasses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StyleClass styleClass)
        {
            if (ModelState.IsValid)
            {
                styleClassService.Value.Edit(styleClass);
                styleClassService.Value.Save();
                return RedirectToAction("Index");
            }
            return View(styleClass);
        }

        // GET: StyleClasses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            

            StyleClass styleClass = styleClassService.Value.FindBy(x => x.ID == id).FirstOrDefault();
            if (styleClass == null)
            {
                return HttpNotFound();
            }
            return View(styleClass);
        }

        // POST: StyleClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            StyleClass styleClass = styleClassService.Value.FindBy(x => x.ID == id).FirstOrDefault();
            styleClassService.Value.Delete(styleClass);
            styleClassService.Value.Save();
            return RedirectToAction("Index");
        }
    }
}
