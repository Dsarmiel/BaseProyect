using First_Proyect.Dtos.Comment;
using First_Proyect.Models;

namespace First_Proyect.Mappers
{
    public static class CommentMappers
    {
        public static CommentDto ToCommentDto (this Comment comment) 
        {
            return new CommentDto
            {
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                StockId = comment.StockId,
                CreatedOn = comment.CreatedOn
            };
        }

        public static Comment ToCommentFromCreate (this CreateCommentRequestDto commentDto) 
        {
            return new Comment
            {
                Title = commentDto.Title,
                StockId = commentDto.StockId,
                Content = commentDto.Content
            };
        }
    }
}
