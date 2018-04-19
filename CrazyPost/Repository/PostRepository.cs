using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrazyPost.Contexts;
using CrazyPost.Models;
using Microsoft.EntityFrameworkCore;

namespace CrazyPost.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly ApiDbContext _context;

        public PostRepository(ApiDbContext context)
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
                .Include(c => c.Comments)
                .SingleOrDefaultAsync(e => e.Id == id);
        }


        public async Task<IEnumerable<Post>> GetAll()
        {
            return await _context.Post
                .Include(c => c.Comments)                
                .ToListAsync();
        }

        public async Task Remove(int id)
        {
            var itemToRemove = await _context.Post.SingleOrDefaultAsync(m => m.Id == id);
            if (itemToRemove != null)
            {
                _context.Post.Remove(itemToRemove);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Update(int id, Post item)
        {
            var itemToUpdate = await _context.Post.SingleOrDefaultAsync(m => m.Id == id);
            if (itemToUpdate != null)
            {
                itemToUpdate.Text = item.Text;
                itemToUpdate.CreatedBy = item.CreatedBy;
                itemToUpdate.UpdateDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }
    }
}
