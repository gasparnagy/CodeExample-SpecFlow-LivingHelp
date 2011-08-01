using System;
using System.Configuration;
using System.Linq;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SpecOverflow.Specs.Support
{
    public static class WebDriverExtensions
    {
        public static void ValidateTextboxValue(this IWebDriver browser, string field, string value)
        {
            IWebElement control = GetFieldControl(browser, field);

            control.Should().NotBeNull("field {0} should be on the page", field);
            control.Value.Should().Be(value, "field {0} should have the value", field);
        }

        public static string GetTextBoxValue(this IWebDriver browser, string field)
        {
            IWebElement control = GetFieldControl(browser, field);

            return control.Value;
        }

        public static void SetTextBoxValue(this IWebDriver browser, string field, string value)
        {
            var control = GetFieldControl(browser, field);
            var wait = new WebDriverWait(browser, BrowserAutomationSession.DefaultTimeout);
            if (!String.IsNullOrEmpty(control.Value))
            {
                control.Clear();
                wait.Until(_ => String.IsNullOrEmpty(control.Value));
            }
            
            control.SendKeys(value);
//            wait.Until( _ => control.Value == value);
            System.Threading.Thread.Sleep(100);
        }

        public static void SubmitForm(this IWebDriver browser)
        {
            GetForm(browser).Submit();
            System.Threading.Thread.Sleep(100);
        }

        public static void ClickButton(this IWebDriver browser, string buttonId)
        {
            browser.FindElements(By.Id(buttonId)).First().Click();
        }

        private static IWebElement GetFieldControl(IWebDriver browser, string field)
        {
            var form = GetForm(browser);
            return form.FindElement(By.Id(field));
        }

        private static IWebElement GetForm(IWebDriver browser)
        {
            return browser.FindElements(By.TagName("form")).First();
        }

        public static void NavigateTo(this IWebDriver browser, string relativeUrl)
        {
             browser.Navigate().GoToUrl(new Uri(new Uri(ConfigurationManager.AppSettings["baseUrl"]), relativeUrl));
        }

        public static DropDown GetDropDown(this IWebDriver browser, string id)
        {
            return browser.FindElement(By.Id(id)).AsDropDown();
        }

        public static DropDown AsDropDown(this IWebElement e)
        {
            return new DropDown(e);
        }

        public class DropDown
        {
            private readonly IWebElement dropDown;

            public DropDown(IWebElement dropDown)
            {
                this.dropDown = dropDown;

                if (dropDown.TagName != "select")
                    throw new ArgumentException("Invalid web element type");
            }

            public string SelectedValue
            {
                get
                {
                    var selectedOption = dropDown.FindElements(By.TagName("option")).Where(e => e.Selected).FirstOrDefault();
                    if (selectedOption == null)
                        return null;

                    return selectedOption.Value;

                }
                set
                {
                    var optionToBeSelected = dropDown.FindElements(By.TagName("option")).Where(e => e.Value == value).FirstOrDefault();
                    if (optionToBeSelected == null)
                        throw new ArgumentException(string.Format("Option with value '{0}' not found.", value));

                    optionToBeSelected.Select();
                }
            }
        }
    }
}
