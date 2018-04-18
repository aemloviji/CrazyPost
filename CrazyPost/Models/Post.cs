using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CrazyPost.Models
{
    public class Post
    {
        public int ID { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string Text { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string CreatedBy { get; set; }

        public DateTime InsertDate { get; set; }

        public DateTime UpdateDate { get; set; }


        public virtual ICollection<Comment> Comment { get; set; }
    }
}
