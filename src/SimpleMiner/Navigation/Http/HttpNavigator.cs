using SimpleMiner.Parsing.Html.Components;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SimpleMiner.Navigation.Http
{
    public class HttpNavigator : IHttpNavigator
    {
        private readonly HttpClient Client;

        public HttpNavigator(HttpClient httpClient)
        {
            Client = httpClient;
        }

        public async Task<HttpResponse<string>> GetAsync(string url)
        {
            return await GetAsync<string>(url);
        }

        public async Task<HttpResponse<TContent>> GetAsync<TContent>(string url)
        {
            var requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url),
            };

            return await ExecuteRequest<TContent>(requestMessage);
        }

        public async Task<HttpResponse<TContent>> ExecuteRequest<TContent>(HttpRequestMessage httpRequestMessage)
        {
            var httpResponse = new HttpResponse<TContent>();
            try
            {
                var httpResponseMessage = await Client.SendAsync(httpRequestMessage);

                httpResponse.Status = httpResponseMessage.StatusCode;
                httpResponse.SetValue((TContent)await httpResponseMessage.Content.ReadHttpContentAsync<TContent>());
                httpResponse.Message = httpResponseMessage.ReasonPhrase;
            }
            catch (Exception e)
            {
                httpResponse.Message = e.ToString();
                httpResponse.Status = HttpStatusCode.BadRequest;
            }

            return httpResponse;
        }

        public Task<HttpResponse<TContent>> PostAsync<TContent>(string url, Dictionary<string, string> parameters)
        {
            var encodedContent = new FormUrlEncodedContent(parameters);

            var requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(url, UriKind.RelativeOrAbsolute),
                Content = encodedContent,
            };

            return ExecuteRequest<TContent>(requestMessage);
        }

        public async Task<HttpResponse<TContent>> SubmitForm<TContent>(HtmlFormComponent form)
        {
            if (form.Method.Equals("GET"))
            {
                throw new NotImplementedException();
            }

            return await PostAsync<TContent>(form.Action, form.Values);
        }

        public async Task<HttpResponse<string>> SubmitForm(HtmlFormComponent form)
        {
            return await SubmitForm<string>(form);
        }

        public async Task<Base64FileResponse> DownloadFile(string url)
        {
            var requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url),
            };

            var response = await ExecuteRequest<byte[]>(requestMessage);
            return new Base64FileResponse(response);
        }
    }
}
