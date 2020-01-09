namespace SimpleMiner.Comunication
{
    public class Response<TContent> : IResponse
    {
        public string Message { get; set; }

        public TContent Content { get; set; }

        public virtual void SetContentValue(object value)
        {
            Content = (TContent)value;
        }
    }

    public interface IResponse
    {
        public string Message { get; set; }
    }
}
