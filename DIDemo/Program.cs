using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DIDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            //告诉系统还有database配置文件
            //对个配置文件中相同配置信息，后加入的配置文件会覆盖先前的配置信息

            .ConfigureAppConfiguration((context,config)=> {
                string env = context.HostingEnvironment.EnvironmentName;
                config.AddJsonFile($"database.{env}.json",true);//动态的调用数据库配置文件,加上true运行不会报错
                config.AddJsonFile("db.json", true);
            })
                .UseStartup<Startup>();
    }
}
