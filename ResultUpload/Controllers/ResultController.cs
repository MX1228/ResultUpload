using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ResultUpload.Models;
using ResultUpload.Repository;
using ResultUpload.ViewModel;

namespace ResultUpload.Controllers
{
    public class ResultController : Controller
    {
        private static string filepath;
        private IResultRepository _resultRepository;
        private IResulTypeRepository _resulTypeRepository;
        //仓储类
        public ResultController(IResultRepository resultRepository, IResulTypeRepository resulTypeRepository)
        {
            _resultRepository = resultRepository;
            _resulTypeRepository = resulTypeRepository;
        }
        //变为异步方法
        public IActionResult Index(int PageIndex=1,int PageSize=5)
        {
            //调用仓储类的数据
            //var result = await _resultRepository.ListAsync();---这个用异步方法
            var result = _resultRepository.PageList(PageIndex,PageSize,out int PageCount);
            ViewBag.Count = PageCount;
            ViewBag.Index = PageIndex;
            return View(result);
        }
        //添加
        public async Task<IActionResult> Add()
        {
            var type = await _resulTypeRepository.TypeListAsync();
            ViewBag.Types = type.Select(r => new SelectListItem
            {
                Text = r.TName,
                Value = r.TID.ToString()
            });
            return View();
        }
        [HttpPost]
        //异步方法                           特性：来自于服务注册,获取环境变量
        public async Task<IActionResult> Add([FromServices]IHostingEnvironment env,ResultModel model)
        {
            //判断是否合理
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //上传路径
            string filename = string.Empty;
            if (model.Attachmet != null)
            {
                                        //文件夹   字符串                   上传文件的扩展名                                     
                filename = Path.Combine("file", Guid.NewGuid().ToString() + Path.GetExtension(model.Attachmet.FileName));
                //using为了及时关闭文件流  //绝对路径   
                using (var stream = new FileStream(Path.Combine(env.WebRootPath, filename), FileMode.CreateNew)) {
                    model.Attachmet.CopyTo(stream);//文件直接复制到wwwroot中的file文件夹中
                } 
                // env.WebRootPath + filename
            }

            await _resultRepository.AddAsync(new Models.Result
            {
                SName = model.SName,
                Title = model.Title,
                Discription = model.Discription,
                Create = DateTime.Now,
                TID=model.TID,
                PassWord=model.PassWord,
                Attachmet=filename
            });
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var result = await _resultRepository.GetByIdAsync(id);
            if (!string.IsNullOrEmpty(result.PassWord))
            {
                return View();
            }
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Details(int id,string password)
        {
            var result = await _resultRepository.GetByIdAsync(id);            
            if (!result.PassWord.Equals(password))
            {
                return BadRequest("密码错误，返回重新输入");
            }
            return View(result);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var type = await _resulTypeRepository.TypeListAsync();
            ViewBag.Type = type.Select(r => new SelectListItem
            {
                Text = r.TName,
                Value = r.TID.ToString()
            });
            Result result = await _resultRepository.GetByIdAsync(id);
            filepath = result.Attachmet;
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromServices]IHostingEnvironment env,ResultModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string filename;
            if (model.Attachmet != null)
            {
                filename = Path.Combine("file", Guid.NewGuid().ToString()+Path.GetExtension(model.Attachmet.FileName));
                using (var stream = new FileStream(Path.Combine(env.WebRootPath, filename), FileMode.CreateNew))
                {
                    model.Attachmet.CopyTo(stream);
                }
            }
            Result r = new Result()
            {
                ID = model.ID,
                SName = model.SName,
                Title = model.Title,
                Discription = model.Discription,
                TID = model.TID,
                Create = DateTime.Now,
                PassWord = model.PassWord,
                Attachmet = filepath
            };
            await _resultRepository.UpdateAsync(r);
            return RedirectToAction("Index");
        }

        //删除
        public async Task<IActionResult> Delete(int id)
        {
            Result result = await _resultRepository.GetByIdAsync(id);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id,string name)
        {
            //查询调用id
            Result result = await _resultRepository.GetByIdAsync(id);
            await _resultRepository.DelAsync(result);
            return RedirectToAction("Index"); 
        }
    }
}