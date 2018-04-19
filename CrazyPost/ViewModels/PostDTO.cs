using System;
using System.Collections.Generic;

namespace CrazyPost.Models
{
    public class PostDTO
    {
        public PostDTO()
        {
            Comments = new List<CommentRawDTO>();
        }

        public int Id { get; set; }

        public string Text { get; set; }

        public string CreatedBy { get; set; }

        public DateTime InsertDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public List<CommentRawDTO> Comments { get; set; }
    }
}