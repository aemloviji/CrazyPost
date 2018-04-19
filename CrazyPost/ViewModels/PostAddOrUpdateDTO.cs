using System;
using Microsoft.AspNetCore.Mvc;

namespace CrazyPost.Models
{
    [ModelMetadataType(typeof(Post))]
    public class PostAddOrUpdateDTO
    {
        public string Text { get; set; }

        public string CreatedBy { get; set; }

        public DateTime InsertDate { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}
