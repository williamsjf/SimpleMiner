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
        /// Cria uma nova ou reaproveita uma instancia já existente de um HttpNavigator.
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
        /// Retorna uma nova instancia do HttpNavigator com as configurações informadas.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        protected HttpNavigator UseHttpNavigator(Action<HttpNavigatorBuilder> action)
        {
            var builder = new HttpNavigatorBuilder();
            action(builder);

            HttpNavigator = new HttpNavigator(builder);

            return (HttpNavigator)HttpNavigator;
        }

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
