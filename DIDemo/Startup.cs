using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DIDemo.Configuration;
using DIDemo.Middlewares;
using DIDemo.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DIDemo
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
            //通过接口的形式注入服务;
            //Transient瞬态模式注入，每一次请求都会产生一个新的实例对象（适合轻量级的）
            //services.AddTransient(typeof(ICount), typeof(Count));
            //services.AddTransient<ICount, Count>();

            //域模式注入，在一个请求域内，只产生一个实例对象；每次web请求会创建一个实例
            //services.AddScoped(typeof(ICount), typeof(Count));
            //services.AddScoped<ICount, Count>();

            //单例注入模式；一旦被被创建实例，就会一直使用这个实例，直到应用停止
            //services.AddSingleton(typeof(ICount), typeof(Count));
            //services.AddSingleton<ICount, Count>();

            //自己注入自己，以实现的形式注入服务
            //services.AddScoped<Count>();
            //services.AddScoped(typeof(Count));

            //需要传参的构造函数的类注入
            //services.AddScoped(typeof(ICount), sp => { return new Count(参数); });
            //services.AddScoped<ICount>(sp => { return new Count(参数); });

            //services.Configure<Database>(Configuration.GetSection("database"));

            //强类型要注入服务
            //调用同一json配置文件下存在两个数据库
            services.Configure<Database>("database", Configuration.GetSection("database"));
            services.Configure<Database>("database2", Configuration.GetSection("database2"));
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //调用中间件
            //app.UseMiddleware<MyMiddleware>();
            //app.Use(),接口原生提供，注册等都用它，Use不会主动短路整个HTTP管道，但也不会主动调用下一个中间件，必须自行调用await next.invoke();
            //app.Use(next =>
            //{
            //    return context =>
            //    {
            //        return context.Response.WriteAsync("abc");
            //        //return Task.CompletedTask;  =>表示完成任务
            //    };
            //});

            //app.Map(),是一个扩展方法，类似于MVC的路由，一般用于一些特殊请求路径的处理
            //访问时加/api
            //app.Map("/api", builder => {
            //    builder.UseMiddleware<MyMiddleware>();
            //});

            //欢迎界面的中间件
            //app.UseWelcomePage();

            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //app.Run()，是一个扩展方法，需要一个RequestDelegate委托，在管道最后一步执行，该方法会使管道短路：中介管道向下执行不会调用next();
            //app.Run(context =>
            //{
            //    return Task.CompletedTask;
            //});
            }
        }
}
