using First_Proyect.Dtos.Comment;
using First_Proyect.Models;

namespace First_Proyect.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> CreateAsync(Comment comment);
        Task<Comment?> UpdateAsync(int id, UpdateCommentRequestDto updateCommentDto);
        Task<Comment?> DeleteAsync(int id);
    }
}
