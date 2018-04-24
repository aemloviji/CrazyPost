using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace CrazyPost.Models
{
    [ModelMetadataType(typeof(Comment))]
    public class AddOrUpdateCommentDTO
    {
        [Required]
        public int PostId { get; set; }

        [Required]
        public string Text { get; set; }
    }
}
