using SimpleMiner.Demo.Investing;
using SimpleMiner.Miner;
using System.Linq;
using System.Net;

namespace SimpleMiner.Demo
{
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

            var parsedCandle = response.Parse()
                 .ExtractModels<Candle>();

            if (!parsedCandle.Success)
            {
                // Handle error here
            }

            var parsedInstrumental = response.Parse()
                .FromSingleNode("//h1[@class='float_lang_base_1 relativeAttr']")
                .ParseInnerText();

            if (!parsedInstrumental.Success)
            {
                // Handle error here
            }

            return $"The '{parsedInstrumental.Text}' current price is '{parsedCandle.Models.First().Close}'.";
        }
    }
}
