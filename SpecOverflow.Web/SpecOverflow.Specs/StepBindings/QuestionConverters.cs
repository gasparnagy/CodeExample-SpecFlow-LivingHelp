using System;
using System.Collections.Generic;
using SpecOverflow.Specs.Support;
using SpecOverflow.Web.Models;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecOverflow.Specs.StepBindings
{
    [Binding]
    public class QuestionConverters
    {
        [StepArgumentTransformation]
        public IEnumerable<Question> ConvertQuestionList(Table table)
        {
            return table.CreateSet(CreateDefaultQuestion);
        }

        [StepArgumentTransformation]
        public Question ConvertQuestion(Table instanceTable)
        {
            return instanceTable.CreateInstance(CreateDefaultQuestion);
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