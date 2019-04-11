namespace SimpleMiner.Parsing.Html
{
    public class ParseBy
    {
        public ParseBy(string tagName, string tagValue)
        {
            TagName = tagName;
            TagValue = tagValue;
        }

        public readonly string TagName;
        public readonly string TagValue;

        public static ParseBy Id(string value)
        {
            return TagDefinition("Id", value);
        }

        private static ParseBy TagDefinition(string tagName, string tagValue)
        {
            return new ParseBy(tagName, tagValue);
        }
    }
}
