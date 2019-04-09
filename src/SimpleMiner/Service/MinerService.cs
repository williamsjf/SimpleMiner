using SimpleMiner.Navigation;
using SimpleMiner.Navigation.Http;

namespace SimpleMiner.Service
{
    public class MinerService : IMinerService
    {
        private readonly INavigator _navigator;

        public MinerService(INavigator navigator)
        {
            _navigator = navigator;
        }

        public IHttpNavigator UseHttpNavigator()
        {
            return (IHttpNavigator)_navigator;
        }

        public void Dispose()
        {

        }
    }
}
