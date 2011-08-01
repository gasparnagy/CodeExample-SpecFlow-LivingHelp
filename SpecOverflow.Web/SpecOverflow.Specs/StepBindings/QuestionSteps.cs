using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpecOverflow.Web.Models;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using SpecOverflow.Specs.Support;

namespace SpecOverflow.Specs.StepBindings
{
    [Binding]
    public class QuestionSteps
    {
        [Given(@"the following questions registered")]
        public void GivenTheFollowingQuestionsRegistered(Table table)
        {
            SpecOverflowEntities entities = new SpecOverflowEntities();

            foreach (var question in table.CreateSet(CreateDefaultQuestion))
            {
                entities.Questions.Add(question);
            }

            entities.SaveChanges();
        }

        private Question CreateDefaultQuestion()
        {
            return new Question
                       {
                           Title = "The title",
                           Body = "The body",
                           DateCreated = DateTime.Now,
                       };
        }
    }
}
