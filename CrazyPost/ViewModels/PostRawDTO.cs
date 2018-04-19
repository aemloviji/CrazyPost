﻿using System;

namespace CrazyPost.Models
{
    public class PostRawDTO
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string CreatedBy { get; set; }

        public DateTime InsertDate { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}