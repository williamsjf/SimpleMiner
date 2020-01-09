using SimpleMiner.Parsing;
using SimpleMiner.Parsing.Html;
using SimpleMiner.Parsing.Html.Components;
using SimpleMiner.Parsing.Html.Results;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace SimpleMiner
{
    public class EmailComponent : HtmlParseResult, IComponent
    {
        public EmailComponent()
        {
            Addresses = new Collection<string>();
        }
        public ICollection<string> Addresses { get; set; }
    }

    public static class EmailExtractorExtensions
    {
        public static EmailComponent ExtractEmails(this HtmlParser parser,
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
            var component = new EmailComponent();
            string pattern = @"[a-z0-9\._]{2,}@[a-z0-9]{2,}\.[a-z0-9]{2,}";

            var matches = Regex.Matches(text, pattern);
            if (matches != null)
            {
                foreach (Match match in matches)
                {
                    if (match.Success)
                    {
                        if (ignoreDuplicate && component.Addresses.Any(x => x == match.Value))
                            continue;

                        component.Addresses.Add(match.Value);
                    }
                }
            }

            if (includeMasked)
            {
                pattern = @"[a-z0-9\*]{2,}\@[a-z0-9\*]{2,}?(?=[\s<])";
                matches = Regex.Matches(text, pattern);
                if (matches != null)
                {
                    foreach (Match match in matches)
                    {
                        if (match.Success)
                        {
                            if (ignoreDuplicate && component.Addresses.Any(x => x == match.Value))
                                continue;

                            component.Addresses.Add(match.Value);
                        }
                    }
                }
            }

            return component;
        }
    }
}
