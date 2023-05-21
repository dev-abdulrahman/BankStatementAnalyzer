using BankStatementAnalyzer.BusinessLogic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BankStatementAnalyzer.WebUI.Controllers.api
{
    public class PackageController : ApiController
    {
        private readonly IPackageService packageService;

        public PackageController(IPackageService packageService)
        {
            this.packageService = packageService;
        }

        [HttpGet]
        [Route("api/Package/Get")]
        public HttpResponseMessage Get()
        {
            var packages = packageService.GetAll().Where(x => x.Status == true);

            List<Package> response = new List<Package>();

            foreach (var package in packages)
            {
                Package pckg = new Package
                {
                    Id = package.ID,
                    Name = package.Name,
                    Rate = (int)decimal.Truncate(package.Rate),
                    Dimension = package.Diemension,
                    UrgentRate = (int)decimal.Truncate(package.UrgentRate),
                    BagCount = package.BagCount,
                    Heading = package.Heading
                };

                string[] desc = package.Description.Split(',');

                foreach (var item in desc)
                {
                    pckg.Description.Add(item);
                }

                response.Add(pckg);
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }

    public class Package
    {
        public Package()
        {
            Description = new List<string>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int Rate { get; set; }

        public string Dimension { get; set; }

        public int UrgentRate { get; set; }

        public int BagCount { get; set; }

        public string Heading { get; set; }

        public List<string> Description { get; set; }
    }
}