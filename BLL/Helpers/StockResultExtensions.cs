using COMMON.DTOs;
using DBMODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Helpers
{
    public static class StockResultExtensions
    {
        public static StockResult ToStockResult(this StockResultDTO dto, int stockId, int? id = null)
        {
            StockResult result = new StockResult();

            result.Id = id.HasValue ? id.Value : 0;
            result.StockId = stockId;
            result.gmtoffset = dto.gmtoffset;
            result.previousClose = dto.previousClose;
            result.volume = dto.volume;
            result.open = dto.open;
            result.close = dto.close;
            result.high = dto.high;
            result.low = dto.low;
            result.timestamp = dto.timestamp;
            result.change = dto.change;
            result.change_p = dto.change_p;

            return result;
        }
    }
}
