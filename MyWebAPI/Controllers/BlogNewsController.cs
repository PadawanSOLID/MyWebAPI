using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyRepositories;

namespace MyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogNewsController : ControllerBase
    {
        private IBlogNewsRepository _blogNewsRepository;

        public BlogNewsController(IBlogNewsRepository blogNewsRepository)
        {
            _blogNewsRepository = blogNewsRepository;
        }

        [HttpGet("BlogNews")]
        public async Task<ActionResult> GetBlogNews()
        {
            var data = await _blogNewsRepository.QueryAllAsync();
            return Ok(data);
        }
    }
}
