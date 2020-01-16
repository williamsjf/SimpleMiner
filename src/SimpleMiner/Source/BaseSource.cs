using SimpleMiner.Comunication.Http;
using SimpleMiner.Parsing;
using System;

namespace SimpleMiner.Miner
{
    public abstract class BaseSource
    {
        protected object HttpNavigator { get; private set; }
        protected object Parser { get; private set; }

        /// <summary>
        /// Create a default instance of http navigator.
        /// When an instance already exists, it is reused.
        /// </summary>
        /// <returns></returns>
        protected HttpNavigator UseHttpNavigator()
        {
            if (HttpNavigator == null)
            {
                HttpNavigator = new HttpNavigator();
            }

            return (HttpNavigator)HttpNavigator;
        }

        /// <summary>
        /// Create a custom instance of http navigator.
        /// </summary>
        /// <param name="action">Http navigator customization</param>
        /// <returns></returns>
        protected HttpNavigator UseHttpNavigator(Action<HttpNavigatorBuilder> action)
        {
            var builder = new HttpNavigatorBuilder();
            action(builder);

            HttpNavigator = new HttpNavigator(builder);

            return (HttpNavigator)HttpNavigator;
        }

        /// <summary>
        /// WARNING: THIS FEATURE IS NOT READY YET
        /// </summary>
        /// <param name="driverPath"></param>
        /// <returns></returns>
        protected SeleniumHttpNavigator UseSeleniumHttpNavigator(string driverPath)
        {
            return new SeleniumHttpNavigator(driverPath);
        }

        protected TParser UseParser<TParser>() where TParser : IParser
        {
            if (Parser == null)
            {
                return default;
            }

            return (TParser)Parser;
        }

        protected TParser UseParser<TParser>(object content) where TParser : IParser
        {
            Parser = Activator
               .CreateInstance<TParser>();

            (Parser as IParser).Load(content);

            return (TParser)Parser;
        }
    }
}
