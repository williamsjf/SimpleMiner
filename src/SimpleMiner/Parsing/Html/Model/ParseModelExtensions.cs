using SimpleMiner.Parsing;
using SimpleMiner.Parsing.Html;
using SimpleMiner.Parsing.Html.Model;
using SimpleMiner.Parsing.Html.Results;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SimpleMiner
{
    public static class ParseModelExtensions
    {
        public static HtmlModelParserColletion<TModel> ExtractModels<TModel>(this HtmlParser parser)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(parser.Html);
            bool success = false;
            var modelCollection = new HtmlModelParserColletion<TModel>();
            if (ObjHasAttribute<BaseXPathAttribute>(typeof(TModel)))
            {
                var baseNodes = GetHtmlNodeCollection<TModel>(parser.Html);
                if (baseNodes == null)
                    return modelCollection;

                foreach (var baseNode in baseNodes)
                {
                    var model = Activator.CreateInstance<TModel>();

                    // Obtém as propriedades do modelo
                    var properties = typeof(TModel).GetProperties();
                    foreach (PropertyInfo propertyInfo in properties)
                    {
                        if (PropertyHasAttribute<SingleNodeXPathAttribute>(propertyInfo))
                        {
                            var singleNode = GetSingleNode(propertyInfo, baseNode);
                            if (singleNode == null)
                                continue;

                            success = true;
                            string text = GetValueFromSingleNode(propertyInfo, singleNode);
                            propertyInfo.SetValue(model, Convert.ChangeType(text, propertyInfo.PropertyType));
                        }

                        if (PropertyHasAttribute<CollectionPropertyXPathAttribute>(propertyInfo))
                        {
                            var nodes = GetHtmlNodeCollection(propertyInfo, baseNode);

                            var list = Activator.CreateInstance(propertyInfo.PropertyType);
                            foreach (var node in nodes)
                            {
                                string text = string.Empty;
                                if (PropertyHasAttribute<SingleNodeXPathAttribute>(propertyInfo))
                                {
                                    text = GetValueFromSingleNode(propertyInfo, baseNode);
                                }
                                else
                                {
                                    text = node.GetValueFromNode();
                                }

                                success = true;
                                list.GetType().GetMethod("Add").Invoke(list, new[] { text });
                            }

                            propertyInfo.SetValue(model, Convert.ChangeType(list, propertyInfo.PropertyType));
                        }
                    }

                    modelCollection.Models.Add(model);
                }
            }
            else
            {
                var model = Activator.CreateInstance<TModel>();
                var properties = typeof(TModel).GetProperties();
                success = false;
                foreach (PropertyInfo propertyInfo in properties)
                {
                    if (PropertyHasAttribute<SingleNodeXPathAttribute>(propertyInfo))
                    {
                        var singleNode = GetSingleNode(propertyInfo, htmlDocument.DocumentNode);
                        if (singleNode == null)
                            continue;

                        success = true;
                        string text = GetValueFromSingleNode(propertyInfo, singleNode);
                        propertyInfo.SetValue(model, Convert.ChangeType(text, propertyInfo.PropertyType));
                    }

                    if (PropertyHasAttribute<CollectionPropertyXPathAttribute>(propertyInfo))
                    {
                        var nodes = GetHtmlNodeCollection(propertyInfo, htmlDocument.DocumentNode);
                        if (nodes == null || !nodes.Any())
                        {
                            continue;
                        }

                        var list = Activator.CreateInstance(propertyInfo.PropertyType);
                        foreach (var node in nodes)
                        {
                            string text = string.Empty;
                            if (PropertyHasAttribute<SingleNodeXPathAttribute>(propertyInfo))
                            {
                                text = GetValueFromSingleNode(propertyInfo, htmlDocument.DocumentNode);
                            }
                            else
                            {
                                text = node.GetValueFromNode();
                            }

                            success = true;
                            list.GetType().GetMethod("Add").Invoke(list, new[] { text });
                        }

                        propertyInfo.SetValue(model, Convert.ChangeType(list, propertyInfo.PropertyType));
                    }
                }

                if (success)
                    modelCollection.Models.Add(model);
            }

            if (!modelCollection.Models.Any())
                modelCollection.Success = false;

            return modelCollection;
        }

        private static string GetValueFromSingleNode(PropertyInfo propertyInfo, HtmlNode singleNode)
        {
            string text = string.Empty;
            if (PropertyHasAttribute<PropertyXpathAttribute>(propertyInfo))
            {
                text = GetValueFromAttribute(propertyInfo, singleNode);
            }
            else
            {
                text = singleNode.GetValueFromNode();
            }

            if (PropertyHasAttribute<RegexAtrribute>(propertyInfo))
            {
                text = GetValueFromRegex(propertyInfo, text);
            }

            return text;
        }

        public static HtmlModelParser<TModel> ExtractModel<TModel>(this HtmlParser htmlParser)
        {
            var modelParser = new HtmlModelParser<TModel>();
            var models = ExtractModels<TModel>(htmlParser);
            if (models.Success)
            {
                modelParser.Model = models.Models.FirstOrDefault();
            }
            else
            {
                modelParser.Success = false;
            }

            return modelParser;
        }

        private static string GetValueFromRegex(PropertyInfo propertyInfo, string textValue)
        {
            // Verifica se possui algum nome de atributo.
            var regexAtrribute = GetAttribute<RegexAtrribute>(propertyInfo);

            foreach (var expression in regexAtrribute.Expressions)
            {
                var match = Regex.Match(textValue, expression);
                if (match.Success)
                {
                    if (!string.IsNullOrEmpty(regexAtrribute.FromGroup))
                    {
                        return match.Groups[regexAtrribute.FromGroup].Value;
                    }

                    return match.Value;
                }
            }

            return string.Empty;
        }

        private static string GetValueFromAttribute(PropertyInfo propertyInfo, HtmlNode node)
        {
            var attribute = GetAttribute<PropertyXpathAttribute>(propertyInfo);

            foreach (var attributeName in attribute.Attributes)
            {
                string value = node.GetAttributeValue(attributeName, string.Empty);
                if (string.IsNullOrEmpty(value))
                {
                    continue;
                }

                return HtmlExtensions.DecodeHtml(value);
            }

            return string.Empty;
        }

        private static HtmlNode GetSingleNode(PropertyInfo propertyInfo, HtmlNode baseNode)
        {
            var singleXpath = GetAttribute<SingleNodeXPathAttribute>(propertyInfo);

            foreach (string xpath in singleXpath.Xpaths)
            {
                var singleNode = baseNode.SelectSingleNode(xpath);
                if (singleNode == null)
                    continue;

                return singleNode;
            }

            return null;
        }

        private static HtmlNodeCollection GetHtmlNodeCollection<TModel>(string html)
        {
            var baseXpaths = GetAttribute<TModel, BaseXPathAttribute>();

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            foreach (var xpath in baseXpaths.Xpaths)
            {
                var nodes = htmlDocument.DocumentNode.SelectNodes(xpath);
                if (nodes == null)
                    continue;

                return nodes;
            }

            return null;
        }

        private static HtmlNodeCollection GetHtmlNodeCollection(PropertyInfo propertyInfo, HtmlNode baseNode)
        {
            var collectionXpath = GetAttribute<CollectionPropertyXPathAttribute>(propertyInfo);

            var nodes = baseNode.SelectNodes(collectionXpath.Xpath);
            if (nodes == null)
                return null;

            return nodes;
        }

        private static TAttribute GetAttribute<TAttribute>(PropertyInfo property) where TAttribute : HtmlParseAttribute
        {
            return property.GetCustomAttributes(false).OfType<TAttribute>()
                .FirstOrDefault();
        }

        private static TAttribute GetAttribute<TModel, TAttribute>() where TAttribute : HtmlParseAttribute
        {
            return typeof(TModel).GetCustomAttributes(false).OfType<TAttribute>()
                .FirstOrDefault();
        }

        //private static TAttribute GetAttribute<TAttribute>(PropertyInfo obj) where TAttribute : HtmlParseAttribute
        //{
        //    return obj.GetCustomAttributes(false).OfType<TAttribute>().FirstOrDefault();
        //}

        public static bool PropertyHasAttribute<TAttribute>(PropertyInfo property)
        {
            var result = property.GetCustomAttributes(false).OfType<TAttribute>();
            if (result == null || !result.Any())
                return false;

            return true;
        }

        public static bool ObjHasAttribute<TAttribute>(Type type) where TAttribute : BaseXPathAttribute
        {
            var result = type.GetCustomAttributes(false).OfType<TAttribute>();
            if (result == null || !result.Any())
                return false;

            return true;
        }
    }
}
