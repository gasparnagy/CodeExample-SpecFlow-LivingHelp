using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpecOverflow.Web.Models;

namespace SpecOverflow.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var dataContext = new SpecOverflowEntities();

            var questions = dataContext.Questions.OrderByDescending(q => q.Views);

            return View(questions);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Ask(QuestionModel question)
        {
            if (!ModelState.IsValid)
                return View();

            var dataContext = new SpecOverflowEntities();

            var dbQuestion = new Question();
            dbQuestion.Title = question.Title;
            dbQuestion.Body = question.Body;
            dbQuestion.DateCreated = DateTime.Now;
            dbQuestion.Votes = 0;
            dbQuestion.Views = 0;

            dataContext.Questions.Add(dbQuestion);
            dataContext.SaveChanges();

            if (Request.IsAjaxRequest())
            {
                return PartialView("_QuestionPartial", dbQuestion);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Ask()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
