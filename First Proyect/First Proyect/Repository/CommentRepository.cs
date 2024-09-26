using First_Proyect.Data;
using First_Proyect.Dtos.Comment;
using First_Proyect.Interfaces;
using First_Proyect.Models;
using Microsoft.EntityFrameworkCore;

namespace First_Proyect.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var commentToDelete = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (commentToDelete == null) 
            {
                return null;
            }
            _context.Comments.Remove(commentToDelete);
            await _context.SaveChangesAsync();
            return commentToDelete;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task<Comment?> UpdateAsync(int id, UpdateCommentRequestDto updateCommentDto)
        {
            var commentToUpdate = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (commentToUpdate == null) 
            {
                return null;
            }

            commentToUpdate.Title = updateCommentDto.Title;
            commentToUpdate.Content = updateCommentDto.Content;

            await _context.SaveChangesAsync();
            return commentToUpdate;
        }
    }
}
