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
}
