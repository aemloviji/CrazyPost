
namespace CrazyPost.Models
{
    public class CommentEnhanceDTO: CommentRawDTO
    {
        public PostRawDTO Post { get; set; }
    }
}
