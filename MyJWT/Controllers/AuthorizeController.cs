using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyModels;
using MyRepositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Utilities;
using static Utilities.ApiResultHelper;
namespace MyJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private IWriterInfoRepository _writerInfoRepository;
        private IOptions<JwtOption> _jwtOption;

        public AuthorizeController(IWriterInfoRepository writerInfoRepository,IOptions<JwtOption> jwtOption)
        {
            _writerInfoRepository = writerInfoRepository;
            _jwtOption = jwtOption;
        }

        [HttpPost("Login")]
        public async Task<ApiResult> Login(string username, string pwd)
        {
            string encrypt = MD5Helper.MD5Encrypt(pwd);
            var writer = await _writerInfoRepository.FindAsync(c => c.UserName == username && c.Password == encrypt);
            if (writer != null)
            {
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name,writer.Name),
                    new Claim("Id",writer.Id.ToString()),
                    new Claim("UserName",writer.UserName),
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOption.Value.Key));
                var token = new JwtSecurityToken(
                    "http://localhost:5145",
                    "http://localhost:5294",
                    claims, DateTime.Now, DateTime.Now.AddHours(1),
                    new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                    );
                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                return Success(jwtToken);
            }
            else
            {
                return Error("Invalid username or password!");
            }

        }
    }
}
