using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResultUpload.ViewModel
{
    public class LoginViewModel
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage ="{0}不能为空")]
        [Display(Name ="用户名")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="{0}不能为空")]
        //生成的输入框是密码框，不是文本框之类的，后续input中可以不加type="password"属性
        [DataType(DataType.Password)]
        [Display(Name ="密码")]
        public string PassWord { get; set; }

        [Display(Name ="记住登录状态")]
        public bool RememberMe { get; set; }

    }
}
