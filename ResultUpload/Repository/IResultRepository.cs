using ResultUpload.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResultUpload.Repository
{
    public interface IResultRepository
    {
        Task<Result>  GetByIdAsync(int id);

        Task<List<Result>> ListAsync();//显示列表

        Task AddAsync(Result result);//添加

        Task<bool> UpdateAsync(Result result);//修改

        List<Result> PageList(int PageIndex, int PageSize, out int PageCount);

        //删除
        Task<bool> DelAsync(Result result);
    }
}
