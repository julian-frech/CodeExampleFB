using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralFinanceManagerUI.Models.UserDepots
{
    [Table("V_DepotComponentsAggregated", Schema = "ANALYSIS")]
    public class V_DepotComponentsAggregated
    {

        [Key, Column(Order = 0)] public int DepotComponentId { get; set; }

        public int DepotId { get; set; }

        public string UserHK { get; set; }

        [Display(Name = "Merged")]
        public DateTime ValidFrom { get; set; }


        public DateTime ValidTo { get; set; }

        public string Symbol { get; set; }

        public int Quantity { get; set; }

        public decimal PositionValueThen { get; set; }

        public decimal PositionValueNow { get; set; }

        public decimal PositionNetValue { get; set; }

        public decimal Percentage { get; set; }


        [NotMapped]
        public string UserName { get; set; }

    }
}
