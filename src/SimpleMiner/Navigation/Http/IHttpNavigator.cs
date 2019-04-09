using System.Threading.Tasks;

namespace SimpleMiner.Navigation.Http
{
    public interface IHttpNavigator : INavigator
    {
        Task<HttpResponse<string>> GetAsync(string url);
        Task<HttpResponse<TContent>> GetAsync<TContent>(string url);
    }
}
