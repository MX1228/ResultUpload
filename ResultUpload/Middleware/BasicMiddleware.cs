using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResultUpload.Middleware
{
    public class BasicMiddleware
    {
        private readonly RequestDelegate _next;
        //定义授权常量
        public const string AuthorizationHeader = "Authorization";
        //定义认证常量
        public const string WWWAuthenticateHeader = "WWW-Authenticate";
        //方便后面传递用户，要登录的用户
        private BasicUser _user;
        public BasicMiddleware(RequestDelegate next,BasicUser user)
        {
            _next = next;
            _user = user;
        }
        public Task Invoke(HttpContext httpContext)
        {
            var Result = httpContext.Request;
            string auth = Result.Headers[AuthorizationHeader];
            //未发送用户名及密码
            if (auth == null)
            {
                //需要认证
                return BasicResult(httpContext);
            }
            //发送用户名和密码
            //取得Base64并解码成字符串
            string[] authParts = auth.Split(' ');
            if (authParts.Length != 2)
            {
                //需要认证
                return BasicResult(httpContext);
            }
            string base64 = authParts[1];//获取加了密的用户名和密码
            string authValue;
            try
            {
                //从base64字符串解析成原来的字节数组
                byte[] bytes = Convert.FromBase64String(base64);
               //将字节数组解析成字符串
                authValue = Encoding.ASCII.GetString(bytes);
            }
            catch (Exception)
            {

                authValue = null;
            }

            if (string.IsNullOrEmpty(authValue))
            {
                //解析出来的用户名和密码字符串为空，需要认证
                return BasicResult(httpContext);
            }
            //解析出具体的密码和用户名
            string userName;
            string passWord;
            int sepIndex = authValue.IndexOf(':');
            if (sepIndex == -1)
            {
                userName = authValue;
                passWord = string.Empty;
            }
            else
            {
                userName = authValue.Substring(0, sepIndex);
                passWord = authValue.Substring(sepIndex + 1);
            }
            //判断用户名和密码
            if (_user.UserName.Equals(userName) && _user.PassWord.Equals(passWord))
            {
                //请求下一个中间件
                return _next(httpContext);
            }
            else
            {
                //需要认证
                return BasicResult(httpContext);
            }
        }

        private static Task BasicResult(HttpContext httpContext)
        {
            httpContext.Response.StatusCode = 401;//未认证
            httpContext.Response.Headers.Add(WWWAuthenticateHeader, "Basic realm=\"localhost\"");
            return Task.FromResult(httpContext);
        }
    }
}
