using System.Net.Http;
using System.Threading.Tasks;

namespace SimpleMiner.Extensions
{
    public static class HttpExtensions
    {
        public static object ReadHttpContent<TContent>(this HttpContent httpContent)
        {
            if (typeof(TContent) == typeof(string))
                return httpContent.ReadAsStringAsync().Result;
            if (typeof(TContent) == typeof(byte[]))
                return httpContent.ReadAsByteArrayAsync().Result;
            else
                return httpContent.ReadAsStreamAsync().Result;
        }
    }
}
