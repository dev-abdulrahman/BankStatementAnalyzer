using BankStatementAnalyzer.BusinessLogic.Common;
using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class SendEmailService : ISendEmailService
    {
        private readonly ISystemSettingService systemSettingService;

        public SendEmailService(ISystemSettingService systemSettingService)
        {
            this.systemSettingService = systemSettingService;
        }

        public async Task<Tuple<bool, Exception>> SendAdminEmail(Customer user, Order order, DeliveryBoy deliveryBoy, string url, string subject, string vendorAddress = null)
        {
            var emailTemplate = systemSettingService
                             .FindBy(
                              x => x.SettingType == SettingType.EmailTemplate)
                             .FirstOrDefault();

            var emailTemplateForNewOrderPickUp = systemSettingService
                                                  .FindBy(
                                                   x => x.SettingType == SettingType.EmailForPickUp)
                                                  .FirstOrDefault();

            var dbEmails = systemSettingService
                                      .FindBy(
                                       x => x.SettingType == SettingType.AdminEmails)
                                      .FirstOrDefault();

            var emails = dbEmails.Value.Split('|');

            var fromMail = ConfigurationManager.AppSettings["fromMail"];
            var add = vendorAddress == null ? $"{order.Address?.Latitude},{order.Address?.Longitude}" : vendorAddress;

            var addressURL = string.Format("http://maps.google.com/maps?daddr={0}", add);
            var adminEmailBody = emailTemplate
                       .Value
                       .Replace
                        ("{{heading}}", subject)
                        .Replace("{{companyName}}", "E-Laundry")
                       .Replace
                        ("{{body}}", emailTemplateForNewOrderPickUp.Value
                        .Replace("{{name}}", deliveryBoy.Name)
                       .Replace("{{CustomerName}}", user.Username)
                       .Replace("{{orderType}}", order.OrderType?.Name)
                       .Replace("{{orderId}}", order.ID.ToString())
                       .Replace("{{paymentype}}", order.PaymentType.ToString())
                       .Replace("{{address}}", order.Address?.DeliveryAddress)
                       .Replace("{{addressurl}}", addressURL)
                       .Replace("{{url}}", url));

            List<string> lstAdmin = new List<string>();

            foreach (var email in emails)
            {
                lstAdmin.Add(email);
            }

            List<string> deliveryBoyList = new List<string>
            {
                deliveryBoy.Email
            };

            MailModel mailModelAdmin = new MailModel
            {
                FromId = fromMail,
                ToList = deliveryBoyList,
                CCList = lstAdmin,
                Subject = subject,
                MessageBody = adminEmailBody
            };

            EmailHelper emailHelperAdmin = new EmailHelper(mailModelAdmin);
            try
            {
                emailHelperAdmin.Send();
            }
            catch (Exception ex)
            {
                return new Tuple<bool, Exception>(false, ex);
            }

            return new Tuple<bool, Exception>(true, new Exception());
        }
    }
}