using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResultUpload.Middleware;
using ResultUpload.Models;
using ResultUpload.Repository;

namespace ResultUpload
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //string server = Configuration["databse:Server"];
            //string db = Configuration["databse:Name"];
            //string uid = Configuration["databse:Uid"];
            //string pwd = Configuration["databse:Pwd"];
            //该种注入方式通过需要传参的构造函数的类注入
            //var connection = $"server={server};Database={db};uid={uid};pwd={pwd}";
            var connection = @"server=.;database=Results;uid=sa;pwd=123456";
            services.AddDbContext<ResultContext>(options =>
            {
                options.UseSqlServer(connection);
            });

            //注入服务
            services.AddScoped<IResultRepository, ResultRepository>();
            services.AddScoped<IResulTypeRepository, ResultTypeRepository>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //登录用户名和密码写死
            app.UseMiddleware<BasicMiddleware>(new BasicUser() { UserName = "admin", PassWord = "123456" });
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Result}/{action=Index}/{id?}");
            });
        }
    }
}
