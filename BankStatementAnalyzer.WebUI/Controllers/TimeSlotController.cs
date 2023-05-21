using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.WebUI.Common.constants;
using BankStatementAnalyzer.WebUI.Common.logger;
using BankStatementAnalyzer.WebUI.Filters;
using System.Linq;
using System.Web.Mvc;

namespace BankStatementAnalyzer.WebUI.Controllers
{
    [RouteAuthorize]
    //[OutputCache(NoStore = true, Duration = 0)]
    public class TimeSlotController : BaseController
    {
        private readonly ITimeSlotService timeSlotService;

        public TimeSlotController(ITimeSlotService timeSlotService,
            IUserMasterService userMasterService,
            ILoggerFacade<BaseController> logger) : base(userMasterService, logger)
        {
            this.timeSlotService = timeSlotService;
        }

        // GET: TimeSlot
        public ActionResult Index()
        {
            
            return View(timeSlotService.GetAll().ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TimeSlot timeSlot)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;

                return View(timeSlot);
            }

            timeSlot.CreatedBy = UserName;

            timeSlotService.Add(timeSlot);
            timeSlotService.Save();

            TempData["success-message"] = ConstantValue.MASTER_CREATE_SUCCSESS_MESSAGE;

            if (timeSlot.CreateAnother)
            {
                return RedirectToAction("Create");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(timeSlotService.FindBy(x => x.ID == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Edit(int id, TimeSlot timeSlot)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;
                return View(timeSlot);
            }

            var modal = timeSlotService.FindBy(x => x.ID == id).FirstOrDefault();

            modal.From = timeSlot.From;
            modal.To = timeSlot.To;
            modal.UpdatedBy = UserName;
            modal.Status = timeSlot.Status;

            timeSlotService.Edit(modal);
            timeSlotService.Save();

            TempData["success-message"] = ConstantValue.MASTER_EDIT_SUCCSESS_MESSAGE;

            return RedirectToAction("Index");
        }
    }
}