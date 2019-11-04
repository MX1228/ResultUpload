using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResultUpload.ViewModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ResultUpload.Models
{
    public class ResultContext:IdentityDbContext
    {
        //有参数的构造方法
        public ResultContext(DbContextOptions<ResultContext> options):base(options)
        {

        }
       
        public DbSet<Result> Results { get; set; }

        public DbSet<ResultType> ResultTypes { get; set; }
        
        public DbSet<ResultUser> ResultUsers { get; set; }
        
        public DbSet<ResultUpload.ViewModel.LoginViewModel> LoginViewModel { get; set; }
    }
}
