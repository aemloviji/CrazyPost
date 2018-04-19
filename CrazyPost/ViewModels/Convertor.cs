using CrazyPost.Models;

namespace CrazyPost.ViewModels
{
    public static class Convertor
    {
        public static PostDTO ToPostDTO(Post item)
        {
            return new PostDTO
            {
                Id = item.Id,
                CreatedBy = item.CreatedBy,
                Text = item.Text,
                InsertDate = item.InsertDate,
                UpdateDate = item.UpdateDate
            };
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



    }
}
