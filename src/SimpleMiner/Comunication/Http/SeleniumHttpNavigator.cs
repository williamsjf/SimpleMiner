using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading.Tasks;

namespace SimpleMiner.Comunication.Http
{
    public class SeleniumHttpNavigator
    {
        protected IWebDriver WebDriver;

        public SeleniumHttpNavigator(string driverPath)
        {
            WebDriver = new ChromeDriver(driverPath);
        }

        public HttpResponse<string> Get(string url) 
        {
            return Navigate(url);
        }

        public HttpResponse<string> Navigate(string url)
        {
            WebDriver.Navigate()
                .GoToUrl(url);

            var httpResponse = new HttpResponse<string>();
            httpResponse.SetContentValue(WebDriver.PageSource);

            return httpResponse;
        }
    }
}
