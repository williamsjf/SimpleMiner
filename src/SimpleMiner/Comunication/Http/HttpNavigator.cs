using SimpleMiner.Extensions;
using SimpleMiner.Parsing.Html.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleMiner.Comunication.Http
{
    public class ResponseSizeHandler : DelegatingHandler
    {        
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);
            if (response.Content != null)
            {
                await response.Content.LoadIntoBufferAsync();
                var bodylength = response.Content.Headers.ContentLength;
                var headerlength = response.Headers.ToString().Length;
            }
            return response;
        }
    }

    public class HttpNavigator
    {
        private CookieContainer Cookies = new CookieContainer();

        protected HttpClient HttpClient { get; set; }

        List<string> UserAgents = new List<string>
        {
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36",
            "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.121 Safari/537.36",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.157 Safari/537.36",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.113 Safari/537.36",
            "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.90 Safari/537.36",
            "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/44.0.2403.157 Safari/537.36",
            "Mozilla/5.0 (Windows NT 10.0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.121 Safari/537.36",
            "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36",
            "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.71 Safari/537.36",
            "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.1 (KHTML, like Gecko) Chrome/21.0.1180.83 Safari/537.1",
            "Mozilla/5.0 (Windows NT 5.1; rv:7.0.1) Gecko/20100101 Firefox/7.0.1",
            "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:54.0) Gecko/20100101 Firefox/54.0",
            "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:40.0) Gecko/20100101 Firefox/40.1",
            "Mozilla/5.0 (Windows NT 5.1; rv:52.0) Gecko/20100101 Firefox/52.0",
            "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:51.0) Gecko/20100101 Firefox/51.0",
            "Mozilla/5.0 (Windows NT 5.1; rv:29.0) Gecko/20100101 Firefox/29.0",
            "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:63.0) Gecko/20100101 Firefox/63.0",
            "Mozilla/5.0 (Windows NT 5.1; rv:6.0.2) Gecko/20100101 Firefox/6.0.2",
            "Mozilla/5.0 (Windows NT 6.3; Win64; x64; rv:58.0) Gecko/20100101 Firefox/58.0",
            "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:63.0) Gecko/20100101 Firefox/63.0",
            "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 1.1.4322)",
            "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1)",
            "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko",
            "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)",
            "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)",
            "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)",
            "Mozilla/4.0 (compatible; MSIE 9.0; Windows NT 6.1; 125LA; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)",
            "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322)",
            "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.0)",
            "Mozilla/5.0 (Windows NT 6.1; Trident/7.0; rv:11.0) like Gecko",
        };

        public HttpNavigator(HttpNavigatorBuilder HttpNavigatorBuilder)
        {
            var httpHandler = HttpNavigatorBuilder.GetHttpHandler();
            httpHandler.CookieContainer = Cookies;

            HttpClient = new HttpClient(httpHandler);
            HttpClient.DefaultRequestHeaders.Add("User-Agent", UserAgents[new Random().Next(0, UserAgents.Count - 1)]);
        }

        public HttpNavigator()
        {
            var builder = new HttpNavigatorBuilder();
            var handler = builder.GetHttpHandler();
            handler.CookieContainer = Cookies;

            HttpClient = HttpClientFactory.Create(handler);
            HttpClient.DefaultRequestHeaders.Add("User-Agent", UserAgents[new Random().Next(0, UserAgents.Count - 1)]);
        }

        public HttpResponse<string> Get(string url, Action<RequestOptionsBuilder> action = null)
        {
            return Get<string>(url, action);
        }

        public HttpResponse<TContent> Get<TContent>(string url, Action<RequestOptionsBuilder> action = null)
        {
            var requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url, UriKind.RelativeOrAbsolute),
            };

            return Send<TContent>(requestMessage, action);
        }

        public HttpResponse<TContent> SubmitForm<TContent>(HtmlFormComponent form, Action<RequestOptionsBuilder> action = null)
        {
            if (form.Method.Equals("GET"))
            {
                throw new NotImplementedException();
            }

            return Post<TContent>(form.Action, form.Values, action);
        }

        public HttpResponse<TContent> Post<TContent>(string url, IDictionary<string, string> parameters, Action<RequestOptionsBuilder> action = null)
        {
            var encodedContent = new FormUrlEncodedContent(parameters);

            var requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(url, UriKind.RelativeOrAbsolute),
                Content = encodedContent,
            };

            return Send<TContent>(requestMessage, action);
        }

        public HttpResponse<TContent> PostAsJson<TContent>(string url, string postData, Action<RequestOptionsBuilder> action = null)
        {
            var requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(url, UriKind.RelativeOrAbsolute),
                Content = new StringContent(postData, System.Text.Encoding.UTF8, "application/json"),
            };

            return Send<TContent>(requestMessage, action);
        }

        public HttpResponse<string> SubmitForm(HtmlFormComponent form, Action<RequestOptionsBuilder> action = null)
        {
            return SubmitForm<string>(form, action);
        }

        protected HttpResponse<TContent> Send<TContent>(HttpRequestMessage httpRequestMessage, Action<RequestOptionsBuilder> action = null)
        {
            var httpResponse = new HttpResponse<TContent>();
            try
            {
                if (HttpClient.BaseAddress == null)
                {
                    HttpClient.BaseAddress = httpRequestMessage.RequestUri;
                }

                SetRequestHeaders(httpRequestMessage, action);
                
                using (var httpResponseMessage = HttpClient.SendAsync(httpRequestMessage).Result)
                {
                    IEnumerable<Cookie> responseCookies = 
                        Cookies.GetCookies(httpRequestMessage.RequestUri).Cast<Cookie>();

                    httpResponse.Status = httpResponseMessage.StatusCode;
                    httpResponse.SetContentValue((TContent)httpResponseMessage.Content.ReadHttpContent<TContent>());
                    httpResponse.Message = httpResponseMessage.ReasonPhrase;

                    var bodylength = httpResponseMessage.Content.Headers.ContentLength;
                }
            }
            catch (Exception e)
            {
                httpResponse.Message = e.ToString();
                httpResponse.Status = HttpStatusCode.BadRequest;
            }

            return httpResponse;
        }

        protected void SetRequestHeaders(HttpRequestMessage httpRequestMessage, Action<RequestOptionsBuilder> action) 
        {
            if (action != null)
            {
                var optionsBuilder = new RequestOptionsBuilder();
                action(optionsBuilder);

                if (optionsBuilder.Options.Headers.Any())
                {
                    foreach (var header in optionsBuilder.Options.Headers)
                    {
                        httpRequestMessage.Headers.Add(header.Key, header.Value);
                    }
                }
            }
        }
    }
}
