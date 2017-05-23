using LarkSSO.Models;
using LarkSSO.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

//JWT官方文档：https://code.msdn.microsoft.com/How-to-achieve-a-bearer-9448db57
//参考博客：http://www.cnblogs.com/onecodeonescript/p/6061714.html

namespace LarkSSO.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return new JsonResult("Hello World!");
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public JsonResult Abc()
        {
            return new JsonResult(new
            {
                Name = "张三",
                Age = 12,
                Sex = true,
                User = User.Identity.Name
            }, new Newtonsoft.Json.JsonSerializerSettings());
        }

        //启用跨域
        [EnableCors("AllowSameDomain")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string userName, string passWord, string role)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsASecretKeyForAspNetCoreAPIToken"));
            var options = new TokenProviderOptions
            {
                Audience = "audience",
                Issuer = "issuer",
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            };
            var tpm = new TokenProvider(options);
            var token = await tpm.GenerateToken(HttpContext, userName, passWord, role);
            if (null != token)
            {
                return new JsonResult(token);
            }
            return NotFound();
        }

        //启用跨域
        [EnableCors("AllowSameDomain")]
        [Authorize(Roles = "admin")]
        public IActionResult AccessView()
        {
            return new JsonResult("this is Resources");
        }
    }
}