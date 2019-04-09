namespace SimpleMiner.Model.Response
{
    public abstract class Response<TContent>
    {
        public string Message { get; set; }
        public TContent Content { get; set; }
    }
}
