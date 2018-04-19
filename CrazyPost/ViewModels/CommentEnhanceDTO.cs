
namespace CrazyPost.Models
{
    public class CommentEnhanceDTO: CommentRawDTO
    {
        public PostDTO Post { get; set; }
    }
}
