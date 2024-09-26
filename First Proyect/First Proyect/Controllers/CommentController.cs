using First_Proyect.Dtos.Comment;
using First_Proyect.Interfaces;
using First_Proyect.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace First_Proyect.Controllers
{
    [Route("api/comment")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepostory _stockRepo;
        public CommentController(ICommentRepository commentRepo, IStockRepostory stockRepo)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            var comments = await _commentRepo.GetAllAsync();
            var commentsDto = comments.Select(c => c.ToCommentDto());

            return Ok(commentsDto);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var comment = await _commentRepo.GetByIdAsync(id);
            if (comment == null) 
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommentRequestDto commentDto) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var stockId = commentDto.StockId;
            if (!await _stockRepo.StockExist(stockId))
            {
                return BadRequest("Stock does not exist");
            }
            var commentModel = commentDto.ToCommentFromCreate();
            await _commentRepo.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto commentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var commentModel = await _commentRepo.UpdateAsync(id, commentDto);
            if(commentModel == null) 
            {
                return NotFound();
            }
            return Ok(commentModel);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var commentModel = await _commentRepo.DeleteAsync(id);
            if(commentModel == null) 
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
