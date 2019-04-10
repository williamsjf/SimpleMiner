namespace SimpleMiner.Parsing.Html
{
    public class ParseBy
    {
        public static ParseTag Tag { get; private set; }

        public static ParseBy Id(string value)
        {
            TagDefinition("Id", value);
            return new ParseBy();
        }

        private static void TagDefinition(string tagName, string tagValue)
        {
            Tag = new ParseTag
            {
                 TagName = tagName,
                 TagValue = tagValue
            };
        }
    }

    public struct ParseTag
    {
        public string TagName { get; set; }
        public string TagValue { get; set; }
    }
}
