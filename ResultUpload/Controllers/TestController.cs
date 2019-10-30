using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ResultUpload.Controllers
{
    public class TestController : Controller
    {
        public IActionResult SayHello()
        {
            return View();
        }
                                          //特性：查询form里面的字符串
        public IActionResult ProcSayHello([FromQuery]string username)
        {
            return Content("hello" + username);
        }
    }
}