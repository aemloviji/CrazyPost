using Microsoft.AspNetCore.Mvc;
using System;

namespace CrazyPost.Models
{
    [ModelMetadataType(typeof(Post))]
    public class PostDTO
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string CreatedBy { get; set; }

        public DateTime InsertDate { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}
