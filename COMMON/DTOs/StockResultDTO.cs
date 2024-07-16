﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMMON.DTOs
{
    public class StockResultDTO
    {
        public string code { get; set; }
        public long timestamp { get; set; }
        public int gmtoffset { get; set; }
        public decimal open { get; set; }
        public decimal high { get; set; }
        public decimal low { get; set; }
        public decimal close { get; set; }
        public decimal volume { get; set; }
        public decimal previousClose { get; set; }
        public decimal change { get; set; }
        public double change_p { get; set; }
    }
}
