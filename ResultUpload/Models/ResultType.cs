using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResultUpload.Models
{
    public class ResultType
    {
        [Key]
        public int TID { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name ="类型名称")]
        public string TName { get; set; }

        public List<Result> Results { get; set; }//导航属性，从Type导到Result表中
    }
}
