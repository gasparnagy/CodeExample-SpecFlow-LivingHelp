using System;
using System.Linq;
using OpenQA.Selenium;
using SpecOverflow.Specs.Support;
using SpecOverflow.Web.Models;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecOverflow.Specs.StepBindings
{
    [Binding]
    public class AskSteps : Steps
    {
        [When(@"I ask a new question with")]
        public void WhenIAskANewQuestionWith(Question question)
        {
            #region alternatives for "reusing" steps
            //BrowserContext.Current.Browser.NavigateTo("/");
            //new HomePageSteps().WhenIGoToTheHomePage();
            //EnsureToBeOnHomePage();
            //homePageDriver.NavigateTo();
            #endregion
            When("I go to the home page");

            BrowserContext.Current.Browser.SetTextBoxValue("Title", question.Title);
            BrowserContext.Current.Browser.SetTextBoxValue("Body", question.Body);
            BrowserContext.Current.Browser.SubmitForm();
        }

        [Then(@"the question should appear at the end of the question list as")]
        public void ThenTheQuestionShouldAppearAtTheEndOfTheQuestionListAs(Table expectedQuestion)
        {
            var actualQuestions = BrowserContext.Current.Browser.FindElements(By.ClassName("question-info"))
                .Select(qi => new Question
                {
                    Title = qi.FindElement(By.ClassName("body")).Text,
                    Views = int.Parse(qi.FindElement(By.CssSelector(".views span")).Text),
                    Votes = int.Parse(qi.FindElement(By.CssSelector(".votes span")).Text)
                });

            var lastQuestion = actualQuestions.Last();

            expectedQuestion.CompareToInstance(lastQuestion);
        }
    }
}
