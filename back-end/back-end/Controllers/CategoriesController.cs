using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace back_end.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IConfiguration _config;

        public CategoriesController(IConfiguration config)
        {
            _config = config;
        }
    }
}
