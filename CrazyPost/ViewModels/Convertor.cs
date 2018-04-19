using CrazyPost.Models;
using System;

namespace CrazyPost.ViewModels
{
    public static class Convertor
    {
        #region post entity convertors
        public static PostEnhanceDTO ToPostEnhanceDTO(Post item)
        {
            var result = new PostEnhanceDTO
            {
                Id = item.Id,
                CreatedBy = item.CreatedBy,
                Text = item.Text,
                InsertDate = item.InsertDate,
                UpdateDate = item.UpdateDate,
            };

            foreach (var comment in item.Comments)
            {
                result.Comments.Add(ToCommentRawDTO(comment));
            }

            return result;
        }

        public static PostRawDTO ToPostRawDTO(Post item)
        {
            var result = new PostRawDTO
            {
                Id = item.Id,
                CreatedBy = item.CreatedBy,
                Text = item.Text,
                InsertDate = item.InsertDate,
                UpdateDate = item.UpdateDate,
            };

            return result;
        }

        public static Post ToPost(PostEnhanceDTO item)
        {
            return new Post
            {
                Id = item.Id,
                CreatedBy = item.CreatedBy,
                Text = item.Text,
                InsertDate = item.InsertDate,
                UpdateDate = item.UpdateDate
            };
        }


        public static Post ToPost(AddOrUpdatePostDTO item, int id = 0)
        {
            return new Post
            {
                Id = id,
                CreatedBy = item.CreatedBy,
                Text = item.Text,
                InsertDate = item.InsertDate,
                UpdateDate = item.UpdateDate
            };
        }
        #endregion

        #region comment entity convertors
        public static CommentRawDTO ToCommentRawDTO(Comment item)
        {
            return new CommentRawDTO
            {
                Id = item.Id,
                Text = item.Text,
                InsertDate = item.InsertDate
            };
        }

        public static CommentEnhanceDTO ToCommentEnhanceDTO(Comment item)
        {
            return new CommentEnhanceDTO
            {
                Id = item.Id,
                Text = item.Text,
                InsertDate = item.InsertDate,
                Post = ToPostRawDTO(item.Post)
            };
        }

        public static Comment ToComment(AddOrUpdateCommentDTO item, int id = 0)
        {
            return new Comment
            {
                Id = id,
                Text = item.Text,
                InsertDate = DateTime.Now,

                PostId = item.PostId
            };
        }
        #endregion


    }
}