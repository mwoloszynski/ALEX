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
    [Table("Exchanges")]
    public class Exchange : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }

        public string OperatingMIC { get; set; }

        public string Country { get; set; }

        public string Currency { get; set; }

        public string CountryISO2 {  get; set; }

        public string CountryISO3 { get; set; }



        [NotMapped]
        public override Enums.EntityType EntityType { get => Enums.EntityType.Exchange; }

    }
}
