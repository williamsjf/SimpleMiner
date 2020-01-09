using System.Collections.Generic;
using System.Linq;

namespace SimpleMiner.Parsing.Html.Model
{
    /// <summary>
    /// Indica que um valor pode ser extraido a partir das expressões informadas.
    /// </summary>
    public class RegexAtrribute : HtmlParseAttribute
    {
        public RegexAtrribute()
        {
            Expressions = new List<string>();
        }

        public List<string> Expressions { get; private set; }
        public string FromGroup { get; set; }

        /// <summary>
        /// Suporta uma lista de expressões que serão submetidas até que uma seja verdadeira.
        /// </summary>
        /// <param name="expression"></param>
        public RegexAtrribute(params string[] expressions)
        {
            Expressions = expressions.ToList();
        }

        /// <summary>
        /// Suporta extração com o nome de um grupo que pode ser aplicado em várias expressões..
        /// </summary>
        /// <param name="fromGroup"></param>
        /// <param name="expressions"></param>
        public RegexAtrribute(string fromGroup, params string[] expressions)
        {
            Expressions = expressions.ToList();
            FromGroup = fromGroup;
        }
    }
}
