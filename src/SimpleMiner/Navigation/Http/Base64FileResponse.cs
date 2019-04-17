using System;

namespace SimpleMiner.Navigation.Http
{
    public class Base64FileResponse : HttpResponse<string>
    {
        public Base64FileResponse(HttpResponse<byte[]> response)
        {
            Status = response.Status;
            Message = response.Message;
            Content = Convert.ToBase64String(response.Content);
        }
    }
}
