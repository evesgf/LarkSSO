using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var secretKey = "mysupersecret_secretkey!123";
            //var signingKy=new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
