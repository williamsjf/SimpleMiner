namespace SimpleMiner.Model.Response
{
    public abstract class Response<TContent>
    {
        public string Message { get; set; }
        public virtual TContent Content { get; protected set; }

        public virtual void SetValue(object value)
        {
            Content = (TContent)value;
        }
    }
}
