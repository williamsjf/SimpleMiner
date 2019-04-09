using SimpleMiner.Navigation.Http;
using System;

namespace SimpleMiner.Service
{
    public interface IMinerService : IDisposable
    {
        IHttpNavigator UseHttpNavigator();
    }
}
