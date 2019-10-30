using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResultUpload.ViewModel
{
    //前台显示的数据
    public class ResultModel
    {
        [Key]//主键
        public int ID { get; set; }

        [Required]
        [MaxLength(10)] //最大长度
        [Display(Name = "姓名")]//显示在前端的名称
        public string SName { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "标题")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "成果概述")]
        public string Discription { get; set; }

        [Display(Name ="类型名称")]
        public int TID { get; set; }

        [Display(Name = "密码")]
        public string PassWord { get; set; }

        [Display(Name = "附件")]
              //接口类型
        public IFormFile Attachmet { get; set; }
    }
}
