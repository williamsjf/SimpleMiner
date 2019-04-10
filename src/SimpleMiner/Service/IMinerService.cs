using SimpleMiner.Navigation;
using SimpleMiner.Parsing;
using System;

namespace SimpleMiner.Service
{
    public interface IMinerService : IDisposable
    {
        TNavigator UseNavigator<TNavigator>() where TNavigator : INavigator;

        TParser UseParser<TParser>(object content) where TParser : IParser;
    }
}
