using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMMON.Helpers
{
    public class Enums
    {
        public enum Result
        {
            Fail = 0,
            Success = 1,
        }

        public enum EntityType
        {
            Exchange = 1,
            Stock = 2,
            StockResult = 3
        }
    }
}
