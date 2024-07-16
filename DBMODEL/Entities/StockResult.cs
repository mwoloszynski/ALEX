using COMMON.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMODEL.Entities
{
    [Table("StockResults")]
    public class StockResult : BaseEntity
    {
        public int StockId { get; set; }
        [Required]
        [ForeignKey(nameof(StockId))]
        public Stock Stock {  get; set; }
        
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



        [NotMapped]
        public override Enums.EntityType EntityType { get => Enums.EntityType.StockResult; }
    }
}
