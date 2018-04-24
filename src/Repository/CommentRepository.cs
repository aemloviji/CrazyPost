using System.Collections.Generic;
using System.Threading.Tasks;
using CrazyPost.Contexts;
using CrazyPost.Models;
using Microsoft.EntityFrameworkCore;

namespace CrazyPost.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApiDbContext _context;

        public CommentRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task Add(Comment item)
        {
            await _context.Comment.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task<Comment> Find(int id)
        {
            return await _context.Comment
                .Include(c => c.Post)
                .SingleOrDefaultAsync(e => e.Id == id);
        }


        public async Task<IEnumerable<Comment>> GetAll()
        {
            return await _context.Comment
                .Include(c => c.Post)
                .ToListAsync();
        }

        public async Task Remove(int id)
        {
            var itemToRemove = await _context.Comment.SingleOrDefaultAsync(m => m.Id == id);
            if (itemToRemove != null)
            {
                _context.Comment.Remove(itemToRemove);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Update(int id, Comment item)
        {
            var itemToUpdate = await _context.Comment.SingleOrDefaultAsync(m => m.Id == id);
            if (itemToUpdate != null)
            {
                itemToUpdate.Text = item.Text;
                await _context.SaveChangesAsync();
            }
        }
    }
}
