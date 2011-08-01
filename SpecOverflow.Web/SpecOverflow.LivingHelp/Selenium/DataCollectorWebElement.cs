using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace SpecOverflow.LivingHelp.Selenium
{
    public class DataCollectorWebElement : IWebElement
    {
        private readonly IWebElement originalWebElement;
        private readonly IWebDriver webDriver;
        private readonly DataCollector dataCollector;

        public DataCollectorWebElement(IWebElement originalWebElement, IWebDriver webDriver, DataCollector dataCollector)
        {
            this.originalWebElement = originalWebElement;
            this.webDriver = webDriver;
            this.dataCollector = dataCollector;
        }

        public IWebElement FindElement(By by)
        {
            return originalWebElement.FindElement(by);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return originalWebElement.FindElements(by);
        }

        public void Clear()
        {
            originalWebElement.Clear();
        }

        public void SendKeys(string text)
        {
            originalWebElement.SendKeys(text);
        }

        public void Submit()
        {
            originalWebElement.Submit();
            dataCollector.OnFormSubmit(webDriver.Url, GetAttribute("id"));
        }

        public void Click()
        {
            originalWebElement.Click();
            dataCollector.OnClick(webDriver.Url, GetAttribute("id"));
        }

        public void Select()
        {
            originalWebElement.Select();
        }

        public string GetAttribute(string attributeName)
        {
            return originalWebElement.GetAttribute(attributeName);
        }

        public bool Toggle()
        {
            return originalWebElement.Toggle();
        }

        public string TagName
        {
            get { return originalWebElement.TagName; }
        }

        public string Text
        {
            get { return originalWebElement.Text; }
        }

        public string Value
        {
            get { return originalWebElement.Value; }
        }

        public bool Enabled
        {
            get { return originalWebElement.Enabled; }
        }

        public bool Selected
        {
            get { return originalWebElement.Selected; }
        }
    }
}
