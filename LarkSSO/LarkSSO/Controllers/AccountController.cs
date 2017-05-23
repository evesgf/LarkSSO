using LarkSSO.Models;
using LarkSSO.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

//JWT�ٷ��ĵ���https://code.msdn.microsoft.com/How-to-achieve-a-bearer-9448db57
//�ο����ͣ�http://www.cnblogs.com/onecodeonescript/p/6061714.html

namespace LarkSSO.Controllers
{
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
                Name = "����",
                Age = 12,
                Sex = true,
                User = User.Identity.Name
            }, new Newtonsoft.Json.JsonSerializerSettings());
        }

        //���ÿ���
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

        //���ÿ���
        [EnableCors("AllowSameDomain")]
        [Authorize(Roles = "admin")]
        public IActionResult AccessView()
        {
            return new JsonResult("this is Resources");
        }
    }
}