using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIDemo.Service
{
    public class Count:ICount
    {
        private int count;
        public int MyCount()
        {
            return count++;
        }
    }
}
