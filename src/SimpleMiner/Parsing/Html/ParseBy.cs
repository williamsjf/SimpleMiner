using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMiner.Parsing.Html
{
    public class ParseBy
    {
        public ParseBy(HtmlParseByType parseByType, string value)
        {
            ParseByType = parseByType;
            Value = value;
        }

        public readonly HtmlParseByType ParseByType;
        public readonly string Value;

        public static ParseBy Id(string value)
        {
            return TagDefinition(HtmlParseByType.Id, value);
        }

        public static ParseBy Xpath(string value)
        {
            return TagDefinition(HtmlParseByType.Xpath, value);
        }

        private static ParseBy TagDefinition(HtmlParseByType parseByType, string value)
        {
            return new ParseBy(parseByType, value);
        }
    }
}
