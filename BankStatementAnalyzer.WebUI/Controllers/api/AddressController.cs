using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.WebUI.ViewModels.Api;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BankStatementAnalyzer.WebUI.Controllers.api
{
    public class AddressController : ApiController
    {
        private readonly ICustomerService customerService;
        private readonly IAddressService addressService;

        public AddressController(ICustomerService customerService,
            IAddressService addressService)
        {
            this.customerService = customerService;
            this.addressService = addressService;
        }

        [HttpGet]
        [Route("api/Address/Get")]
        public HttpResponseMessage Get(string customerId)
        {
            var user = customerService.FindBy(x => x.UID == customerId).FirstOrDefault();

            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Invalid customer details." });
            }

            var addressList = (from address in addressService.GetAll().ToList()
                               join customer in customerService.GetAll().ToList() on address.CustomerID equals customer.Id
                               where address.CustomerID == user.Id & address.Status == true
                               select new AddressViewModel
                               {
                                   Id = address.ID,
                                   Name = address.Name,
                                   Area = address.Area,
                                   CustomerID = user.UID,
                                   DeliveryAddress = address.DeliveryAddress,
                                   Flat = address.Flat,
                                   LandMark = address.LandMark,
                                   Latitude = address.Latitude,
                                   Longitude = address.Longitude,
                                   AddressType = address.AddressType,
                                   Pincode = address.Pincode
                               });

            return Request.CreateResponse(HttpStatusCode.OK, addressList.ToList());
        }

        [HttpPost]
        [Route("api/Address/Post")]
        public HttpResponseMessage Post(AddressViewModel address)
        {
            var user = customerService.FindBy(x => x.UID == address.CustomerID).FirstOrDefault();

            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Invalid customer details." });
            }

            Address model = new Address
            {
                CustomerID = user.Id,
                DeliveryAddress = address.DeliveryAddress,
                Name = address.Name,
                Flat = address.Flat,
                LandMark = address.LandMark,
                Area = address.Area,
                CreatedBy = user.Username,
                Latitude = address.Latitude,
                Longitude = address.Longitude,
                AddressType = address.AddressType,
                Pincode = address.Pincode
            };

            addressService.Add(model);

            try
            {
                addressService.Save();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.InnerException);
            }

            return Request.CreateResponse(HttpStatusCode.Created, address);
        }

        [HttpPost]
        [Route("api/Address/Edit")]
        public HttpResponseMessage Edit(AddressViewModel address)
        {
            var user = customerService.FindBy(x => x.UID == address.CustomerID).FirstOrDefault();

            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Invalid customer details." });
            }

            var dbAddress = addressService.FindBy(x => x.ID == address.Id).FirstOrDefault();

            if (dbAddress == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Invalid address details." });
            }

            dbAddress.CustomerID = user.Id;
            dbAddress.Name = address.Name;
            dbAddress.DeliveryAddress = address.DeliveryAddress;
            dbAddress.Flat = address.Flat;
            dbAddress.LandMark = address.LandMark;
            dbAddress.Area = address.Area;
            dbAddress.UpdatedBy = user.Username;
            dbAddress.Latitude = address.Latitude;
            dbAddress.Longitude = address.Longitude;
            dbAddress.UpdatedDate = DateTime.Now;
            dbAddress.AddressType = address.AddressType;
            dbAddress.Pincode = address.Pincode;

            addressService.Edit(dbAddress);

            try
            {
                addressService.Save();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.InnerException);
            }

            return Request.CreateResponse(HttpStatusCode.Created, address);
        }

        [HttpPost]
        [Route("api/Address/Delete")]
        public IHttpActionResult Delete(DeleteVM deleteVM)
        {
            var dbAddress = addressService.FindBy(x => x.ID == deleteVM.Id).FirstOrDefault();

            if (dbAddress == null)
            {
                return NotFound();
            }

            dbAddress.Status = false;

            addressService.Edit(dbAddress);
            addressService.Save();

            return Ok(new { message = "Success" });
        }
    }
}