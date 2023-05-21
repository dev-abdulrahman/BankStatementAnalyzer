using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.WebUI.ViewModels;
using BankStatementAnalyzer.WebUI.ViewModels.Api;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BankStatementAnalyzer.WebUI.Controllers.api
{
    [AllowAnonymous]
    public class UserController : ApiController
    {
        private readonly ICustomerService customerService;
        private readonly IOrderService orderService;
        private readonly IOrderDetailService orderDetailService;
        private readonly IAddressService addressService;
        private readonly ICustomerRefferalCodeMappingService customerRefferalCodeMappingService;
        private readonly ICompanyService companyService;

        public UserController(ICustomerService customerService,
            IOrderDetailService orderDetailService,
            IOrderService orderService, IAddressService addressService,
            ICustomerRefferalCodeMappingService customerRefferalCodeMappingService,
            ICompanyService companyService)
        {
            this.customerService = customerService;
            this.orderDetailService = orderDetailService;
            this.orderService = orderService;
            this.addressService = addressService;
            this.customerRefferalCodeMappingService = customerRefferalCodeMappingService;
            this.companyService = companyService;
        }

        [HttpPost]
        [Route("api/User/Post")]
        public HttpResponseMessage Post(Customer customer)
        {
            if (!string.IsNullOrWhiteSpace(customer.FriendCode) && customerService.FindBy(x => x.ReferralCode == customer.FriendCode).FirstOrDefault() == null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { result = false, message = "Coupon details not found." });
            }

            if (string.IsNullOrWhiteSpace(customer.PhoneNumber))
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { result = false, message = "Phone number cannot be null" });
            }

            if (string.IsNullOrWhiteSpace(customer.Username))
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { result = false, message = "Username cannot be null" });
            }

            //if (customerService.FindBy(x => x.PhoneNumber == customer.PhoneNumber).FirstOrDefault() != null)
            //{
            //    return Request.CreateResponse(HttpStatusCode.BadRequest, new { result = false, message = "User already registered with same details." });
            //}

            //if(customerService.FindBy(x => x.UID == customer.UID).Any())
            //{
            //    return Request.CreateResponse(HttpStatusCode.BadRequest, new { result = false, message = "User already registered with same UID." });
            //}

            var updateModel = customerService.FindBy(x => x.UID == customer.UID || x.PhoneNumber == customer.PhoneNumber).FirstOrDefault();
            if (updateModel != null)
            {
                updateModel.Username = customer.Username;
                updateModel.UID = customer.UID;
                updateModel.PhoneNumber = customer.PhoneNumber;
                updateModel.Gender = customer.Gender;
                updateModel.MaritalStatus = customer.MaritalStatus;
                updateModel.Email = customer.Email;
                updateModel.Area = customer.Area;
                updateModel.HouseNo = customer.HouseNo;
                updateModel.NickName = customer.NickName;
                updateModel.Pincode = customer.Pincode;
                updateModel.Street = customer.Street;
                updateModel.DeviceId = customer.DeviceId;
                updateModel.ReferFromCode = customer.FriendCode;
                updateModel.BirthDate = customer.BirthDate;
                updateModel.Image = customer.Image;
                //model.ReferralCode = Guid.NewGuid().ToString().Substring(0, 5).ToUpper();
                customerService.Edit(updateModel);
            }
            else
            {
                updateModel = new BankStatementAnalyzer.Models.Customer
                {
                    Username = customer.Username,
                    UID = customer.UID,
                    PhoneNumber = customer.PhoneNumber,
                    Gender = customer.Gender,
                    MaritalStatus = customer.MaritalStatus,
                    Email = customer.Email,
                    Area = customer.Area,
                    HouseNo = customer.HouseNo,
                    NickName = customer.NickName,
                    Pincode = customer.Pincode,
                    Street = customer.Street,
                    DeviceId = customer.DeviceId,
                    ReferFromCode = customer.FriendCode,
                    BirthDate = customer.BirthDate,
                    Image = customer.Image
                };
                //model.ReferralCode = Guid.NewGuid().ToString().Substring(0, 5).ToUpper();
                customerService.Add(updateModel);
            }
            
            try
            {
                customerService.Save();

                //if (!string.IsNullOrWhiteSpace(customer.FriendCode))
                //{
                //    var customerRefferal = customerRefferalCodeMappingService.FindBy(x => x.CustomerId == model.Id && x.SelfCode == model.ReferralCode && x.RefferalCode == customer.ReferralCode).FirstOrDefault();

                //    if (customerRefferal == null)
                //    {
                //        BankStatementAnalyzer.Models.CustomerRefferalCodeMapping customerRefferalCode = new BankStatementAnalyzer.Models.CustomerRefferalCodeMapping
                //        {
                //            CustomerId = model.Id,
                //            IsCodeUsed = false,
                //            SelfCode = model.ReferralCode,
                //            RefferalCode = customer.FriendCode
                //        };

                //        customerRefferalCodeMappingService.Add(customerRefferalCode);
                //        customerRefferalCodeMappingService.Save();
                //    }
                //}
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { result = false, message = "User registration failed." });
            }

            return Request.CreateResponse(HttpStatusCode.OK, new { result = true, updateModel.Id });
        }

        [HttpPost]
        [Route("api/User/Put")]
        public HttpResponseMessage Put(Customer customer)
        {
            if (string.IsNullOrWhiteSpace(customer.PhoneNumber))
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { result = false, message = "Phone number cannot be null" });
            }

            if (string.IsNullOrWhiteSpace(customer.Username))
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { result = false, message = "Username cannot be null" });
            }

            var model = customerService.FindBy(x => x.UID == customer.UID).FirstOrDefault();

            model.Username = customer.Username;
            model.UID = customer.UID;
            model.PhoneNumber = customer.PhoneNumber;
            model.Area = customer.Area;
            model.HouseNo = customer.HouseNo;
            model.NickName = customer.NickName;
            model.Pincode = customer.Pincode;
            model.Street = customer.Street;
            model.Email = customer.Email;
            model.DeviceId = customer.DeviceId;
            model.Gender = customer.Gender;
            model.MaritalStatus = customer.MaritalStatus;
            model.BirthDate = customer.BirthDate;
            model.Image = customer.Image;

            customerService.Edit(model);

            try
            {
                customerService.Save();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { result = false, message = "User update failed." });
            }

            return Request.CreateResponse(HttpStatusCode.OK, new { result = true });
        }

        [HttpPost]
        [Route("api/User/UpdateProfile")]
        public HttpResponseMessage UpdateProfile(UpdateProfileViewModel userProfile)
        {
            if (string.IsNullOrWhiteSpace(userProfile.ImageUrl))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { result = false, message = "Image URL cannot be null" });
            }

            var model = customerService.FindBy(x => x.Id == userProfile.UserId).FirstOrDefault();
            if(model == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { result = false, message = "No User found with the given Id." });
            }

            model.Image = userProfile.ImageUrl;
            customerService.Edit(model);

            try
            {
                customerService.Save();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { result = false, message = "UserProfile update failed." });
            }

            return Request.CreateResponse(HttpStatusCode.OK, new { result = true });
        }


        [HttpGet]
        [Route("api/User/Get")]
        public HttpResponseMessage Get(string uid)
        {
            var customer = customerService.FindBy(x => x.UID == uid).FirstOrDefault();

            if (customer == null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { result = false, message = "Customer details not found." });
            }

            Customer model = new Customer
            {
                Id = customer.Id,
                UID = customer.UID,
                Username = customer.Username,
                PhoneNumber = customer.PhoneNumber,
                Gender = customer.Gender,
                MaritalStatus = customer.MaritalStatus,
                Email = customer.Email,
                Area = customer.Area,
                HouseNo = customer.HouseNo,
                NickName = customer.NickName,
                Pincode = customer.Pincode,
                Street = customer.Street,
                DeviceId = customer.DeviceId,
                ReferralCode = customer.ReferralCode,
                BirthDate = customer.BirthDate,
                Image = customer.Image
            };

            return Request.CreateResponse(HttpStatusCode.OK, new { result = true, model });
        }

        [HttpGet]
        [Route("api/User/CheckPhone")]
        public HttpResponseMessage CheckPhone(string phoneNummber)
        {
            var customer = customerService.FindBy(x => x.PhoneNumber == phoneNummber).FirstOrDefault();

            if (customer == null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { result = false });
            }

            return Request.CreateResponse(HttpStatusCode.OK, new { result = true });
        }

        [HttpGet]
        [Route("api/User/ValidateReferralCode")]
        public HttpResponseMessage ValidateReferralCode(string companyKey, string code)
        {
            var company = companyService.FindBy(x => x.Key == companyKey).FirstOrDefault();

            if (company == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Company details not found." });
            }

            var customer = customerService.FindBy(x => x.ReferralCode == code).FirstOrDefault();

            if (customer == null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { result = false });
            }

            return Request.CreateResponse(HttpStatusCode.OK, new { result = true });
        }

    }
}