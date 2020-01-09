using System.Collections.Generic;
using System.Linq;

namespace SimpleMiner.Parsing.Html.Model
{
    public class BaseXPathAttribute : HtmlParseAttribute
    {
        public List<string> Xpaths { get; private set; }
        public BaseXPathAttribute(params string[] xpaths)
        {
            Xpaths = xpaths.ToList();
        }
    }
}
