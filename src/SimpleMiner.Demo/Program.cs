using SimpleMiner.Miner;
using SimpleMiner.Parsing.Html.Model;
using System;
using System.Linq;
using System.Net;

namespace SimpleMiner.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var source = new InvestingSource();
            var result = source.GetData();

            Console.WriteLine(result);

            Console.ReadKey();
        }
    }

    public class InvestingSource : BaseSource
    {
        public string GetData()
        {
            var response = UseHttpNavigator()
                .Get("https://br.investing.com/crypto/bitcoin/btc-usd-historical-data");

            if (response.Status != HttpStatusCode.OK)
            {
                // Handle error here
            }

            var candleModels = response.Parse()
                 .ExtractModels<Candle>();

            if (!candleModels.Success)
            {
                // Handle error here
            }

            var instrumental = response.Parse()
                .FromSingleNode("//h1[@class='float_lang_base_1 relativeAttr']")
                .ParseInnerText();

            if (!instrumental.Success)
            {
                // Handle error here
            }

            return $"The '{instrumental.Text}' current price is '{candleModels.Models.First().Close}'.";
        }
    }

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
