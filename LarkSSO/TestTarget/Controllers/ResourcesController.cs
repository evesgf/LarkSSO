using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestTarget.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class ResourcesController : Controller
    {
        //∆Ù”√øÁ”Ú
        [EnableCors("AllowSameDomain")]
        [Authorize(Roles = "admin")]
        public IActionResult GetResources()
        {
            return new JsonResult("this is Resources");
        }
    }
}