using CrazyPost.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrazyPost.Repository
{
    public interface ICommentRepository
    {
        Task Add(Comment item);
        Task<IEnumerable<Comment>> GetAll();
        Task<Comment> Find(int id);
        Task Remove(int id);
        Task Update(int id, Comment item);
    }
}
