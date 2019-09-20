using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovie.Services
{
    public class FlagRepository : IFlagRepository
    {

        public Flag GetFlag(string country)
        {
            var flag = new Flag { Value = country };

            return flag;
;       }
    }
}
