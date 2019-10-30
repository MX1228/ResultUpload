using ResultUpload.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResultUpload.Repository
{
    public interface IResulTypeRepository
    {
        Task<List<ResultType>> TypeListAsync();
    }
}
