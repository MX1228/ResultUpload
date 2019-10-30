using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResultUpload.ViewModel
{
    public class ResultViewModel
    {
        public int ID { get; set; }
        //学生姓名
        public string SName { get; set; }
        //标题
        public string Title { get; set; }
        //描述
        public string Discription { get; set; }
        //类别
        public string Type { get; set; }
        //附件
        public string Attachmet { get; set; }
    }
}
