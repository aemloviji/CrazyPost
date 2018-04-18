using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrazyPost.Contexts;
using CrazyPost.Models;
using Microsoft.EntityFrameworkCore;

namespace CrazyPost.Repository
{
    public class PostRepository : IPostRepository
    {
        CrazyPostContext _context;

        public PostRepository(CrazyPostContext context)
        {
            _context = context;
        }

        public async Task Add(Post item)
        {
            await _context.Post.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task<Post> Find(int id)
        {
            return await _context.Post
                .Where(e => e.ID == id)
                .SingleOrDefaultAsync();
        }


        public async Task<IEnumerable<Post>> GetAll()
        {
            return await _context.Post.ToListAsync();
        }

        public async Task Remove(int id)
        {
            var itemToRemove = await _context.Post.SingleOrDefaultAsync(m => m.ID == id);
            if (itemToRemove != null)
            {
                _context.Post.Remove(itemToRemove);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Update(Post item)
        {
            var itemToUpdate = await _context.Post.SingleOrDefaultAsync(m => m.ID == item.ID);
            if (itemToUpdate != null)
            {
                itemToUpdate.Comment = item.Comment;
                itemToUpdate.CreatedBy = item.CreatedBy;
                itemToUpdate.UpdateDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }
    }
}
