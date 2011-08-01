using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SpecOverflow.Web.Models
{
    public class QuestionModel
    {
        [Required(ErrorMessage = "body is missing")]
        public string Title { get; set; }

        [Required(ErrorMessage = "title is missing")]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }
    }
}