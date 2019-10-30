using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResultUpload.Models
{
    public class Result
    {
        [Key]//主键
        public int ID { get; set; }

        [Required]
        [MaxLength(10)] //最大长度
        [Display(Name ="姓名")]//显示在前端的名称
        public string SName { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name ="标题")]
        public string Title { get; set; }

        [Required]
        [Display(Name ="成果概述")]
        public string Discription { get; set; }
        
        [Display(Name ="创建时间")]
        public DateTime Create { get; set; }

        [Display(Name ="类型名称")]
        public int TID { get; set; }
        public ResultType Type { get; set; }//导航属性

        [Display(Name ="密码")]
        public string PassWord { get; set; }

        [Display(Name ="附件")]
        public string Attachmet { get; set; }
    }
}
