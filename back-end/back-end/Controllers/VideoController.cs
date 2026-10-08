using Microsoft.AspNetCore.Mvc;
using business_layer;

namespace back_end.Controllers
{
    [Route("api/videos")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly clsVideos _clsVideos;

        public VideoController(clsVideos clsVideos)
        {
            _clsVideos = clsVideos;
        }

        /// <summary>
        /// Returns a presigned URL you can open in a browser to view the object.
        /// Example: GET /api/videos/presigned-url?key=thumbnails/1/thumbnail.jpg
        /// </summary>
        [HttpGet("presigned-url")]
        public async Task<IActionResult> GetPresignedUrl([FromQuery] string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return BadRequest("'key' query parameter is required.");

            var url = await _clsVideos.GetVideoPresignedUrlAsync(key);
            return Ok(new { url });
        }
    }
}
