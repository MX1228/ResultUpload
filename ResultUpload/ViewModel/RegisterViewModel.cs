using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResultUpload.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name ="用户名")]
        public string UserName { get; set; }

        [Required]
        //要符合email格式，类似于正则
        [EmailAddress]
        [Display(Name ="Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(16,ErrorMessage ="{0}长度必须大于{2}位小于{1}位",MinimumLength =6)]
        [DataType(DataType.Password)]
        [Display(Name ="密码")]
        public string PassWord { get; set; }
        
        [DataType(DataType.Password)]
        [Display(Name ="确认密码")]
        [Compare("PassWord",ErrorMessage ="两次密码不一致")]
        public string ConfirmPassWord { get; set; }
    }
}
