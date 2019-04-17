using System.Net.Http;
using System.Threading.Tasks;

namespace SimpleMiner.Navigation.Http
{
    public static class HttpExtensions
    {
        public static async Task<object> ReadHttpContentAsync<TContent>(this HttpContent httpContent)
        {
            if (typeof(TContent) == typeof(string))
                return await httpContent.ReadAsStringAsync();
            if (typeof(TContent) == typeof(byte[]))
                return await httpContent.ReadAsByteArrayAsync();
            else
                return await httpContent.ReadAsAsync<TContent>();
        }
    }
}
