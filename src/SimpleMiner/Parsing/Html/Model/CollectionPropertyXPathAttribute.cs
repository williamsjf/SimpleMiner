namespace SimpleMiner.Parsing.Html.Model
{
    public class CollectionPropertyXPathAttribute : HtmlParseAttribute
    {
        public string Xpath { get; private set; }
        public CollectionPropertyXPathAttribute(string xpath)
        {
            Xpath = xpath;
        }
    }
}
