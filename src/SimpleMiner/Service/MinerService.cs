using SimpleMiner.Navigation;
using SimpleMiner.Parsing;
using System;

namespace SimpleMiner.Service
{
    public class MinerService : IMinerService
    {
        private readonly INavigator _navigator;

        public MinerService(
            INavigator navigator)
        {
            _navigator = navigator;
        }

        public TNavigator UseNavigator<TNavigator>() where TNavigator : INavigator
        {
            return (TNavigator)_navigator;
        }

        public TParser UseParser<TParser>(object content) where TParser : IParser, new()
        {
            var parserInstance = Activator
                .CreateInstance<TParser>();

            parserInstance
                .LoadContent(content);

            return parserInstance;
        }

        public void Dispose()
        {

        }
    }
}
