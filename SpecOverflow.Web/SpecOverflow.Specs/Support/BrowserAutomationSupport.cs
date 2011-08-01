using System;
using System.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using SpecOverflow.Web.Models;
using TechTalk.SpecFlow;

namespace SpecOverflow.Specs.Support
{
    [Binding]
    public class BrowserAutomationSupport
    {
        [BeforeTestRun]
        public static void StartTestServer()
        {
        }

        [BeforeScenario]
        public void BeforeWebScenario()
        {
            if (!BrowserAutomationSession.IsRunning)
                BrowserAutomationSession.Start();
        }

        [AfterScenario]
        public void AfterWebScenario()
        {
            if (ScenarioContext.Current.TestError != null)
            {
                Console.WriteLine("Browser page source for failing test: {0}",
                    BrowserAutomationSession.Browser.PageSource);
            }

            bool browserPerScenario;
            if (bool.TryParse(ConfigurationManager.AppSettings["browserPerScenario"], out browserPerScenario) && browserPerScenario)
                BrowserAutomationSession.Stop();
        }

        [AfterTestRun]
        public static void StopBrowser()
        {
            if (BrowserAutomationSession.IsRunning)
                BrowserAutomationSession.Stop();
        }
    }
}
