using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using SpecOverflow.LivingHelp;
using SpecOverflow.LivingHelp.Selenium;
using TechTalk.SpecFlow;

namespace SpecOverflow.Specs.Support
{
    public class BrowserContext 
    {
        public static BrowserContext Current
        {
            get
            {
                return (BrowserContext)ScenarioContext.Current.GetBindingInstance(typeof(BrowserContext));
            }
        }

        public IWebDriver Browser { get; private set; }

//        public BrowserContext()
//        {
//            Browser = BrowserAutomationSession.Browser;
//        }
//
        public BrowserContext(DataCollector dataCollector)
        {
            Browser = new DataCollectorWebDriver(BrowserAutomationSession.Browser, dataCollector);
        }
    }
}
