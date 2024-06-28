using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interface;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using api.Dtos.Comment;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockrepository _stockRepo;

        public CommentController(ICommentRepository commentRepo, IStockrepository stockRepo)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comments = await _commentRepo.GetAll();
            var commentDtos = comments.Select(c => c.ToCommentDto());
            return Ok(commentDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await _commentRepo.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommentDto commentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!await _stockRepo.StockExists(commentDto.StockId ?? 0))
            {
                return BadRequest("Stock does not exist");
            }

            var commentModel = commentDto.ToCommentFromCreate(commentDto.StockId ?? 0);
            await _commentRepo.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCommentRequestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await _commentRepo.UpdateAsync(id,updateDto.ToCommentFromUpdate());
            if (comment == null)
            {
                return NotFound("Comment not found");
            }

            return Ok(comment.ToCommentDto());

            
        }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }       
        var commentModel = await _commentRepo.DeleteAsync(id);
        if (commentModel == null)
        {
            return NotFound("Comment does not exist");
        }

        return Ok(commentModel);
    }



    }
}
