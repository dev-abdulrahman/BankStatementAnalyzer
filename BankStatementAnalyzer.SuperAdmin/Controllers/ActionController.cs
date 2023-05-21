using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.SuperAdmin.Common.logger;
using BankStatementAnalyzer.SuperAdmin.Filters;
using System;
using System.Linq;
using System.Web.Mvc;
using Action = BankStatementAnalyzer.Models.Action;

namespace BankStatementAnalyzer.SuperAdmin.Controllers
{
    [RouteAuthorize]
    //[OutputCache(NoStore = true, Duration = 0)]
    public class ActionController : BaseController
    {

        private readonly Lazy<IActionService> actionService;
        private readonly Lazy<ILoggerFacade<ActionController>> logger;

        public ActionController(Lazy<IActionService> actionService,
            Lazy<ILoggerFacade<ActionController>> logger,
            Lazy<IUserMasterService> userMasterService) : base(userMasterService.Value)
        {
            this.actionService = actionService;
            this.logger = logger;
        }

        public ActionResult Index()
        {
            var actiondata = actionService.Value.GetAll();
            return View(actiondata);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Action model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Name) && string.IsNullOrWhiteSpace(model.DisplayName))
                {
                    TempData["warning-message"] = "Name and Display name can not be empty.";

                    return View(model);
                }

                string[] name = model.Name.Split('.');
                string[] displayName = model.DisplayName.Split('.');

                if (name.Count() != 3 && displayName.Count() != 3)
                {
                    TempData["warning-message"] = "Name and Display name are not in specified format.";

                    return View(model);
                }

                if (actionService.Value.IsExist(model.Name))
                {
                    TempData["warning-message"] = "Name already exist.";

                    return View("create", model);
                }

                model.Feature = name[1];

                actionService.Value.Add(model);
                actionService.Value.Save();

                TempData["success-message"] = "Action added successfully.";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                logger.Value.Error(ex.Message, ex);

                return View("Error");
            }
        }


        public ActionResult Edit(int id)
        {
            var actionModel = getActionByID(id); ;

            return View(actionModel);
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult EditPost(Action model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Name) && string.IsNullOrWhiteSpace(model.DisplayName))
                {
                    TempData["warning-message"] = "Name and Display name can not be empty.";

                    return View(model);
                }

                string[] name = model.Name.Split('.');
                string[] displayName = model.DisplayName.Split('.');

                if (name.Count() != 3 && displayName.Count() != 3)
                {
                    TempData["warning-message"] = "Name and Display name are not in specified format.";

                    return View(model);
                }

                var oldRecord = getActionByID(model.ID);

                if (oldRecord.Name == model.Name && oldRecord.DisplayName == model.DisplayName)
                {
                    oldRecord.ModifiedBy = UserName;
                    oldRecord.ModifiedOn = DateTime.Now;

                    oldRecord.Feature = name[1];
                    actionService.Value.Edit(oldRecord);
                    actionService.Value.Save();
                }
                else
                {
                    var allActions = actionService.Value.FindBy(x => x.Name != oldRecord.Name && x.DisplayName != oldRecord.DisplayName).ToList();
                    var isExist = allActions.Any(x => x.Name == model.Name && x.DisplayName == model.DisplayName);

                    if (isExist)
                    {
                        TempData["warning-message"] = "Action with same name already exist.";

                        return View(model);
                    }

                    else
                    {
                        oldRecord.Feature = name[1];
                        oldRecord.Name = model.Name;
                        oldRecord.DisplayName = model.DisplayName;
                        oldRecord.ModifiedBy = UserName;
                        oldRecord.ModifiedOn = DateTime.Now;

                        actionService.Value.Edit(oldRecord);
                        actionService.Value.Save();
                    }
                }

                TempData["success-message"] = "Action edited successfully.";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                logger.Value.Error(ex.Message, ex);

                return View("Error");
            }
        }

        private Action getActionByID(int id)
        {
            return actionService.Value.FindBy(x => x.ID == id).FirstOrDefault();
        }

        public ActionResult Delete(int id)
        {
            var actionModel = actionService.Value.FindBy(x => x.ID == id).SingleOrDefault();

            return View(actionModel);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeletePost(int id)
        {
            Action model = new Action();

            try
            {
                var actionModel = actionService.Value.FindBy(x => x.ID == id).SingleOrDefault();

                actionService.Value.Delete(actionModel);
                actionService.Value.Save();

                return RedirectToAction("Index");
            }

            catch (Exception ex)
            {
                logger.Value.Error(ex.Message, ex);
                return View(model);
            }
        }

    }
}