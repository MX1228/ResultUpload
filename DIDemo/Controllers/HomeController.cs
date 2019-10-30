using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DIDemo.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using DIDemo.Configuration;

namespace DIDemo.Controllers
{
    public class HomeController : Controller
    {
        //弱类型
        //通过构造方法依赖注入
        //只要告诉configuration IConfiguration的类型
        //DI--负责实例化应用程序中的对象及建立这些对象之间的依赖；维护对象之间的生命周期

        //强类型
        private IConfiguration _configuration;

        private IOptionsSnapshot<Database> _database;
        //IOptions是单例模式，实例化后的对象唯一，程序运行期间，配置文件发生变化，刷新数据不会发生变化
        //IOptionsSnapshot属于Scoped类型服务，每次请求都会重新加载配置数据（不管配置有没有发生改变，刷新之后都会重新加载配置文件）
        //IOptionsMonitor监听模式，该类型能自动监控配置文件，自动加载最新配置，没有value，有CurrentValue（配置没有发生改变不会重新加载，发生改变之后会重新加载）
        public HomeController(IConfiguration configuration,IOptionsSnapshot<Database> databse)
        {
            _configuration = configuration;
            _database = databse;
        }

        public IActionResult Index()
        {
            //获取配置文件中的值
            string str1 = _configuration["option1"];   //获取option1的值
            string str2 = _configuration["option2:suboption2:subkey1"];
            string str3 = _configuration["database:Name"];    //获取数据库的名字
            string strName = _configuration["student:0:Name"];//student下第一个名字
            string strAge = _configuration["studet:0:Age"];   //student下第一个年龄
            string strNames = _configuration["student:1:Name"];//student下第二个名字
            ViewBag.str1 = str1;
            //Database db = _database.Value;
            Database db = _database.Get("database");
            ViewBag.database = $"Server:{db.Server},Name:{db.Name}";  //获取数据库的名称和Server
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
