using SimpleMiner.Navigation;
using SimpleMiner.Parsing;

namespace SimpleMiner.Service
{
    public class MinerService : IMinerService
    {
        private readonly INavigator _navigator;
        private readonly IParser _parser;

        public MinerService(
            INavigator navigator,
            IParser parser)
        {
            _navigator = navigator;
            _parser = parser;
        }

        public TNavigator UseNavigator<TNavigator>() where TNavigator : INavigator
        {
            return (TNavigator)_navigator;
        }

        public TParser UseParser<TParser>(object content) where TParser : IParser
        {
            var parser = (TParser)_parser;
            parser.LoadContent(content);

            return parser;
        }

        public void Dispose()
        {

        }
    }
}
