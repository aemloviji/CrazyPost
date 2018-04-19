using CrazyPost.Models;

namespace CrazyPost.ViewModels
{
    public static class Convertor
    {
        public static PostDTO ToPostDTO(Post item)
        {
            var result = new PostDTO
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

        public static Post ToPost(PostDTO item)
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


        public static Post ToPost(PostAddOrUpdateDTO item, int id = 0)
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
                //Post = item.Post
            };
        }
    }
}