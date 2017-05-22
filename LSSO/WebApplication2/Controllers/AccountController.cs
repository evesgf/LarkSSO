using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApplication2.Helper;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    //[Produces("application/json")]
    [Route("api/v1/[Account]/[action]")]
    public class AccountController : Controller
    {
        [HttpPost]
        [Authorize(Roles = "admin")]
        public JsonResult ABC()
        {
            return new JsonResult(new
            {
                Name="ÕÅÈý",
                Age=12,
                Sex=true,
                User=User.Identity.Name
            },new Newtonsoft.Json.JsonSerializerSettings());
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string username, string password, string role)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsASecretKeyForAspNetCoreAPIToken"));
            var options = new TokenProviderOptions
            {
                Audience = "audience",
                Issuer = "issuer",
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            };
            var tpm = new TokenProvider(options);
            var token = await tpm.GenerateToken(HttpContext, username, password, role);
            if (null != token)
            {
                return new JsonResult(token);
            }
            else
            {
                return NotFound();
            }
        }
    }
}