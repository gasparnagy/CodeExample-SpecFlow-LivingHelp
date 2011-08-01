using System;
using OpenQA.Selenium;

namespace SpecOverflow.LivingHelp.Selenium
{
    public class DataCollectorNavigation : INavigation
    {
        private readonly INavigation originalNavigation;
        private readonly IWebDriver webDriver;
        private readonly DataCollector dataCollector;

        public DataCollectorNavigation(INavigation originalNavigation, IWebDriver webDriver, DataCollector dataCollector)
        {
            this.originalNavigation = originalNavigation;
            this.webDriver = webDriver;
            this.dataCollector = dataCollector;
        }

        public void Back()
        {
            originalNavigation.Back();
            dataCollector.OnNavigate(webDriver.Url);
        }

        public void Forward()
        {
            originalNavigation.Forward();
            dataCollector.OnNavigate(webDriver.Url);
        }

        public void GoToUrl(string url)
        {
            originalNavigation.GoToUrl(url);
            dataCollector.OnNavigate(webDriver.Url);
        }

        public void GoToUrl(Uri url)
        {
            originalNavigation.GoToUrl(url);
            dataCollector.OnNavigate(webDriver.Url);
        }

        public void Refresh()
        {
            originalNavigation.Refresh();
            dataCollector.OnNavigate(webDriver.Url);
        }
    }
}
