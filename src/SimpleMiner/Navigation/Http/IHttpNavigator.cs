using SimpleMiner.Parsing.Html.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleMiner.Navigation.Http
{
    public interface IHttpNavigator : INavigator
    {
        Task<HttpResponse<string>> GetAsync(string url);
        Task<HttpResponse<TContent>> GetAsync<TContent>(string url);
        Task<HttpResponse<TContent>> PostAsync<TContent>(string url, Dictionary<string, string> parameters);
        Task<HttpResponse<TContent>> SubmitForm<TContent>(HtmlFormComponent form);
        Task<HttpResponse<string>> SubmitForm(HtmlFormComponent form);

        Task<Base64FileResponse> DownloadFile(string url);
    }
}
