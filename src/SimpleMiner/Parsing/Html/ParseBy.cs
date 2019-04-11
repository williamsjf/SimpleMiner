namespace SimpleMiner.Parsing.Html
{
    public class ParseBy
    {
        public ParseBy(ParseByType parseByType, string value)
        {
            ParseByType = parseByType;
            Value = value;
        }

        public readonly ParseByType ParseByType;
        public readonly string Value;

        public static ParseBy Id(string value)
        {
            return TagDefinition(ParseByType.Id, value);
        }

        public static ParseBy Xpath(string value)
        {
            return TagDefinition(ParseByType.Xpath, value);
        }

        private static ParseBy TagDefinition(ParseByType parseByType, string value)
        {
            return new ParseBy(parseByType, value);
        }
    }
}
