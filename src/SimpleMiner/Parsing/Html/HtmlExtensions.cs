using HtmlAgilityPack;
using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace SimpleMiner.Parsing.Html
{
    public static class HtmlExtensions
    {
        public static string GetValueFromNode(this HtmlNode htmlNode, string xpath = "")
        {
            if (htmlNode == null)
                return string.Empty;

            if (!string.IsNullOrEmpty(xpath))
                htmlNode = htmlNode.SelectSingleNode(xpath);

            if (htmlNode == null)
                return string.Empty;

            string value = RemoveSpaces(htmlNode.InnerText);
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            value = DecodeHtml(value);
            value = RemoveSpaces(value);

            return value;
        }

        public static string DecodeHtml(string value)
        {
            value = WebUtility.HtmlDecode(value);
            value = WebUtility.HtmlDecode(value);

            return value;
        }

        public static string RemoveSpaces(string html, bool singleLine = false)
        {
            html = DecodeHtml(html);
            string value = string.Empty;
            try
            {
                if (!singleLine)
                {
                    string[] lines = html.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                    StringBuilder sb = new StringBuilder();
                    foreach (var line in lines)
                        sb.AppendLine(Regex.Replace(line, @"\s*(<[^>]+>)\s*", "$1"));

                    value = sb.ToString();
                }

                value = Regex.Replace(html, @"\s*(<[^>]+>)\s*", "$1");
            }
            catch (Exception)
            {
                return html;
            }

            return value.Trim();
        }

        public static string BuildXPath(this ParseBy parseBy)
        {
            if (parseBy.ParseByType == HtmlParseByType.Class)
                return $"//*[@class='{parseBy.Value}']";

            if (parseBy.ParseByType == HtmlParseByType.Id)
                return $"//*[@id='{parseBy.Value}']";

            return parseBy.Value;
        }
    }
}
