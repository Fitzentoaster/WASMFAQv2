using Microsoft.AspNetCore.Mvc;
using WASMFAQv2.Shared.Interfaces;
using WASMFAQv2.Shared.Models;


namespace WASMFAQv2.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FAQController : ControllerBase
    {
        private readonly IQnASetRepository _qnaSetRepository;

        public FAQController(IQnASetRepository qnaSetRepository)
        {
            _qnaSetRepository = qnaSetRepository;
        }

        [HttpGet("qnasets")]
        public async Task<IActionResult> GetQnASets()
        {
            var qnaSets = await _qnaSetRepository.GetQnASetsAsync();
            return Ok(qnaSets);
        }

        [HttpGet("qnasets/{id}")]
        public async Task<IActionResult> GetQnASetById(int id)
        {
            var qnaSet = await _qnaSetRepository.GetQnASetByIdAsync(id);
            return Ok(qnaSet);
        }

        [HttpPost("qnasets")]
        public async Task<IActionResult> AddQnASet([FromBody] QnASet qnaSet)
        {
            if (await _qnaSetRepository.AddQnASetAsync(qnaSet))
            {
                return CreatedAtAction(nameof(GetQnASetById), new { id = qnaSet.QnASetId }, qnaSet);
            }
            return BadRequest();
        }

        [HttpPut("qnasets")]
        public async Task<IActionResult> UpdateQnASet([FromBody] QnASet qnaSet)
        {
            if (await _qnaSetRepository.UpdateQnASetAsync(qnaSet))
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpDelete("qnasets/{id}")]
        public async Task<IActionResult> DeleteQnASet(int id)
        {
            if (await _qnaSetRepository.DeleteQnASetAsync(id))
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpGet("qnasets/{id}/questions")]
        public async Task<IActionResult> GetQuestionsByQnASetId(int id)
        {
            var questions = await _qnaSetRepository.GetQuestionsByQnASetIdAsync(id);
            return Ok(questions);
        }

        [HttpPost("qnasets/{id}/questions")]
        public async Task<IActionResult> AddQnA(QnA qna)
        {
            if (await _qnaSetRepository.AddQnAAsync(qna))
            {
                return CreatedAtAction(nameof(GetQuestionsByQnASetId), new { id = qna.QnaId }, qna);
            }
            return BadRequest();
        }

        [HttpDelete("qnasets/questions/{id}")]
        public async Task<IActionResult> DeleteQnA(int id)
        {
            if (await _qnaSetRepository.DeleteQnAAsync(id))
            {
                return NoContent();
            }
            return NotFound();
        }
        [HttpPut("qnasets/questions/{id}")]
        public async Task<IActionResult> UpdateQnA(QnA qna)
        {
            if (await _qnaSetRepository.UpdateQnAAsync(qna))
            {
                return NoContent();
            }
            return BadRequest();
        }
        [HttpGet("faq")]
        public async Task<IActionResult> GetFAQ()
        {
            var faq = await _qnaSetRepository.GetFAQAsync();
            return Ok(faq);
        }
        [HttpPost("qnasets/normalize-sort-order")]
        public async Task<IActionResult> NormalizeQnASetSortOrder()
        {
            if (await _qnaSetRepository.NormalizeQnASetSortOrderAsync())
            {
                return NoContent();
            }
            return BadRequest();
        }
        [HttpPost("qnasets/questions/normalize-sort-order")]
        public async Task<IActionResult> NormalizeQnASortOrder()
        {
            if (await _qnaSetRepository.NormalizeQnASortOrderAsync())
            {
                return NoContent();
            }
            return BadRequest();
        }
        [HttpPut("faq")]
        public async Task<IActionResult> UpdateFAQ([FromBody] FAQ faq)
        {
            if (await _qnaSetRepository.UpdateFAQAsync(faq))
            {
                return NoContent();
            }
            return BadRequest();
        }
        [HttpPost("faq")]
        public async Task<IActionResult> CreateFAQ([FromBody] FAQ faq)
        {
            if (await _qnaSetRepository.CreateFAQAsync(faq))
            {
                return CreatedAtAction(nameof(GetFAQ), new { id = faq.FAQId }, faq);
            }
            return BadRequest();
        }
    }
}
