using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class TypeInfoController : ControllerBase
    {
        private ITypeInfoRepository _typeInfoRepository;

        public TypeInfoController(ITypeInfoRepository typeInfoRepository)
        {
            _typeInfoRepository = typeInfoRepository;
        }
        [HttpGet("Types")]
        public async Task<ApiResult> GetTypes()
        {
            var types=await _typeInfoRepository.QueryAllAsync();
            return Success(types);
        }
        [HttpPost("Create")]
        public async Task<ApiResult> Create(string name)
        {
            if (string.IsNullOrEmpty(name)) return Error("Name is empty!");
            TypeInfo ti = new()
            {
                Name = name
            };
            var r = await _typeInfoRepository.CreateAsync(ti);
            if(!r) return Error("Create failed!");
            return Success(r);
        }
        [HttpPut("Edit")]
        public async Task<ApiResult> Edit(int id,string name)
        {
            var ti = await _typeInfoRepository.FindAsync(id);
            if (ti == null) return Error("TypeInfo not found!");
            ti.Name = name;
            var r = await _typeInfoRepository.EditAsync(ti);
            if (!r) return Error("Edit failed!");
            return Success(ti);
        }
        [HttpDelete("Delete")]
        public async Task<ApiResult> Delete(int id)
        {
            var r = await _typeInfoRepository.DeleteAsync(id);
            if (!r) return Error("Delete failed!");
            return Success(r);
        }
    }
}
