using business_layer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace back_end.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly clsComments _clsComments;

        public CommentsController(clsComments clsComments)
        {
            _clsComments = clsComments;
        }

        [HttpGet("{videoId}")]
        public async Task<IActionResult> GetCommentsAsync(int videoId)
        {
            var comments = await _clsComments.GetCommentsByVideoIdAsync(videoId);
            if (comments == null)
            {
                return NotFound("not found video with id " + videoId);
            }

            return Ok(comments);
        }
    }
}
