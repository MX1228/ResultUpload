using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ResultUpload.Models;

namespace ResultUpload.Repository
{
    public class ResultTypeRepository : IResulTypeRepository
    {
        private ResultContext resultContext;
        public ResultTypeRepository(ResultContext _resultContext)
        {
            resultContext = _resultContext;
        }
        public Task<List<ResultType>> TypeListAsync()
        {
            return resultContext.ResultTypes.ToListAsync();
        }
    }
}
