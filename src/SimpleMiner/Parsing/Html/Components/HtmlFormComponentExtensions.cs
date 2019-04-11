using HtmlAgilityPack;
using SimpleMiner.Extensions;
using SimpleMiner.Parsing.Html;
using SimpleMiner.Parsing.Html.Components;
using System.Collections.Generic;
using System.Linq;

namespace SimpleMiner
{
    public static class HtmlFormComponentExtensions
    {
        public static IList<HtmlFormComponent> GetForms(this IHtmlParser htmlParser, ParseBy parseBy)
        {
            return GetForms(htmlParser.Html, parseBy.Value, parseBy.ParseByType.GetDescription());
        }

        public static HtmlFormComponent GetForm(this IHtmlParser htmlParser, ParseBy parseBy)
        {
            return GetForms(htmlParser.Html, parseBy.Value, parseBy.ParseByType.GetDescription())
                .FirstOrDefault();
        }

        private static IList<HtmlFormComponent> GetForms(string html, string nameOrId, string attributeName)
        {
            // Fonte : http://stackoverflow.com/questions/9623609/using-htmlagilitypack-to-get-all-the-values-of-a-select-element
            HtmlNode.ElementsFlags.Remove("form");
            HtmlNode.ElementsFlags.Remove("option");
            HtmlNode.ElementsFlags.Remove("select");

            var document = new HtmlDocument();
            document.LoadHtml(html);

            var formNodes = document.DocumentNode.SelectNodes("//form");
            if (formNodes != null)
            {
                var forms = GetHtmlForm(formNodes, nameOrId, attributeName);
                if (forms != null)
                {
                    var list = new List<HtmlFormComponent>();
                    foreach (var nodeForm in forms)
                    {
                        var htmlForm = new HtmlFormComponent();
                        FillFormHeader(htmlForm, nodeForm);
                        GetInputValues(htmlForm, nodeForm);
                        GetSelectValues(htmlForm, nodeForm);

                        list.Add(htmlForm);
                    }

                    return list;
                }
            }

            return null;
        }

        private static IEnumerable<HtmlNode> GetHtmlForm(HtmlNodeCollection forms, string formNameOrId, string attributeName)
        {
            foreach (var item in forms)
            {
                string name = item.GetAttributeValue(attributeName, string.Empty);
                if (name == formNameOrId)
                    yield return item;
            }
        }

        private static void GetSelectValues(HtmlFormComponent form, HtmlNode nodeForm)
        {
            var selectNodes = nodeForm.SelectNodes(".//select");
            if (selectNodes != null)
            {
                foreach (var select in selectNodes)
                {
                    string selectName = select.GetAttributeValue("name", string.Empty);

                    HtmlFormValues selectValues = new HtmlFormValues();
                    selectValues.SelectName = selectName;

                    foreach (var item in select.SelectNodes("./option"))
                    {
                        var key = item.GetAttributeValue("value", string.Empty);
                        var value = item.GetValueFromNode();

                        selectValues.SelectValues.Add(key, value);
                    }

                    form.SelectValues.Add(selectValues);
                }
            }
        }

        private static void FillFormHeader(HtmlFormComponent form, HtmlNode nodeForm)
        {
            form.Action = nodeForm.GetAttributeValue("action", string.Empty);
            form.Method = nodeForm.GetAttributeValue("method", string.Empty);
            form.Name = nodeForm.GetAttributeValue("name", string.Empty);
            form.Id = nodeForm.GetAttributeValue("id", string.Empty);
        }

        private static void GetInputValues(HtmlFormComponent form, HtmlNode formNode)
        {
            foreach (HtmlNode input in formNode.SelectNodes(".//input"))
            {
                string key = input.GetAttributeValue("name", string.Empty);
                string value = input.GetAttributeValue("value", string.Empty);

                if (string.IsNullOrEmpty(key) || form.Values.ContainsKey(key))
                    continue;

                form.Values.Add(key, value);
            }
        }
    }
}
