using Microsoft.EntityFrameworkCore;
using ResultUpload.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResultUpload.Repository
{
    //利用EF来实现
    //实现接口
    public class ResultRepository : IResultRepository
    {
        private ResultContext resultContext;
        //上下文的实例对象
        public ResultRepository(ResultContext _resultContext)
        {
            resultContext = _resultContext;
        }
        //添加
        public Task AddAsync(Result result)
        {
            resultContext.Results.Add(result);
            return resultContext.SaveChangesAsync();
        }

        public async Task<bool> DelAsync(Result result)
        {
            resultContext.Entry<Result>(result).State = EntityState.Deleted;
            return await resultContext.SaveChangesAsync() > 0;
        }

        //根据id查找对象
        public Task<Result> GetByIdAsync(int id)
        {
            return resultContext.Results.Include<Result,ResultType>(i=>i.Type).FirstOrDefaultAsync(r=>r.ID==id);
        }
        //查找所有
        public Task<List<Result>> ListAsync()
        {                                 
                                         //导航属性，包括两张表
            return resultContext.Results.Include<Result,ResultType>(r=>r.Type).ToListAsync();
        }

        public List<Result> PageList(int PageIndex, int PageSize, out int PageCount)
        {
            var query = resultContext.Results.Include<Result, ResultType>(r => r.Type).AsQueryable();
            //总共有多少条记录
            var count = query.Count();
            //页数
            PageCount = count % PageSize == 0 ? count / PageSize : count / PageSize + 1;
                                //排序                                 跳过多少条记录
            var results = query.OrderByDescending(p => p.Create).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
            return results;             
        }

        //更新修改
        public async Task<bool> UpdateAsync(Result result)
        {
            resultContext.Results.Update(result);
            return await resultContext.SaveChangesAsync()>0;//更新到数据库
        }
    }
}
