using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using SpecOverflow.Specs.Support;
using SpecOverflow.Web.Models;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecOverflow.Specs.StepBindings
{
    public class ValidationError
    {
        public string Message { get; set; }
    }

    [Binding]
    public class ValidationSteps
    {
        [Then(@"the following validation errors should be displayed")]
        public void ThenTheFollowingValidationErrorsShouldBeDisplayed(Table table)
        {
            var actualValidationErrors = Enumerable.Empty<ValidationError>();

            var validationSummary = BrowserContext.Current.Browser.FindElement(By.ClassName("validation-summary-errors"));
            if (validationSummary != null)
            {
                actualValidationErrors = validationSummary.FindElements(By.TagName("li")).Select(li => new ValidationError {Message = li.Text});
            }

            table.CompareToSet(actualValidationErrors);
        }
    }
}
