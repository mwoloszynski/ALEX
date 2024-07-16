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
    [Table("Stocks")]
    public class Stock : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }

        public int ExchangeId {  get; set; }
        [Required]
        [ForeignKey(nameof(ExchangeId))]
        public Exchange Exchange { get; set; }



        [NotMapped]
        public override Enums.EntityType EntityType { get => Enums.EntityType.Stock; }
    }
}
