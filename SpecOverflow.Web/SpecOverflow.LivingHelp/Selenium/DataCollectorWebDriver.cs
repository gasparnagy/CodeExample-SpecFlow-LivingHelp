using System.Collections.ObjectModel;
using System.Linq;
using OpenQA.Selenium;

namespace SpecOverflow.LivingHelp.Selenium
{
    public class DataCollectorWebDriver : IWebDriver
    {
        private readonly IWebDriver originalWebDriver;
        private readonly DataCollector dataCollector;

        public DataCollectorWebDriver(IWebDriver originalWebDriver, DataCollector dataCollector)
        {
            this.originalWebDriver = originalWebDriver;
            this.dataCollector = dataCollector;
        }

        public IWebElement FindElement(By by)
        {
            return new DataCollectorWebElement(originalWebDriver.FindElement(by), this, dataCollector);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return originalWebDriver.FindElements(by).Select(e => (IWebElement)new DataCollectorWebElement(e, this, dataCollector)).ToList().AsReadOnly();
        }

        public void Dispose()
        {
            originalWebDriver.Dispose();
        }

        public void Close()
        {
            originalWebDriver.Close();
        }

        public void Quit()
        {
            originalWebDriver.Quit();
        }

        public IOptions Manage()
        {
            return originalWebDriver.Manage();
        }

        public INavigation Navigate()
        {
            return new DataCollectorNavigation(originalWebDriver.Navigate(), this, dataCollector);
        }

        public ITargetLocator SwitchTo()
        {
            return originalWebDriver.SwitchTo();
        }

        public ReadOnlyCollection<string> GetWindowHandles()
        {
            return originalWebDriver.GetWindowHandles();
        }

        public string GetWindowHandle()
        {
            return originalWebDriver.GetWindowHandle();
        }

        public string Url
        {
            get { return originalWebDriver.Url; }
            set
            {
                originalWebDriver.Url = value;
                dataCollector.OnNavigate(value);
            }
        }

        public string Title
        {
            get { return originalWebDriver.Title; }
        }

        public string PageSource
        {
            get { return originalWebDriver.PageSource; }
        }
    }
}
