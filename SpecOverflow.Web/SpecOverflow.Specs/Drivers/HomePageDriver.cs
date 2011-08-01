using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using SpecOverflow.Specs.Support;
using SpecOverflow.Web.Models;

namespace SpecOverflow.Specs.Drivers
{
    public class HomePageDriver
    {
        public void NavigateTo()
        {
            BrowserContext.Current.Browser.NavigateTo("/");
        }

        public IEnumerable<Question> GetDisplayedQuestions()
        {
            return BrowserContext.Current.Browser.FindElements(By.ClassName("question-info"))
                            .Select(qi => new Question
                            {
                                Title = qi.FindElement(By.ClassName("body")).Text,
                                Views = int.Parse(qi.FindElement(By.CssSelector(".views span")).Text),
                                Votes = int.Parse(qi.FindElement(By.CssSelector(".votes span")).Text)
                            });            
        }
    }
}
