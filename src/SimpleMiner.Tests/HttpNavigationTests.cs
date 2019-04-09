using SimpleMiner.Navigation.Http;
using System;
using System.Net.Http;
using Xunit;

namespace SimpleMiner.Tests
{
    public class HttpNavigationTests
    {
        [Fact]
        public void Test1()
        {
            var navigator = new HttpNavigator(new HttpClient());

            var result = navigator.GetAsync("http://www.tjrj.jus.br/").Result;
        }
    }
}
