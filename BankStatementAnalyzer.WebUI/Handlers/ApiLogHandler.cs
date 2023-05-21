using BankStatementAnalyzer.WebUI.Common.logger;
using BankStatementAnalyzer.WebUI.ViewModels.Api;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BankStatementAnalyzer.WebUI.Handlers
{
    public class ApiLogHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var logMetadata = await buildRequestMetadata(request);
            var response = await base.SendAsync(request, cancellationToken);
            logMetadata = await buildResponseMetadata(logMetadata, response);
            await sendToLog(request, logMetadata);
            return response;
        }

        private async Task<ApiMetadata> buildResponseMetadata(ApiMetadata logMetadata, HttpResponseMessage response)
        {
            logMetadata.ResponseStatusCode = response.StatusCode;
            if (response.Content != null)
            {
                logMetadata.ResponseBody = await response.Content.ReadAsStringAsync();
                logMetadata.ResponseContentType = response.Content.Headers.ContentType.MediaType;
            }
            logMetadata.ResponseTimestamp = DateTime.Now;
            return logMetadata;
        }

        private async Task<bool> sendToLog(HttpRequestMessage request, ApiMetadata logMetadata)
        {
            ILoggerFacade<ApiLogHandler> logger = (ILoggerFacade<ApiLogHandler>)request.GetDependencyScope()
                  .GetService(typeof(ILoggerFacade<ApiLogHandler>));

            logger.Info(Newtonsoft.Json.JsonConvert.SerializeObject(logMetadata));

            return true;
        }

        private async Task<ApiMetadata> buildRequestMetadata(HttpRequestMessage request)
        {
            string body = null;
            if (request.Content.IsMimeMultipartContent())
                body = string.Format("file attachment {0} bytes", request.Content.Headers.ContentLength);
            else
                body = await request.Content.ReadAsStringAsync();

            ApiMetadata log = new ApiMetadata
            {
                RequestMethod = request.Method.Method,
                RequestTimestamp = DateTime.Now,
                RequestBody = body,
                RequestUri = request.RequestUri.ToString()
            };
            return log;
        }
    }
}