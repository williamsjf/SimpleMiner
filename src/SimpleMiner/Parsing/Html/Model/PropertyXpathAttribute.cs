using System.Collections.Generic;
using System.Linq;

namespace SimpleMiner.Parsing.Html.Model
{
    /// <summary>
    /// Utilizado quando necessário extrair um valor de algum atributo.
    /// </summary>
    public class PropertyXpathAttribute : HtmlParseAttribute
    {
        public List<string> Attributes { get; private set; }

        public PropertyXpathAttribute(params string[] attributes)
        {
            Attributes = attributes.ToList();
        }
    }
}
