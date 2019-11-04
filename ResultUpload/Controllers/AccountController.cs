using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ResultUpload.Models;
using ResultUpload.ViewModel;

namespace ResultUpload.Controllers
{
    public class AccountController : Controller
    {
        //用于记录日志
        private readonly ILogger<AccountController> _logger;
        //处理用户相关逻辑,增删改查等
        public UserManager<ResultUser> UserManager { get; }
        //处理注册登录相关业务逻辑
        public SignInManager<ResultUser> SignInManager { get; }

        public AccountController(UserManager<ResultUser> userManager, SignInManager<ResultUser> signInManager, ILogger<AccountController> logger)
        {
            _logger = logger;
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        //用来检测服务器请求是否被篡改，只在post下有用
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //如果登录失败是否锁定用户
            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.PassWord, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                //向日志文件写信息
                _logger.LogInformation("Login in {userName}", model.UserName);
                //跳转主页
                return RedirectToAction("Index", "Result");
            }
            else
            {
                _logger.LogWarning("Failed to log in {userName}", model.UserName);
                //第一个参数为key，第二个参数为值
                ModelState.AddModelError("", "用户名或者密码错误");
                return View(model);
            }
        }

        public IActionResult Register()
        {
            return ViewBag();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //注册
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var user = new ResultUser { UserName = model.UserName, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.PassWord);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User {usserName} was created.", model.UserName);
                    return RedirectToAction("Login");
                }
                foreach(var error in result.Errors){
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            var userName = HttpContext.User.Identity.Name;
            await SignInManager.SignOutAsync();//清楚cookie
            _logger.LogInformation("{userName} logged out", userName);
            return RedirectToAction("Login", "Account");
        }

    }
}