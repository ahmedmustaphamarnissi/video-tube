using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace back_end.Controllers
{
    [Route("api/videos")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IConfiguration _config;

        public VideoController(IConfiguration config)
        {
            _config = config;
        }


    }
}
