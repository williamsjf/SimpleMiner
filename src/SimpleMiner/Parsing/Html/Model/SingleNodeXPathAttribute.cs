using System.Collections.Generic;
using System.Linq;

namespace SimpleMiner.Parsing.Html.Model
{
    /// <summary>
    /// Indica que o xpath será usado em uma propriedade dentro de um modelo.
    /// </summary>
    public class SingleNodeXPathAttribute : HtmlParseAttribute
    {
        public List<string> Xpaths { get; private set; }
        public SingleNodeXPathAttribute(params string [] xpaths)
        {
            Xpaths = xpaths.ToList();
        }
    }
}
