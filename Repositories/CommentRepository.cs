using System;
using RealPetApi.Models;

namespace RealPetApi.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DataContext _context;

        public CommentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateComment(Comment commentToCreate)
        {
            await _context.Comments.AddAsync(commentToCreate);
            var created = await _context.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> DeleteComment(int commentId)
        {
            var comment = await GetComment(commentId);

            _context.Comments.Remove(comment);

            var removed = await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Comment> GetComment(int commentId)
        {
            return await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
        }

        public async Task<bool> UpdateComment(Comment commentToUpdate)
        {
            _context.Comments.Update(commentToUpdate);

            var updated = await _context.SaveChangesAsync();
            return updated > 0;
        }
    }
}

