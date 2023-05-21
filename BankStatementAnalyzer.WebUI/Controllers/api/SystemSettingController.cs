using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BankStatementAnalyzer.WebUI.Controllers.api
{
    [AllowAnonymous]
    public class SystemSettingController : ApiController
    {
        private readonly ISystemSettingService systemSettingService;

        public SystemSettingController(ISystemSettingService systemSettingService)
        {
            this.systemSettingService = systemSettingService;
        }

        [Route("api/SystemSetting/Get")]
        public HttpResponseMessage Get()
        {
            var val = systemSettingService.FindBy(x => x.IsVisibleToAdmin == true).ToList();

            if (val.Count() > 0)
            {
                List<SystemSettings> SystemSetting = new List<SystemSettings>();
                foreach (var item in val)
                {
                    SystemSettings systemSettings = new SystemSettings
                    {
                        Comments = item.Comments,
                        Id = item.ID,
                        Name = item.Name,
                        SettingType = item.SettingType.ToString(),
                        Value = item.Value
                    };

                    SystemSetting.Add(systemSettings);
                }
                return Request.CreateResponse(HttpStatusCode.OK, SystemSetting);
            }

            return Request.CreateResponse(HttpStatusCode.OK, new { message = "System setting not found." });
        }

        public class SystemSettings
        {
            public int Id { get; set; }

            public string SettingType { get; set; }

            public string Name { get; set; }

            public string Value { get; set; }

            public string Comments { get; set; }
        }
    }
}