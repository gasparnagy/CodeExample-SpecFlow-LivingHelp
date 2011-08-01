using System;

namespace SpecOverflow.Web.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int Votes { get; set; }
        public int Views { get; set; }
        public DateTime DateCreated { get; set; }
    }
}