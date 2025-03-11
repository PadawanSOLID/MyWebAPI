using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyModels;
using MyRepositories;
using Utilities;
using static Utilities.ApiResultHelper;
namespace MyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WriterInfoController : ControllerBase
    {
        private IWriterInfoRepository _writerInfoRepository;

        public WriterInfoController(IWriterInfoRepository writerInfoRepository)
        {
            _writerInfoRepository = writerInfoRepository;
        }
        [HttpGet("Writers")]
        public async Task<ApiResult> GetWriters()
        {
            var writers = await _writerInfoRepository.QueryAllAsync();
            return Success(writers);
        }
        [HttpPost("Create")]    
        public async Task<ApiResult> Create(string name,string userName,string pwd)
        {
            WriterInfo wi = new()
            {
                Name = name,
                UserName = userName,
                Password = pwd
            };
            var r = await _writerInfoRepository.CreateAsync(wi);
            if (!r) return Error("Create failed!");
            return Success(r);
        }
    }
}
