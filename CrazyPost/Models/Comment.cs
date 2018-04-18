using System;

namespace CrazyPost.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime InsertDate { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
