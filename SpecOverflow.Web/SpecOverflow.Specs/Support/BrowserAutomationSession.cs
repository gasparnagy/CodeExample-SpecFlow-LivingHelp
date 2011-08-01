using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace SpecOverflow.Specs.Support
{
    public class BrowserAutomationSession
    {
        private static IWebDriver browser;
        public static IWebDriver Browser { get { return browser; } }

        public static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(10);

        public static void Start()
        {
            //browser = new InternetExplorerDriver();
            browser = new FirefoxDriver();
            browser.Manage().Timeouts().ImplicitlyWait(DefaultTimeout);

            IsRunning = true;
        }

        public static bool IsRunning { get; private set;  }

        public static void Stop()
        {
            if (!IsRunning)
                return;

            browser.Quit();
            browser.Dispose();

            IsRunning = false;
        }
    }
}
