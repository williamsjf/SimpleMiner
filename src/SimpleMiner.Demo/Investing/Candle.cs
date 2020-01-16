using SimpleMiner.Parsing.Html.Model;

namespace SimpleMiner.Demo.Investing
{
    [BaseXPath("//table[@id='curr_table']/tbody/tr")]
    public class Candle
    {
        [SingleNodeXPath("./td[1]")]
        public string Data { get; set; }

        [SingleNodeXPath("./td[2]")]
        public string Close { get; set; }

        [SingleNodeXPath("./td[3]")]
        public string Open { get; set; }

        [SingleNodeXPath("./td[4]")]
        public string Max { get; set; }

        [SingleNodeXPath("./td[5]")]
        public string Min { get; set; }
    }
}
