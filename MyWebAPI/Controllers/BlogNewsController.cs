using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyModels;
using MyModels.DTO;
using MyRepositories;
using SqlSugar;
using Utilities;
using static Utilities.ApiResultHelper;
namespace MyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BlogNewsController : ControllerBase
    {
        private IBlogNewsRepository _blogNewsRepository;

        public BlogNewsController(IBlogNewsRepository blogNewsRepository)
        {
            _blogNewsRepository = blogNewsRepository;
        }

        [HttpGet("BlogNews")]
        public async Task<ActionResult<ApiResult>> GetBlogNews()
        {
            int id = int.Parse(User.FindFirst("Id").Value);
            var data = await _blogNewsRepository.QueryAsync(n => n.WriterId == id);
            if (data == null) return Error("None blogs!");
            return Success(data);
        }
        [HttpPost("Create")]
        public async Task<ActionResult<ApiResult>> Create(string title, string content, int typeId)
        {
            BlogNews blogNews = new()
            {
                Content = content,
                Time = DateTime.Now,
                Title = title,
                TypeId = typeId,
                WriterId = int.Parse(User.FindFirst("Id").Value)
            };
            var r = await _blogNewsRepository.CreateAsync(blogNews);
            if (r) return Success(blogNews);
            return Error("Create failed!");
        }
        [HttpDelete("Delete")]
        public async Task<ActionResult<ApiResult>> Delete(int id)
        {
            var r = await _blogNewsRepository.DeleteAsync(id);
            if (!r) return Error("Delete failed!");
            return Success(r);
        }

        [HttpPut("Edit")]
        public async Task<ActionResult<ApiResult>> Edit(int id, string title, string content, int typeId)
        {
            var blogNews = await _blogNewsRepository.FindAsync(id);
            if (blogNews == null) return Error("Blog not found!");
            blogNews.Title = title;
            blogNews.Content = content;
            blogNews.TypeId = typeId;
            var r = await _blogNewsRepository.EditAsync(blogNews);
            if (!r) return Error("Edit failed!");
            return Success(blogNews);
        }

        [HttpGet("BlogNewsPage")]
        public async Task<ApiResult> GetBlogNewsPage([FromServices]IMapper mapper, int page,int size)
        {
            RefAsync<int> total = 0;
            var blognews=await _blogNewsRepository.QueryAsync(page, size, total);
            try
            {
                var blognewwDTO = mapper.Map<List<BlogNewsDTO>>(blognews);
                return Success(blognewwDTO, total);
            }
            catch (Exception)
            {
                return Error("AutoMapper failed!");
            }
        }
    }
}
