using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DIDemo.Middlewares
{
    public class MyMiddleware
    {
        //第一步:
        private RequestDelegate _next;
        public MyMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        //第二步 异步
        public async Task InvokeAsync(HttpContext context)
        {
           await  context.Response.WriteAsync("test");
            await _next.Invoke(context);
        }
    }
}
