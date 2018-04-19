using CrazyPost.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrazyPost.Repository
{
    public interface IPostRepository
    {
        Task Add(Post item);
        Task<IEnumerable<Post>> GetAll();
        Task<Post> Find(int id);
        Task Remove(int id);
        Task Update(int id, Post item);

    }
}
