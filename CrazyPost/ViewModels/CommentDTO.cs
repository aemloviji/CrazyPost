using System;

namespace CrazyPost.Models
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime InsertDate { get; set; }
        public PostDTO Post { get; set; }
    }
}
