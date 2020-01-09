using System.Net;

namespace SimpleMiner.Comunication.Http
{
    public class HttpResponse<TContent> : Response<TContent>
    {
        public HttpStatusCode Status { get; set; }
    }
}
