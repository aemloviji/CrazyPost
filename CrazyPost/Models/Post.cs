using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CrazyPost.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        public DateTime InsertDate { get; set; }

        public DateTime UpdateDate { get; set; }


        //public virtual ICollection<Comment> Comment { get; set; }
        public List<Comment> Comment { get; set; }
    }
}
