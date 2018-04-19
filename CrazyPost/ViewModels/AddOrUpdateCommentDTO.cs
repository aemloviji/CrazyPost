using System;
using Microsoft.AspNetCore.Mvc;

namespace CrazyPost.Models
{
    [ModelMetadataType(typeof(Comment))]
    public class AddOrUpdateCommentDTO
    {
        public string Text { get; set; }
    }
}
