using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIDemo.Configuration
{
    public class Database
    {
        //跟配置文件appsetting文件中的键的名称一致
        public string Server { get; set; }
        public string Name { get; set; }
        public string Uid { get; set; }
        public string Password { get; set; }


    }
}
