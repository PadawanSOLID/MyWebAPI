using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyModels;
using MyModels.DTO;
using MyRepositories;
using Utilities;
using static Utilities.ApiResultHelper;
namespace MyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        public async Task<ApiResult> Create(string name, string userName, string pwd)
        {
            WriterInfo wi = new()
            {
                Name = name,
                UserName = userName,
                Password = MD5Helper.MD5Encrypt(pwd)
            };
            var oldWriter = await _writerInfoRepository.FindAsync(n => n.UserName == userName);
            if (oldWriter != null) return Error("User already exists!");
            var r = await _writerInfoRepository.CreateAsync(wi);
            if (!r) return Error("Create failed!");
            return Success(r);
        }

        [HttpPut("Edit")]
        public async Task<ApiResult> Edit(string name)
        {
            int id = int.Parse(User.FindFirst("Id").Value);
            var wi = await _writerInfoRepository.FindAsync(id);
            if (wi == null) return Error("Writer not found!");
            wi.Name = name;
            var r = await _writerInfoRepository.EditAsync(wi);
            if (!r) return Error("Edit failed!");
            return Success(wi);
        }

        [HttpGet("FindWriter")]
        public async Task<ApiResult> FindWriter([FromServices]IMapper mapper, int id)
        {
            var writer=await _writerInfoRepository.FindAsync(id);
            var writerDTO = mapper.Map<WriterDTO>(writer);
            return Success(writerDTO);
        }
    }
}
