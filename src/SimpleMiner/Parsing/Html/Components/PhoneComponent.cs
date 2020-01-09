using SimpleMiner.Parsing.Html.Results;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace SimpleMiner.Parsing.Html.Components
{
    public class PhoneComponent : HtmlParseResult, IComponent
    {
        public PhoneComponent()
        {
            Phones = new Collection<string>();
        }

        public ICollection<string> Phones { get; set; }
    }

    public static class PhoneComponentExtensions
    {
        public static PhoneComponent ExtractPhones(this HtmlParser parser,
            bool onlyInnerText = false, bool includeMasked = false, bool ignoreDuplicate = true)
        {
            string text;
            if (onlyInnerText)
            {
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(parser.Html);
                text = htmlDocument.DocumentNode.InnerText;
            }
            else
            {
                text = parser.Html;
            }

            text = HtmlExtensions.DecodeHtml(text);
            string pattern = @"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$";

            var component = new PhoneComponent();
            var matches = Regex.Matches(text, pattern);
            if (matches != null)
            {
                foreach (Match match in matches)
                {
                    if (match.Success)
                    {
                        if (ignoreDuplicate && component.Phones.Any(x => x == match.Value))
                            continue;

                        component.Phones.Add(match.Value);
                    }
                }
            }

            if (includeMasked)
            {
                pattern = @"[\+0-9\*]{8,15}?(?=[\s<])";
                matches = Regex.Matches(text, pattern);
                if (matches != null)
                {
                    foreach (Match match in matches)
                    {
                        if (match.Success)
                        {
                            if (ignoreDuplicate && component.Phones.Any(x => x == match.Value))
                                continue;

                            component.Phones.Add(match.Value);
                        }
                    }
                }
            }

            return component;
        }
    }
}
