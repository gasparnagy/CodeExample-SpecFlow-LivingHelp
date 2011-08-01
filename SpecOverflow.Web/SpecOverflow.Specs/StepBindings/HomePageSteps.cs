using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using SpecOverflow.Specs.Support;
using SpecOverflow.Web.Models;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using FluentAssertions;

namespace SpecOverflow.Specs.StepBindings
{
    [Binding]
    public class HomePageSteps
    {
        [When(@"I go to the home page")]
        [Given(@"I'm on the home page")]
        public void WhenIGoToTheHomePage()
        {
            BrowserContext.Current.Browser.NavigateTo("/");
        }

        [Then(@"there should be (.+) questions in the list")]
        public void ThenThereShouldBeNQuestionsInTheList(int expectedCount)
        {
            int actualCount = BrowserContext.Current.Browser.FindElements(By.ClassName("question-info")).Count();

            actualCount.Should().Be(expectedCount, "the number of questions should be {0}", expectedCount);
        }

        [Then(@"the following questions should be displayed in this order")]
        public void ThenTheFollowingQuestionsShouldBeDisplayed(Table table)
        {
            var actualQuestions = BrowserContext.Current.Browser.FindElements(By.ClassName("question-info"))
                .Select(qi => new Question
                                  {
                                      Title = qi.FindElement(By.ClassName("body")).Text,
                                      Views = int.Parse(qi.FindElement(By.CssSelector(".views span")).Text),
                                      Votes = int.Parse(qi.FindElement(By.CssSelector(".votes span")).Text)
                                  });

            actualQuestions.ToProjection(table).SequenceEqual(table.ToProjection<Question>())
                .Should().BeTrue("the questions on the home page should be the same and in the same order");
        }

        [Then(@"toucans like bananas")]
        public void ThenToucansLikeBananas()
        {
            // this should be true
        }
    }
}
