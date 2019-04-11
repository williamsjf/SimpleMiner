using System;

namespace SimpleMiner
{
    public class NavigationSettings
    {
        public TimeSpan TimeOut { get; set; }
    }

    public class HttpNavigatorSettingsBuilder
    {
        public HttpNavigatorSettingsBuilder()
        {
            Settings = new NavigationSettings();
        }

        private NavigationSettings Settings { get; set; }

        /// <summary>
        /// Maximum waiting time for the response of a request
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public HttpNavigatorSettingsBuilder NavigationTimeOut(int seconds)
        {
            Settings.TimeOut = 
                TimeSpan.FromSeconds(seconds);

            return this;
        }

        /// <summary>
        /// Maximum waiting time for the response of a request
        /// </summary>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public HttpNavigatorSettingsBuilder NavigationTimeOut(TimeSpan timeSpan)
        {
            Settings.TimeOut = timeSpan;
            return this;
        }
    }
}
