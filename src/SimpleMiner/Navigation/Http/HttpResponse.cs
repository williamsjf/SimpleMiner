using SimpleMiner.Model.Response;
using System.Net;

namespace SimpleMiner.Navigation.Http
{
    public class HttpResponse<TContent> : Response<TContent>
    {
        public HttpStatusCode Status { get; set; }
    }
}
