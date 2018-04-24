using System;

namespace CrazyPost.Models
{
    public class CommentRawDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime InsertDate { get; set; }
    }
}
