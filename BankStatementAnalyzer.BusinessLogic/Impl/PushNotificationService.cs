using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class PushNotificationService : IPushNotificationService
    {
        private readonly IPushNotificationRepository PushNotificationRepository;

        public PushNotificationService(IPushNotificationRepository PushNotificationRepository)
        {
            this.PushNotificationRepository = PushNotificationRepository;
        }

        public void Add(PushNotification entity)
        {
            PushNotificationRepository.Add(entity);
        }

        public void Delete(PushNotification entity)
        {
            PushNotificationRepository.Delete(entity);
        }

        public void Edit(PushNotification entity)
        {
            PushNotificationRepository.Edit(entity);
        }

        public IQueryable<PushNotification> FindBy(Expression<Func<PushNotification, bool>> predicate)
        {
            return PushNotificationRepository.FindBy(predicate);
        }

        public IQueryable<PushNotification> GetAll()
        {
            return PushNotificationRepository.GetAll();
        }

        public void Save()
        {
            PushNotificationRepository.Save();
        }

        public async Task<bool> Send(string title, string text, string notificationKeyOrDeviceId, string serverValue, string url)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var data = new
                {
                    title,
                    text,
                };

                //var body = new
                //{
                //    priority = "HIGH",
                //    notification = new
                //    {
                //        title,
                //        body = text,
                //        image = "http://admin.kgnschool.com/images/logo/icon.jpeg"
                //    },
                //    to = notificationKeyOrDeviceId
                //};

                var body = new
                {
                    priority = "HIGH",
                    notification = new
                    {
                        title,
                        body = text
                    },
                    to = notificationKeyOrDeviceId
                };

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"key={serverValue}");
                //httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Sender", $"id={serverId.Value}");

                HttpResponseMessage httpResponse = await httpClient.PostAsJsonAsync($"{url}", body);
                var response = await httpResponse.Content.ReadAsStringAsync();

                return httpResponse.IsSuccessStatusCode;
            }
        }
    }
}