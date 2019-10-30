using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResultUpload.Repository;
using ResultUpload.ViewModel;

namespace ResultUpload.API
{
    [Route("api/Result")]
    [ApiController]
    public class ResultController : Controller
    {
        private IResultRepository _resultRepository;
        private IResulTypeRepository _resulTypeRepository;
        public ResultController(IResultRepository resultRepository, IResulTypeRepository resulTypeRepository)
        {
            _resultRepository = resultRepository;
            _resulTypeRepository = resulTypeRepository; 
        }

        [HttpGet]
        public IActionResult Get(int pageindex,int pagesize)
        {
            var results = _resultRepository.PageList(pageindex, pagesize, out int pagecount);
            ViewBag.PageCount = pagecount;
            ViewBag.PageIndex = pageindex;
            var resultList = results.Select(r => new ResultViewModel
            {
                ID = r.ID,
                SName = string.IsNullOrEmpty(r.PassWord) ? r.SName : "",
                Title = string.IsNullOrEmpty(r.PassWord) ? r.Title : "加密内容",
                Discription = string.IsNullOrEmpty(r.PassWord) ? r.Discription : "",
                Attachmet = string.IsNullOrEmpty(r.PassWord) ? r.Attachmet : "",
                Type = r.Type.TName
            });
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id,string password)
        {
            var result = await _resultRepository.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();//404错误,没有发现第二条记录
            }
            if (!string.IsNullOrEmpty(result.PassWord) && !result.PassWord.Equals(password))
            {
                return Unauthorized();//401错误，未授权
            }
            var resultView = new ResultViewModel
            {
                ID = result.ID,
                SName = result.SName,
                Title = result.Title,
                Discription = result.Discription,
                Attachmet = result.Attachmet,
                Type = result.Type.TName
            };
            return Ok(resultView);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ResultModel model)
        {
            await _resultRepository.AddAsync(new Models.Result
            {
                SName = model.SName,
                Title = model.Title,
                Discription = model.Discription,
                Create = DateTime.Now,
                TID = model.TID
            });
            return Content("ok");
        }
    }
}