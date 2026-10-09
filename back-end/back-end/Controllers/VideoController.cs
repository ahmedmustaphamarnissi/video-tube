using business_layer;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet]
        public async Task<IActionResult> GetHomeVideosAsync(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0 || pageSize <= 0)
                return BadRequest("Page number and page size must be greater than zero.");

            var res = await _clsVideos.GetHomeVideosAsync(pageNumber, pageSize);
            return Ok(res);
        }

        [HttpGet("category/{id}")]
        public async Task<IActionResult> GetVideosByCategoryAsync(int categouryId, int pageNumber, int pageSize)
        {
            if (pageNumber <= 0 || pageSize <= 0)
                return BadRequest("Page number and page size must be greater than zero.");

            var res = await _clsVideos.GetVideosByCategoryAsync(categouryId, pageNumber, pageSize);

            if (res == null || res.Count == 0)
                return NotFound($"Category with ID {categouryId} not found.");
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVideoDetailsAsync(int id)
        {
            var video = await _clsVideos.GetVideoDetailsAsync(id);
            if (video == null)
                return NotFound($"Video with ID {id} not found.");
            return Ok(video);
        }

    }
}
